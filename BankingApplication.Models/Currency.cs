using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Currency
    {
        #region Properties
        public string Name { get; set; }
        public decimal ExchangeRate { get; set; }
        #endregion
        public Currency()
        {

        }
        public Currency(string name, decimal exchangeRate)
        {
            Name = name;
            ExchangeRate = exchangeRate;
        }
        
    }
}
