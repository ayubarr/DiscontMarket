document.addEventListener('DOMContentLoaded', function () {
    // Находим все элементы с классами square-button или category-item
    const buttons = document.querySelectorAll('.square-button, .category-item');

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            // Получаем значение из атрибута data-category
            const category = button.getAttribute('data-category');

            // Функция для получения минимальной цены
            function getMinPrice(category) {
                return fetch('api/Filter/get-min', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(category)
                })
                    .then(response => response.json());
            }

            // Функция для получения максимальной цены
            function getMaxPrice(category) {
                return fetch('api/Filter/get-max', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(category)
                })
                    .then(response => response.json());
            }

            // Получаем минимальные и максимальные значения цен
            Promise.all([getMinPrice(category), getMaxPrice(category)])
                .then(([minPrice, maxPrice]) => {


                // Проверяем, если значение data-category одно из требуемых
                let targetUrl = '';
                if (category === 'discount' || category === 'damagedpackage' || category === 'minordefects') {
                    targetUrl = `catalog.html?${category}=&instock=true&minprice=${minPrice}&maxprice=${maxPrice}&preordertomorrow=true&preorderlater=true&${category}=true`;
                } else {
                    targetUrl = `catalog.html?${category}=&instock=true&minprice=${minPrice}&maxprice=${maxPrice}&preordertomorrow=true&preorderlater=true`;
                }

                // После отправки запроса, проверяем текущий URL и выполняем редирект
                const currentUrl = window.location.href;

                // Проверяем, есть ли значение data-category
                if (category) {
                    if (currentUrl.endsWith(targetUrl)) {
                        console.log('Вы уже на этой странице.');
                        return; // Останавливаем дальнейшее выполнение
                    } else {
                    // Отправляем POST-запрос на сервер с категорией
                    fetch('api/Product/get-all', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({ CategoryDTO: {Name : category}}),
                    })
                    .then(response => response.json())
                    .then(data => {
                        console.log('Данные успешно отправлены:', data);

                        // Перенаправляем на catalog.html с добавлением параметра
                        window.location.href = targetUrl;
                    })
                    .catch(error => {
                        console.error('Ошибка при отправке данных на сервер:', error);
                    });
                }
                } else {
                    console.error('Отсутствует атрибут data-category у кнопки:', button);
                    }

            })  
            .catch(error => console.error('Ошибка при получении данных цен:', error));
        });
    });
});
