document.addEventListener('DOMContentLoaded', () => {
    fetch('maintenance.php')
        .then(response => response.json())
        .then(data => {
            if (data.is_under_maintenance) {
                document.body.innerHTML = `
                    <div style="display: flex; align-items: center; justify-content: center; flex-direction: column;
                                height: 100vh; background-color: white; font-size: 24px; 
                                font-family: Ubuntu, sans-serif; font-weight: 300; color: black;">
                        <span>Сайт закрыт на техническое обслуживание</span>
                        <span style="font-weight: 500; color: #005BFF;">Принимаем заказы по номеру +7 981 210-48-31</span>
                    </div>`;
            }
        });
});