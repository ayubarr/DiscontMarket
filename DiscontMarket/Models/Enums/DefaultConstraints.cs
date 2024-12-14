using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscontMarket.Domain.Models.Enums
{
    public struct DefaultConstraints
    {
        // Длина строк
        public const int DefaultLength = 50;                  // Допустимая длина строки
        public const int DefaultMaxLength = 5000;             // Допустимая максимальная длина строки 

        // Точность чисел
        public const int DefaultPrecision = 12;              // Допустимая длина числа
        public const int DefaultMaxPrecision = 15;           // Допустимая максимальная длина числа

        // Точность дробной части чисел
        public const int DefaultPrecisionDecimalPoint = 1;   // Допустимая длина числа после запятой
        public const int DefaultMaxPrecisionDecimalPoint = 2; // Допустимая максимальная длина числа после запятой
    }

}
