<?php
$filename = 'maintenance.json';

// Чтение текущего состояния
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (file_exists($filename)) {
        echo file_get_contents($filename);
    } else {
        echo json_encode(["is_under_maintenance" => false]);
    }
    exit;
}

// Обновление состояния через POST
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $input = json_decode(file_get_contents('php://input'), true);
    if (isset($input['is_under_maintenance'])) {
        $newState = [
            "is_under_maintenance" => $input['is_under_maintenance']
        ];
        file_put_contents($filename, json_encode($newState));
        echo json_encode(["success" => true]);
    } else {
        echo json_encode(["success" => false, "message" => "Invalid input"]);
    }
    exit;
}
?>
