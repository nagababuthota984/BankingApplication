using BankingApplication.Services;
using System;
using BankingApplication.Models;
using BankingApplication.Database;

namespace BankingApplication.CLI
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to ABC Bank!\n");

            while (true)
            {

                WelcomeMenu Choice = UserInput.ShowMenu();
                switch (Choice)
                {
                    //done
                    case WelcomeMenu.CreateAccount:
                        Console.WriteLine("\t-------Account Creation-------\n");
                        Account NewAccount = new Account();
                        NewAccount.Name = UserInput.AskUser("Name");
                        NewAccount.Age = int.Parse(UserInput.AskUser("age"));
                        NewAccount.Gender = UserInput.AskUser("Gender");
                        NewAccount.Dob = DateTime.Parse(UserInput.AskUser("Date of Birth"));
                        NewAccount.ContactNumber = Double.Parse(UserInput.AskUser("Contact Number"));
                        NewAccount.AadharNumber = Double.Parse(UserInput.AskUser("Aadhar Number"));
                        NewAccount.PanNumber = Double.Parse(UserInput.AskUser("PAN Number"));
                        NewAccount.Address = UserInput.AskUser("Address");
                        NewAccount.AccountType = UserInput.AskUser("Account Type(Savings/Current)");
                        
                        AccountCreationService acc = new AccountCreationService();
                        Account FreshAccount = acc.CreateAccount(NewAccount);
                        UserOutput.AccountCreationSuccess(FreshAccount.Name, FreshAccount.AccountNumber.ToString());
                        break;
                    case WelcomeMenu.Deposit:
                        Console.WriteLine("\t-------Money Deposit-------\n");
                        double AccNumber = Double.Parse(UserInput.AskUser("Account Number"));
                        int Amount = int.Parse(UserInput.AskUser("Amount to Deposit"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            DepositAmountService deposit = new DepositAmountService();
                            deposit.DepositAmount(AccNumber, Amount);
                            UserOutput.Success("Credited");
                            UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[AccNumber]["balance"]));
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    //done
                    case WelcomeMenu.Withdraw:
                        Console.WriteLine("\n-------Amount Withdrawl-------\n");
                        AccNumber = Double.Parse(UserInput.AskUser("Account number"));
                        Amount = int.Parse(UserInput.AskUser("Amount to Withdraw"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            UserOutput.GreetUser(DataStructures.Accounts[AccNumber]["Name"]);
                            WithdrawService ws = new WithdrawService();
                            ws.WithdrawAmount(AccNumber, Amount);
                            UserOutput.Success("Debited");
                            UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[AccNumber]["Balance"]));
                        }
                        catch (InsufficientBalanceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch(InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                        break;
                    //done
                    case WelcomeMenu.Transfer:
                        Console.WriteLine("-------Amount Transfer-------\n");
                        AccNumber = Double.Parse(UserInput.AskUser("Sender Account number"));
                        double receiver = Double.Parse(UserInput.AskUser("Receiver Account Number"));
                        Amount = int.Parse(UserInput.AskUser("Amount to Transfer"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            AccountValidatorService.ValidateAccount(receiver);
                            TransferService ts = new TransferService();
                            ts.TransferAmount(AccNumber, receiver, Amount);
                            UserOutput.Success(DataStructures.Accounts[receiver]["Name"], Amount);
                            UserOutput.ShowBalance(int.Parse(DataStructures.Accounts[AccNumber]["Balance"]));

                        }
                        catch (InsufficientBalanceException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidAmountException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                        break;
                    case WelcomeMenu.PrintStatement:
                        Console.WriteLine("-------Transaction History-------\n");
                        AccNumber = Double.Parse(UserInput.AskUser("AccountNumber"));
                        try
                        {
                            AccountValidatorService.ValidateAccount(AccNumber);
                            PrintStatementService ps = new PrintStatementService();
                            UserOutput.ShowTransactions(ps.FetchTransactionHistory(AccNumber));

                        }
                        catch (AccountDoesntExistException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    default:
                        Environment.Exit(0);
                        break;
                }


            }
        }
    }
}
