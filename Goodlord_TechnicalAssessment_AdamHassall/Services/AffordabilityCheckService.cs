using Goodlord_TechnicalAssessment_AdamHassall.Data;
using Goodlord_TechnicalAssessment_AdamHassall.Enums;
using Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors;
using System.Collections;
using System.Transactions;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services
{
    public class AffordabilityCheckService
    {
        private readonly ICSVProcessorFactory _csvProcessorFactory;

        private const decimal AffordabiltyMultiplier = 1.25m;

        public AffordabilityCheckService(ICSVProcessorFactory csvImportService)
        {
            _csvProcessorFactory = csvImportService;
        }
        public virtual decimal GetAverageIncome()
        {
            var processor = _csvProcessorFactory.CreateBankTransactionProcessor();
            IEnumerable<BankTransaction> bankTransactions = processor.ProcessCSV("Input/bank_statement.csv");

            // Filter for recurring income transactions
            var recurringTransactions = bankTransactions
                    .GroupBy(transaction => new
                    {
                        transaction.Details,
                        transaction.PaymentType
                    })
                    .Where(group => group.Count() > 1) // Keep groups with occurrences in multiple months
                    .SelectMany(group => group);

            var monthlyAverages = recurringTransactions
                .GroupBy(t => t.Date.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    TotalMoneyIn = group.Sum(t => t.MoneyIn),
                    TotalMoneyOut = group.Sum(t => t.MoneyOut),
                    //AverageNetIncome = group.Average(t=> t.MoneyIn?? 0 - t.MoneyOut ??0)
                });

            var overallAverage = monthlyAverages.Average(monthData => monthData.TotalMoneyIn ?? 0 - monthData.TotalMoneyOut ?? 0);

            return overallAverage;
        }

        public virtual IEnumerable<Property> GetListOfAffordableProperties()
        {
            var processor = _csvProcessorFactory.CreatePropertyProcessor();
            IEnumerable<Property> properties = processor.ProcessCSV("Input/properties.csv");

            return properties.Where(property => GetAffordabilityThreshold(property.PricePerCalandarMonth) < GetAverageIncome()).ToList();
            
        }

        public decimal GetAffordabiltyMultiplier()
        {
            return AffordabiltyMultiplier;
        }

        public decimal GetAffordabilityThreshold(decimal pricePerCalandarMonth)
        {
            return pricePerCalandarMonth + pricePerCalandarMonth * AffordabiltyMultiplier;
        }
    }
}
