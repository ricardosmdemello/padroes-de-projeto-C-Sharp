using DesignPatterns.Behavioral.Visitor;

Console.WriteLine("=== Padrão Visitor ===\n");

IShape[] shapes =
{
    new Circle(5),
    new Rectangle(4, 6)
};

var area = new AreaVisitor();
var perimeter = new PerimeterVisitor();

foreach (var shape in shapes)
{
    Console.WriteLine($"{shape.GetType().Name}: " +
                      $"área = {shape.Accept(area):F2}, " +
                      $"perímetro = {shape.Accept(perimeter):F2}");
}
