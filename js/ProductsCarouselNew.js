const carouselnew = document.querySelector('.products-carousel-new');

carouselnew.addEventListener('wheel', (event) => {
event.preventDefault(); // Предотвращает вертикальную прокрутку страницы
const scrollAmountnew = event.deltaY * 2; // Увеличиваем величину прокрутки для плавного эффекта

carouselnew.scrollBy({
    left: scrollAmountnew,
    behavior: 'smooth'
});
});