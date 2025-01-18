using System.Xml.Serialization;

namespace DesignPattern.Creational
{
    public static class ExtensionMetheds
    {
        public static T? DeepCopyXml<T>(this T self)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T?)s.Deserialize(ms);
            }
        }
    }
    public class Point
    {
        public int X, Y;

        public Point() { }

        public override string? ToString()
        {
            return $"X: {X} - Y: {Y}".ToString();
        }
    }

    public class Line
    {
        public Point? Start, End;

        public Line() { }

        public Line? DeepCopy()
        {
            return ExtensionMetheds.DeepCopyXml(this);
        }

        public override string? ToString()
        {
            return $"Start: {Start} - End: {End}";
        }
    }
    public class Prototype
    {
        /*public static void Main(string[] args)
        {
            var line = new Line();
            line.Start = new Point() { X = 2, Y = 3 };
            line.End = new Point() { X = 5, Y = -6};

            var deepCopy = line.DeepCopy();
            deepCopy.Start.X = 100;
            deepCopy.End.Y = 100;
            Console.WriteLine(line);
            Console.WriteLine(deepCopy);
        }*/
    }
}

// - Use: to deal with clone object problem
// - Shallow copy vs Deep copy
// + Shallow copy: copy reference of reference attributes, changed new <=> change old
// + Deep copy: fully extinct copy, not affect old one.
// - Many solution to deep copy an object:
// + Copy Constructor
// + Serialization 
// + Reuse ICloneable interface
// + Build interface/extension methods to deep copy