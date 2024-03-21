using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvHelper.TypeConversion;

namespace Goodlord_TechnicalAssessment_AdamHassall.TypeConverter
{
    public class MoneyConverter: DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null; // Handle empty values (or throw an exception if needed)
            }

            // Remove the currency symbol
            var cleanValue = text.TrimStart('£');

            // Parse as decimal using the appropriate CultureInfo
            if (decimal.TryParse(cleanValue, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-GB"), out decimal result))
            {
                return result;
            }
            else
            {
                throw new FormatException($"Unable to parse money in value: {text}");
            }
        }
    }
}
