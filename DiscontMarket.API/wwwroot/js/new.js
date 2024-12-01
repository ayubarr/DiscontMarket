document.addEventListener('DOMContentLoaded', function () {
    // Подгрузка лучших новинок
    fetch('api/Product/get-all', {
        method: 'POST',
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
    if (card) {
        const productId = card.getAttribute('data-id');
        console.log('id',productId);
        if (productId) {
            window.location.href = `product.html?id=${productId}`;
        }
    }
});