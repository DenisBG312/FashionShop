using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common
{
    public class EntityValidationConstants
    {
        public static class Payment
        {
            public const int AmountMinValue = 0;
            public const int AmountMaxValue = 999999;
        }

        public static class Product
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;
            public const int AmountMinValue = 0;
            public const int AmountMaxValue = 999999;
            public const int StockQuantityMinValue = 0;
            public const int StockQuantityMaxValue = int.MaxValue;
        }

        public static class OrderProduct
        {
            public const int QuantityMinValue = 0;
            public const int QuantityMaxValue = 100;
            public const int AmountMinValue = 0;
            public const int AmountMaxValue = 999999;
        }

        public static class Gender
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;
        }

        public static class ClothingType
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;
        }
    }
}
