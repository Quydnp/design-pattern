namespace DesignPattern.Structural
{
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 10) ? "crawling" : "too young";
        }
    }

    public class Dragon 
    {
        private int _age;
        private Bird _bird;
        private Lizard _lizard;

        public Dragon()
        {
            _bird = new Bird();
            _lizard = new Lizard();
        }

        public int Age
        {
            get => _age;       
            set 
            {
                _age = value;
                _bird.Age = value;
                _lizard.Age = value;
            }
        }

        public string Fly()
        {
            return _bird.Fly();
        }

        public string Crawl()
        {
            return _lizard.Crawl();
        }
    }
    public class Decorator
    {
        /*static void Main(string[] args)
        {
            Dragon dragon = new Dragon()
            {
                Age = 11,
            };
            //dragon.Age = 5;
            Console.WriteLine(dragon.Fly());
            Console.WriteLine(dragon.Crawl());
        }*/
    }
}

// - Use: Adding behaviors to individual objects without inheriting from them.