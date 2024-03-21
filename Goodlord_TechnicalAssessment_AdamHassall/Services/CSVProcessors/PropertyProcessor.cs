using CsvHelper.Configuration;
using CsvHelper;
using Goodlord_TechnicalAssessment_AdamHassall.CsvClassMaps;
using Goodlord_TechnicalAssessment_AdamHassall.Data;
using System.Globalization;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors
{
    public class PropertyProcessor : ICSVProcessor<Property>
    {
        public IEnumerable<Property> ProcessCSV(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<PropertyModelMap>();
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = csv.GetRecord<Property>();
                    yield return record;
                }
            }
        }
    }
}
