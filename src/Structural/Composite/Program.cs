using DesignPatterns.Structural.Composite;

Console.WriteLine("=== Padrão Composite ===\n");

var root = new DirectoryComposite("projeto")
    .Add(new FileLeaf("README.md", 4))
    .Add(new DirectoryComposite("src")
        .Add(new FileLeaf("Program.cs", 8))
        .Add(new FileLeaf("Utils.cs", 12)))
    .Add(new DirectoryComposite("assets")
        .Add(new FileLeaf("logo.png", 250)));

root.Print();

Console.WriteLine($"\nTamanho total do projeto: {root.GetSize()} KB");
