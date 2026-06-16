using DesignPatterns.Creational.Prototype;

Console.WriteLine("=== Padrão Prototype ===\n");

var original = new Circle
{
    Color = "vermelho",
    Radius = 10,
    Position = new Position { X = 5, Y = 5 }
};

var clone = (Circle)original.Clone();
clone.Color = "azul";
clone.Position.X = 99; // altera apenas o clone

Console.WriteLine($"Original: {original}");
Console.WriteLine($"Clone:    {clone}");
Console.WriteLine();
Console.WriteLine("A posição do original permaneceu intacta (cópia profunda): " +
                  $"{original.Position.X == 5}");
