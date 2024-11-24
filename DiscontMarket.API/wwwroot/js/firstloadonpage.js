document.addEventListener('DOMContentLoaded', function () {
    // Получаем параметры из URL
    const urlParams = new URLSearchParams(window.location.search);
    
    // Получаем первый ключ из параметров URL (категория)
    const category = urlParams.keys().next().value;

    if (category) {
        // Отправляем POST-запрос на сервер с категорией
        fetch('http://192.168.192.59/сайт/filters.php', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ CategoryDTO: {Name : category}}),
        })
        .then(response => response.json())
        .then(data => {
            console.log('Данные успешно получены:', data);

            // Проверяем, что мы на странице catalog.html и есть контейнер для карточек
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
    } else {
        console.error('Не удалось получить категорию из URL.');
    }
});
