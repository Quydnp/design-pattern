namespace DesignPattern.Creational
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Person(int id, string name)
        {
            this.Name = name;
            this.Id = id;
        }

        public override string? ToString()
        {
            return $"Id: {this.Id} - Name: {this.Name}";
        }
    }

    public class PersonFactory
    {
        List<Person> lst = new List<Person>();

        public Person CreatePerson(string name)
        {
            var id = lst.Count == 0 ? 0 : lst[lst.Count - 1].Id + 1;
            var newPerson = new Person(id, name);
            lst.Add(newPerson);
            return newPerson;
        }
    }
    public class Factory
    {
        /*public static void Main(string[] args)
        {
            var pf = new PersonFactory();
            var p1 = pf.CreatePerson("quy");
            var p2 = pf.CreatePerson("an");
            Console.WriteLine(p1);
            Console.WriteLine(p2);
        }*/
    }
}
