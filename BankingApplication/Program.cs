using BankingApplication.Services;
using System;
using BankingApplication.UserInteraction;
using BankingApplication.Exceptions;
using BankingApplication.Models;
namespace BankingApplication.CLI
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("\nWelcome to ABC Bank!\n");

            while (true)
            {

                Console.WriteLine("\nChoose any one option:\n1.Create Account\n2.Deposit\n3.Withdraw\n4.Transfer Amount\n5.Print Transaction history\n");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {

                    case (int)Menu.MenuItems.CreateAccount:
                        Console.WriteLine("\t-------Account Creation-------\n");
                        Account newAccount = new Account();
                        newAccount.name = UserInput.AskUser("Name");
                        newAccount.age = int.Parse(UserInput.AskUser("age"));
                        newAccount.gender = UserInput.AskUser("Gender");
                        newAccount.dob = DateTime.Parse(UserInput.AskUser("Date of Birth"));
                        newAccount.contactNumber = Double.Parse(UserInput.AskUser("Contact Number"));
                        newAccount.aadharNumber = Double.Parse(UserInput.AskUser("Aadhar Number"));
                        newAccount.panNumber = Double.Parse(UserInput.AskUser("PAN Number"));
                        newAccount.address = UserInput.AskUser("Address");

                        AccountCreationService acc = new AccountCreationService();
                        acc.CreateAccount(newAccount);
                        
                        break;
                    case (int)Menu.MenuItems.Deposit:
                        Console.WriteLine("\t-------Money Deposit-------\n");
                        double accNumber = Double.Parse(UserInput.AskUser("Account Number"));
                        int amount = int.Parse(UserInput.AskUser("amount to Deposit"));
                        try
                        {
                            DepositAmountService deposit = new DepositAmountService();
                            deposit.DepositAmount(accNumber, amount);
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
                    case (int)Menu.MenuItems.Withdraw:
                        Console.WriteLine("\n-------Amount Withdrawl-------\n");
                        accNumber = Double.Parse(UserInput.AskUser("Account number"));
                        amount = int.Parse(UserInput.AskUser("Amount to Withdraw"));
                        try
                        {
                            WithdrawService ws = new WithdrawService();
                            ws.WithdrawAmount(accNumber, amount);
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
                    case (int)Menu.MenuItems.Transfer:
                        Console.WriteLine("-------Amount Transfer-------\n");
                        accNumber = Double.Parse(UserInput.AskUser("Sender Account number"));
                        double receiver = Double.Parse(UserInput.AskUser("Receiver Account Number"));
                        amount = int.Parse(UserInput.AskUser("Amount to Transfer"));
                        try
                        {
                            TransferService ts = new TransferService();
                            ts.TransferAmount(accNumber, receiver, amount);
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
                    case (int)Menu.MenuItems.PrintStatement:
                        Console.WriteLine("-------Transaction History-------\n");
                        accNumber = Double.Parse(UserInput.AskUser("AccountNumber"));
                        try
                        {
                            PrintStatementService ps = new PrintStatementService();
                            ps.PrintTransactionHistory(accNumber);
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
