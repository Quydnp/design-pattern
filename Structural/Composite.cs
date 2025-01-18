using System.Collections;

namespace DesignPattern.Structural1
{
    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }

    public class Composite
    {
        /*static void Main(string[] args)
        {
            var value = new SingleValue { Value = 1 };
            var manyValues = new ManyValues() { 1, 2, 3};
            Console.WriteLine(value.Sum());
            Console.WriteLine(manyValues.Sum());
        }*/
    }
}

// - Use: treating individual objects and compositions of objects in a uniform manner.