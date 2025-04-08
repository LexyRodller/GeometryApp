using System;
using System.Collections.Generic;

namespace GeometryApp.Models;

    public class Polygon : Shape
    {
        public List<(double X, double Y)> Vertices { get; }

        public Polygon(List<(double X, double Y)> vertices) : base(vertices[0].X, vertices[0].Y)
        {
            Vertices = vertices;
        }

        public override (double x1, double y1, double x2, double y2) GetBoundingRectangle()
        {
            double minX = double.MaxValue, minY = double.MaxValue, maxX = double.MinValue, maxY = double.MinValue;
            foreach (var v in Vertices)
            {
                minX = Math.Min(minX, v.X);
                minY = Math.Min(minY, v.Y);
                maxX = Math.Max(maxX, v.X);
                maxY = Math.Max(maxY, v.Y);
            }
            return (minX, minY, maxX, maxY);
        }

        public override double GetArea()
        {
            double area = 0;
            int n = Vertices.Count;
            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                area += (Vertices[i].X * Vertices[j].Y) - (Vertices[j].X * Vertices[i].Y);
            }
            return Math.Abs(area / 2.0);
        }

}