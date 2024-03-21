using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvHelper.TypeConversion;
using Goodlord_TechnicalAssessment_AdamHassall.Enums;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Goodlord_TechnicalAssessment_AdamHassall.TypeConverter
{
    public class PaymentTYpeConverter: DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var memberType = memberMapData.Member.MemberType();

            // Ensure the property is an enum
            if (!memberType.IsEnum)
            {
                return base.ConvertFromString(text, row, memberMapData);
            }

            // Find the enum field matching the 'Name' attribute value
            foreach (var field in memberType.GetFields())
            {
                var nameAttribute = field.GetCustomAttribute<DisplayAttribute>();
                if (nameAttribute?.Name == text)
                {
                    return field.GetValue(null); // Return the enum value
                }
            }

            // Handle a potential mismatch (throw an exception or return a default)
            throw new FormatException($"Unable to parse enum value: {text}");
        }
    }
}
