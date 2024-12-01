const usernameField = document.getElementById('username');
const passwordField = document.getElementById('password');
const authButton = document.getElementById('auth-btn');
const errorMsg = document.getElementById('error-msg');
const authContainer = document.getElementById('auth-container');
const dashboardContainer = document.getElementById('dashboard-container');
const captchaContainer = document.createElement('div');
const sessionTimerDisplay = document.createElement('div');
let attemptCounter = parseInt(localStorage.getItem('attempts')) || 0;
let blockTime = parseInt(localStorage.getItem('blockTime')) || 0;
let sessionEndTime = parseInt(localStorage.getItem('sessionEndTime')) || 0;

// Стили для таймера
sessionTimerDisplay.style.cssText = `
    position: fixed; top: 10px; left: 10px; background: rgba(0, 0, 0, 0.8); 
    color: white; padding: 5px 10px; font-size: 14px; border-radius: 5px; display: none;
`;
document.body.appendChild(sessionTimerDisplay);

// Добавление капчи
captchaContainer.innerHTML = `
    <label for="captcha-input">Введите капчу: <span id="captcha-text"></span></label>
    <input type="text" id="captcha-input" placeholder="Капча">
`;
captchaContainer.style.display = 'none'; // Скрыто по умолчанию
authContainer.appendChild(captchaContainer);

function generateCaptcha() {
    const captcha = Math.random().toString(36).substring(2, 8).toUpperCase();
    document.getElementById('captcha-text').textContent = captcha;
    return captcha;
}

let currentCaptcha = generateCaptcha();

function checkBlockStatus() {
    const now = Date.now();
    if (blockTime && now < blockTime) {
        const remaining = Math.ceil((blockTime - now) / 60000); // Осталось в минутах
        errorMsg.textContent = `Вход заблокирован. Повторите попытку через ${remaining} мин.`;
        return false;
    }

    // Сброс блокировки, если время истекло
    if (blockTime && now >= blockTime) {
        blockTime = 0;
        attemptCounter = 0;
        localStorage.removeItem('blockTime');
        localStorage.setItem('attempts', attemptCounter);
        return true;
    }
    return true;
}

function startSessionTimer() {
    const interval = setInterval(() => {
        const now = Date.now();
        const remainingTime = Math.max(sessionEndTime - now, 0);
        const minutes = Math.floor(remainingTime / 60000);
        const seconds = Math.floor((remainingTime % 60000) / 1000);

        sessionTimerDisplay.textContent = `Сеанс истекает через: ${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;
        sessionTimerDisplay.style.display = 'block';

        if (remainingTime <= 0) {
            clearInterval(interval);
            sessionTimerDisplay.style.display = 'none';
            alert('Сессия истекла. Авторизуйтесь снова.');
            localStorage.removeItem('sessionEndTime');
            location.reload(); // Перезагрузка страницы
        }
    }, 1000);
}

authButton.addEventListener('click', () => {
    const username = usernameField.value;
    const password = passwordField.value;
    const captchaInput = document.getElementById('captcha-input').value;

    // Проверка состояния блокировки
    if (!checkBlockStatus()) return;

    // Проверка капчи, если она активирована
    if (captchaContainer.style.display !== 'none' && captchaInput.toUpperCase() !== currentCaptcha) {
        errorMsg.textContent = 'Капча введена неверно.';
        currentCaptcha = generateCaptcha(); // Генерация новой капчи
        return;
    }

    // Отправка данных на сервер для проверки
    fetch('api/User/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    })
    .then(response => response.json())
    .then(data => {
        if (data.statusCode === 200 && data.isSuccess) {
            // Успешный вход
            authContainer.classList.remove('active');
            dashboardContainer.classList.add('active');
            errorMsg.textContent = '';
            localStorage.removeItem('attempts');
            attemptCounter = 0;

            sessionEndTime = Date.now() + 60 * 60000; // Сессия длится 10 минут
            localStorage.setItem('sessionEndTime', sessionEndTime);

            startSessionTimer();
        } else {
            // Ошибка входа
            attemptCounter++;
            localStorage.setItem('attempts', attemptCounter);
            errorMsg.textContent = `Ошибка: ${data.message}. Осталось попыток: ${3 - attemptCounter}`;

            if (attemptCounter >= 3) {
                blockTime = Date.now() + 10 * 60000; // Блокировка на 10 минут
                localStorage.setItem('blockTime', blockTime);
                errorMsg.textContent = 'Вход заблокирован на 10 минут.';
            } else {
                // Активируем капчу
                captchaContainer.style.display = 'block';
                currentCaptcha = generateCaptcha();
            }
        }
    })
    .catch(error => {
        errorMsg.textContent = 'Ошибка соединения с сервером.';
        console.error('Ошибка:', error);
    });
});

// Проверка блокировки и активной сессии при загрузке страницы
if (!checkBlockStatus()) {
    captchaContainer.style.display = 'block';
    currentCaptcha = generateCaptcha();
} else if (sessionEndTime > Date.now()) {
    authContainer.classList.remove('active');
    dashboardContainer.classList.add('active');
    startSessionTimer();
}

const editAdsBtn = document.getElementById('edit-ads-btn');
const backBtn = document.getElementById('back-btn');
const editAdsContainer = document.getElementById('edit-ads-container');

// Переключение между меню и редактированием рекламы
editAdsBtn.addEventListener('click', () => {
    dashboardContainer.style.display = 'none';
    editAdsContainer.style.display = 'block';
});

backBtn.addEventListener('click', () => {
    editAdsContainer.style.display = 'none';
    dashboardContainer.style.display = 'block';
});

// Обработка загрузки файлов
document.querySelectorAll('.file-input').forEach(input => {
    input.addEventListener('change', event => {
        const file = event.target.files[0];
        const field = event.target.closest('.ad-field');
        const path = field.dataset.path;
        const requiredWidth = parseInt(field.dataset.width);
        const requiredHeight = parseInt(field.dataset.height);

        if (!file) return;

        // Проверка формата
        if (file.type !== 'image/png') {
            alert('Допускаются только файлы формата .png');
            return;
        }

        // Проверка размеров изображения
        const reader = new FileReader();
        reader.onload = function (e) {
            const imgTemp = new Image();
            imgTemp.onload = function () {
                if (imgTemp.width !== requiredWidth || imgTemp.height !== requiredHeight) {
                    alert(`Неверный размер. Требуется ${requiredWidth}x${requiredHeight}px`);
                    return;
                }

                // Если все проверки пройдены, отправляем файл на сервер
                const formData = new FormData();
                formData.append('file', file);
                formData.append('path', path);
                formData.append('width', requiredWidth);
                formData.append('height', requiredHeight);

                fetch('upload.php', {
                    method: 'POST',
                    body: formData
                })
                    .then(response => response.json())
                    .then(data => {
                        if (data.success) {
                            alert('Файл успешно загружен');
                            const img = field.querySelector('img');
                            img.src = e.target.result; // Обновляем превью изображения
                        } else {
                            alert(data.error || 'Ошибка загрузки');
                        }
                    })
                    .catch(err => alert('Ошибка соединения с сервером'));
            };
            imgTemp.src = e.target.result;
        };
        reader.readAsDataURL(file);
    });
});

document.getElementById('manage-products-btn').addEventListener('click', () => {
    document.getElementById('manage-products-container').style.display = 'block';
    document.getElementById('dashboard-container').style.display = 'none';
});

// Возврат на панель управления
document.getElementById('back-btn-products').addEventListener('click', () => {
    document.getElementById('manage-products-container').style.display = 'none';
    document.getElementById('dashboard-container').style.display = 'block';
});

// Обработчик выбора категории
document.getElementById('category-select').addEventListener('change', (event) => {
    const category = event.target.value;
    updateCharacteristicsFields(category);
});

// Вызов функции для инициализации характеристик при загрузке страницы
window.addEventListener('DOMContentLoaded', function () {
    updateCharacteristics();  // При первой загрузке отображаем характеристики для первой категории
});

// Загрузка данных характеристик из PHP (JSON)
let categoryFilters = {};

// Получаем фильтры через GET-запрос
fetch('api/Filter/get-all', {
    method: 'GET',
    headers: {
        'Content-Type': 'application/json'
    },
})
    .then(response => response.json())
    .then(data => {
        categoryFilters = data;
    })
    .catch(error => {
        console.error('Ошибка загрузки фильтров:', error);
    });

document.getElementById('product-images').addEventListener('change', (event) => {
    const files = event.target.files;
    const validFiles = [];
    const dataTransfer = new DataTransfer(); // Создание нового объекта для хранения файлов

    Array.from(files).forEach((file) => {
        if (!file.name.toLowerCase().endsWith('.png')) {
            alert(`Файл ${file.name} не является изображением .png`);
            return;
        }

        const img = new Image();
        img.src = URL.createObjectURL(file);
        img.onload = () => {
            if (img.width !== 490 || img.height !== 490) {
                alert(`Изображение ${file.name} должно быть размером 490x490 пикселей.`);
            } else {
                validFiles.push(file);
                dataTransfer.items.add(file); // Добавляем только валидные файлы
            }

            // Обновляем input только после завершения проверки каждого файла
            if (validFiles.length === dataTransfer.items.length) {
                event.target.files = dataTransfer.files;
            }
        };
    });

    // Удаляем все файлы из input, пока не пройдет проверка
    event.target.files = dataTransfer.files;
});

// Функция для обновления характеристик в зависимости от категории
function updateCharacteristicsFields(category) {
    const container = document.getElementById('characteristics-container');
    container.innerHTML = ''; // Очищаем старые поля

    // Проверка на существующие фильтры для выбранной категории
    if (category && categoryFilters[category]) {
        const filters = categoryFilters[category].filters;
        filters.forEach(filter => {
            const filterHTML = `
                <label for="${filter.title}">${filter.title}:</label>
                <select id="${filter.title}" required>
                    ${filter.options.map(option => `<option value="${option.value}">${option.label}</option>`).join('')}
                </select>
            `;
            container.innerHTML += filterHTML;
        });
    }
}

// Обработчик сохранения товара
document.getElementById('save-product-btn').addEventListener('click', () => {
    const title = document.getElementById('product-title').value;
    const description = document.getElementById('product-description').value;
    const fullDescription = document.getElementById('product-full-description').value;
    const price = document.getElementById('product-price').value;
    const images = document.getElementById('product-images').files;
    const status = document.getElementById('product-status').value;
    const category = document.getElementById('category-select').value;
    const availability = document.getElementById('product-availability').value;

    // Проверка обязательных полей
    if (!category || !title || !description || !fullDescription || !price || images.length === 0 || !status) {
        alert("Пожалуйста, заполните все обязательные поля.");
        return;
    }

    // Проверка на отрицательное значение цены
    if (parseFloat(price) <= 0) {
        alert("Цена должна быть положительным числом.");
        return;
    }

    const characteristics = [];
    const filters = categoryFilters[category]?.filters;
    if (filters) {
        filters.forEach(filter => {
            const value = document.getElementById(filter.title)?.value;
            if (value) {
                characteristics.push({
                    name: filter.title,
                    value: value
                });
            }
        });
    }

    // Сохранение изображений в папке ./items/productimages/
    const imagePaths = [];
    Array.from(images).forEach((file, index) => {
        const formData = new FormData();
        formData.append('image', file);
        formData.append('imageName', `productimage_${Date.now()}_${index}.png`);

        // Отправка изображения на сервер с сохранением
        fetch('saveimage.php', {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            // Добавляем путь к изображению в массив
            imagePaths.push(data.imagePath);
        })
        .catch(error => {
            console.error('Ошибка сохранения изображения:', error);
        });
    });

    // Создание объекта товара
    const newProduct = {
        title,
        description,
        fullDescription,
        price,
        status,
        availability,
        images: imagePaths,
        characteristics
    };

    console.log(newProduct);

    // Отправка данных товара на сервер
    fetch('api/Products/create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newProduct)
    })
    .then(response => response.json())
    .then(data => {
        console.log('Товар успешно сохранен:', data);

        resetProductForm();
    })
    .catch(error => {
        console.error('Ошибка отправки данных товара:', error);

        resetProductForm();
    });
});

function resetProductForm() {
    document.getElementById('product-title').value = '';
    document.getElementById('product-description').value = '';
    document.getElementById('product-full-description').value = '';
    document.getElementById('product-price').value = '';
    document.getElementById('product-status').value = '';
    document.getElementById('category-select').value = 'Выберите категорию';  // Сброс категории
    document.getElementById('product-images').value = '';  // Очистка файлов

    // Очистка характеристик
    const container = document.getElementById('characteristics-container');
    container.innerHTML = '';
}




document.getElementById('delete-products-btn').addEventListener('click', () => {
    document.getElementById('dashboard-container').style.display = 'none';
    document.getElementById('delete-products-container').style.display = 'block';
});

// Возврат на панель управления
document.getElementById('back-btn-delete').addEventListener('click', () => {
    document.getElementById('delete-products-container').style.display = 'none';
    document.getElementById('dashboard-container').style.display = 'block';
});

document.getElementById('delete-product-btn').addEventListener('click', () => {
    const title = document.getElementById('product-delete-title').value.trim();
    
    if (title) {
        fetch('http://192.168.192.59/сайт/delete_product.php', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title })
        })
        .then(response => response.json())
        .then(data => {
            document.getElementById('delete-result').innerText = data.message;
        })
        .catch(error => {
            console.error('Ошибка:', error);
        });
    } else {
        alert('Введите название товара.');
    }
});



const maintenanceBtn = document.getElementById('maintenance-btn');

// Проверка текущего состояния
fetch('maintenance.php')
    .then(response => response.json())
    .then(data => {
        if (data.is_under_maintenance) {
            maintenanceBtn.textContent = 'Завершить технические работы';
        } else {
            maintenanceBtn.textContent = 'Закрыть сайт на техническое обслуживание';
        }
    });

// Обработка нажатия кнопки
maintenanceBtn.addEventListener('click', () => {
    const isUnderMaintenance = maintenanceBtn.textContent.includes('Закрыть');
    fetch('maintenance.php', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ is_under_maintenance: isUnderMaintenance })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            maintenanceBtn.textContent = isUnderMaintenance
                ? 'Завершить технические работы'
                : 'Закрыть сайт на техническое обслуживание';
        } else {
            alert('Ошибка обновления статуса');
        }
    });
});

// Создаём переменную для хранения ссылки на плашку
let maintenanceBanner;

// Проверка текущего состояния
fetch('maintenance.php')
    .then(response => response.json())
    .then(data => {
        if (data.is_under_maintenance) {
            showBanner(); // Показать плашку, если сайт в режиме обслуживания
        }
    });

// Функция для показа плашки
function showBanner() {
    if (!maintenanceBanner) { // Создаем только если плашки нет
        maintenanceBanner = document.createElement('div');
        maintenanceBanner.style.cssText = `
            position: fixed; top: 0; left: 0; width: 100%; 
            background-color: red; color: white; text-align: center; 
            padding: 10px; font-size: 18px; z-index: 1000;`;
        maintenanceBanner.textContent = 'Сайт закрыт на техническое обслуживание';
        document.body.prepend(maintenanceBanner);
    }
}

// Функция для скрытия плашки
function hideBanner() {
    if (maintenanceBanner) {
        maintenanceBanner.remove(); // Удаляем элемент
        maintenanceBanner = null;  // Сбрасываем переменную
    }
}

// Обработка нажатия кнопки
maintenanceBtn.addEventListener('click', () => {
    const isUnderMaintenance = maintenanceBtn.textContent.includes('Закрыть');
    fetch('maintenance.php', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ is_under_maintenance: isUnderMaintenance })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            maintenanceBtn.textContent = isUnderMaintenance
                ? 'Завершить технические работы'
                : 'Закрыть сайт на техническое обслуживание';

            // Динамическое управление плашкой
            if (isUnderMaintenance) {
                showBanner(); // Показываем плашку
            } else {
                hideBanner(); // Убираем плашку
            }

        } else {
            alert('Ошибка обновления статуса');
        }
    });
});