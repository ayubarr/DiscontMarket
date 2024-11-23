document.addEventListener('DOMContentLoaded', () => {
    const minPriceInput = document.getElementById('minprice');
    const maxPriceInput = document.getElementById('maxprice');
    const minSlider = document.getElementById('price-slider-min');
    const maxSlider = document.getElementById('price-slider-max');
    const sliderTrack = document.querySelector('.slider-track');
    const filtersContainer = document.getElementById('filters-container');
    const sortingOptions = document.querySelectorAll('.sorting-option');
    const maxValue = parseInt(maxSlider?.max || 0);

    const params = new URLSearchParams(window.location.search);

    // Установить "По популярности" активным по умолчанию
    let activeSort = params.get('sort') || null; // По умолчанию "null"

    // Активируем соответствующую опцию сортировки при загрузке страницы
    sortingOptions.forEach(option => {
        if (option.dataset.sort === activeSort) {
            option.classList.add('active');
        } else if (!activeSort && option.dataset.sort === 'popularity') {
            option.classList.add('active'); // Если параметр sort отсутствует, ставим активной опцию "по популярности"
        }
    });

    // Обработчик клика для переключения активного состояния сортировки
    sortingOptions.forEach(option => {
        option.addEventListener('click', () => {
            // Снимаем класс active со всех надписей
            sortingOptions.forEach(item => item.classList.remove('active'));
    
            // Добавляем класс active к текущему элементу
            option.classList.add('active');
    
            // Обновляем сортировку в URL
            updateSortingInURL(option.dataset.sort);
        });
    });

    function updateSliderTrack() {
        const minValue = parseInt(minSlider.value);
        const maxValueSlider = parseInt(maxSlider.value);
        const percentageMin = (minValue / maxValue) * 100;
        const percentageMax = (maxValueSlider / maxValue) * 100;

        sliderTrack.style.left = `${percentageMin}%`;
        sliderTrack.style.width = `${percentageMax - percentageMin}%`;
    }

    function updateInputsFromSliders() {
        const minValue = parseInt(minSlider.value);
        const maxValueSlider = parseInt(maxSlider.value);

        if (minValue > maxValueSlider) {
            minSlider.value = maxValueSlider;
        }

        minPriceInput.value = minSlider.value;
        maxPriceInput.value = maxSlider.value;

        updateSliderTrack();
    }

    function updateSlidersFromInputs() {
        let minValue = parseInt(minPriceInput.value) || 0;
        let maxValueSlider = parseInt(maxPriceInput.value) || maxValue;

        if (minValue < 0) minValue = 0;
        if (minValue > maxValue) minValue = maxValue;
        if (maxValueSlider < 0) maxValueSlider = 0;
        if (maxValueSlider > maxValue) maxValueSlider = maxValue;

        if (minValue > maxValueSlider) {
            minValue = maxValueSlider;
        }

        minPriceInput.value = minValue;
        maxPriceInput.value = maxValueSlider;

        minSlider.value = minValue;
        maxSlider.value = maxValueSlider;

        updateSliderTrack();
    }

    filtersContainer.addEventListener('click', (event) => {
        // Проверяем, был ли клик по чекбоксу
        if (event.target.classList.contains('checkbox')) {
            event.target.classList.toggle('active');
            updateFiltersInURL(); // Обновление URL при выборе фильтра
            // window.location.reload();                                          РАСКОМЕНТИТЬ
        }
    });

    minSlider?.addEventListener('input', updateInputsFromSliders);
    maxSlider?.addEventListener('input', updateInputsFromSliders);
    minPriceInput?.addEventListener('input', updateSlidersFromInputs);
    maxPriceInput?.addEventListener('input', updateSlidersFromInputs);

    minPriceInput?.addEventListener('blur', updateSlidersFromInputs);
    maxPriceInput?.addEventListener('blur', updateSlidersFromInputs);

    updateSliderTrack();

    const category = params.keys().next().value;

    fetch('js/json/SortedFilters.json')
        .then(response => response.json())
        .then(data => {
            const categoryFilters = data[category];
            if (categoryFilters) {
                categoryFilters.filters.forEach(filter => {
                    const filterBlock = document.createElement('div');
                    filterBlock.classList.add('filter-block');

                    const filterTitle = document.createElement('div');
                    filterTitle.classList.add('filter-title');
                    filterTitle.textContent = filter.title;

                    const filterOptions = document.createElement('div');
                    filterOptions.classList.add('filter-options');
                    filterOptions.style.display = 'none';

                    filter.options.forEach(option => {
                        const filterOption = document.createElement('div');
                        filterOption.classList.add('filter-option');

                        const checkbox = document.createElement('div');
                        checkbox.classList.add('checkbox');
                        checkbox.setAttribute('data-filter', option.value);

                        // Проверка, был ли уже этот фильтр в URL
                        if (params.has(option.value)) {
                            checkbox.classList.add('active');
                        }

                        const span = document.createElement('span');
                        span.textContent = option.label;

                        filterOption.appendChild(checkbox);
                        filterOption.appendChild(span);
                        filterOptions.appendChild(filterOption);
                    });

                    filterTitle.addEventListener('click', () => {
                        const isVisible = filterOptions.style.display === 'block';
                        filterOptions.style.display = isVisible ? 'none' : 'block';
                    });

                    filterBlock.appendChild(filterTitle);
                    filterBlock.appendChild(filterOptions);
                    filtersContainer.appendChild(filterBlock);
                });
            }
        })
        .catch(error => {
            console.error('Ошибка загрузки фильтров:', error);
        });

    // Функция для обновления URL с выбранными фильтрами
    function updateFiltersInURL() {
        const newParams = new URLSearchParams(window.location.search);
    
        // Добавляем активные фильтры
        const checkboxes = filtersContainer.querySelectorAll('.checkbox');
        checkboxes.forEach(checkbox => {
            const filterId = checkbox.getAttribute('data-filter');
            if (checkbox.classList.contains('active')) {
                newParams.set(filterId, true);
            } else {
                newParams.delete(filterId);
            }
        });
    
        // Добавляем значения слайдера
        const minValue = minSlider.value;
        const maxValue = maxSlider.value;
    
        // Записываем минимальную цену
        newParams.set('minprice', minValue);
    
        // Записываем максимальную цену
        newParams.set('maxprice', maxValue);
    
        // Обновляем адресную строку без перезагрузки страницы
        window.history.replaceState({}, '', '?' + newParams.toString());

        sendFiltersToServer();
    }

    // Функция для обновления сортировки в URL
    function updateSortingInURL(sortBy) {
        const newParams = new URLSearchParams(window.location.search);

        // Записываем параметр сортировки
        newParams.set('sort', sortBy);

        // Обновляем адресную строку без перезагрузки страницы
        window.history.replaceState({}, '', '?' + newParams.toString());

        sendFiltersToServer();
    }
    
    // Обновление URL при изменении значений слайдеров или инпутов
    function handleSliderInput() {
        updateInputsFromSliders(); // Синхронизация инпутов
        updateFiltersInURL(); // Обновление URL
    }
    
    // Привязываем события
    minSlider.addEventListener('change', handleSliderInput);
    maxSlider.addEventListener('change', handleSliderInput);
    minPriceInput.addEventListener('change', () => {
        updateSlidersFromInputs(); // Синхронизация слайдеров
        updateFiltersInURL(); // Обновление URL
    });
    maxPriceInput.addEventListener('change', () => {
        updateSlidersFromInputs(); // Синхронизация слайдеров
        updateFiltersInURL(); // Обновление URL
    });
    
    // Сохранение состояния слайдера при загрузке страницы
    function initSliderValues() {
        const minPrice = params.get('minprice');
        const maxPrice = params.get('maxprice');
    
        if (minPrice) {
            minSlider.value = minPrice;
            minPriceInput.value = minPrice;
        }
        if (maxPrice) {
            maxSlider.value = maxPrice;
            maxPriceInput.value = maxPrice;
        }
    
        updateSliderTrack();
    }
    
    // Инициализируем состояние слайдеров после загрузки
    initSliderValues();

    // Инициализация активных фильтров при загрузке страницы
    function initActiveFilters() {
        const checkboxes = filtersContainer.querySelectorAll('.checkbox');
        checkboxes.forEach(checkbox => {
            const filterId = checkbox.getAttribute('data-filter');
            if (params.has(filterId)) {
                checkbox.classList.add('active');
            }
        });
    }
    
    initActiveFilters();

    function getActiveFilters() {
        const activeFilters = {};
    
        // Сортировка
        const activeSort = document.querySelector('.sorting-option.active');
        if (activeSort) {
            activeFilters.sort = activeSort.dataset.sort;
        }
    
        // Фильтры по чекбоксам
        const stockCategories = ['instock', 'preorderlater', 'preordertomorrow'];
        const statusCategories = ['discount', 'damagedpackage', 'minordefects'];
        const brandsFilters = ['Samsung', 'LG', 'Xiaomi', 'Panasonic'];
        const screenRezolution = ['HD', 'Full HD', 'QHD', '4K'];
        const itemColor = ['white', 'black', 'silver'];
    
        activeFilters.stock = [];
        activeFilters.status = [];
        activeFilters.brand = [];
        activeFilters.rezolution = [];
        activeFilters.color = [];
    
        const checkboxes = filtersContainer.querySelectorAll('.checkbox');
        checkboxes.forEach(checkbox => {
            if (checkbox.classList.contains('active')) {
                const filterId = checkbox.getAttribute('data-filter');
                
                if (stockCategories.includes(filterId)) {
                    activeFilters.stock.push(filterId);
                } else if (statusCategories.includes(filterId)) {
                    activeFilters.status.push(filterId);
                }
                else if (brandsFilters.includes(filterId)) {
                    activeFilters.brand.push(filterId);
                }
                else if (screenRezolution.includes(filterId)) {
                    activeFilters.rezolution.push(filterId);
                }
                else if (itemColor.includes(filterId)) {
                    activeFilters.color.push(filterId);
                } else {
                    activeFilters[filterId] = true;
                }
            }
        });
    
        // Значения слайдеров
        const minPrice = minSlider.value;
        const maxPrice = maxSlider.value;
        activeFilters['minprice'] = minPrice;
        activeFilters['maxprice'] = maxPrice;
    
        return activeFilters;
    }    

    function sendFiltersToServer() {
        const filters = getActiveFilters(); // Получение активных фильтров
        const params = new URLSearchParams(filters);

        // Добавляем категорию из URL
        const category = new URLSearchParams(window.location.search).get('category');
        if (category) {
            params.append('category', category);
        }

        // Отправка GET-запроса с параметрами строки
        fetch(`/api/product/get-all?${params.toString()}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                console.log('Данные успешно получены:', data);
                // Обработка ответа от сервера
            })
            .catch(error => {
                console.error('Ошибка при отправке данных на сервер:', error);
            });
    }

    
    
});
