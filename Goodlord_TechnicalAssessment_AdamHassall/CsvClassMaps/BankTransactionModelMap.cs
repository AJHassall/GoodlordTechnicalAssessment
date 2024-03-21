using CsvHelper.Configuration;
using Goodlord_TechnicalAssessment_AdamHassall.Data;
using Goodlord_TechnicalAssessment_AdamHassall.TypeConverter;

namespace Goodlord_TechnicalAssessment_AdamHassall.CsvClassMaps
{
    public class BankTransactionModelMap: ClassMap<BankTransaction>
    {
        public BankTransactionModelMap() 
        {
            Map(m => m.Date).Name("Date").TypeConverter<DateConverter>();
            Map(m => m.PaymentType).Name("Payment Type").TypeConverter<PaymentTYpeConverter>();
            Map(m => m.Details).Name("Details");
            Map(m => m.MoneyOut).Name("Money Out").TypeConverter<MoneyConverter>();
            Map(m => m.MoneyIn).Name("Money In").TypeConverter<MoneyConverter>();
            Map(m => m.Balance).Name("Balance").TypeConverter<MoneyConverter>();
        }
    }
}
