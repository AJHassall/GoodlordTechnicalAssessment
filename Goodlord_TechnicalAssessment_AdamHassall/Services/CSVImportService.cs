using CsvHelper;
using CsvHelper.Configuration;
using Goodlord_TechnicalAssessment_AdamHassall.CsvClassMaps;
using Goodlord_TechnicalAssessment_AdamHassall.Data;
using System.Formats.Asn1;
using System.Globalization;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services
{
    public class CSVImportService
    {
        public virtual IEnumerable<BankTransaction> ProcessCSVBankStatement(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                ShouldSkipRecord = args =>
                {
                    var rawRow = args.Row.Parser.RawRow;
                    return rawRow < 10 || rawRow == 11;
                }
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<BankTransactionModelMap>();
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = csv.GetRecord<BankTransaction>();
                    yield return record;
                }
            }
        }
        public virtual IEnumerable<Property> ProcessCSVProperties(string filePath)
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
