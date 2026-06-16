namespace DesignPatterns.Behavioral.Visitor;

/// <summary>
/// Padrão Visitor.
///
/// Permite adicionar novas operações a uma hierarquia de objetos sem alterar
/// as classes dos elementos. As operações ficam em "visitantes"; cada elemento
/// apenas implementa <c>Accept</c> e chama o método correspondente do visitante
/// (double dispatch).
/// </summary>
public interface IShapeVisitor
{
    double Visit(Circle circle);
    double Visit(Rectangle rectangle);
}

/// <summary>Elemento: aceita um visitante.</summary>
public interface IShape
{
    double Accept(IShapeVisitor visitor);
}

public sealed class Circle : IShape
{
    public double Radius { get; }
    public Circle(double radius) => Radius = radius;
    public double Accept(IShapeVisitor visitor) => visitor.Visit(this);
}

public sealed class Rectangle : IShape
{
    public double Width { get; }
    public double Height { get; }
    public Rectangle(double width, double height) => (Width, Height) = (width, height);
    public double Accept(IShapeVisitor visitor) => visitor.Visit(this);
}

/// <summary>Visitante concreto: calcula a área de cada forma.</summary>
public sealed class AreaVisitor : IShapeVisitor
{
    public double Visit(Circle circle) => Math.PI * circle.Radius * circle.Radius;
    public double Visit(Rectangle rectangle) => rectangle.Width * rectangle.Height;
}

/// <summary>Visitante concreto: calcula o perímetro de cada forma.</summary>
public sealed class PerimeterVisitor : IShapeVisitor
{
    public double Visit(Circle circle) => 2 * Math.PI * circle.Radius;
    public double Visit(Rectangle rectangle) => 2 * (rectangle.Width + rectangle.Height);
}
