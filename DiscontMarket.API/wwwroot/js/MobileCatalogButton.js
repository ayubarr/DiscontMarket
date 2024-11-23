document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("toggleButton");
    const buttonContainer = document.querySelector(".button-container");

    // Отображение кнопки при разрешении <= 869px
    function checkWindowSize() {
        if (window.innerWidth <= 869) {
            toggleButton.style.display = "flex";
        } else {
            toggleButton.style.display = "none";
            buttonContainer.style.display = "block"; // Показываем контейнер на больших экранах
        }
    }

    // Начальное состояние контейнера при загрузке страницы
    buttonContainer.style.display = "none";  // Контейнер скрыт по умолчанию

    // Добавляем событие клика
    toggleButton.addEventListener("click", function () {
        if (buttonContainer.style.display === "none" || buttonContainer.style.display === "") {
            buttonContainer.style.display = "flex";
        } else {
            buttonContainer.style.display = "none";
        }
    });

    // Проверяем размер окна при загрузке и изменении
    window.addEventListener("resize", checkWindowSize);
    checkWindowSize();
});
