namespace DesignPattern.Creational
{
    public sealed class Singleton
    {
        // Static variable to hold the single instance
        private static Lazy<Singleton>? _instance;

        // Lock object to ensure thread-safety
        private static readonly object _lock = new object();

        // Private constructor to prevent instantiation from outside the class
        private Singleton()
        {
            Console.WriteLine("Singleton instance is created.");
        }

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock) // Ensure only one thread creates the instance
                    {
                        if (_instance == null)
                        {
                            _instance = new Lazy<Singleton>();
                        }
                    }
                }
                return _instance.Value;
            }
        }
        public void ShowMessage(string message)
        {
            Console.WriteLine($"Message from Singleton: {message}");
        }

        public object SingletonMethod()
        {
            return Instance;
        }
    }

    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var obj1 = func();
            var obj2 = func();
            return obj1.Equals(obj2);
        }
    }

    class Program
    {
        /*static void Main(string[] args)
        {
            var singleton1 = Singleton.Instance;
            singleton1.ShowMessage("Hello from the first call!");

            var singleton2 = Singleton.Instance;
            singleton2.ShowMessage("Hello from the second call!");

            Console.WriteLine($"Are the two instances equal? {ReferenceEquals(singleton1, singleton2)}");
            Console.WriteLine($"Is this an singleton method? {SingletonTester.IsSingleton(singleton1.SingletonMethod)} ");
        }*/
    }
}

// - Use: to ensure that we have only one instance for a class.
