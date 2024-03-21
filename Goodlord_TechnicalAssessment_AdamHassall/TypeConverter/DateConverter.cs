using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Http;

namespace Goodlord_TechnicalAssessment_AdamHassall.TypeConverter
{
    public class DateConverter: DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {


            if (string.IsNullOrEmpty(text))
            {
                return DateTime.MinValue; // Handle empty values (or throw an exception if needed)
            }

            string replacedStr = text.Substring(0, 4) // Remove "st nd rd th" suffixes from date string
                         .Replace("nd", "")
                         .Replace("th", "")
                         .Replace("rd", "")
                         .Replace("st", "")
                         + text.Substring(4);

            DateTime parsedDate;
            CultureInfo culture = new CultureInfo("en-GB");
            if (DateTime.TryParseExact(replacedStr, "d MMMM yyyy", CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                throw new FormatException($"Unable to parse date in value: {text}");
            }
        }
    }
}
