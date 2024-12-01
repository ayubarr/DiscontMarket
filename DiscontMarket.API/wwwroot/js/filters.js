export function updateFiltersInURL() {
    const newParams = new URLSearchParams(window.location.search);

    // Добавляем активные фильтры
    const checkboxes = filtersContainer.querySelectorAll('.checkbox');
    checkboxes.forEach(checkbox => {
        const filterId = checkbox.getAttribute('data-filter');
        if (checkbox.classList.contains('active')) {
            newParams.set(filterId, true);
        } else {
            newParams.delete(filterId);
        }
    });

    // Добавляем значения слайдера
    const minValue = minSlider.value;
    const maxValue = maxSlider.value;

    // Записываем минимальную цену
    newParams.set('minprice', minValue);

    // Записываем максимальную цену
    newParams.set('maxprice', maxValue);

    // Обновляем адресную строку без перезагрузки страницы
    window.history.replaceState({}, '', '?' + newParams.toString());

    sendFiltersToServer();
}

export function updateSortingInURL(sortBy) {
    const newParams = new URLSearchParams(window.location.search);

    // Записываем параметр сортировки
    newParams.set('sort', sortBy);

    // Обновляем адресную строку без перезагрузки страницы
    window.history.replaceState({}, '', '?' + newParams.toString());

    sendFiltersToServer();
    window.location.reload();
}

export function getActiveFilters() {
    const activeFilters = {};

    // Сортировка
    const activeSort = document.querySelector('.sorting-option.active');
    if (activeSort) {
        activeFilters.sort = activeSort.dataset.sort;
    }

    // Фильтры по чекбоксам
    const stockCategories = ['instock', 'preorderlater', 'preordertomorrow'];
    const statusCategories = ['discount', 'damagedpackage', 'minordefects'];
    const brandsFilters = ['Samsung', 'LG', 'Xiaomi', 'Panasonic'];
    const screenResolution = ['HD', 'Full HD', 'QHD', '4K'];
    const itemColor = ['white', 'black', 'silver'];

    activeFilters.availability = [];
    activeFilters.status = [];

    activeFilters.AttributeDTOs = [];
    activeFilters.BrandDTOs = [];

    const checkboxes = filtersContainer.querySelectorAll('.checkbox');
    checkboxes.forEach(checkbox => {
        if (checkbox.classList.contains('active')) {
            const filterId = checkbox.getAttribute('data-filter');
            

            if (activeFilters.BrandDTOs.length !== 0) {
                console.warn('BrandDTOs пуст.');
            }
            if (activeFilters.AttributeDTOs.length !== 0) {
                console.warn('AttributeDTOs пуст.');
            }

            if (stockCategories.includes(filterId)) {
                activeFilters.availability.push(filterId);
            } else if (statusCategories.includes(filterId)) {
                activeFilters.status.push(filterId);
            }
            // Проверяем фильтры только если категория `tv`
            else if (brandsFilters.includes(filterId)) {
                activeFilters.BrandDTOs.push(filterId);
            } else if (screenResolution.includes(filterId) || itemColor.includes(filterId)) {
                activeFilters.AttributeDTOs.push(filterId);
            }
            else {
                activeFilters[filterId] = true;
            }
        }
    });

    // Значения слайдеров
    const minPrice = minSlider.value;
    const maxPrice = maxSlider.value;
    activeFilters['minprice'] = minPrice;
    activeFilters['maxprice'] = maxPrice;

    return activeFilters;
}   

export function sendFiltersToServer() {
    const filters = getActiveFilters();
    const category = params.keys().next().value; // Получаем категорию из URL

    // Добавляем категорию в объект фильтров
    filters.CategoryDTO = {Name : category};

    fetch('http://192.168.192.59/сайт/filters.php', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(filters),
    })
    .then(response => response.json())
    .then(data => {
        console.log('Данные успешно получены:', data);

        const container = document.querySelector('.main-items-section');

        if (!container) {
            console.error('Контейнер для карточек не найден. Убедитесь, что HTML содержит .main-items-section .products-carousel.');
            return;
        }

        // Очищаем контейнер перед добавлением новых карточек
        container.innerHTML = '';

        // Проверяем, что данные корректны
        if (data.data && Array.isArray(data.data)) {
            data.data.forEach(product => {
                const card = document.createElement('div');
                card.classList.add('product-card-main');

                // Создаем содержимое карточки
                card.innerHTML = `
                    <img src="${product.image || 'items/filters/no-image.png'}" alt="Товар" class="product-image">
                    <div class="product-separator-main"></div>
                    <p class="product-name-main">${product.productName}</p>
                    <div class="product-price-container-main">
                        <span class="product-price-main">${product.price} ₽</span>
                        <button class="order-button-main">Оформить заказ</button>
                    </div>
                    <div class="compare-prices-wrapper">
                        <span class="compare-prices-main" data-product-name="${product.productName}">Сравнить цены</span>
                    </div>
                `;

                // Добавляем карточку в контейнер
                container.appendChild(card);
            });

            // Добавляем обработчики для всех кнопок "Сравнить цены"
            const compareButtons = container.querySelectorAll('.compare-prices-main');
            compareButtons.forEach(button => {
                button.addEventListener('click', () => {
                    const productName = button.getAttribute('data-product-name');
                    const yandexMarketURL = `https://market.yandex.ru/search?text=${encodeURIComponent(productName)}`;
                    window.open(yandexMarketURL, '_blank'); // Открываем ссылку в новой вкладке
                });
            });
        } else {
            console.warn('Нет данных товаров для отображения.');
        }
    })
    .catch(error => {
        console.error('Ошибка при получении данных:', error);
    });
}