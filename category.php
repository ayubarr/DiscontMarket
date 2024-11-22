<?php
// Установим заголовки для разрешения CORS, если запрос идет с другого домена
header('Access-Control-Allow-Origin: *');
header('Content-Type: application/json');

// Получаем входящие данные JSON
$inputData = json_decode(file_get_contents('php://input'), true);

// Проверяем, что данные были получены
if (isset($inputData['category'])) {
    $category = $inputData['category'];

    // Здесь можно выполнить любую логику с полученной категорией, например, сохранить в базе данных
    // Для демонстрации, просто отправим обратно полученные данные
    $response = [
        'status' => 'success',
        'category' => $category,
        'message' => 'Данные успешно получены.'
    ];

    // Отправляем ответ в формате JSON
    echo json_encode($response);
} else {
    // Если данные не были получены, отправляем ошибку
    $response = [
        'status' => 'error',
        'message' => 'Отсутствует параметр category.'
    ];

    echo json_encode($response);
}
?>
