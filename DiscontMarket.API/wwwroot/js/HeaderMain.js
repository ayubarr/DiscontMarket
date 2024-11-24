function updatePlaceholder() {
    const searchInput = document.querySelector('.search-input');
    const catalogText = document.querySelector('.catalog-text');

    const headerContainer = document.querySelector('.header-container');
    const contactInfo = document.querySelector('.contact-info');
    const workingHours = document.querySelector('.working-hours');
    const dropdownButton = document.getElementById('dropdownButton');
    const hiddenLocation = document.getElementById('hiddenLocation');
    const hiddenNavMenu = document.getElementById('hiddenNavMenu');
    const logo = document.getElementById('logo'); // Логотип

    if (window.innerWidth <= 869) {
        headerContainer.appendChild(logo); // Перемещаем логотип в верхний контейнер
        headerContainer.appendChild(contactInfo);
        headerContainer.appendChild(workingHours);
        dropdownButton.style.display = 'flex'; // Показываем кнопку
        hiddenLocation.style.display = 'block'; // Показываем скрытую локацию
        hiddenNavMenu.style.display = 'block'; // Показываем скрытое меню
    } else {
        const headerBottom = document.querySelector('.header-bottom');
        headerBottom.appendChild(contactInfo);
        headerBottom.appendChild(workingHours);
        dropdownButton.style.display = 'none'; // Скрываем кнопку
        hiddenLocation.style.display = 'none'; // Скрываем локацию
        hiddenNavMenu.style.display = 'none'; // Скрываем меню
        headerBottom.insertBefore(logo, headerBottom.firstChild); // Возвращаем логотип в нижний хедер
    }


}

document.getElementById('catalog-button-dropdown').addEventListener('click', function(event) {
    const dropdownContent = document.getElementById('dropdownContentCatalog');
    dropdownContent.style.display = dropdownContent.style.display === 'flex' ? 'none' : 'flex';
    event.stopPropagation();

    if (dropdownContent.style.display === 'flex') {
        // Отображаем подкатегории для первой категории по умолчанию
        const firstCategory = document.querySelector('.category-item.active');
        const categoryData = firstCategory.getAttribute('data-category');
        document.querySelectorAll('.subcategory-container').forEach(subcat => {
            subcat.style.display = subcat.id === categoryData ? 'block' : 'none';
        });
    }
});


// Закрытие меню при клике вне его области
document.addEventListener('click', function(event) {
    const dropdownContent = document.getElementById('dropdownContentCatalog');
    if (!dropdownContent.contains(event.target) && event.target.id !== 'catalog-button-dropdown') {
        dropdownContent.style.display = 'none';
        // Убираем активный класс с категории, если меню закрывается
        document.querySelectorAll('.category-item').forEach(item => {
            item.classList.remove('active');
        });
    }
});

// Отображение подкатегорий при наведении на категорию
document.querySelectorAll('.category-item').forEach(item => {
    item.addEventListener('mouseenter', function() {
        const categoryId = this.getAttribute('data-category');
        // Скрываем все подкатегории
        document.querySelectorAll('.subcategory-container').forEach(container => {
            container.style.display = 'none';
        });
        // Показываем подкатегории для выбранной категории
        document.getElementById(categoryId).style.display = 'block';

        // Убираем активный класс с других категорий
        document.querySelectorAll('.category-item').forEach(category => {
            category.classList.remove('active');
        });

        // Добавляем активный класс для текущей категории
        this.classList.add('active');
    });

    // Переход по категории
    item.addEventListener('click', function(event) {
        const categoryLink = this.querySelector('a');
        if (categoryLink) {
            window.location.href = categoryLink.href; // Переход по ссылке категории
        }
    });
});

// Переход по ссылке подкатегорий
document.querySelectorAll('.subcategory-container a').forEach(link => {
    link.addEventListener('click', function(event) {
        window.location.href = link.href; // Переход по ссылке подкатегории
    });
});

// Обновляем placeholder при загрузке страницы и изменении размера окна
window.addEventListener('load', updatePlaceholder);
window.addEventListener('resize', updatePlaceholder);

// Обработчик события для открытия/закрытия выпадающего списка
document.getElementById('dropdownButton').addEventListener('click', function(event) {
    const dropdownContent = document.getElementById('dropdownContent');
    // Переключаем отображение списка
    dropdownContent.style.display = dropdownContent.style.display === 'block' ? 'none' : 'block';
    
    // Останавливаем дальнейшее распространение события, чтобы не сработал внешний обработчик
    event.stopPropagation();
});

// Добавляем обработчик для касания (мобильные устройства) и кликов (ПК)
['touchstart'].forEach(eventType => {
    document.addEventListener(eventType, function (event) {
        const dropdownContent = document.getElementById('dropdownContent');
        const dropdownButton = document.getElementById('dropdownButton');
        
        // Проверяем, был ли клик/касание вне кнопки и списка
        if (!dropdownContent.contains(event.target) && event.target !== dropdownButton) {
            dropdownContent.style.display = 'none'; // Закрываем список
        }
    });
});

