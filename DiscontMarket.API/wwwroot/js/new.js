document.addEventListener('DOMContentLoaded', function () {
    // Подгрузка лучших новинок
    fetch('api/Product/get-all-news', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    .then(response => response.json())
    .then(data => {
        console.log('Лучшие новинки успешно получены:', data);
        const newProductsContainer = document.querySelector('.products-carousel-new');

        if (!newProductsContainer) {
            console.error('Контейнер для лучших новинок не найден.');
            return;
        }

        newProductsContainer.innerHTML = ''; // Очистить контейнер перед добавлением новинок

        const newProducts = data.data; // Допустим, ответ содержит массив в `newProducts`
        
        if (newProducts && Array.isArray(newProducts)) {
            newProducts.forEach(product => {
                const productCard = document.createElement('div');
                productCard.classList.add('product-card');
                productCard.setAttribute('data-id', product.id);

                productCard.innerHTML = `
                    <img src="${product.image || 'items/filters/no-image.png'}" alt="Товар" class="product-image">
                    <div class="product-separator"></div>
                    <p class="product-name">${product.productName}</p>
                    <div class="product-price-container">
                        <span class="product-price">${product.price} ₽</span>
                        <button class="order-button">Оформить заказ</button>
                    </div>
                    <div class="compare-prices-wrapper">
                        <span class="compare-prices" data-product-name="${product.productName}">Сравнить цены</span>
                    </div>
                `;
                newProductsContainer.appendChild(productCard);
            });

            // Добавляем обработчики для сравнения цен
            const compareButtons = newProductsContainer.querySelectorAll('.compare-prices');
            compareButtons.forEach(button => {
                button.addEventListener('click', (event) => {
                    event.stopPropagation();
                    const productName = button.getAttribute('data-product-name');
                    const yandexMarketURL = `https://market.yandex.ru/search?text=${encodeURIComponent(productName)}`;
                    window.open(yandexMarketURL, '_blank');
                });
            });

            function showOrderConfirmation() {
                let orderCard = document.querySelector('.order-card');
                let overlay = document.querySelector('.overlay');

                // Если слой затемнения еще не создан, создаем его
                if (!overlay) {
                    overlay = document.createElement('div');
                    overlay.classList.add('overlay');
                    document.body.appendChild(overlay);
                }

                // Если карточка еще не создана, создаем её
                if (!orderCard) {
                    orderCard = document.createElement('div');
                    orderCard.classList.add('order-card');

                    orderCard.innerHTML = `
                <div class="order-card-content">
                    <span class="close-order-card">&times;</span>
                    <p class="order-message">Заказ успешно оформлен</p>
                    <p class="order-phone">Номер для связи с магазином: <span>8 (981) 210-48-31</span></p>
                    <button class="call-button">Позвонить</button>
                </div>
            `;
                    document.body.appendChild(orderCard);
                }

                // Показываем карточку и слой затемнения
                orderCard.style.display = 'block';
                overlay.style.display = 'block';

                // Обработчик закрытия карточки по крестику
                orderCard.querySelector('.close-order-card').addEventListener('click', closeOrderCard);

                // Закрытие при клике на затемненный слой
                overlay.addEventListener('click', closeOrderCard);

                function closeOrderCard() {
                    orderCard.style.display = 'none';
                    overlay.style.display = 'none';
                }

                // Обработчик кнопки Позвонить
                orderCard.querySelector('.call-button').addEventListener('click', () => {
                    window.location.href = 'tel:+79812104831';
                });
            }

            // Добавление обработчика события на кнопку "Оформить заказ"
            document.querySelectorAll('.order-button').forEach(button => {
                button.addEventListener('click', showOrderConfirmation);
            });


        } else {
            console.warn('Нет данных о лучших новинках для отображения.');
        }
    })
    .catch(error => {
        console.error('Ошибка при получении лучших новинок:', error);
    });
});

// Обработчик для кликов по карточкам товаров из результатов поиска
document.querySelector('.products-carousel-new').addEventListener('click', function(event) {
    const card = event.target.closest('.product-card');
    console.log('Да');
    if (card && !event.target.classList.contains('order-button')) {
        const productId = card.getAttribute('data-id');
        console.log('id',productId);
        if (productId) {
            window.location.href = `product.html?id=${productId}`;
        }
    }
});