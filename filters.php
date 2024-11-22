<?php
// Установить заголовки для обработки JSON
header('Content-Type: application/json');

// Получаем данные из тела запроса
$inputData = file_get_contents('php://input');

// Преобразуем данные JSON в ассоциативный массив PHP
$data = json_decode($inputData, true);

// Проверяем, были ли данные получены корректно
if ($data === null) {
    // Если ошибка в данных, отправляем сообщение об ошибке
    echo json_encode(['status' => 'error', 'message' => 'Invalid JSON data']);
    exit;
}

// Дальше можно обработать данные, например, вывести их
echo json_encode([
    'status' => 'success',
    'message' => 'Filters received successfully',
    'received_data' => $data
]);

?>
