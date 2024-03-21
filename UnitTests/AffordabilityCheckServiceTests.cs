using Goodlord_TechnicalAssessment_AdamHassall.Data;
using Goodlord_TechnicalAssessment_AdamHassall.Enums;
using Goodlord_TechnicalAssessment_AdamHassall.Services;
using Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors;
using Moq;
using Newtonsoft.Json.Linq;

namespace UnitTests
{
    public class Tests
    {
        private readonly Mock<ICSVProcessorFactory> mockCsvProcessorFactory = new Mock<ICSVProcessorFactory>();

        [SetUp]
        public void Setup()
        {
            IEnumerable<Property> propertiesData = new List<Property>
            {
                new Property(1, "1, oxford street", 300),
                new Property(2, "12, st john avenue", 750m),
                new Property(3, "flat 43, expensive block", 1200m),
                new Property(4, "flat 44, expensive block", 1150)
            };

            IEnumerable<BankTransaction> statementsData = new List<BankTransaction> {
                new BankTransaction(new DateTime(2020, 1, 1), PaymentTypeEnum.DirectDebit,   "Gas & Electricity", 95.06m,   null,     1200.04m),
                new BankTransaction(new DateTime(2020, 1, 2), PaymentTypeEnum.ATM,           "HSBC Holborn",      20.00m,   null,     1180.04m),
                new BankTransaction(new DateTime(2020, 1, 3), PaymentTypeEnum.StandingOrder, "London Room",       500.00m,  null,     680.04m),
                new BankTransaction(new DateTime(2020, 1, 4), PaymentTypeEnum.BankCredit,    "Awesome Job Ltd",   null,     1254.23m, 1934.27m),
                new BankTransaction(new DateTime(2020, 2, 1), PaymentTypeEnum.DirectDebit,   "Gas & Electricity", 95.06m,   null,     1839.21m),
                new BankTransaction(new DateTime(2020, 2, 2), PaymentTypeEnum.ATM,           "@Random",           50.00m,   null,     1789.21m),
                new BankTransaction(new DateTime(2020, 2, 3), PaymentTypeEnum.StandingOrder, "London Room",       500.00m,  null,     1289.21m),
                new BankTransaction(new DateTime(2020, 2, 4), PaymentTypeEnum.BankCredit,    "Awesome Job Ltd",   null,     1254.23m, 2543.44m)
            }; // date, type, description, money out, money in, balance

            mockCsvProcessorFactory.Setup(r => r.CreatePropertyProcessor())
                .Returns(() =>
                {
                    var propertyProcesssorMock = new Mock<ICSVProcessor<Property>>();

                    propertyProcesssorMock
                        .Setup(i => i.ProcessCSV(It.IsAny<string>()))
                        .Returns(propertiesData);

                    return propertyProcesssorMock.Object;
                });

            mockCsvProcessorFactory.Setup(r => r.CreateBankTransactionProcessor())
                .Returns(() =>
                {
                    var propertyProcesssorMock = new Mock<ICSVProcessor<BankTransaction>>();

                    propertyProcesssorMock
                        .Setup(i => i.ProcessCSV(It.IsAny<string>()))
                        .Returns(statementsData);

                    return propertyProcesssorMock.Object;
                });


        }

        [Test]
        public void Affordability_Check_Service_Contains_Expected_Property()
        {
            var affordabilityCheckService = new AffordabilityCheckService(mockCsvProcessorFactory.Object);

            Property expected = new Property(1, "1, Oxford Street", 300);

            var propertiesInList = affordabilityCheckService.GetListOfAffordableProperties()
                .FirstOrDefault(property =>
                     property.Id.Equals(expected.Id)
                     && property.Address.Equals(expected.Address, StringComparison.OrdinalIgnoreCase)
                     && property.PricePerCalandarMonth.Equals(expected.PricePerCalandarMonth)
                    );
            Assert.IsNotNull(propertiesInList);

            Assert.Pass();
        }

        [Test]
        public void Affordability_Threshold_Calculates_Correctly()
        {
            var affordabilityCheckService = new AffordabilityCheckService(mockCsvProcessorFactory.Object);
            Property expected = new Property(1, "1, Oxford Street", 300);
            var affordabilityThreshhold = affordabilityCheckService.GetAffordabilityThreshold(expected.PricePerCalandarMonth);

            Assert.That(affordabilityThreshhold, 
                Is.EqualTo(expected.PricePerCalandarMonth + expected.PricePerCalandarMonth * affordabilityCheckService.GetAffordabiltyMultiplier() ));

            Assert.Pass();
        }

        [Test]
        public void GetAverageIncome_CalculatesCorrectly()
        {
            var affordabilityCheckService = new AffordabilityCheckService(mockCsvProcessorFactory.Object);
            var expectedAverageIncome = 1254.23m; // Based on your sample data

            var actualAverageIncome = affordabilityCheckService.GetAverageIncome();

            Assert.That(actualAverageIncome, Is.EqualTo(expectedAverageIncome));
        }

        [Test]
        public void GetAffordabilityThreshold_HandlesZeroPrice()
        {
            var affordabilityCheckService = new AffordabilityCheckService(mockCsvProcessorFactory.Object);
            var pricePerCalendarMonth = 0m;
            var expectedAffordabilityThreshold = 0m; // Or adjust if you have different logic

            var actualAffordabilityThreshold = affordabilityCheckService.GetAffordabilityThreshold(pricePerCalendarMonth);

            Assert.That(actualAffordabilityThreshold, Is.EqualTo(expectedAffordabilityThreshold));
        }


    }
}