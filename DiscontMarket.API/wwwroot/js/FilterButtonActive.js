document.addEventListener('DOMContentLoaded', () => {
    const mediaQuery = window.matchMedia('(max-width: 869px)');

    let filtersToggle, sortingToggle, filtersContainer, sortingContainer, filtersClose;
    let filtersVisible = false;
    let sortingVisible = false;

    // Функции-обработчики
    const toggleFilters = () => {
        filtersVisible = !filtersVisible;
        if (filtersVisible) {
            filtersContainer.classList.add('show');
            sortingVisible = false; // Закрыть сортировку, если открыты фильтры
            sortingContainer.style.display = 'none';
        } else {
            filtersContainer.classList.remove('show');
        }
    };

    const toggleSorting = (event) => {
        event.stopPropagation(); // Предотвращение закрытия сортировки при клике по кнопке
        sortingVisible = !sortingVisible;
        if (sortingVisible) {
            sortingContainer.style.display = 'block';
        } else {
            sortingContainer.style.display = 'none';
        }
    };

    const closeFilters = () => {
        filtersVisible = false;
        filtersContainer.classList.remove('show');
    };

    const closeSortingOnClickOutside = (event) => {
        if (
            !sortingContainer.contains(event.target) && // Если клик не внутри сортировки
            !sortingToggle.contains(event.target)      // И не по кнопке сортировки
        ) {
            sortingVisible = false;
            sortingContainer.style.display = 'none';
        }
    };

    const preventCloseOnClickInside = (event) => {
        event.stopPropagation();
    };

    // Инициализация обработчиков
    const initListeners = () => {
        filtersToggle = document.getElementById('filters-toggle');
        sortingToggle = document.getElementById('sorting-toggle');
        filtersContainer = document.getElementById('filters-container');
        sortingContainer = document.getElementById('sorting-container');
        filtersClose = document.getElementById('filters-close');

        filtersToggle.addEventListener('click', toggleFilters);
        sortingToggle.addEventListener('click', toggleSorting);
        filtersClose.addEventListener('click', closeFilters);
        document.addEventListener('click', closeSortingOnClickOutside);
        filtersContainer.addEventListener('click', preventCloseOnClickInside);
    };

    // Проверяем начальное состояние медиазапроса
    if (mediaQuery.matches) {
        initListeners();
    }

    // Отслеживаем изменения медиазапроса
    mediaQuery.addEventListener('change', (event) => {
        if (event.matches) {
            initListeners();
        }
    });
});
