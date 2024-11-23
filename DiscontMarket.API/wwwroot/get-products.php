<?php
header('Content-Type: application/json');

// Чтение содержимого JSON файла
$jsonFile = 'products.json';
$products = json_decode(file_get_contents($jsonFile), true);

// Проверка на ошибку при декодировании JSON
if ($products === null) {
    echo json_encode(['status' => 'error', 'message' => 'Invalid JSON data']);
    exit;
}

$inputData = file_get_contents('php://input');
$data = json_decode($inputData, true);

// Проверка данных, отправленных на сервер
if ($data === null) {
    echo json_encode(['status' => 'error', 'message' => 'Invalid JSON in request']);
    exit;
}

// Применение фильтров
$filteredProducts = array_filter($products, function ($product) use ($data) {
    return 
        $product['category'] === $data['category'] &&
        $product['in-stock'] === $data['in-stock'] &&
        $product['price'] >= $data['min-price'] &&
        $product['price'] <= $data['max-price'] &&
        $product['preorder-later'] === $data['preorder-later'] &&
        $product['preorder-tomorrow'] === $data['preorder-tomorrow'];
});

// Ответ в формате JSON
echo json_encode([
    'status' => 'success',
    'message' => 'Filters applied successfully',
    'products' => array_values($filteredProducts)
]);
?>
