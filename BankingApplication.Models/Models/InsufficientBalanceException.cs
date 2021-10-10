using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Models
{
    public class InsufficientBalanceException:Exception
    {
        public InsufficientBalanceException(string message): base(message)
        {
                
        }
    }
}
