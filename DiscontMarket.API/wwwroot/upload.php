<?php
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $targetPath = $_POST['path']; // Путь к файлу, который нужно заменить
    $file = $_FILES['file'];     // Загруженный файл

    // Проверяем наличие файла
    if (!$file || !isset($file['tmp_name'])) {
        http_response_code(400);
        echo json_encode(['error' => 'Файл не был загружен']);
        exit;
    }

    // Проверка формата файла
    $fileType = mime_content_type($file['tmp_name']);
    if ($fileType !== 'image/png') {
        http_response_code(400);
        echo json_encode(['error' => 'Допускаются только файлы формата .png']);
        exit;
    }

    // Проверка размера изображения
    list($width, $height) = getimagesize($file['tmp_name']);
    $requiredWidth = (int)$_POST['width'];
    $requiredHeight = (int)$_POST['height'];

    if ($width !== $requiredWidth || $height !== $requiredHeight) {
        http_response_code(400);
        echo json_encode(['error' => "Размер изображения должен быть {$requiredWidth}x{$requiredHeight}"]);
        exit;
    }

    // Перемещение файла в нужное место
    if (move_uploaded_file($file['tmp_name'], $targetPath)) {
        echo json_encode(['success' => true, 'message' => 'Файл успешно заменен']);
    } else {
        http_response_code(500);
        echo json_encode(['error' => 'Не удалось сохранить файл']);
    }
}
