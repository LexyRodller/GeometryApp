namespace GeometryApp.Models
{
    public abstract class Shape
    {
        public double X { get; set; }
        public double Y { get; set; }

        protected Shape(double x, double y)
        {
            X = x;
            Y = y;
        }

        public abstract (double x1, double y1, double x2, double y2) GetBoundingRectangle();
        public abstract double GetArea();
    }
}