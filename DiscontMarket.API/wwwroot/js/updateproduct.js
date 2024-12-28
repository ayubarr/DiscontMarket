document.addEventListener("DOMContentLoaded", function () {
    const urlParams = new URLSearchParams(window.location.search);
    const productId = parseInt(urlParams.get('id'), 10); // Преобразование в int с основанием 10
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

    function showOrderConfirmation() {
        let orderCard = document.querySelector('.order-card');
        let overlay = document.querySelector('.overlay');

        // Если слой затемнения еще не создан, создаем его
        if (!overlay) {
            overlay = document.createElement('div');
            overlay.classList.add('overlay');
            document.body.appendChild(overlay);
        }

        // Если карточка еще не создана, создаем её
        if (!orderCard) {
            orderCard = document.createElement('div');
            orderCard.classList.add('order-card');

            orderCard.innerHTML = `
                <div class="order-card-content">
                    <span class="close-order-card">&times;</span>
                    <p class="order-phone">Для завершения оформления заказа оставьте свой номер телефона или свяжитесь с менеджером по телефону: <span>8 (981) 210-48-31</span></p>
                    <input type="tel" class="phone-input" placeholder="+7XXXXXXXXXX" maxlength="12" oninput="if (!this.value.startsWith('+7')) this.value = '+7' + this.value.replace(/[^0-9]/g, '').slice(2); else this.value = '+7' + this.value.slice(2).replace(/[^0-9]/g, '');" />
                    <input type="name" class="name-input" placeholder="Введите ФИО" maxlength="50"/>
                    <button class="submit-button">Отправить</button>
                    <button class="call-button">Позвонить</button>
                </div>
            `;
            document.body.appendChild(orderCard);

            // Закрытие окна по клику на крестик
            orderCard.querySelector('.close-order-card').addEventListener('click', () => {
                orderCard.style.display = 'none';
            });

            // Обработчик для кнопки "Отправить"
            orderCard.querySelector('.submit-button').addEventListener('click', () => {
                let phoneInput = orderCard.querySelector('.phone-input').value.trim();
                let nameInput = orderCard.querySelector('.name-input').value.trim();
                let currentURL = window.location.href;

                if (phoneInput.length === 12) {
                    // Первый запрос для проверки почты
                    fetch('api/Order/get-email', { 
                        method: 'GET',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                    })
                    .then(response => response.json()) // Парсим JSON-ответ
                    .then(data => {
                        if (data?.data) { // Проверяем email из ответа
                            // Второй запрос для отправки данных
                            let currentDateTime = new Date().toLocaleString("ru-RU", {
                                year: 'numeric',
                                month: '2-digit',
                                day: '2-digit',
                                hour: '2-digit',
                                minute: '2-digit'
                            }).replace(',', '');

                            let order = {
                                phoneNumber: phoneInput,
                                email: data.data,
                                name: nameInput,
                                url: currentURL,
                                datetime: currentDateTime
                            };

                            fetch('api/Order/send-info', { 
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json',
                                },
                                body: JSON.stringify(order),
                            })
                            .then(response => {
                                if (response.ok) {
                                    alert(`Номер телефона ${phoneInput} успешно отправлен!`);
                                    orderCard.style.display = 'none';
                                    overlay.style.display = 'none';
                                } else {
                                    alert('Ошибка при отправке номера. Попробуйте еще раз.');
                                }
                            })
                            .catch(error => {
                                console.error('Ошибка при отправке данных:', error);
                                alert('Произошла ошибка при отправке.');
                            });
                        } else {
                            alert('Ошибка: email не найден.');
                        }
                    })
                    .catch(error => {
                        console.error('Ошибка при получении email:', error);
                        alert('Произошла ошибка при получении email.');
                    });
                } else {
                    alert('Пожалуйста, введите номер телефона.');
                }
            });
        }

        // Показываем карточку и слой затемнения
        orderCard.style.display = 'block';
        overlay.style.display = 'block';

        // Обработчик закрытия карточки по крестику
        orderCard.querySelector('.close-order-card').addEventListener('click', closeOrderCard);

        // Закрытие при клике на затемненный слой
        overlay.addEventListener('click', closeOrderCard);

        function closeOrderCard() {
            orderCard.style.display = 'none';
            overlay.style.display = 'none';
        }

        // Обработчик кнопки Позвонить
        orderCard.querySelector('.call-button').addEventListener('click', () => {
            window.location.href = 'tel:+79812104831';
        });
    }

    // Добавление обработчика события на кнопку "Оформить заказ"
    document.querySelectorAll('.order-button-product').forEach(button => {
        button.addEventListener('click', showOrderConfirmation);
    });

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
        body: JSON.stringify(productId),
    })
    .then((response) => response.json())
    .then((data) => {
        renderProductDetails(data);
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