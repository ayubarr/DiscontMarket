<?php
header('Content-Type: application/json');

// Проверка входящих данных
$input = json_decode(file_get_contents('php://input'), true);

if ($input['action'] === 'getCategoryFilters') {
    // Данные фильтров для категорий
    $categoryFilters = [
        "tv" => [
            "filters" => [
                [
                    "title" => "Разрешение экрана",
                    "options" => [
                        ["label" => "HD", "value" => "HD"],
                        ["label" => "Full HD", "value" => "Full HD"],
                        ["label" => "QHD", "value" => "QHD"],
                        ["label" => "4K", "value" => "4K"]
                    ]
                ],
                [
                    "title" => "Бренды",
                    "options" => [
                        ["label" => "Samsung", "value" => "Samsung"],
                        ["label" => "LG", "value" => "LG"],
                        ["label" => "Xiaomi", "value" => "Xiaomi"],
                        ["label" => "Panasonic", "value" => "Panasonic"]
                    ]
                ],
                [
                    "title" => "Тип матрицы",
                    "options" => [
                        ["label" => "LED", "value" => "LED"],
                        ["label" => "OLED", "value" => "OLED"],
                        ["label" => "QLED", "value" => "QLED"]
                    ]
                ],
                [
                    "title" => "Частота обновления",
                    "options" => [
                        ["label" => "60Hz", "value" => "60Hz"],
                        ["label" => "120Hz", "value" => "120Hz"],
                        ["label" => "240Hz", "value" => "240Hz"]
                    ]
                ],
                [
                    "title" => "Диагональ",
                    "options" => [
                        ["label" => "32 дюйма", "value" => "32"],
                        ["label" => "40 дюймов", "value" => "40"],
                        ["label" => "50 дюймов", "value" => "50"],
                        ["label" => "65 дюймов", "value" => "65"]
                    ]
                ],
                [
                    "title" => "Цвет",
                    "options" => [
                        ["label" => "Белый", "value" => "white"],
                        ["label" => "Черный", "value" => "black"],
                        ["label" => "Серебристый", "value" => "silver"]
                    ]
                ]
            ]
        ],
        "tablet" => [
            "filters" => [
                [
                    "title" => "Размер экрана",
                    "options" => [
                        ["label" => "7 дюймов", "value" => "7"],
                        ["label" => "8 дюймов", "value" => "8"],
                        ["label" => "10 дюймов", "value" => "10"]
                    ]
                ],
                [
                    "title" => "Бренды",
                    "options" => [
                        ["label" => "POCO", "value" => "POCO"],
                        ["label" => "LG", "value" => "LG"],
                        ["label" => "BOSCH", "value" => "BOSCH"],
                        ["label" => "REDMI", "value" => "REDMI"]
                    ]
                ],
                [
                    "title" => "Объем памяти",
                    "options" => [
                        ["label" => "32 ГБ", "value" => "32gb"],
                        ["label" => "64 ГБ", "value" => "64gb"],
                        ["label" => "128 ГБ", "value" => "128gb"]
                    ]
                ],
                [
                    "title" => "Цвет",
                    "options" => [
                        ["label" => "Черный", "value" => "black"],
                        ["label" => "Белый", "value" => "white"],
                        ["label" => "Синий", "value" => "blue"]
                    ]
                ],
                [
                    "title" => "Тип подключения",
                    "options" => [
                        ["label" => "Wi-Fi", "value" => "wifi"],
                        ["label" => "Wi-Fi + LTE", "value" => "wifi-lte"]
                    ]
                ],
                [
                    "title" => "Операционная система",
                    "options" => [
                        ["label" => "Android", "value" => "android"],
                        ["label" => "iOS", "value" => "ios"]
                    ]
                ]
            ]
        ],
        "fridge" => [
            "filters" => [
                [
                    "title" => "Тип холодильника",
                    "options" => [
                        ["label" => "Двухкамерный", "value" => "two-chamber"],
                        ["label" => "Однокамерный", "value" => "single-chamber"],
                        ["label" => "С морозильником", "value" => "with-freezer"]
                    ]
                ],
                [
                    "title" => "Бренды",
                    "options" => [
                        ["label" => "Samsung", "value" => "Samsung"],
                        ["label" => "LG", "value" => "LG"],
                        ["label" => "Xiaomi", "value" => "Xiaomi"],
                        ["label" => "Panasonic", "value" => "Panasonic"]
                    ]
                ],
                [
                    "title" => "Объем",
                    "options" => [
                        ["label" => "200 л", "value" => "200l"],
                        ["label" => "300 л", "value" => "300l"],
                        ["label" => "400 л", "value" => "400l"]
                    ]
                ],
                [
                    "title" => "Класс энергопотребления",
                    "options" => [
                        ["label" => "A++", "value" => "a++"],
                        ["label" => "A+", "value" => "a+"],
                        ["label" => "A", "value" => "a"]
                    ]
                ],
                [
                    "title" => "Цвет",
                    "options" => [
                        ["label" => "Белый", "value" => "white"],
                        ["label" => "Черный", "value" => "black"],
                        ["label" => "Металлик", "value" => "metallic"]
                    ]
                ],
                [
                    "title" => "Управление",
                    "options" => [
                        ["label" => "Электронное", "value" => "electronic"],
                        ["label" => "Механическое", "value" => "mechanical"]
                    ]
                ]
            ]
        ],
        "smallappliances" => [
            "filters" => [
                [
                    "title" => "Тип устройства",
                    "options" => [
                        ["label" => "Чайник", "value" => "kettle"],
                        ["label" => "Тостер", "value" => "toaster"],
                        ["label" => "Миксер", "value" => "mixer"]
                    ]
                ],
                [
                    "title" => "Бренды",
                    "options" => [
                        ["label" => "Samsung", "value" => "Samsung"],
                        ["label" => "LG", "value" => "LG"],
                        ["label" => "Xiaomi", "value" => "Xiaomi"],
                        ["label" => "Panasonic", "value" => "Panasonic"]
                    ]
                ],
                [
                    "title" => "Материал",
                    "options" => [
                        ["label" => "Пластик", "value" => "plastic"],
                        ["label" => "Металл", "value" => "metal"]
                    ]
                ],
                [
                    "title" => "Цвет",
                    "options" => [
                        ["label" => "Белый", "value" => "white"],
                        ["label" => "Черный", "value" => "black"],
                        ["label" => "Красный", "value" => "red"]
                    ]
                ],
                [
                    "title" => "Мощность",
                    "options" => [
                        ["label" => "500 Вт", "value" => "500w"],
                        ["label" => "1000 Вт", "value" => "1000w"],
                        ["label" => "1500 Вт", "value" => "1500w"]
                    ]
                ]
            ]
        ]
    ];

    echo json_encode($categoryFilters);
} else {
    echo json_encode(['error' => 'Invalid action']);
}
?>
