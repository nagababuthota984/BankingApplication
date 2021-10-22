using BankingApplication.Database;
using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication.Services
{
    public class AccountService
    {
        public AccountService()
        {
            DataLoaderService.LoadData();
        }

        public decimal DepositAmount(string accNumber, decimal amount, string bankname)
        {

            //update current object's available balance.
            try
            {
                Account UserAccount = new Account(); 
                Bank UserBank = new Bank();
                Utilities.ValidateAccount(accNumber, bankname);
                Utilities.ValidateBalance(accNumber, bankname, 0, amount);
                foreach (var bank in RbiStorage.Banks)
                {
                    if (bank.BankName == bankname)
                    {
                        foreach (var account in bank.Accounts)
                        {
                            if (account.AccountNumber == accNumber)
                            {
                                account.Balance += amount;
                                UserAccount = account;
                                UserBank = bank;
                                break;

                            }
                        }
                    }
                }
                Transaction NewTrans = new Transaction(TransType.Credit,DateTime.Now, amount, UserAccount.Balance,UserBank.BankId,UserAccount.AccountId);
                UserAccount.Transactions.Add(NewTrans);
                DataReaderWriter.WriteData(RbiStorage.Banks);
                return UserAccount.Balance;
            }
            catch (InvalidAmountException e)
            {
                throw new InvalidAmountException(e.Message);
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
        public decimal WithdrawAmount(string accNumber,string bankname, decimal amount)
        {
            //withdraws money and updates account details.


            DataLoaderService.LoadData();
            try
            {
                Account UserAccount = new Account();
                Bank UserBank = new Bank();
                Utilities.ValidateAccount(accNumber, bankname);
                Utilities.ValidateBalance(accNumber, bankname, amount, 0);
                foreach (var bank in RbiStorage.Banks)
                {
                    if (bank.BankName == bankname)
                    {
                        foreach (var account in bank.Accounts)
                        {
                            if (account.AccountNumber == accNumber)
                            {
                                account.Balance -= amount;
                                UserAccount = account;
                                UserBank = bank;
                                break;
                            }
                        }
                    }
                }
                Transaction NewTrans = new Transaction(TransType.Debit, DateTime.Now, amount, UserAccount.Balance, UserBank.BankId, UserAccount.AccountId);
                UserAccount.Transactions.Add(NewTrans);
                DataReaderWriter.WriteData(RbiStorage.Banks);
                return UserAccount.Balance;

            }
            catch (InsufficientBalanceException e)
            {
                throw new InsufficientBalanceException(e.Message);
            }
            catch (InvalidAmountException e)
            {
                throw new InvalidAmountException(e.Message);
            }
            catch (AccountDoesntExistException e)
            {
                throw new AccountDoesntExistException(e.Message);
            }
        }
        public List<string> TransferAmount(string senderaccnumber, string senderbank, string receiveraccnumber, string receiverbank, decimal amount)
        {
            //transfers money from one accc to another
            try
            {
                string SenderName = Utilities.ValidateAccount(senderaccnumber, senderbank);
                string ReceiverName = Utilities.ValidateAccount(receiveraccnumber, receiverbank);
                //balance validator
                Utilities.ValidateBalance(senderaccnumber, senderbank,  amount, 0);
                Utilities.ValidateBalance(receiveraccnumber, receiverbank,0, amount);


                //calculate sender and receiver balances
                decimal SenderBalance = WithdrawAmount(senderaccnumber, senderbank, amount);
                DepositAmount(receiveraccnumber, amount, receiverbank);
                return new List<string>() {  ReceiverName, amount.ToString(), SenderBalance.ToString() };

                
            }
            catch (InsufficientBalanceException e)
            {
                throw new InsufficientBalanceException(e.Message);
            }
            catch (InvalidAmountException e)
            {
                throw new InvalidAmountException(e.Message);
            }
            catch(AccountDoesntExistException e)
            {
                throw new AccountDoesntExistException(e.Message);
            }
            catch(InvalidBankException e)
            {
                throw new InvalidBankException(e.Message);
            }


        }
        
        public List<string> FetchTransactionHistory(string AccNumber,string bankname)
        {
            try
            {
                //prints user's acount history
                List<string> Transactions = new List<string>();
                Utilities.ValidateAccount(AccNumber, bankname);
                Account UserAccount = Utilities.FetchAccount(AccNumber, bankname);
                if (UserAccount.Transactions.Count > 0)    //If there is atleast one transaction.
                {
                    foreach(var transaction in UserAccount.Transactions)
                    {
                        string temp = $"{transaction.TransId}|{transaction.Type}|{transaction.TransactionAmount}|{transaction.BalanceAmount}|{transaction.On}";
                        
                        Transactions.Add(temp);
                    }
                    return Transactions;
                }
                else
                {
                    return (new List<string>() { "None transactions recorded so far!" });
                }
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


    }
}

