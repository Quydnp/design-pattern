namespace DesignPattern.Structural
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public SquareToRectangleAdapter(Square square)
        {
            this._square = square;
        }

        private Square _square;
        public int Width => _square.Side;

        public int Height => _square.Side;

    }
    public class Adapter
    {
        /*static void Main(string[] args)
        {
            Square square = new Square()
            {
                Side = 9,
            };
            Console.WriteLine(ExtensionMethods.Area(new SquareToRectangleAdapter(square)));
            Console.WriteLine(new SquareToRectangleAdapter(square).Area());
        }*/
    }
}

// - Use: getting the interface you want from the interface you have.
