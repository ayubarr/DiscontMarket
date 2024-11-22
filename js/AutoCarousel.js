document.addEventListener("DOMContentLoaded", function () {
    const carousel = document.getElementById("carousel");
    const dots = document.querySelectorAll(".dot");
    const prevBtn = document.getElementById("prevBtn");
    const nextBtn = document.getElementById("nextBtn");
    let currentIndex = 0;
    const items = document.querySelectorAll(".carousel-item");
    const totalItems = items.length;
    let autoScrollInterval;

    // Функция для переключения изображений
    function goToSlide(index) {
        if (index < 0) {
            index = totalItems - 1;
        } else if (index >= totalItems) {
            index = 0;
        }
        carousel.style.transform = `translateX(-${index * 100}%)`;
        currentIndex = index;
        updateIndicators();
        resetAutoScroll();
    }

    // Обновляем индикаторы
    function updateIndicators() {
        dots.forEach(dot => dot.classList.remove("active"));
        dots[currentIndex].classList.add("active");
    }

    // Свайп влево или вправо
    carousel.addEventListener('touchstart', handleTouchStart, false);
    carousel.addEventListener('touchend', handleTouchEnd, false);

    let touchStartX = 0;

    function handleTouchStart(e) {
        touchStartX = e.changedTouches[0].clientX;
    }

    function handleTouchEnd(e) {
        let touchEndX = e.changedTouches[0].clientX;
        if (touchStartX - touchEndX > 50) {
            goToSlide(currentIndex + 1);
        } else if (touchEndX - touchStartX > 50) {
            goToSlide(currentIndex - 1);
        }
    }

    // Обработчик кликов по кнопкам для ПК
    prevBtn.addEventListener("click", function () {
        goToSlide(currentIndex - 1);
    });

    nextBtn.addEventListener("click", function () {
        goToSlide(currentIndex + 1);
    });

    // Обновляем таймер авто-свайпа
    function resetAutoScroll() {
        clearInterval(autoScrollInterval);
        startAutoScroll();
    }

    // Автоматический свайп каждые 5 секунд
    function startAutoScroll() {
        autoScrollInterval = setInterval(() => {
            goToSlide(currentIndex + 1);
        }, 5000);
    }

    startAutoScroll(); // Инициализация авто-свайпа

    // Инициализация первого индикатора
    updateIndicators();

    // Обработчики кликов по точкам
    dots.forEach(dot => {
        dot.addEventListener("click", function () {
            goToSlide(parseInt(dot.getAttribute("data-index")));
        });
    });

    // Появление кнопок при приближении курсора
    const carouselContainer = document.querySelector(".carousel-container");

    carouselContainer.addEventListener("mouseover", function () {
        prevBtn.style.opacity = 1;
        nextBtn.style.opacity = 1;
    });

    carouselContainer.addEventListener("mouseout", function () {
        prevBtn.style.opacity = 0;
        nextBtn.style.opacity = 0;
    });
});
