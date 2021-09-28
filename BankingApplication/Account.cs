using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BankingApplication
{
    public class Account : Person
    {

        // public double accountNumber;
        public double accountNumber;
        public string accountType;
        public double balance;
        public const int MIN_BALANCE = 250;
        public static Dictionary<double, Dictionary<string, string>> accounts;
        public static Dictionary<double, string> transactions;
        public Account()
        {
            double accountNumber = 0;
            string accountType = "";
            double balance = 0;
            accounts = DataReaderWriter.readAccounts();
            transactions = DataReaderWriter.readTransactions();


        }
       

        public void depositAmount()
        {
            //deposits money and updates account details.
            int amount;
            double accNumber;
            Console.WriteLine("Enter your Account Number: ");
            accNumber = Convert.ToDouble(Console.ReadLine());
            
            if (accounts.ContainsKey(accNumber))   //check for validity.
            {
                Console.WriteLine("Hello! {0}",accounts[accNumber]["name"]);
                Console.WriteLine("Enter amount to deposit: ");
                amount = Convert.ToInt32(Console.ReadLine());
                if (amount > 0)
                {
                    //update current object's available balance.
                    string details = DateTime.Now + " " + amount+"INR" + " Credited";
                    amount = Convert.ToInt32(accounts[accNumber]["balance"]) + amount;
                    accounts[accNumber]["balance"] = Convert.ToString(amount);
                    DataReaderWriter.writeAccounts(accounts);
                    Console.WriteLine("Amount has been credited successfully!");
                    Console.WriteLine("Current balance ->{0}\n\n", accounts[accNumber]["balance"]);

                    //making the transaction
                    if (!transactions.ContainsKey(accNumber))
                    {
                        transactions.Add(accNumber, details);
                    }
                    else
                    {
                        transactions[accNumber] += "," + details;
                    }
                    DataReaderWriter.writeTransactions(transactions);
                }
                else
                {
                    Console.WriteLine("Invalid amount to deposit.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist!");
            }
        }
        public void withdrawAmount()
        {
            //withdraws money and updates account details.
            int amount = 0;
            double accNumber;
            Console.WriteLine("Enter your Account Number: ");
            accNumber = Convert.ToDouble(Console.ReadLine());

            if (accounts.ContainsKey(accNumber))   //check for validity.
            {
                Console.WriteLine("Hello! {0}", accounts[accNumber]["name"]);
                Console.WriteLine("Enter amount to withdraw: ");
                amount = Convert.ToInt32(Console.ReadLine());
                if (amount > 0)
                {
                    if (amount <= Convert.ToInt32(accounts[accNumber]["balance"]))
                    {
                        //update current object's available balance.
                        string details = DateTime.Now + " " + amount + "INR" + " Dedited";
                        amount = Convert.ToInt32(accounts[accNumber]["balance"]) - amount;
                        accounts[accNumber]["balance"] = Convert.ToString(amount);
                        DataReaderWriter.writeAccounts(accounts);
                        Console.WriteLine("Amount has been debited successfully!");
                        Console.WriteLine("Current balance ->{0}\n\n", accounts[accNumber]["balance"]);
                        //making the transaction
                        if (!transactions.ContainsKey(accNumber))
                        {
                            transactions.Add(accNumber, details);
                        }
                        else
                        {
                            transactions[accNumber] += "," + details;
                        }
                        DataReaderWriter.writeTransactions(transactions);


                    }
                    else
                    {
                        Console.WriteLine("Insufficient account balance.\n");
                    }


                }
                else
                {
                    Console.WriteLine("Invalid amount to withdraw.\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist.");
                return;
            }
        }
        public void transferAmount()
        {
            //transfers money from one accc to another
            double senderAcc;
            double receiverAcc;
            int amount;
            Console.WriteLine("Enter Sender's account number: ");
            senderAcc = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Receiver's account number: ");
            receiverAcc = Convert.ToDouble(Console.ReadLine());
            if (accounts.ContainsKey(senderAcc))//check whether both of the accounts exist.
            {
                if (accounts.ContainsKey(receiverAcc))
                {
                    Console.WriteLine("Enter amount to Transfer: ");
                    amount = Convert.ToInt32(Console.ReadLine());
                    if (amount > 0)
                    {
                        if (amount <= Convert.ToInt32(accounts[senderAcc]["balance"]))
                        {
                            //generate transaction details for both sender and receiver.
                            string receiverDetails = DateTime.Now + " " + amount + "INR" + " Transfered from " + accounts[senderAcc]["name"];
                            string senderDetails = DateTime.Now + " " + amount + "INR" + " Transfered to " + accounts[receiverAcc]["name"];
                                                                                                            
                            int senderBalance = Convert.ToInt32(accounts[senderAcc]["balance"]) - amount;   //calculate sender and receiver balances
                            int receiverBalance = Convert.ToInt32(accounts[receiverAcc]["balance"]) + amount;

                            accounts[senderAcc]["balance"] = Convert.ToString(senderBalance);            //update sender and receiver balances.
                            accounts[receiverAcc]["balance"] = Convert.ToString(receiverBalance);

                            DataReaderWriter.writeAccounts(accounts);

                            Console.WriteLine("Amount has been transferred to {0} successfully!", accounts[receiverAcc]["name"]);
                            Console.WriteLine("Current balance ->{0}\n\n", accounts[senderAcc]["balance"]);


                            //lgging the transaction for sender
                            if (!transactions.ContainsKey(senderAcc))
                            {
                                transactions.Add(senderAcc, senderDetails);
                            }
                            else
                            {
                                transactions[senderAcc] += "," + senderDetails;
                            }
                            //logging the transaction for receiver.
                            if (!transactions.ContainsKey(receiverAcc))
                            {
                                transactions.Add(receiverAcc, receiverDetails);
                            }
                            else
                            {
                                transactions[receiverAcc] += "," + receiverDetails;
                            }


                            DataReaderWriter.writeTransactions(transactions);


                        }
                        else
                        {
                            Console.WriteLine("Insufficient account balance.\n");
                        }



                    }
                    else
                    {
                        Console.WriteLine("Invalid receipient account number.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid sender account number.");
                }

            }
        }
        public void printTransactionHistory()
        {
            //prints user's acount history
            Console.WriteLine("Enter your account number: ");
            double accNumber = Convert.ToDouble(Console.ReadLine());
            if(accounts.ContainsKey(accNumber))
            {
                if(transactions.ContainsKey(accNumber))
                {
                    string trans = transactions[accNumber];
                    string[] transList = trans.Split(",");
                    int count = 1;
                    foreach(string transaction in transList)
                    {
                        Console.WriteLine("{0}\t{1}",count,transaction);
                        count++;
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("None transactions recorded so far!");
                }
            }
            else
            {
                Console.WriteLine("Account doesn't exist.");
                return;
            }

        }
    }
}
