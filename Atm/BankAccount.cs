using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }
        public double balance { get; set; }


        public void Deposit(double amount)
        {
            if(amount >= 0)
            {
                if (amount < 1000)
                {
                    balance = balance + amount;
                }
                else
                {
                    throw new InvalidException("Amount is too large.");
                }
            }
            else
            {
                throw new InvalidException("Deposit amount can not be negative.");
            }

        }

        public void Withdraw(double amount)
        {
            if(amount >= 0)
            {
                if (balance >= amount)
                {
                    balance = balance - amount;
                }else
                {
                    throw new BalanceException("Insufficient Funds.");
                }
            }
            else 
            {
                throw new NegativeException("Withdraw amount can not be negative.");
            }          
        }
    }
}
