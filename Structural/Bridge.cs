namespace DesignPattern.Structural1
{
    public class Bridge
    {
        /*static void Main(string[] args)
        {
            Square square = new Square(new RasterRenderer());
            Triangle triangle = new Triangle(new VectorRenderer());

            Console.WriteLine(square.ToString());
            Console.WriteLine(triangle.ToString());
        }*/
    }

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "pixels";
    }

    public abstract class Shape
    {
        protected IRenderer renderer;
        public string Name { get; set; } = string.Empty;

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }

        public override string ToString() => $"Drawing {Name} as {renderer.WhatToRenderAs}";
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }

        public override string ToString() => $"Drawing {Name} as {renderer.WhatToRenderAs}";
    }
}

// - Use: connecting components together through abstractions (interface/ abstract classes).