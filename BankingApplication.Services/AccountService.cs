using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApplication.Services
{
    public class AccountService
    {
        public AccountService()
        {
            DataLoaderService.LoadData();
        }

        public void DepositAmount(string accNumber, decimal amount, string bankname)
        {

            //update current object's available balance.
            try
            {
                Account UserAccount = new Account(); 
                Bank UserBank = new Bank();
                Utilities.ValidateAccount(accNumber, bankname);
                Utilities.ValidateBalance(accNumber, bankname, 0, amount);
                foreach (var bank in RBIStorage.Banks)
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
                TransactionService.RecordTransaction(UserAccount, TransactionType.Credit, DateTime.Now, amount, UserBank.BankId);
                DataLoaderService.WriteData(RBIStorage.Banks);
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
        public void WithdrawAmount(string accNumber,string bankname, decimal amount)
        {
            //withdraws money and updates account details.


            DataLoaderService.LoadData();
            try
            {
                Account UserAccount = Utilities.FetchAccount(accNumber, bankname);
                Bank UserBank = new Bank();
                Utilities.ValidateAccount(accNumber, bankname);
                Utilities.ValidateBalance(accNumber, bankname, amount, 0);
                
                TransactionService.RecordTransaction(UserAccount, TransactionType.Credit, DateTime.Now, amount, UserAccount.BankId, true, ModeOfTransfer.RTGS);
                DataLoaderService.WriteData(RBIStorage.Banks);

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
        public void TransferAmount(string senderaccnumber, string senderbank, string receiveraccnumber, string receiverbank, decimal amount)
        {
            //needs a lot of logical modifications.
            //transfers money from one accc to another
            try
            {
                Utilities.ValidateAccount(senderaccnumber, senderbank);
                Utilities.ValidateAccount(receiveraccnumber, receiverbank);
                //balance validator
                Utilities.ValidateBalance(senderaccnumber, senderbank,  amount, 0);
                Utilities.ValidateBalance(receiveraccnumber, receiverbank,0, amount);


                //calculate sender and receiver balances
                WithdrawAmount(senderaccnumber, senderbank, amount);
                DepositAmount(receiveraccnumber, amount, receiverbank);
                
                //rtgs and imps to be added.
                
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
        
        public List<Transaction> FetchTransactionHistory(string AccNumber,string bankname)
        {
            try
            {
                //prints user's acount history
                List<string> Transactions = new List<string>();
                Utilities.ValidateAccount(AccNumber, bankname);
                Account userAccount = Utilities.FetchAccount(AccNumber, bankname);
                return userAccount.Transactions;
                
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

