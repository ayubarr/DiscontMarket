document.addEventListener('DOMContentLoaded', function () {
    const logo = document.getElementById('logo');

    if (logo) {
        logo.addEventListener('click', function () {
            window.location.href = 'index.html'; // Переход на главную страницу
        });
    }
});
