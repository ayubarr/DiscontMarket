<?php
// Установить заголовки для обработки JSON
header('Content-Type: application/json');

// Получаем данные из тела запроса
$inputData = file_get_contents('php://input');

// Преобразуем данные JSON в ассоциативный массив PHP
$data = json_decode($inputData, true);

// Проверяем, были ли данные получены корректно
if ($data === null) {
    echo json_encode(['status' => 'error', 'message' => 'Invalid JSON data']);
    exit;
}

// Проверяем категории "stock" и "status"
$stock = $data['stock'] ?? [];
$status = $data['status'] ?? [];

// Пример обработки массивов
$response = [
    'stock_categories' => $stock,
    'status_categories' => $status,
    'other_data' => $data
];

// Отправляем ответ
echo json_encode(['status' => 'success', 'data' => $response]);
