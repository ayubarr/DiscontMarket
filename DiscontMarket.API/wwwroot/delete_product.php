<?php
header('Content-Type: application/json');

// Заранее заданные товары
$products = [
    [
        "productId" => "101",
        "title" => "Телевизор Xiaomi Redmi TV 4K 2025",
        "rating" => 4.8,
        "description" => "Ультрасовременный телевизор с разрешением 4K и поддержкой Smart TV.",
        "price" => 29990
    ],
    [
        "productId" => "102",
        "title" => "Холодильник Samsung No Frost 2024",
        "rating" => 4.7,
        "description" => "Современный холодильник с функцией No Frost.",
        "price" => 39990
    ]
];

// Получение данных из POST-запроса
$input = json_decode(file_get_contents('php://input'), true);
$titleToDelete = $input['title'] ?? '';

// Поиск товара
$foundIndex = -1;
foreach ($products as $index => $product) {
    if (strcasecmp($product['title'], $titleToDelete) === 0) {
        $foundIndex = $index;
        break;
    }
}

// Удаление товара
if ($foundIndex !== -1) {
    array_splice($products, $foundIndex, 1);
    echo json_encode(['message' => "Товар \"$titleToDelete\" успешно удалён."]);
} else {
    echo json_encode(['message' => "Товар \"$titleToDelete\" не найден."]);
}
?>
