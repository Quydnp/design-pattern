namespace DesignPattern.Behavioral
{
    // Interface for all states
    public interface IState
    {
        void InsertCoin();
        void EjectCoin();
        void Dispense();
    }

    // Concrete state: No coin inserted
    public class NoCoinState : IState
    {
        private VendingMachine _machine;

        public NoCoinState(VendingMachine machine)
        {
            _machine = machine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("💰 Coin inserted.");
            _machine.SetState(_machine.HasCoinState);
            _machine.SetHasCoin(true);
        }

        public void EjectCoin()
        {
            Console.WriteLine("🚫 No coint to return.");
        }

        public void Dispense()
        {
            Console.WriteLine("🚫 Insert coin before buying.");
        }
    }

    // Concrete state: Coin inserted
    public class HasCoinState : IState
    {
        private VendingMachine _machine;

        public HasCoinState(VendingMachine machine)
        {
            _machine = machine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("🚫 Coin inserted. You cannot insert more");
        }

        public void EjectCoin()
        {
            Console.WriteLine("💵 Coin has been returned.");
            _machine.SetState(_machine.NoCoinState);
            _machine.SetHasCoin(false);
        }

        public void Dispense()
        {
            Console.WriteLine("🥤 Processing...");
            _machine.ReleaseProduct();
            _machine.SetHasCoin(false);
            if (_machine.Stock > 0)
            {
                _machine.SetState(_machine.NoCoinState);
            }
            else
            {
                _machine.SetState(_machine.SoldOutState);
            }
        }
    }

    // Concrete state: Sold out
    public class SoldOutState : IState
    {
        private VendingMachine _machine;

        public SoldOutState(VendingMachine machine)
        {
            _machine = machine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("🚫 Sold out. Cannot receive coin.");
        }

        public void EjectCoin()
        {
            if (_machine.HasCoin)
            {
                Console.WriteLine("💵 Coin returned.");
                _machine.SetHasCoin(false);
            }
            else
                Console.WriteLine("🚫 No coin to return");
        }

        public void Dispense()
        {
            if (_machine.HasCoin)
            {
                Console.WriteLine("🚫 Sold out. Cannot release product. Coin returned.");
                _machine.SetHasCoin(false);
            }
            else
                Console.WriteLine("🚫 Sold out. Cannot release product.");
        }
    }

    // Concrete state: Mainternance
    public class MaintenanceState : IState
    {
        private VendingMachine _machine;
        public MaintenanceState(VendingMachine machine)
        {
            _machine = machine;
        }
        public void InsertCoin()
        {
            Console.WriteLine("🚫 Machine is under maintenance.");
        }
        public void EjectCoin()
        {
            Console.WriteLine("🚫 Machine is under maintenance.");
        }
        public void Dispense()
        {
            Console.WriteLine("🚫 Machine is under maintenance.");
        }
    }

    // Context: Vending machine
    public class VendingMachine
    {
        public IState NoCoinState { get; private set; }
        public IState HasCoinState { get; private set; }
        public IState SoldOutState { get; private set; }
        public IState MaintenanceState { get; private set; }

        private IState _currentState;
        public int Stock { get; private set; }
        public bool HasCoin { get; private set; }

        public VendingMachine(int stock)
        {
            NoCoinState = new NoCoinState(this);
            HasCoinState = new HasCoinState(this);
            SoldOutState = new SoldOutState(this);
            MaintenanceState = new MaintenanceState(this);

            Stock = stock;
            _currentState = Stock > 0 ? NoCoinState : SoldOutState;
        }

        public void SetState(IState state)
        {
            _currentState = state;
        }
        public void SetHasCoin(bool value)
        {
            HasCoin = value;
        }


        public void InsertCoin() => _currentState.InsertCoin();
        public void EjectCoin() => _currentState.EjectCoin();
        public void PressButton() => _currentState.Dispense();
        public void StartMaintenance()
        {
            Console.WriteLine("⚠️ Machine is now in maintenance mode.");
            SetState(MaintenanceState);
        }

        public void FinishMaintenance()
        {
            Console.WriteLine("✅ Maintenance completed. Machine is back in service.");
            if (Stock > 0)
                SetState(NoCoinState);
            else
                SetState(SoldOutState);
        }

        public void ReleaseProduct()
        {
            if (Stock > 0)
            {
                Stock--;
                Console.WriteLine("✅ Product released. Product left: " + Stock);
            }
        }

        public void Restock(int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("🚫 Invalid restock quantity. Must be greater than zero.");
                return;
            }

            if (_currentState == MaintenanceState)
            {
                Console.WriteLine("⚠️ Machine is under maintenance. Finish maintenance before restocking.");
                return;
            }

            Stock += quantity;

            if (_currentState == SoldOutState)
                SetState(NoCoinState);

            Console.WriteLine("✅ Restocked. Product left: " + Stock);
        }

    }

    public class State
    {
        /*public static void Main()
        {
            // encoding utf8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var vendingMachine = new VendingMachine(3);
            vendingMachine.InsertCoin();
            vendingMachine.PressButton();
            vendingMachine.InsertCoin();
            vendingMachine.InsertCoin();
            vendingMachine.PressButton();
            vendingMachine.PressButton();
            vendingMachine.PressButton();
            vendingMachine.PressButton();
        }*/
    }
}
