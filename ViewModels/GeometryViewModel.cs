using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using GeometryApp.Models;
using ReactiveUI;

namespace GeometryApp.ViewModels
{
    public class GeometryViewModel : ReactiveObject
    {
        public ObservableCollection<string> ShapeTypes { get; } = new()
        {
            "Ellipse", "Line", "Point", "Polygon"
        };

        private int _selectedShapeIndex;
        public int SelectedShapeIndex
        {
            get => _selectedShapeIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedShapeIndex, value);
                UpdateVisibility();
            }
        }

        public double EllipseCenterX { get; set; }
        public double EllipseCenterY { get; set; }
        public double EllipseRadiusX { get; set; }
        public double EllipseRadiusY { get; set; }

        public double LineStartX { get; set; }
        public double LineStartY { get; set; }
        public double LineEndX { get; set; }
        public double LineEndY { get; set; }

        public double PointX { get; set; }
        public double PointY { get; set; }

        public string? PolygonVertices { get; set; }

        private string _shapeInfo = string.Empty;
        public string ShapeInfo
        {
            get => _shapeInfo;
            set => this.RaiseAndSetIfChanged(ref _shapeInfo, value);
        }

        public ReactiveCommand<Unit, Unit> ShowShapeInfoCommand { get; }

        public bool IsEllipseSelected => SelectedShapeIndex == 0;
        public bool IsLineSelected => SelectedShapeIndex == 1;
        public bool IsPointSelected => SelectedShapeIndex == 2;
        public bool IsPolygonSelected => SelectedShapeIndex == 3;

        private int _vertexCount;
        public int VertexCount
        {
            get => _vertexCount;
            set => this.RaiseAndSetIfChanged(ref _vertexCount, value);
        }

        public ObservableCollection<VertexInput> VertexInputs { get; } = new();
    
        public bool HasVertexInputs => VertexInputs.Count > 0;

        public ReactiveCommand<Unit, Unit> GenerateVertexFieldsCommand { get; }
        public ReactiveCommand<Unit, Unit> SetPolygonVerticesCommand { get; }
        public GeometryViewModel()
        {
            ShowShapeInfoCommand = ReactiveCommand.Create(ShowShapeInfo);
            GenerateVertexFieldsCommand = ReactiveCommand.Create(GenerateVertexFields);
            SetPolygonVerticesCommand = ReactiveCommand.Create(SetPolygonVertices);
        }

        private void GenerateVertexFields()
        {
            VertexInputs.Clear();
        
            for (int i = 0; i < VertexCount; i++)
            {
                VertexInputs.Add(new VertexInput());
            }
        
            this.RaisePropertyChanged(nameof(HasVertexInputs));
        }

        private void SetPolygonVertices()
        {
            var vertices = string.Join(";", VertexInputs.Select(v => $"{v.X},{v.Y}"));
            PolygonVertices = vertices;
        }
        
        public class VertexInput : ReactiveObject
        {
            private double _x;
            public double X
            {
                get => _x;
                set => this.RaiseAndSetIfChanged(ref _x, value);
            }

            private double _y;
            public double Y
            {
                get => _y;
                set => this.RaiseAndSetIfChanged(ref _y, value);
            }
        }

        private void ShowShapeInfo()
        {
            Shape shape = SelectedShapeIndex switch
            {
                0 => new Ellipse(EllipseCenterX, EllipseCenterY, EllipseRadiusX, EllipseRadiusY),
                1 => new Line(LineStartX, LineStartY, LineEndX, LineEndY),
                2 => new Point(PointX, PointY),
                3 => new Polygon(ParsePolygonVertices(PolygonVertices)),
                _ => throw new ArgumentOutOfRangeException(nameof(SelectedShapeIndex), "Неизвестный тип фигуры")
            };

            var boundaryArea = shape.GetBoundingRectangle();
            ShapeInfo = $"Type: {shape.GetType().Name}\n" +
                $"Area: {shape.GetArea()}\n" +
                $"Boundaru area: ({boundaryArea.x1}, {boundaryArea.y1}) to ({boundaryArea.x2}, {boundaryArea.y2})";
        }


        private List<(double X, double Y)> ParsePolygonVertices(string? input)
        {
            if (input == null) return new List<(double, double)>();
            return input.Split(';')
                        .Select(pair => pair.Split(','))
                        .Where(parts => parts.Length == 2 && double.TryParse(parts[0], out _) && double.TryParse(parts[1], out _))
                        .Select(parts => (double.Parse(parts[0]), double.Parse(parts[1])))
                        .ToList();
        }

        private void UpdateVisibility()
        {
            
            this.RaisePropertyChanged(nameof(IsEllipseSelected));
            this.RaisePropertyChanged(nameof(IsLineSelected));
            this.RaisePropertyChanged(nameof(IsPointSelected));
            this.RaisePropertyChanged(nameof(IsPolygonSelected));
        }
    }
}