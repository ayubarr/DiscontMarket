document.querySelector('.search-input').addEventListener('input', function() {
    const resultsContainer = document.querySelector('.search-results');
    const query = this.value.trim(); // Убираем лишние пробелы

    if (query.length <= 1) {
        resultsContainer.innerHTML = '';
        resultsContainer.style.display = 'none'; // Скрыть контейнер
    } else if (query.length >= 2) {
        fetch('api/Product/get-by-name', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ query: query })
        })
        .then(response => response.json())
        .then(data => {
            resultsContainer.innerHTML = '';
            resultsContainer.style.display = 'flex';

            if (Array.isArray(data)) {
                if (data.length > 0) {
                    data.forEach(product => { // Показываем максимум 10 карточек
                        console.log(product.productId);
                        resultsContainer.innerHTML += `
                            <div class="product-card-search" data-id="${product.productId}">
                                <img src="${product.images[0]}" alt="${product.title}">
                                <span>${product.title}</span>
                            </div>`;
                    });
                } else {
                    resultsContainer.innerHTML = '<div class="no-results">Ничего не найдено</div>';
                }
            } else if (typeof data === 'object' && data.error) {
                resultsContainer.innerHTML = `<div class="no-results">${data.error}</div>`;
            }
        })
        .catch(error => {
            console.error('Ошибка при поиске:', error);
        });
    }
});

// Обработчик для кликов по карточкам товаров из результатов поиска
document.querySelector('.search-results').addEventListener('click', function(event) {
    const card = event.target.closest('.product-card-search');
    console.log('Да');
    if (card) {
        const productId = card.getAttribute('data-id');
        if (productId) {
            window.location.href = `product.html?id=${productId}`;
        }
    }
});