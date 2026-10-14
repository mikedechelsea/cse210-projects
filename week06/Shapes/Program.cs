using System;
using System.Collections.Generic;

class Shape
{
    private string _color;

    public Shape(string color)
    {
        _color = color;
    }

    public string GetColor() => _color;
    public void SetColor(string color) => _color = color;

    public virtual double GetArea() => 0;
}

class Square : Shape
{
    private double _side;

    public Square(string color, double side) : base(color)
    {
        _side = side;
    }

    public override double GetArea() => _side * _side;
}

class Rectangle : Shape
{
    private double _length;
    private double _width;

    public Rectangle(string color, double length, double width) : base(color)
    {
        _length = length;
        _width = width;
    }

    public override double GetArea() => _length * _width;
}

class Circle : Shape
{
    private double _radius;

    public Circle(string color, double radius) : base(color)
    {
        _radius = radius;
    }

    public override double GetArea() => Math.PI * _radius * _radius;
}

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>
        {
            new Square("red", 5),
            new Rectangle("blue", 4, 7),
            new Circle("green", 3),
            new Square("yellow", 2.5),
            new Circle("purple", 6)
        };

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor(),-10} Area: {shape.GetArea():F2}");
        }
    }
}
