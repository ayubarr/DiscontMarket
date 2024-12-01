<?php
// Установите заголовки для CORS и JSON-ответа
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json");

// Получение данных из POST-запроса
$data = json_decode(file_get_contents("php://input"), true);
$username = $data['username'];
$password = $data['password'];

// Предопределенные логин и пароль
$validUsername = "admin";
$validPassword = "1234";

// Проверка логина и пароля
if ($username === $validUsername && $password === $validPassword) {
    $response = [
        "statusCode" => 200,
        "isSuccess" => true,
        "message" => "Авторизация успешна!"
    ];
} else {
    $response = [
        "statusCode" => 401,
        "isSuccess" => false,
        "message" => "Неверный логин или пароль"
    ];
}

// Вывод JSON-ответа
echo json_encode($response);
?>
