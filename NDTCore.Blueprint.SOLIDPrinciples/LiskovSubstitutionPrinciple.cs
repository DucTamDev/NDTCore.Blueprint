namespace NDTCore.Blueprint.SOLIDPrinciples
{
    // Violation of Liskov Substitution Principle (LSP) example
    public class Rectangle
    {
        public virtual double Width { get; set; }
        public virtual double Height { get; set; }

        public double GetArea()
        {
            return Width * Height;
        }
    }

    public class Square : Rectangle
    {
        public override double Width
        {
            get { return base.Width; }
            set { base.Width = base.Height = value; }
        }

        public override double Height
        {
            get { return base.Height; }
            set { base.Width = base.Height = value; }
        }
    }


    // Refactored code to follow LSP
    public interface IShape
    {
        double GetArea();
    }

    public class RectangleLSP : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public double GetArea()
        {
            return Width * Height;
        }
    }

    public class SquareLSP : IShape
    {
        public double SideLength { get; set; }

        public double GetArea()
        {
            return SideLength * SideLength;
        }
    }
}
