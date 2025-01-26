const submitButton = document.querySelector('.submit-button-return');

// Функция отправки данных через fetch
submitButton.addEventListener('click', function() {
    // Собирать данные из формы
    const phone = document.querySelector('.phone-input-return').value;
    const name = document.querySelector('.name-input-return').value;
    const email = document.querySelector('.email-input-return').value;
    const subject = document.querySelector('.subject-input-return').value;
    const message = document.querySelector('.message-input-return').value;

    // Проверка на пустые поля
    if (!phone || !name || !email || !subject || !message) {
        alert('Пожалуйста, заполните все поля.');
        return;
    }

    // Создаем объект с данными формы
    const formData = {
        phone: phone,
        name: name,
        email: email,
        subject: subject,
        message: message
    };

    // Отправка данных через fetch
    fetch('api/Order/send-info', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
    .then(response => response.json())
    .then(data => {
        alert('Форма успешно отправлена!');
    })
    .catch(error => {
        console.error('Ошибка:', error);
        alert('Произошла ошибка при отправке формы.');
    });
});
