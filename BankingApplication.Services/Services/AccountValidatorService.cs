using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Services
{
    public class AccountValidatorService
    {
        public static void ValidateAccount(double AccNumber)
        {
            DataLoaderService.LoadData();
            if (!DataStructures.Accounts.ContainsKey(AccNumber))
            {
                throw new AccountDoesntExistException("Invalid account number. Please provide a valid one.");
            }
            else
            {
                return;
            }
        }
    }
}
