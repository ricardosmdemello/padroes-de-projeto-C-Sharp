using DesignPatterns.Creational.Builder;

Console.WriteLine("=== Padrão Builder ===\n");

// Construção customizada, passo a passo, com API fluente.
var custom = new PizzaBuilder()
    .WithSize("grande")
    .WithDough("integral")
    .AddTopping("frango")
    .AddTopping("catupiry")
    .AddTopping("milho")
    .Build();

Console.WriteLine("Pizza personalizada:");
Console.WriteLine(custom);

// Construção via Director (receita pronta).
Console.WriteLine("\nPizza pelo Director (Margherita):");
Console.WriteLine(new PizzaDirector().MakeMargherita());
