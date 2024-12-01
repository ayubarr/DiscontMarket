let currentIndex = 0;

    function updateLargeImage() {
        const thumbnails = document.querySelectorAll('.thumbnail');
        if (thumbnails.length > 0) {
            largeImage.src = thumbnails[currentIndex].src;
            thumbnails.forEach((thumbnail, index) => {
                thumbnail.classList.toggle('active', index === currentIndex);
            });
            updateIndicators();
        }
    }

    function createIndicators() {
        const indicatorContainer = document.querySelector('.indicator-container');
        const thumbnails = document.querySelectorAll('.thumbnail');
        indicatorContainer.innerHTML = '';
        thumbnails.forEach((_, index) => {
            const indicator = document.createElement('div');
            indicator.classList.add('indicator');
            if (index === currentIndex) indicator.classList.add('active');
            indicator.addEventListener('click', () => {
                currentIndex = index;
                updateLargeImage();
            });
            indicatorContainer.appendChild(indicator);
        });
    }

    function updateIndicators() {
        const indicators = document.querySelectorAll('.indicator');
        indicators.forEach((indicator, index) => {
            indicator.classList.toggle('active', index === currentIndex);
        });
    }

    function nextImage() {
        const thumbnails = document.querySelectorAll('.thumbnail');
        currentIndex = (currentIndex + 1) % thumbnails.length;
        updateLargeImage();
    }

    function prevImage() {
        const thumbnails = document.querySelectorAll('.thumbnail');
        currentIndex = (currentIndex - 1 + thumbnails.length) % thumbnails.length;
        updateLargeImage();
    }

    // Свайп
    let startX = 0;

    largeImage.addEventListener('touchstart', (e) => {
        startX = e.touches[0].clientX;
    });

    largeImage.addEventListener('touchend', (e) => {
        const endX = e.changedTouches[0].clientX;
        if (startX - endX > 50) nextImage();
        if (endX - startX > 50) prevImage();
    });

    // Инициализация
    document.addEventListener('DOMContentLoaded', () => {
        createIndicators();
        updateLargeImage();
    });

    function changeImage(thumbnail) {
    const largeImage = document.getElementById('largeImage');
    const allThumbnails = document.querySelectorAll('.thumbnail');

    largeImage.src = thumbnail.src;

    allThumbnails.forEach(img => img.classList.remove('active'));
    thumbnail.classList.add('active');
    }

    function enableZoom() {
        largeImage.addEventListener('mouseenter', zoomIn);
        largeImage.addEventListener('mousemove', zoomMove);
        largeImage.addEventListener('mouseleave', zoomOut);
    }

    function disableZoom() {
        largeImage.removeEventListener('mouseenter', zoomIn);
        largeImage.removeEventListener('mousemove', zoomMove);
        largeImage.removeEventListener('mouseleave', zoomOut);
    }

    function checkWindowSize() {
        if (window.innerWidth >= 870) {
            enableZoom();
        } else {
            disableZoom();
        }
    }

    function zoomIn() {
        largeImage.style.transform = 'scale(3)';
    }

    function zoomMove(event) {
        const rect = largeImage.getBoundingClientRect();
        const x = ((event.clientX - rect.left) / rect.width) * 100;
        const y = ((event.clientY - rect.top) / rect.height) * 100;
        largeImage.style.transformOrigin = `${x}% ${y}%`;
    }

    function zoomOut() {
        largeImage.style.transform = 'scale(1)';
    }

    // Инициализация при загрузке страницы
    document.addEventListener('DOMContentLoaded', checkWindowSize);
    // Проверка при изменении размеров окна
    window.addEventListener('resize', checkWindowSize);