using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Services
{
    public class BalanceValidatorService
    {
        public static void ValidateBalance(double AccNumber,int Requested=0,int DepositAmount=0)
        {
            int Balance = Convert.ToInt32(DataStructures.Accounts[AccNumber]["balance"]);
            if(Requested<0 || DepositAmount<0)
            {
                throw new InvalidAmountException("Invalid Amount to Process.");
            }
            else if (Balance <= Requested)
            {
                throw new InsufficientBalanceException("Insufficient Balance.");
            }
            else
            {
                return;
            }
        }
    }
}
