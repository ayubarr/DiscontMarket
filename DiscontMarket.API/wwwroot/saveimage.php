<?php
if (isset($_FILES['image'])) {
    $targetDir = 'items/productimages/';
    $imageName = $_POST['imageName'];
    $targetFile = $targetDir . basename($imageName);

    if (move_uploaded_file($_FILES['image']['tmp_name'], $targetFile)) {
        // Возвращаем путь к изображению
        echo json_encode(["imagePath" => $targetFile]);
    } else {
        echo json_encode(["error" => "Ошибка при сохранении изображения."]);
    }
} else {
    echo json_encode(["error" => "Изображение не найдено."]);
}
?>
