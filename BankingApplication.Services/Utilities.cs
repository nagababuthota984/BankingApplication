using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Services
{
    public class Utilities
    {
        //Contains all the helper methods needed for AccountService and BankServices.
        public static void ValidateBalance(string AccNumber, string bankname, decimal Requested = 0, decimal DepositAmount = 0)
        {

            List<Account> Accounts = FetchAccountsFromBank(bankname);
            Account acc = new Account();

            foreach (var account in Accounts)
            {
                if (account.AccountNumber == AccNumber)
                {
                    acc = account;
                }
            }
            decimal Balance = acc.Balance;
            if (Requested < 0 || DepositAmount < 0)
            {
                throw new InvalidAmountException("Invalid Amount to Process.");
            }
            else if (Balance < Requested)
            {
                throw new InsufficientBalanceException("Insufficient Balance.");
            }
            else
            {
                return;
            }
        }
        internal static bool IsDuplicateAccountNumber(string accountNumber, string bankid)
        {

            var RequiredBank = RBIStorage.Banks.SingleOrDefault(bank => bank.BankId == bankid);
            if (RequiredBank != null)
            {
                foreach (var Acc in RequiredBank.Accounts)
                {
                    if (Acc.AccountNumber == accountNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static void ValidateAccount(string accNumber, string bankname)
        {
            DataLoaderService.LoadData();
            try
            {
                Bank RequiredBank = FetchBank(bankname);
                FetchAccount(accNumber, bankname);
            }
            catch(InvalidBankException e)
            {
                throw new InvalidBankException(e.Message);
            }
            catch(AccountDoesntExistException e)
            {
                throw new AccountDoesntExistException(e.Message);
            }

        }
        internal static string GenerateRandomNumber(int length)
        {
            Random r = new Random();          //account number generator.
            string NumberString = "";
            int i;
            for (i = 1; i < length; i++)
            {
                NumberString += r.Next(0, 9).ToString();
            }
            return NumberString;
        }
        public static List<Account> FetchAccountsFromBank(string bankname)
        {
            DataLoaderService.LoadData();
            Bank bank = RBIStorage.Banks.SingleOrDefault(bank => bank.BankName == bankname);
            if (bank != null)
            {
                return bank.Accounts;
            }
            else
            {
                throw new InvalidBankException("Please Enter Valid Bank Details.");

            }
        }
        public static Account FetchAccount(string accNumber, string bankname)
        {
            List<Account> Accounts = FetchAccountsFromBank(bankname);
            try
            {
                Account account = Accounts.Single(acc => acc.AccountNumber == accNumber);
                return account;
            }
            catch (Exception e)
            {
                throw new AccountDoesntExistException("Account does Not exists. Please enter valid account number.");
            }

        }
        public static decimal FetchBalance(string accountNumber, string bankname)
        {
            Account UserAccount = FetchAccount(accountNumber, bankname);
            return UserAccount.Balance;
        }
        internal static Bank FetchBank(string bankName)
        {
           
             return RBIStorage.Banks.SingleOrDefault(bank => bank.BankName == bankName);
            
        }
    }
}
