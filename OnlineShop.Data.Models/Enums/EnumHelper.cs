using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Models.Enums
{
    public static class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
            var displayAttribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Name : value.ToString();
        }
    }
}
