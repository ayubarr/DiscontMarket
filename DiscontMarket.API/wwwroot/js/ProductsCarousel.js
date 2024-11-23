const carousel = document.querySelector('.products-carousel');

carousel.addEventListener('wheel', (event) => {
event.preventDefault(); // Предотвращает вертикальную прокрутку страницы
const scrollAmount = event.deltaY * 2; // Увеличиваем величину прокрутки для плавного эффекта

carousel.scrollBy({
    left: scrollAmount,
    behavior: 'smooth'
});
});