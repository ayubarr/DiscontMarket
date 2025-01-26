fetch('api/User/get-user-data', {
    method: 'GET',
    headers: {
        'Content-Type': 'application/json'
    }
})
    .then(response => {
        if (!response.ok) {
            throw new Error('Ошибка сети или сервера');
        }
        return response.json();
    })
    .then(data => {
        // Предполагается, что сервер возвращает новый URL в поле `techLink`
        if (data.techLink) {
            document.querySelectorAll('a').forEach(link => {
                if (link.textContent === 'Тех.поддержка') {
                    link.href = data.techLink;
                }
            });
        }
        if (data.vkLink) {
            // Изменение href для всех элементов с классом 'VK'
            document.querySelectorAll('.VK').forEach(link => {
                link.href = data.vkLink;
            });
        }
        if (data.tgLink) {
            // Изменение href для всех элементов с классом 'TG'
            document.querySelectorAll('.TG').forEach(link => {
                link.href = data.tgLink;
            });
        }
        if (data.wtLink) {
            // Изменение href для всех элементов с классом 'WT'
            document.querySelectorAll('.WT').forEach(link => {
                link.href = data.wtLink;
            });
        }
        if (data.time) {
            // Изменение текста для всех элементов с классом 'working-hours-time'
            document.querySelectorAll('.working-hours-time').forEach(element => {
                element.textContent = data.time;
            });
        }
        if (data.phoneNumber) {
            const rawPhoneNumber = data.phoneNumber; // Исходный номер телефона
        
            // Форматирование номера в нужный вид
            const formattedPhoneNumber = rawPhoneNumber.replace(
                /^(\d{1})(\d{3})(\d{3})(\d{2})(\d{2})$/,
                '+$1 ($2) $3-$4-$5'
            );
        
            // Изменение href и текста для всех ссылок с атрибутом href="tel:"
            document.querySelectorAll('a[href^="tel:"]').forEach(link => {
                link.href = `tel:${rawPhoneNumber}`; // Храним исходный номер в href
                link.textContent = formattedPhoneNumber; // Текст в ссылке в нужном формате
            });
        }
        
        if (data.returnText) {
            document.querySelectorAll('.returntext').forEach(element => {
                element.textContent = data.returnText; // Меняем текст на новый
            });
        } 
        if (data.textAdress && data.hrefAdress && data.hrefmapAdress) {
            document.querySelectorAll('.location-link').forEach(link => {
                link.href = data.hrefAdress;
                link.querySelector('.location-text').textContent = data.textAdress;
            });

            document.querySelectorAll('.contact-address').forEach(link => {
                link.href = data.hrefAdress;
                link.textContent = data.textAdress;
            });

            document.querySelectorAll('.mapframe').forEach(link => {
                link.src = data.hrefmapAdress;
            });
        }    
        if (data.textInfo) {
            document.querySelectorAll('.contact-text').forEach(element => {
                element.textContent = data.textInfo; // Меняем текст на новый
            });
        }          
    })
    .catch(error => {
        console.error('Ошибка:', error);
    });
    
// Установка ссылки по умолчанию до завершения запроса для всех элементов с классом 'support-link'
//document.querySelectorAll('.support-link').forEach(link => {
//    link.href = 'https://www.youtube.com/';
//});
