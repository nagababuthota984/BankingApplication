using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApplication
{
    class Transfer
    {
        double senderAcc;
        double receiverAcc;
        int amount;
        public Transfer()
        {
            amount = 0;
        }
        public void transferAmount()
        {
            //transfers money from one accc to another
            
            Console.WriteLine("Enter Sender's account number: ");
            senderAcc = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Receiver's account number: ");
            receiverAcc = Convert.ToDouble(Console.ReadLine());
            if (Account.accounts.ContainsKey(senderAcc))//check whether both of the accounts exist.
            {
                if (Account.accounts.ContainsKey(receiverAcc))
                {
                    Console.WriteLine("Enter amount to Transfer: ");
                    amount = Convert.ToInt32(Console.ReadLine());
                    if (amount > 0)
                    {
                        if (amount <= Convert.ToInt32(Account.accounts[senderAcc]["balance"]))
                        {
                            //generate transaction details for both sender and receiver.
                            string receiverDetails = DateTime.Now + " " + amount + "INR" + " Transfered from " + Account.accounts[senderAcc]["name"];
                            string senderDetails = DateTime.Now + " " + amount + "INR" + " Transfered to " + Account.accounts[receiverAcc]["name"];

                            int senderBalance = Convert.ToInt32(Account.accounts[senderAcc]["balance"]) - amount;   //calculate sender and receiver balances
                            int receiverBalance = Convert.ToInt32(Account.accounts[receiverAcc]["balance"]) + amount;

                            Account.accounts[senderAcc]["balance"] = Convert.ToString(senderBalance);            //update sender and receiver balances.
                            Account.accounts[receiverAcc]["balance"] = Convert.ToString(receiverBalance);

                            DataReaderWriter.writeAccounts(Account.accounts);

                            Console.WriteLine("Amount has been transferred to {0} successfully!", Account.accounts[receiverAcc]["name"]);
                            Console.WriteLine("Current balance ->{0}\n\n", Account.accounts[senderAcc]["balance"]);


                            //lgging the transaction for sender
                            if (!Account.transactions.ContainsKey(senderAcc))
                            {
                                Account.transactions.Add(senderAcc, senderDetails);
                            }
                            else
                            {
                                Account.transactions[senderAcc] += "," + senderDetails;
                            }
                            //logging the transaction for receiver.
                            if (!Account.transactions.ContainsKey(receiverAcc))
                            {
                                Account.transactions.Add(receiverAcc, receiverDetails);
                            }
                            else
                            {
                                Account.transactions[receiverAcc] += "," + receiverDetails;
                            }


                            DataReaderWriter.writeTransactions(Account.transactions);


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
    }
}
