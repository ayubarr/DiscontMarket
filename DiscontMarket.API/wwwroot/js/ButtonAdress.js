document.addEventListener('DOMContentLoaded', function () {
    // Находим все элементы с классами square-button или category-item
    const buttons = document.querySelectorAll('.square-button, .category-item');

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            // Получаем значение из атрибута data-category
            const category = button.getAttribute('data-category');

            // Проверяем, есть ли значение data-category
            if (category) {
                // Отправляем POST-запрос на сервер с категорией
                fetch('api/Product/get-all', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ CategoryDTO: { Name: category } })
                })
                    .then(response => response.json())
                    .then(data => {
                        console.log('Данные успешно отправлены:', data);

                        // После отправки запроса, проверяем текущий URL и выполняем редирект
                        const currentUrl = window.location.href;
                        const targetUrl = `catalog.html?${category}=&instock=true&minprice=0&maxprice=100000&preordertomorrow=true&preorderlater=true`; // Фильтры не передаются, дописаны для стартовых галочек

                        if (currentUrl.endsWith(targetUrl)) {
                            console.log('Вы уже на этой странице.');
                            return; // Останавливаем дальнейшее выполнение
                        }

                        // Перенаправляем на catalog.html с добавлением параметра
                        window.location.href = targetUrl;
                    })
                    .catch(error => {
                        console.error('Ошибка при отправке данных на сервер:', error);
                    });
            } else {
                console.error('Отсутствует атрибут data-category у кнопки:', button);
            }
        });
    });
});
