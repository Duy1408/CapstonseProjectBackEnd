using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.Enums.EnumHelpers
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public EnumDescriptionAttribute(string description)
        {
            Description = description;
        }
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription<T>(this T enumValue) where T : Enum
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false)
                                     .FirstOrDefault() as EnumDescriptionAttribute;
            return attribute?.Description ?? enumValue.ToString();
        }
    }


}
