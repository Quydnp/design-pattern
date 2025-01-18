namespace DesignPattern.Behavioral
{
    // Subject interface
    public interface IStockMarket
    {
        void Attach(IStockObserver observer);
        void Detach(IStockObserver observer);
        void Notify();
    }

    // Observer interface
    public interface IStockObserver
    {
        void Update(decimal price);
    }

    // Concrete Subject
    public class StockMarket : IStockMarket
    {
        private List<IStockObserver> _observers = new List<IStockObserver>();
        private decimal _price;

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                Notify();
            }
        }

        public void Attach(IStockObserver observer)
        {
            if (!_observers.Contains(observer)) // Prevent duplicate observers
            {
                _observers.Add(observer);
                Console.WriteLine($"{observer.GetType().Name} attached.");
            }
            else
            {
                Console.WriteLine($"{observer.GetType().Name} is already attached.");
            }
        }

        public void Detach(IStockObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine($"{observer.GetType().Name} detached.");
            }
            else
            {
                Console.WriteLine($"{observer.GetType().Name} was not attached.");
            }
        }

        public void Notify()
        {
            if (_observers.Count == 0)
            {
                Console.WriteLine("No observers to notify.");
                return;
            }

            foreach (var observer in _observers)
            {
                observer.Update(_price);
            }
        }
    }

    // Concrete Observer
    public class MobileAppDisplay : IStockObserver
    {
        public void Update(decimal price)
        {
            Console.WriteLine($"Mobile App: Stock Price is {price}");
        }
    }

    // Concrete Observer
    public class DesktopAppDisplay : IStockObserver
    {
        public void Update(decimal price)
        {
            Console.WriteLine($"Desktop App: Stock Price is {price}");

        }
    }

    #region Observer Pattern using Events Handler
    // Publisher: Stock
    public class Stock
    {
        private decimal _price;

        // Define an event using EventHandler
        public event EventHandler<PriceChangedEventArgs> PriceChanged;

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPriceChanged(new PriceChangedEventArgs(_price));
                }
            }
        }

        // Method to raise the event
        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e); // Notify all subscribers
        }
    }

    // Custom EventArgs to pass data to observers
    public class PriceChangedEventArgs : EventArgs
    {
        public decimal NewPrice { get; }

        public PriceChangedEventArgs(decimal newPrice)
        {
            NewPrice = newPrice;
        }
    }

    // Observer: MobileApp
    public class MobileApp
    {
        public void Subscribe(Stock stock)
        {
            stock.PriceChanged += OnPriceChanged;
        }

        public void Unsubscribe(Stock stock)
        {
            stock.PriceChanged -= OnPriceChanged;
        }

        private void OnPriceChanged(object sender, PriceChangedEventArgs e)
        {
            Console.WriteLine($"MobileApp: Stock price updated to {e.NewPrice:C}");
        }
    }

    // Observer: DesktopApp
    public class DesktopApp
    {
        public void Subscribe(Stock stock)
        {
            stock.PriceChanged += OnPriceChanged;
        }

        public void Unsubscribe(Stock stock)
        {
            stock.PriceChanged -= OnPriceChanged;
        }

        private void OnPriceChanged(object sender, PriceChangedEventArgs e)
        {
            Console.WriteLine($"DesktopApp: Stock price updated to {e.NewPrice:C}");
        }
    }
    #endregion

    public class Observer
    {
        /*static void Main(string[] args)
        {
            // Normal
            var stockMarket = new StockMarket();
            var mobileAppDisplay = new MobileAppDisplay();
            var desktopAppDisplay = new DesktopAppDisplay();

            stockMarket.Attach(mobileAppDisplay);
            stockMarket.Attach(desktopAppDisplay);

            stockMarket.Price = 100;
            stockMarket.Price = 200;

            stockMarket.Detach(mobileAppDisplay);

            stockMarket.Price = 300;

            // Using event handler
            Console.WriteLine("\nUsing Event Handler");
            var stock = new Stock();
            var mobileApp = new MobileApp();
            var desktopApp = new DesktopApp();

            // Subscribing to the Stock's PriceChanged event
            mobileApp.Subscribe(stock);
            desktopApp.Subscribe(stock);

            // Simulate price changes
            stock.Price = 100;
            stock.Price = 200;

            // Unsubscribe MobileApp
            Console.WriteLine("\nUnsubscribing MobileApp...\n");
            mobileApp.Unsubscribe(stock);

            stock.Price = 300;
        }*/

    }
}
