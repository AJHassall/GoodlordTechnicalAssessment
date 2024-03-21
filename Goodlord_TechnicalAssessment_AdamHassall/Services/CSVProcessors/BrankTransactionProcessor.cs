using CsvHelper.Configuration;
using CsvHelper;
using Goodlord_TechnicalAssessment_AdamHassall.CsvClassMaps;
using Goodlord_TechnicalAssessment_AdamHassall.Data;
using System.Globalization;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors
{
    public class BrankTransactionProcessor : ICSVProcessor<BankTransaction>
    {
        public IEnumerable<BankTransaction> ProcessCSV(string filePath)
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
    }
}
