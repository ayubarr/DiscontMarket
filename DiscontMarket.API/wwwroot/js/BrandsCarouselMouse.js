const brandsCarousel = document.querySelector('.brands-carousel');
let isDragging = false;
let startX;
let scrollLeft;

// Начинаем отслеживать нажатие
brandsCarousel.addEventListener('mousedown', (e) => {
    isDragging = true;
    startX = e.pageX - brandsCarousel.offsetLeft;
    scrollLeft = brandsCarousel.scrollLeft;
    brandsCarousel.classList.add('no-select'); // Отключаем выделение
    brandsCarousel.classList.add('active');    // Добавляем класс активного состояния
});

// Завершаем отслеживание при отпускании мыши
brandsCarousel.addEventListener('mouseleave', () => {
    isDragging = false;
    brandsCarousel.classList.remove('no-select'); // Включаем выделение
    brandsCarousel.classList.remove('active');    // Убираем класс активного состояния
});

brandsCarousel.addEventListener('mouseup', () => {
    isDragging = false;
    brandsCarousel.classList.remove('no-select'); // Включаем выделение
    brandsCarousel.classList.remove('active');    // Убираем класс активного состояния
});

// Реализация перетаскивания при перемещении мыши
brandsCarousel.addEventListener('mousemove', (e) => {
    if (!isDragging) return; // Прекращаем выполнение, если не перетаскиваем
    e.preventDefault();
    const x = e.pageX - brandsCarousel.offsetLeft;
    const walk = (x - startX) * 4; // Скорость прокрутки
    brandsCarousel.scrollLeft = scrollLeft - walk;
});