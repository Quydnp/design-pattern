namespace DesignPattern.Structural
{
    public class Person
    {
        public int Age { get; set; }

        public Person(int age)
        {
            Age = age;
        }
        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {

            return "driving while drunk";
        }
    }

    public class ResponsiblePerson
    {
        private Person _person;

        public ResponsiblePerson(Person person)
        {
            _person = person;
        }

        public int Age
        {
            get => _person.Age;
            set
            {
                _person.Age = value;
            }
        }

        public string Drink()
        {
            return (Age < 18) ? "too young" : _person.Drink();
        }

        public string Drive()
        {

            return (Age < 16) ? "too young" : _person.Drive();
        }

        public string DrinkAndDrive()
        {

            return "dead";
        }

        public string DrinkAndDrive2()
        {
            return (Age < 16) ? "too young" : _person.DrinkAndDrive();
        }
    }
    public class Proxy
    {
        /*public static void Main()
        {
            var person = new Person(15);
            var responsiblePerson = new ResponsiblePerson(person);
            Console.WriteLine(responsiblePerson.Drink());
            Console.WriteLine(responsiblePerson.Drive());
            Console.WriteLine(responsiblePerson.DrinkAndDrive());
        }*/
    }
}

// - Use: The Proxy pattern is used to provide a surrogate or placeholder object, which references an underlying object.
// The proxy object controls access to the underlying object, allowing you to perform something
// either before or after the request gets through to the underlying object.

