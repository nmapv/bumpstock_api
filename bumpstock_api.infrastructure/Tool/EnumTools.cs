using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace bumpstock_api.infrastructure.Tool
{
    public static class EnumTools
    {
        public static string GetDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            if (fi == null)
                return string.Empty;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
    }
    public class ValidEnumValueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type enumType = value.GetType();
            bool valid = Enum.IsDefined(enumType, value);
            if (!valid)
            {
                return new ValidationResult(String.Format("{0} is not a valid value for type {1}", value, enumType.Name));
            }
            return ValidationResult.Success;
        }
    }
}
