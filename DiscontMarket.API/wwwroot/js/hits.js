document.addEventListener('DOMContentLoaded', function () {
    // Подгрузка хитов продаж
    fetch('api/Product/get-all-hits', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    .then(response => response.json())
    .then(data => {
        console.log('Хиты продаж успешно получены:', data);
        const hitsContainer = document.querySelector('.products-carousel');

        if (!hitsContainer) {
            console.error('Контейнер для хитов продаж не найден.');
            return;
        }

        hitsContainer.innerHTML = ''; // Очистить контейнер перед добавлением хитов

        const hits = data.data; // Допустим, ответ содержит массив в `hits`
        
        if (hits && Array.isArray(hits)) {
            hits.forEach(hit => {
                const hitCard = document.createElement('div');
                hitCard.classList.add('product-card');
                hitCard.setAttribute('data-id', hit.id);

                hitCard.innerHTML = `
                    <img src="${hit.image || 'items/filters/no-image.png'}" alt="Товар" class="product-image">
                    <div class="product-separator"></div>
                    <p class="product-name">${hit.productName}</p>
                    <div class="product-price-container">
                        <span class="product-price">${hit.price} ₽</span>
                        <button class="order-button">Оформить заказ</button>
                    </div>
                    <div class="compare-prices-wrapper">
                        <span class="compare-prices" data-product-name="${hit.productName}">Сравнить цены</span>
                    </div>
                `;
                hitsContainer.appendChild(hitCard);
            });

            // Добавляем обработчики для сравнения цен
            const compareButtons = hitsContainer.querySelectorAll('.compare-prices');
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
            console.warn('Нет данных о хитах продаж для отображения.');
        }
    })
    .catch(error => {
        console.error('Ошибка при получении хитов продаж:', error);
    });
});

// Обработчик для кликов по карточкам товаров из результатов поиска
document.querySelector('.products-carousel').addEventListener('click', function(event) {
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