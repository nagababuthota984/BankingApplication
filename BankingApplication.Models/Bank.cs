﻿using System.Collections.Generic;

namespace BankingApplication.Models
{
    public class Bank : BaseBank
    {

        public List<Account> Accounts { get; set; }
        
        public decimal SelfRTGS { get; set; }
        public decimal SelfIMPS { get; set; }
        public decimal OtherRTGS { get; set; }
        public decimal OtherIMPS { get; set; }
        public decimal Balance { get; set; }
        public Currency DefaultCurrency { get; set; }
        public List<Transaction> Transactions  { get; set; }
        public List<Staff> Employees { get; set; }
        public List<Currency> SupportedCurrency { get; set; }
    }
}
