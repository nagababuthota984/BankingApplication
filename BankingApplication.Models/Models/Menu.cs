using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class Menu
    {
        public enum MenuItems
        {
            CreateAccount=1,
            Deposit,
            Withdraw,
            Transfer,
            PrintStatement
        }
    }
}
