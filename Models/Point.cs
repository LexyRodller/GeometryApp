namespace GeometryApp.Models
{
    public class Point : Shape
    {
        public Point(double x, double y) : base(x, y) { }

        public override (double x1, double y1, double x2, double y2) GetBoundingRectangle()
        {
            return (X, Y, X, Y);
        }

        public override double GetArea()
        {
            return 0;
        }
    }
}