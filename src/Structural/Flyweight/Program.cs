using DesignPatterns.Structural.Flyweight;

Console.WriteLine("=== Padrão Flyweight ===\n");

var factory = new TreeTypeFactory();
var forest = new List<Tree>();

// Plantamos 1000 árvores, mas só de 2 tipos distintos.
for (int i = 0; i < 1000; i++)
{
    var type = i % 2 == 0
        ? factory.GetTreeType("Carvalho", "verde", "textura-carvalho")
        : factory.GetTreeType("Pinheiro", "verde-escuro", "textura-pinheiro");

    forest.Add(new Tree(i, i * 2, type));
}

Console.WriteLine();
forest[0].Draw();
forest[1].Draw();

Console.WriteLine();
Console.WriteLine($"Árvores na floresta: {forest.Count}");
Console.WriteLine($"Objetos de tipo (flyweights) criados: {factory.CreatedCount}");
