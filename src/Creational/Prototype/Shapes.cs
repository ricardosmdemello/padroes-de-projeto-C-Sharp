namespace DesignPatterns.Creational.Prototype;

/// <summary>
/// Padrão Prototype.
///
/// Permite criar novos objetos copiando um protótipo existente, em vez de
/// instanciá-los do zero. É útil quando a construção é cara ou quando se quer
/// produzir variações a partir de um modelo já configurado.
///
/// O exemplo demonstra a diferença entre cópia rasa (shallow) e cópia profunda
/// (deep): a referência aninhada <see cref="Position"/> precisa ser clonada
/// para que os clones sejam realmente independentes.
/// </summary>
public abstract class Shape
{
    public string Color { get; set; } = "preto";
    public Position Position { get; set; } = new();

    /// <summary>Cria uma cópia profunda deste objeto.</summary>
    public abstract Shape Clone();

    public override string ToString() => $"{GetType().Name} {Color} em ({Position.X}, {Position.Y})";
}

/// <summary>Tipo de referência aninhado — exige clonagem explícita.</summary>
public sealed class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position Copy() => new() { X = X, Y = Y };
}

public sealed class Circle : Shape
{
    public int Radius { get; set; }

    public override Shape Clone() => new Circle
    {
        Color = Color,
        Radius = Radius,
        Position = Position.Copy() // cópia profunda da referência aninhada
    };

    public override string ToString() => $"{base.ToString()}, raio {Radius}";
}

public sealed class Rectangle : Shape
{
    public int Width { get; set; }
    public int Height { get; set; }

    public override Shape Clone() => new Rectangle
    {
        Color = Color,
        Width = Width,
        Height = Height,
        Position = Position.Copy()
    };

    public override string ToString() => $"{base.ToString()}, {Width}x{Height}";
}
