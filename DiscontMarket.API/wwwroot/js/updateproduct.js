document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const productId = urlParams.get('id');
    console.log(productId);
    const characteristicsList = document.getElementById("characteristicsList");
    const productFullDescription = document.getElementById("productFullDescription");
    const productDescription = document.getElementById("productDescription");
    const productTitle = document.getElementById("productTitle");
    const ratingContainer = document.getElementById("ratingContainer");
    const ratingValue = document.getElementById("ratingValue");
    const productPrice = document.getElementById("productPrice");
    const priceValue = document.querySelector(".price-value");
    const imageListContainer = document.querySelector(".image-list"); // Контейнер для изображений
    const largeImage = document.getElementById("largeImage"); // Большое изображение

    // Функция для рендера данных товара
    function renderProductDetails(data) {
        // Заполняем название товара
        if (data.title) {
            productTitle.textContent = data.title;
            productTitle.setAttribute("data-title", data.title);
        }

        // Заполняем рейтинг и звезды
        if (data.rating) {
            ratingContainer.setAttribute("data-rating", data.rating);
            ratingValue.textContent = `${data.rating} из 5`;

            // Заполнение звезд
            const rating = parseFloat(data.rating);
            ratingContainer.innerHTML = ""; // Очищаем старые звезды
            for (let i = 0; i < 5; i++) {
                const star = document.createElement("div");
                star.className = "star";
                const fill = document.createElement("div");
                fill.className = "fill";
                const remaining = Math.max(0, Math.min(1, rating - i));
                const fillPercentage = remaining * 100;
                fill.style.background = `linear-gradient(to right, #005BFF ${fillPercentage}%, transparent ${fillPercentage}%)`;
                star.appendChild(fill);
                ratingContainer.appendChild(star);
            }
        }

        // Заполняем описание товара
        if (data.description) {
            productDescription.textContent = data.description;
        } else {
            productDescription.textContent = "Описание товара отсутствует.";
        }

        // Заполняем полное описание товара
        if (data.fullDescription) {
            productFullDescription.textContent = data.fullDescription;
        } else {
            productFullDescription.textContent = "Полное описание товара отсутствует.";
        }

        // Заполняем цену
        if (data.price) {
            productPrice.setAttribute("data-price", data.price);
            priceValue.textContent = data.price;
        }

        // Заполняем характеристики товара
        characteristicsList.innerHTML = "";
        if (data.characteristics && data.characteristics.length > 0) {
            data.characteristics.forEach((item) => {
                const characteristicItem = document.createElement("div");
                characteristicItem.className = "characteristic-item";

                const nameSpan = document.createElement("span");
                nameSpan.className = "characteristic-name";
                nameSpan.textContent = item.name;

                const dots = document.createElement("div");
                dots.className = "dots";

                const valueSpan = document.createElement("span");
                valueSpan.className = "characteristic-value";
                valueSpan.textContent = item.value;

                characteristicItem.appendChild(nameSpan);
                characteristicItem.appendChild(dots);
                characteristicItem.appendChild(valueSpan);
                characteristicsList.appendChild(characteristicItem);
            });
        } else {
            characteristicsList.innerHTML = "<p>Характеристики не указаны.</p>";
        }

        // Заполняем изображения
        if (data.images && data.images.length > 0) {
            imageListContainer.innerHTML = ""; // Очищаем старые изображения
            data.images.forEach((imagePath, index) => {
                const imgElement = document.createElement("img");
                imgElement.src = imagePath;
                imgElement.alt = `Image ${index + 1}`;
                imgElement.className = `thumbnail ${index === 0 ? "active" : ""}`;
                imgElement.onclick = () => {
                    changeImage(imgElement);
                };
                imageListContainer.appendChild(imgElement);
            });
            // Устанавливаем первое изображение как большое
            largeImage.src = data.images[0];
            currentIndex = 0; // Сброс текущего индекса
            createIndicators(); // Создание индикаторов
        } else {
            largeImage.src = "items/filters/no-image.png";
        }
    }

    // Обработчик смены большого изображения
    function changeImage(imgElement) {
        const thumbnails = document.querySelectorAll(".thumbnail");
        thumbnails.forEach((thumbnail) => thumbnail.classList.remove("active"));
        imgElement.classList.add("active");
        largeImage.src = imgElement.src;
    }

    // Получение данных товара через запрос
    fetch("api/Product/get-by-id", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ productId: productId }),
    })
        .then((response) => response.json())
        .then((data) => {
            renderProductDetails(data);
            console.log('Успех', data);
            if (data.title) {
                document.title = data.title;
            } else {
                document.title = "Товар не найден";
            }
        })
        .catch((error) => {
            console.error("Ошибка загрузки данных о товаре:", error);
        });
});