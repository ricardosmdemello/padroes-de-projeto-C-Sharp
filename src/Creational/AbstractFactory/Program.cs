using DesignPatterns.Creational.AbstractFactory;

Console.WriteLine("=== Padrão Abstract Factory ===\n");

// O sistema operacional decide qual família de componentes usar.
IGuiFactory factory = OperatingSystem.IsWindows()
    ? new WindowsFactory()
    : new MacFactory();

Console.WriteLine("Renderizando a UI com a fábrica do SO atual:");
new Application(factory).RenderUi();

Console.WriteLine("\nForçando a família macOS:");
new Application(new MacFactory()).RenderUi();

Console.WriteLine("\nForçando a família Windows:");
new Application(new WindowsFactory()).RenderUi();
