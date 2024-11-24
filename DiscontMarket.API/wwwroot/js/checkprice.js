document.addEventListener('DOMContentLoaded', () => {
    const container = document.querySelector('.main-items-section .products-carousel');

    if (!container) {
        console.error('Контейнер для карточек не найден. Убедитесь, что HTML содержит .main-items-section .products-carousel.');
        return;
    }

    // Функция для добавления обработчиков кнопок "Сравнить цены"
    function addComparePricesHandlers() {
        const compareButtons = container.querySelectorAll('.compare-prices');
        compareButtons.forEach(button => {
            button.addEventListener('click', () => {
                const productName = button.getAttribute('data-product-name');
                const yandexMarketURL = `https://market.yandex.ru/search?text=${encodeURIComponent(productName)}`;
                window.open(yandexMarketURL, '_blank'); // Открываем ссылку в новой вкладке
            });
        });
    }

    // Экспорт функции для использования в основном скрипте
    window.addComparePricesHandlers = addComparePricesHandlers;
});
