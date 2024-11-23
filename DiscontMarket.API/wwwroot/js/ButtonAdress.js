document.addEventListener('DOMContentLoaded', function () {
    // Находим все элементы с классами square-button или category-item
    const buttons = document.querySelectorAll('.square-button, .category-item');

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            // Получаем значение из атрибута data-category
            const category = button.getAttribute('data-category');

            // Формируем параметры строки запроса
            const params = new URLSearchParams();

            if (category) {
                params.append('category', category);
            }

            // Отправляем GET-запрос с параметрами
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

                    // Логика редиректа
                    const currentUrl = window.location.href;
                    const targetUrl = `catalog.html?${category}=&instock=true&minprice=0&maxprice=100000&preordertomorrow=true&preorderlater=true`;

                    if (currentUrl.endsWith(targetUrl)) {
                        console.log('Вы уже на этой странице.');
                        return;
                    }

                    // Перенаправление
                    window.location.href = targetUrl;
                })
                .catch(error => {
                    console.error('Ошибка при получении данных с сервера:', error);
                });

        });
    });
});

