document.addEventListener('DOMContentLoaded', function () {
    const loadingScreen = document.getElementById('loading-screen');

    // Скрываем экран загрузки через 1 секунду
    setTimeout(() => {
        loadingScreen.classList.add('hidden');
    }, 1000);
});
