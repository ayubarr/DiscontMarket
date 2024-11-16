namespace DiscontMarket.Domain.Models.Enums
{
    public struct PgSqlColumnTypes
    {
        // Текстовые типы
        public const string Varchar = "varchar";                     // Строка фиксированной длины
        public const string Text = "text";                           // Длинные строки без ограничения длины
        public const string Char = "char";                           // Один символ или фиксированная длина строки
        public const string Json = "json";                           // JSON
        public const string Jsonb = "jsonb";                         // JSON с бинарной оптимизацией
        public const string Uuid = "uuid";                           // Универсальный уникальный идентификатор

        // Числовые типы
        public const string SmallInt = "smallint";                   // Целые числа (2 байта)
        public const string Integer = "integer";                     // Целые числа (4 байта)
        public const string BigInt = "bigint";                       // Целые числа (8 байт)
        public const string Numeric = "numeric";                     // Числа с фиксированной точностью
        public const string Real = "real";                           // Числа с плавающей точкой (4 байта)
        public const string DoublePrecision = "double precision";    // Числа с плавающей точкой (8 байт)
        public const string Serial = "serial";                       // Автоинкремент для integer
        public const string BigSerial = "bigserial";                 // Автоинкремент для bigint

        // Дата и время
        public const string Timestamp = "timestamp";                 // Дата и время без часового пояса
        public const string Timestamptz = "timestamptz";             // Дата и время с часовым поясом
        public const string Date = "date";                           // Только дата
        public const string Time = "time";                           // Только время
        public const string Timetz = "timetz";                       // Время с часовым поясом
        public const string Interval = "interval";                   // Интервалы времени

        // Булевый тип
        public const string Boolean = "boolean";                     // true/false

        // Бинарные типы
        public const string Bytea = "bytea";                         // Бинарные данные

        // Геометрические типы
        public const string Point = "point";                         // Точка
        public const string Line = "line";                           // Прямая
        public const string Lseg = "lseg";                           // Отрезок
        public const string Box = "box";                             // Прямоугольник
        public const string Path = "path";                           // Путь
        public const string Polygon = "polygon";                     // Многоугольник
        public const string Circle = "circle";                       // Круг

        // Сетевые типы
        public const string Inet = "inet";                           // IP-адрес
        public const string Cidr = "cidr";                           // Сетевой адрес
        public const string MacAddr = "macaddr";                     // MAC-адрес
        public const string MacAddr8 = "macaddr8";                   // MAC-адрес (8-байтовый)

        // Специальные типы
        public const string Bit = "bit";                             // Биты фиксированной длины
        public const string VarBit = "varbit";                       // Биты переменной длины
        public const string TsQuery = "tsquery";                     // Поисковый запрос в полнотекстовом поиске
        public const string TsVector = "tsvector";                   // Полнотекстовый поисковый документ
        public const string Xml = "xml";                             // XML-данные
    }
}
