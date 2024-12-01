document.addEventListener('DOMContentLoaded', function () {
    // Подгрузка хитов продаж
    fetch('http://192.168.192.59/сайт/filters.php', {
        method: 'POST',
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
    if (card) {
        const productId = card.getAttribute('data-id');
        console.log('id',productId);
        if (productId) {
            window.location.href = `product.html?id=${productId}`;
        }
    }
});