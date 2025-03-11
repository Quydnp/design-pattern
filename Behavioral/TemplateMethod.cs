using System;

namespace DesignPattern.Behavioral
{
    public class BankAccount
    {
        public string Name { get; set; } = string.Empty;
        private int _balance { get; set; }

        public BankAccount(string name, int balance)
        {
            Name = name;
            _balance = balance;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
        }

        public void Withdraw(int amount)
        {
            _balance -= amount;
        }

        public int GetBalance()
        {
            return _balance;
        }
    }

    public abstract class BankTransaction
    {
        protected BankAccount _account;
        public BankTransaction(BankAccount account)
        {
            _account = account;
        }
        public void Execute()
        {
            CheckBalance();
            if (SendConfirmation())
            {
                Process();
                LogTransaction();
                DisplayBalance();
            }
            else Console.WriteLine("Transaction cancelled.");
        }
        private void CheckBalance()
        {
            Console.WriteLine("Checking balance...");
            Console.WriteLine($"Current balance: {_account.GetBalance()}$");
        }
        protected abstract void Process();
        private bool SendConfirmation()
        {
            Console.WriteLine("Do you want to process transaction?");
            char response = Console.ReadKey().KeyChar;
            Console.WriteLine();
            bool isEqual = char.ToLowerInvariant(response) == char.ToLowerInvariant('y');
            if (isEqual)
            {
                return true;
            }
            return false;
        }
        protected abstract void LogTransaction();
        private void DisplayBalance()
        {
            Console.WriteLine($"💰 Current balance: {_account.GetBalance()}$");
        }
    }

    public class DepositTransaction : BankTransaction
    {
        private int _amount;
        public DepositTransaction(BankAccount account, int amount) : base(account)
        {
            _amount = amount;
        }
        protected override void Process()
        {
            Console.WriteLine($"Depositing {_amount}...");
            _account.Deposit(_amount);
        }
        protected override void LogTransaction()
        {
            Console.WriteLine($"Log: Deposit {_amount}");
        }
    }

    
    public class WithdrawTransaction : BankTransaction
    {
        private int _amount;
        public WithdrawTransaction(BankAccount account, int amount) : base(account)
        {
            _amount = amount;
        }
        protected override void Process()
        {
            if (_account.GetBalance() < _amount)
            {
                Console.WriteLine($"🚫 Transaction failed: Your balance is not enough to withdraw {_amount}$.");
                return;
            }
            Console.WriteLine($"Withdrawing {_amount}...");
            _account.Withdraw(_amount);
        }
        protected override void LogTransaction()
        {
            Console.WriteLine($"Log: Withdraw {_amount}");
        }
    }

    public class TransferTransaction : BankTransaction
    {
        private int _amount;
        private BankAccount _destinationAccount;
        public TransferTransaction(BankAccount account, BankAccount destinationAccount, int amount) : base(account)
        {
            _amount = amount;
            _destinationAccount = destinationAccount;
        }

        protected override void Process()
        {
            if (_account.GetBalance() < _amount)
            {
                Console.WriteLine($"🚫 Transaction failed: Your balance is not enough to transfer {_amount}$.");
                return;
            }
            Console.WriteLine($"Transfering {_amount}...");
            _account.Withdraw(_amount);
            _destinationAccount.Deposit(_amount);
        }
        protected override void LogTransaction()
        {
            Console.WriteLine($"Log: Transfer {_amount} to {_destinationAccount.Name}");
        }
    }

    public class BillPaymentTransaction : BankTransaction
    {
        private int _amount;
        private int _transactionFee;
        public BillPaymentTransaction(BankAccount account, int amount, int transactionFee) : base(account)
        {
            _amount = amount;
            _transactionFee = transactionFee;
        }
        protected override void Process()
        {
            if (_account.GetBalance() < _amount + _transactionFee)
            {
                Console.WriteLine($"🚫 Transaction failed: Your balance is not enough to pay the bill of {_amount}$.");
                return;
            }
            Console.WriteLine($"Paying bill of {_amount}...");
            _account.Withdraw(_amount + _transactionFee);
        }

        protected override void LogTransaction()
        {
            Console.WriteLine($"Log: Pay the bill of {_amount} and transaction fee {_transactionFee}");
        }
    }

    public class TemplateMethod
    {
        /*public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var account = new BankAccount("John Doe", 1000);
            var depositTransaction = new DepositTransaction(account, 500);
            depositTransaction.Execute();
            var withdrawTransaction = new WithdrawTransaction(account, 200);
            withdrawTransaction.Execute();
            var destinationAccount = new BankAccount("Jane Doe", 500);
            var transferTransaction = new TransferTransaction(account, destinationAccount, 300);
            transferTransaction.Execute();
            var billPaymentTransaction = new BillPaymentTransaction(account, 100, 10);
            billPaymentTransaction.Execute();
        }*/
    }
}
