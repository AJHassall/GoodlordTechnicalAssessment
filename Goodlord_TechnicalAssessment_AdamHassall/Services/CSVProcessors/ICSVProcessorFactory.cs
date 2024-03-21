using Goodlord_TechnicalAssessment_AdamHassall.Data;
using System.Transactions;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors
{
    public interface ICSVProcessorFactory
    {
        ICSVProcessor<Property> CreatePropertyProcessor();
        ICSVProcessor<BankTransaction> CreateBankTransactionProcessor();
    }
}
