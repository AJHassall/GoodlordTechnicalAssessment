﻿using Goodlord_TechnicalAssessment_AdamHassall.Enums;

namespace Goodlord_TechnicalAssessment_AdamHassall.Data
{
    public class BankTransaction
    {
        //Date,Payment Type,Details,Money Out,Money In,Balance

        public DateTime Date { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public string Details{ get; set; }
        public decimal? MoneyOut { get; set; }
        public decimal? MoneyIn { get; set; }
        public decimal Balance { get; set; }

        public BankTransaction(DateTime date, PaymentTypeEnum paymentType, string details, decimal? moneyOut, decimal? moneyIn, decimal balance)
        {
            Date = date;
            PaymentType = paymentType;
            Details = details;
            MoneyOut = moneyOut;
            MoneyIn = moneyIn;
            Balance = balance;
        }

        public BankTransaction()
        {

        }
    }
}
