using Goodlord_TechnicalAssessment_AdamHassall.Data;
using Goodlord_TechnicalAssessment_AdamHassall.Enums;
using System.Collections;

namespace Goodlord_TechnicalAssessment_AdamHassall.Services
{
    public class AffordabilityCheckService
    {
        private readonly CSVImportService _csvImportService;

        private const decimal AffordabiltyMultiplier = 1.25m;

        public AffordabilityCheckService(CSVImportService csvImportService)
        {
            _csvImportService = csvImportService;
        }
        public virtual decimal GetAverageIncome()
        {
            IEnumerable<BankTransaction> bankTransactions = _csvImportService.ProcessCSVBankStatement("Input/bank_statement.csv");

            // Filter for recurring income transactions
            var recurringIncomeTransactions = bankTransactions
                .Where(transaction => transaction.PaymentType == PaymentTypeEnum.BankCredit)
                .Where(transaction => transaction.MoneyIn != null)
                .GroupBy(transaction => transaction.Date);

            // Calculate monthly income totals
            var monthlyIncomeTotals = recurringIncomeTransactions
            .Select(group => new
            {
                JobDescription = group.Key,  // Include JobDescription
                MonthlyIncomeTotals = group.GroupBy(transaction => transaction.Date.Month)
                                           .Select(monthGroup => new
                                           {
                                               Month = monthGroup.Key,
                                               TotalIncome = monthGroup.Sum(transaction => transaction.MoneyIn)
                                           })
            });

            var averageMonthlySum = monthlyIncomeTotals
                .Average(jobData => jobData.MonthlyIncomeTotals.Sum(monthData => monthData.TotalIncome));
            

            return averageMonthlySum ??= 0;

        }

        public virtual IEnumerable<Property> GetListOfAffordableProperties()
        {
            IEnumerable<Property> properties = _csvImportService.ProcessCSVProperties("Input/properties.csv");

            return properties.Where(property => GetAffordabilityThreshhold(property.PricePerCalandarMonth) < GetAverageIncome()).ToList();
            
        }

        public decimal GetAffordabiltyMultiplier()
        {
            return AffordabiltyMultiplier;
        }

        public decimal GetAffordabilityThreshhold(decimal pricePerCalandarMonth)
        {
            return pricePerCalandarMonth + pricePerCalandarMonth * AffordabiltyMultiplier;
        }
    }
}
