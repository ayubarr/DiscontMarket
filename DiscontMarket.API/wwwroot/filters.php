<?php
// Установить заголовки для обработки JSON
header('Content-Type: application/json');

// Получаем данные из GET и POST запросов
$getParams = $_GET;
$postParams = json_decode(file_get_contents('php://input'), true);

// Пример данных товаров
$products = [
    [
        "productName" => "Samsung 4K",
        "price" => 50000,
        "quantity" => 10,
        "productAvailability" => "instock",
        "productStatus" => "discount",
        "userID" => 101,
        "brendId" => 1
    ],
    [
        "productName" => "Смартфон Xiaomi Redmi Note 12",
        "price" => 20000,
        "quantity" => 25,
        "productAvailability" => "preorderlater",
        "productStatus" => "discount",
        "userID" => 102,
        "brendId" => 2
    ],
    [
        "productName" => "Ноутбук LG Gram",
        "price" => 80000,
        "quantity" => 5,
        "productAvailability" => "preordertomorrow",
        "productStatus" => "discount",
        "userID" => 103,
        "brendId" => 3
    ],
    [
        "productName" => "Саундбар Panasonic",
        "price" => 15000,
        "quantity" => 7,
        "productAvailability" => "instock",
        "productStatus" => "discount",
        "userID" => 104,
        "brendId" => 4
    ],
    [
        "productName" => "Смартфон 12",
        "price" => 1100,
        "quantity" => 85,
        "productAvailability" => "preorderlater",
        "productStatus" => "discount",
        "userID" => 105,
        "brendId" => 2
    ],
    [
        "productName" => "Ноутбук",
        "price" => 60000,
        "quantity" => 20,
        "productAvailability" => "preordertomorrow",
        "productStatus" => "discount",
        "userID" => 106,
        "brendId" => 3
    ],
    [
        "productName" => "Саундбар",
        "price" => 30000,
        "quantity" => 21,
        "productAvailability" => "instock",
        "productStatus" => "discount",
        "userID" => 107,
        "brendId" => 4
    ]
];

// Отправляем данные товаров вместе с полученными параметрами
echo json_encode([
    "status" => "success",
    "receivedGetParams" => $getParams,  // Данные из GET-запроса
    "receivedPostParams" => $postParams, // Данные из POST-запроса
    "data" => $products
]);
