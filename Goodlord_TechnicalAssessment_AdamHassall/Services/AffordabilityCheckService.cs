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
        public virtual Decimal GetAverageIncome()
        {
            IEnumerable<BankTransaction> bankTransactions = _csvImportService.ProcessCSVBankStatement("Input/bank_statement.csv");

            decimal averageIncome = bankTransactions
                .Where(t => t.PaymentType == PaymentTypeEnum.BankCredit && t.MoneyIn != null)
                .GroupBy(t => t.Details)
                .Where(g => g.Count() > 1)  // Ensure a recurring income description exists
                .Select(group => group.Average(t => t.MoneyIn!.Value))
                .FirstOrDefault(0m); // Return the first average or 0m if none found 

            
            return averageIncome;

        }

        public virtual IEnumerable<Property> GetListOfAffordableProperties()
        {
            IEnumerable<Property> properties = _csvImportService.ProcessCSVProperties("Input/properties.csv");

            return properties.Where(property => GetAffordabilityThreshhold(property.PricePerCalandarMonth) < GetAverageIncome());
            
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
