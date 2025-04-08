using System;

namespace GeometryApp.Models
{
    public class Ellipse : Shape
    {
        public double RadiusX { get; set; }
        public double RadiusY { get; set; }

        public Ellipse(double x, double y, double radiusX, double radiusY) : base(x, y)
        {
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public override (double x1, double y1, double x2, double y2) GetBoundingRectangle()
        {
            return (X - RadiusX, Y - RadiusY, X + RadiusX, Y + RadiusY);
        }

        public override double GetArea()
        {
            return Math.PI * RadiusX * RadiusY;
        }
    }
}