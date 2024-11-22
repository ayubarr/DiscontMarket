const brands_carousel = document.querySelector('.brands-carousel');
 
brands_carousel.addEventListener('wheel', (event) => {
    event.preventDefault(); // Предотвращает вертикальную прокрутку страницы
    const scrollAmountnew2 = event.deltaY * 4; // Увеличиваем величину прокрутки для плавного эффекта
    
    brands_carousel.scrollBy({
        left: scrollAmountnew2,
        behavior: 'smooth'
    });
});