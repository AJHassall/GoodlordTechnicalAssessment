
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Goodlord_TechnicalAssessment_AdamHassall.Enums
{
    public enum PaymentTypeEnum
    {
        None = 0,
        [Display(Name = "ATM")]
        ATM,
        [Display(Name = "Direct Debit")]
        DirectDebit,
        [Display(Name = "Card Payment")]
        CardPayment,
        [Display(Name = "Bank Credit")]
        BankCredit,
        [Display(Name = "Standing Order")]
        StandingOrder,
        [Display(Name = "Bank Transfer")]
        BankTransfer
    }
}