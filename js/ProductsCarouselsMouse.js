    // Функция для добавления drag-scroll
    function enableDragScroll(carouselSelector) {
        const carousel = document.querySelector(carouselSelector);
        let isDragging = false;
        let startX;
        let scrollLeft;
      
        carousel.addEventListener('mousedown', (e) => {
            isDragging = true;
            startX = e.pageX - carousel.offsetLeft;
            scrollLeft = carousel.scrollLeft;
            carousel.classList.add('no-select'); // Отключаем выделение
            carousel.classList.add('active');    // Добавляем класс для активного состояния
        });
      
        carousel.addEventListener('mouseleave', () => {
            if (isDragging) {
                isDragging = false;
                carousel.classList.remove('no-select'); // Включаем выделение
                carousel.classList.remove('active');    // Убираем класс активного состояния
            }
        });
      
        carousel.addEventListener('mouseup', () => {
            isDragging = false;
            carousel.classList.remove('no-select'); // Включаем выделение
            carousel.classList.remove('active');    // Убираем класс активного состояния
        });
      
        carousel.addEventListener('mousemove', (e) => {
            if (!isDragging) return;
            e.preventDefault();
            const x = e.pageX - carousel.offsetLeft;
            const walk = (x - startX) * 2;
            carousel.scrollLeft = scrollLeft - walk;
        });
        
    }
    
    // Подключение drag-scroll для каруселей
    enableDragScroll('.products-carousel-new');
    enableDragScroll('.products-carousel');