document.addEventListener('DOMContentLoaded', () => {
    const minPriceInput = document.getElementById('minprice');
    const maxPriceInput = document.getElementById('maxprice');
    const minSlider = document.getElementById('price-slider-min');
    const maxSlider = document.getElementById('price-slider-max');
    const sliderTrack = document.querySelector('.slider-track');
    const filtersContainer = document.getElementById('filters-container');
    const sortingOptions = document.querySelectorAll('.sorting-option');
    const maxValue = parseInt(maxSlider?.max || 0);

    const params = new URLSearchParams(window.location.search);
    const category = params.keys().next().value;
    console.log('Категория', category);

    let attributedtos = []; // Сюда будем записывать атрибуты
    let branddtos = []; // Сюда будем записывать бренды

    let categoryFilters = {};

    fetch('api/Attribute/get-all-names', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(category)
    })
    .then(response => response.json())
    .then(data => {

        // Обновление categoryFilters данными из PHP
        categoryFilters = {
            brands: data.brands,
            attributes: data.attributes,
        };
        
        console.log('Обновленные категории:', categoryFilters);
        
    let activeFilters = {};

    // Установить "По популярности" активным по умолчанию
    let activeSort = params.get('sort') || null; // По умолчанию "null"

    // Активируем соответствующую опцию сортировки при загрузке страницы
    sortingOptions.forEach(option => {
        if (option.dataset.sort === activeSort) {
            option.classList.add('active');
        } else if (!activeSort && option.dataset.sort === 'popularity') {
            option.classList.add('active'); // Если параметр sort отсутствует, ставим активной опцию "по популярности"
        }
    });

    // Обработчик клика для переключения активного состояния сортировки
    sortingOptions.forEach(option => {
        option.addEventListener('click', () => {
            // Снимаем класс active со всех надписей
            sortingOptions.forEach(item => item.classList.remove('active'));
    
            // Добавляем класс active к текущему элементу
            option.classList.add('active');
    
            // Обновляем сортировку в URL
            updateSortingInURL(option.dataset.sort);
        });
    });

    function updateSliderTrack() {
        const minValue = parseInt(minSlider.value);
        const maxValueSlider = parseInt(maxSlider.value);
        const percentageMin = (minValue / maxValue) * 100;
        const percentageMax = (maxValueSlider / maxValue) * 100;

        sliderTrack.style.left = `${percentageMin}%`;
        sliderTrack.style.width = `${percentageMax - percentageMin}%`;
    }

    function updateInputsFromSliders() {
        const minValue = parseInt(minSlider.value);
        const maxValueSlider = parseInt(maxSlider.value);

        if (minValue > maxValueSlider) {
            minSlider.value = maxValueSlider;
        }

        minPriceInput.value = minSlider.value;
        maxPriceInput.value = maxSlider.value;

        updateSliderTrack();
    }

    function updateSlidersFromInputs() {
        let minValue = parseInt(minPriceInput.value) || 0;
        let maxValueSlider = parseInt(maxPriceInput.value) || maxValue;

        if (minValue < 0) minValue = 0;
        if (minValue > maxValue) minValue = maxValue;
        if (maxValueSlider < 0) maxValueSlider = 0;
        if (maxValueSlider > maxValue) maxValueSlider = maxValue;

        if (minValue > maxValueSlider) {
            minValue = maxValueSlider;
        }

        minPriceInput.value = minValue;
        maxPriceInput.value = maxValueSlider;

        minSlider.value = minValue;
        maxSlider.value = maxValueSlider;

        updateSliderTrack();
    }

    filtersContainer.addEventListener('click', (event) => {
        // Проверяем, был ли клик по чекбоксу
        if (event.target.classList.contains('checkbox')) {
            event.target.classList.toggle('active');
            updateFiltersInURL(); // Обновление URL при выборе фильтра
            window.location.reload();
        }
    });

    minSlider?.addEventListener('input', updateInputsFromSliders);
    maxSlider?.addEventListener('input', updateInputsFromSliders);
    minPriceInput?.addEventListener('input', updateSlidersFromInputs);
    maxPriceInput?.addEventListener('input', updateSlidersFromInputs);

    minPriceInput?.addEventListener('blur', updateSlidersFromInputs);
    maxPriceInput?.addEventListener('blur', updateSlidersFromInputs);

    updateSliderTrack();


       fetch('api/Filter/get-filters')
        .then(response => response.json())
        .then(data => {
            const categoryFilters = data[category];
            if (categoryFilters) {
                categoryFilters.filters.forEach(filter => {
                    const filterBlock = document.createElement('div');
                    filterBlock.classList.add('filter-block');

                    const filterTitle = document.createElement('div');
                    filterTitle.classList.add('filter-title');
                    filterTitle.textContent = filter.title;

                    const filterOptions = document.createElement('div');
                    filterOptions.classList.add('filter-options');
                    filterOptions.style.display = 'none';

                    filter.options.forEach(option => {
                        const filterOption = document.createElement('div');
                        filterOption.classList.add('filter-option');

                        const checkbox = document.createElement('div');
                        checkbox.classList.add('checkbox');
                        checkbox.setAttribute('data-filter', option.value);

                        // Проверка, был ли уже этот фильтр в URL
                        if (params.has(option.value)) {
                            checkbox.classList.add('active');
                        }

                        const span = document.createElement('span');
                        span.textContent = option.label;

                        filterOption.appendChild(checkbox);
                        filterOption.appendChild(span);
                        filterOptions.appendChild(filterOption);
                    });

                    filterTitle.addEventListener('click', () => {
                        const isVisible = filterOptions.style.display === 'block';
                        filterOptions.style.display = isVisible ? 'none' : 'block';
                    });

                    filterBlock.appendChild(filterTitle);
                    filterBlock.appendChild(filterOptions);
                    filtersContainer.appendChild(filterBlock);
                });
            }
        })
        .catch(error => {
            console.error('Ошибка загрузки фильтров:', error);
        });

    // Функция для обновления URL с выбранными фильтрами
    function updateFiltersInURL() {
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

        attributedtos = Array.from(newParams.keys()).filter(key => categoryFilters[category]?.attributes.includes(key));
        branddtos = Array.from(newParams.keys()).filter(key => categoryFilters[category]?.brands.includes(key));

    
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

    // Функция для обновления сортировки в URL
    function updateSortingInURL(sortBy) {
        const newParams = new URLSearchParams(window.location.search);

        // Записываем параметр сортировки
        newParams.set('sort', sortBy);

        // Обновляем адресную строку без перезагрузки страницы
        window.history.replaceState({}, '', '?' + newParams.toString());

        sendFiltersToServer();
        window.location.reload();
    }
    
    // Обновление URL при изменении значений слайдеров или инпутов
    function handleSliderInput() {
        updateInputsFromSliders(); // Синхронизация инпутов
        updateFiltersInURL(); // Обновление URL
        window.location.reload();
    }
    
    // Привязываем события
    minSlider.addEventListener('change', handleSliderInput);
    maxSlider.addEventListener('change', handleSliderInput);
    minPriceInput.addEventListener('change', () => {
        updateSlidersFromInputs(); // Синхронизация слайдеров
        updateFiltersInURL(); // Обновление URL
        window.location.reload();
    });
    maxPriceInput.addEventListener('change', () => {
        updateSlidersFromInputs(); // Синхронизация слайдеров
        updateFiltersInURL(); // Обновление URL
        window.location.reload();
    });
    
    // Сохранение состояния слайдера при загрузке страницы
    function initSliderValues() {
        const minPrice = params.get('minprice');
        const maxPrice = params.get('maxprice');
    
        if (minPrice) {
            minSlider.value = minPrice;
            minPriceInput.value = minPrice;
        }
        if (maxPrice) {
            maxSlider.value = maxPrice;
            maxPriceInput.value = maxPrice;
        }
    
        updateSliderTrack();
    }
    
    // Инициализируем состояние слайдеров после загрузки
    initSliderValues();

    // Инициализация активных фильтров при загрузке страницы
    function initActiveFilters() {

        console.log("initActiveFilters")
        const currentCategoryFilters = categoryFilters;
        if (!currentCategoryFilters) return;
    
        params.forEach((value, key) => {
            if (currentCategoryFilters.brands.includes(key) && !branddtos.includes(key)) {
                branddtos.push(key);
            } else if (currentCategoryFilters.attributes.includes(key) && !attributedtos.includes(key)) {
                attributedtos.push(key);
            }
        });
    
        branddtos = branddtos.filter(filter => params.has(filter));
        attributedtos = attributedtos.filter(filter => params.has(filter));
    
        console.log('AttributeDTOs:', attributedtos);
        console.log('BrandDTOs:', branddtos);
    
        const checkboxes = filtersContainer.querySelectorAll('.checkbox');
        checkboxes.forEach(checkbox => {
            const filterId = checkbox.getAttribute('data-filter');
            if (params.has(filterId)) {
                checkbox.classList.add('active');
                console.log(`Фильтр ${filterId} активирован из URL`);
            }
        });

    }    

    function getActiveFilters() {
        // Сортировка
        const activeSort = document.querySelector('.sorting-option.active');
        if (activeSort) {
            activeFilters.sort = activeSort.dataset.sort;
        }

        const stockCategories = ['instock', 'preorderlater', 'preordertomorrow'];
        const statusCategories = ['discount', 'damagedpackage', 'minordefects'];

        activeFilters = {
            availability: [],
            status: [],
        };

        activeFilters.Attributes = [...attributedtos];
        activeFilters.Brands = [...branddtos];

        console.log('activeFilters.Attributes: ', activeFilters.Attributes);
        console.log('activeFilters.Brands: ', activeFilters.Brands);

 
        const currentCategoryFilters = categoryFilters[category] || {}; 

        const checkboxes = filtersContainer.querySelectorAll('.checkbox');
        checkboxes.forEach(checkbox => {
        if (checkbox.classList.contains('active')) {
            const filterId = checkbox.getAttribute('data-filter');
            if (stockCategories.includes(filterId)) {
                activeFilters.availability.push(filterId);
            } else if (statusCategories.includes(filterId)) {
                activeFilters.status.push(filterId);
            }
            else if (currentCategoryFilters.brands?.includes(filterId)) {
                if (!activeFilters.Brands.includes(filterId)) {
                    activeFilters.Brands.push(filterId);
                }
            } else if (currentCategoryFilters.attributes?.includes(filterId)) {
                if (!activeFilters.Attributes.includes(filterId)) {
                    activeFilters.Attributes.push(filterId);
                }
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

        initSliderValues();
        initActiveFilters();

        let currentIndex = 8; // Начальный индекс для следующих товаров

        function sendFiltersToServer() {
            const filters = getActiveFilters();
    
        filters.CategoryDTO = { Name: category }; // Добавляем категорию в фильтры
    
        fetch('api/Product/get-all', {
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
            const showMoreButton = document.getElementById('show-more');
            const productCountElement = document.querySelector('.product-count');
    
            if (!container) {
                console.error('Контейнер для карточек не найден.');
                return;
            }
    
            container.innerHTML = ''; // Очищаем контейнер
            currentIndex = 8; // Сбрасываем индекс при обновлении фильтров
            const products = data.data;
    
            if (products && Array.isArray(products)) {
                const totalProducts = products.length;
                if (productCountElement) {
                    productCountElement.textContent = `${totalProducts} товаров`;
                }
    
                renderProductCards(products.slice(0, 8), container); // Показываем первые 5 товаров
    
                if (products.length > 8) {
                    showMoreButton.style.display = 'block';
                } else {
                    showMoreButton.style.display = 'none';
                }
    
                showMoreButton.onclick = () => {
                    // Подгружаем следующие 5 товаров
                    const nextProducts = products.slice(currentIndex, currentIndex + 8);
                    renderProductCards(nextProducts, container);
    
                    // Обновляем текущий индекс
                    currentIndex += 8;
    
                    if (currentIndex >= products.length) {
                        showMoreButton.style.display = 'none'; // Скрываем кнопку, если товаров больше нет
                    }
                };
    
                container.addEventListener('click', (event) => {
                    const target = event.target;
                    const card = target.closest('.product-card-main');
                    if (card && !target.closest('.order-button-main') && !target.closest('.compare-prices-main')) {
                        const productId = card.getAttribute('data-id');
                        if (productId) {
                            window.location.href = `product.html?id=${productId}`;
                        }
                    }
                });
    
            } else {
                console.warn('Нет данных товаров для отображения.');
            }
        })
        .catch(error => {
            console.error('Ошибка при получении данных:', error);
        });
    }
    
    function renderProductCards(products, container) {
        products.forEach(product => {
            const card = document.createElement('div');
            card.classList.add('product-card-main');
            if (product.id !== undefined) {
                card.setAttribute('data-id', product.id.toString());
            }
    
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
            container.appendChild(card);
        });
    
        const compareButtons = container.querySelectorAll('.compare-prices-main');
        compareButtons.forEach(button => {
            button.addEventListener('click', (event) => {
                event.stopPropagation();
                const productName = button.getAttribute('data-product-name');
                const yandexMarketURL = `https://market.yandex.ru/search?text=${encodeURIComponent(productName)}`;
                window.open(yandexMarketURL, '_blank');
            });
        });
    }
    

    sendFiltersToServer();
    })
    .catch(error => {
        console.error('Ошибка при загрузке данных:', error);
    });
    
});
