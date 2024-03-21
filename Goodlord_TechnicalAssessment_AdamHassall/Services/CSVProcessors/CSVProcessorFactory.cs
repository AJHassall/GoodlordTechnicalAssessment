using Goodlord_TechnicalAssessment_AdamHassall.Data;
using System.Transactions;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors
{
    public class CSVProcessorFactory : ICSVProcessorFactory
    {
        public ICSVProcessor<Property> CreatePropertyProcessor()
        {
            return new PropertyProcessor();
        }
        public ICSVProcessor<BankTransaction> CreateBankTransactionProcessor()
        {
            return new BrankTransactionProcessor();
        }
    }
}
