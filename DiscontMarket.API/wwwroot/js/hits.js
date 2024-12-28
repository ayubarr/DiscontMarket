let currentURL;
document.addEventListener('DOMContentLoaded', function () {
    // Подгрузка хитов продаж
    fetch('api/Product/get-all-hits', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    .then(response => response.json())
    .then(data => {
        const hitsContainer = document.querySelector('.products-carousel');

        if (!hitsContainer) {
            return;
        }

        hitsContainer.innerHTML = ''; // Очистить контейнер перед добавлением хитов

        const hits = data.data; // Допустим, ответ содержит массив в `hits`

        if (hits && Array.isArray(hits)) {
            hits.forEach(hit => {
                const hitCard = document.createElement('div');
                hitCard.classList.add('product-card');
                hitCard.setAttribute('data-id', hit.id);

                let status = 'Дисконт';

                if (hit.productStatus === 'discount' || hit.productStatus === 'Discount') {
                    status = 'Дисконт';
                }
                else if (hit.productStatus === 'damagedpackage' || hit.productStatus === 'DamagedPackage') {
                    status = 'П/У';
                }
                else if (hit.productStatus === 'minordefects' || hit.productStatus === 'MinorDefects') {
                    status = 'Мелкие дефекты';
                }
                else {
                    status = 'None';
                }

                hitCard.innerHTML = `
                <img src="${hit.image || 'items/filters/no-image.png'}" alt="Товар" class="product-image">
                <div class="product-separator"></div>
                <p class="product-name">${hit.productName}</p>
                <div class="product-price-container">
                    <span class="product-price">${hit.price} ₽</span>
                    <button class="order-button">Оформить заказ</button>
                </div>
                <div class="compare-prices-wrapper">
                    <span class="compare-status-main">${status}</span>
                    <span class="compare-prices" data-product-name="${hit.productName}">Сравнить цены</span>
                </div>
            `;
                hitsContainer.appendChild(hitCard);
            });

            // Добавляем обработчики для сравнения цен
            const compareButtons = hitsContainer.querySelectorAll('.compare-prices');
            compareButtons.forEach(button => {
                button.addEventListener('click', (event) => {
                    event.stopPropagation();
                    const productName = button.getAttribute('data-product-name');
                    const sanitizedProductName = productName
                        .replace(/["']/g, '')  // Удаляем кавычки
                        .replace(/\s+/g, ' ') // Убираем лишние пробелы
                        .trim();              // Убираем пробелы по краям
                    const yandexMarketURL = `https://market.yandex.ru/search?text=${encodeURIComponent(sanitizedProductName)}`;
                    window.open(yandexMarketURL, '_blank');
                });
            });

            function showStatusInfo(statusText) {
                let statusCard = document.querySelector('.status-card');
                let overlay = document.querySelector('.overlay');

                // Если слой затемнения еще не создан, создаем его
                if (!overlay) {
                    overlay = document.createElement('div');
                    overlay.classList.add('overlay');
                    document.body.appendChild(overlay);
                }

                // Если карточка еще не создана, создаем её
                if (!statusCard) {
                    statusCard = document.createElement('div');
                    statusCard.classList.add('status-card');

                    statusCard.innerHTML = `
                    <div class="status-card-content">
                        <span class="close-status-card">&times;</span>
                        <p class="status-message">${statusText}</p>
                        <button class="call-button">Позвонить</button>
                    </div>
                `;
                    document.body.appendChild(statusCard);
                }

                // Устанавливаем текст в карточке
                statusCard.querySelector('.status-message').textContent = statusText;

                // Показываем карточку и слой затемнения
                statusCard.style.display = 'block';
                overlay.style.display = 'block';

                // Обработчик закрытия карточки по крестику
                statusCard.querySelector('.close-status-card').addEventListener('click', closeStatusCard);

                // Закрытие при клике на затемненный слой
                overlay.addEventListener('click', closeStatusCard);

                function closeStatusCard() {
                    statusCard.style.display = 'none';
                    overlay.style.display = 'none';
                }
                // Обработчик кнопки Позвонить
                statusCard.querySelector('.call-button').addEventListener('click', () => {
                    window.location.href = 'tel:+79812104831';
                });
            }

            // Добавляем обработчик на элементы с классом compare-status-main
            document.querySelectorAll('.compare-status-main').forEach(button => {
                button.addEventListener('click', (event) => {
                    event.stopPropagation();
                    const productStatus = button.textContent.trim();

                    let statusText = 'Уценённый товар может иметь незначительные повреждения не влияющие на работоспособность. Более точную информацию можно узнать у менеджера.';


                    showStatusInfo(statusText);
                });
            });

            function showOrderConfirmation() {
                let orderCard = document.querySelector('.order-card');
                let overlay = document.querySelector('.overlay');

                // Если слой затемнения еще не создан, создаем его
                if (!overlay) {
                    overlay = document.createElement('div');
                    overlay.classList.add('overlay');
                    document.body.appendChild(overlay);
                }

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
            document.querySelectorAll('.order-button').forEach(button => {
                button.addEventListener('click', (event) => {
                    const target = event.target;
                    const card = target.closest('.product-card');

                    if (card) {
                        const productId = card.getAttribute('data-id');
                        if (productId) {
                            let domain = window.location.origin;
                            currentURL = `${domain}/product.html?id=${productId}`;
                        }
                    }

                    // Вызов функции для отображения подтверждения заказа
                    showOrderConfirmation();
                });
            });
        } else {
            console.warn('Нет данных о хитах продаж для отображения.');
        }
    })
    .catch(error => {
        console.error('Ошибка при получении хитов продаж:', error);
    });
});

// Обработчик для кликов по карточкам товаров из результатов поиска
document.querySelector('.products-carousel').addEventListener('click', function (event) {
    const card = event.target.closest('.product-card');
    if (card && !event.target.classList.contains('order-button')) {
        const productId = card.getAttribute('data-id');
        if (productId) {
            window.location.href = `product.html?id=${productId}`;
        }
    }
});