using System.Reflection;

// Menu único: lista os 23 padrões GoF por categoria e executa o escolhido.
//
// Cada padrão é um projeto executável próprio; aqui ele é referenciado como
// biblioteca e seu demonstrativo (o ponto de entrada gerado pelo Program.cs) é
// invocado em processo via reflection. Assim cada padrão continua rodável
// individualmente com `dotnet run --project ...`, e também por este menu.

var patterns = new List<Pattern>
{
    // Criacionais
    new("Criacionais", "Singleton", "Singleton"),
    new("Criacionais", "Factory Method", "FactoryMethod"),
    new("Criacionais", "Abstract Factory", "AbstractFactory"),
    new("Criacionais", "Builder", "Builder"),
    new("Criacionais", "Prototype", "Prototype"),
    // Estruturais
    new("Estruturais", "Adapter", "Adapter"),
    new("Estruturais", "Bridge", "Bridge"),
    new("Estruturais", "Composite", "Composite"),
    new("Estruturais", "Decorator", "Decorator"),
    new("Estruturais", "Facade", "Facade"),
    new("Estruturais", "Flyweight", "Flyweight"),
    new("Estruturais", "Proxy", "Proxy"),
    // Comportamentais
    new("Comportamentais", "Chain of Responsibility", "ChainOfResponsibility"),
    new("Comportamentais", "Command", "Command"),
    new("Comportamentais", "Interpreter", "Interpreter"),
    new("Comportamentais", "Iterator", "Iterator"),
    new("Comportamentais", "Mediator", "Mediator"),
    new("Comportamentais", "Memento", "Memento"),
    new("Comportamentais", "Observer", "Observer"),
    new("Comportamentais", "State", "State"),
    new("Comportamentais", "Strategy", "Strategy"),
    new("Comportamentais", "Template Method", "TemplateMethod"),
    new("Comportamentais", "Visitor", "Visitor"),
};

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    ShowMenu(patterns);
    Console.Write("\nEscolha uma opção: ");
    var input = Console.ReadLine()?.Trim();

    if (input is null || input.Equals("0") ||
        input.Equals("s", StringComparison.OrdinalIgnoreCase) ||
        input.Equals("q", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("\nAté logo!");
        return;
    }

    if (input.Equals("a", StringComparison.OrdinalIgnoreCase))
    {
        for (var i = 0; i < patterns.Count; i++)
            RunPattern(patterns[i], i + 1, patterns.Count);
        Pause();
        continue;
    }

    if (int.TryParse(input, out var choice) && choice >= 1 && choice <= patterns.Count)
    {
        RunPattern(patterns[choice - 1]);
        Pause();
    }
    else
    {
        Console.WriteLine("Opção inválida. Tente novamente.");
        Pause();
    }
}

static void ShowMenu(List<Pattern> patterns)
{
    // Console.Clear() lança exceção quando a saída está redirecionada (pipe/CI).
    if (!Console.IsOutputRedirected)
    {
        try { Console.Clear(); } catch (IOException) { /* sem console interativo */ }
    }

    Console.WriteLine("╔════════════════════════════════════════════╗");
    Console.WriteLine("║       PADRÕES DE PROJETO (GoF) EM C#         ║");
    Console.WriteLine("╚════════════════════════════════════════════╝");

    string? currentCategory = null;
    for (var i = 0; i < patterns.Count; i++)
    {
        if (patterns[i].Category != currentCategory)
        {
            currentCategory = patterns[i].Category;
            Console.WriteLine($"\n  {currentCategory}");
        }
        Console.WriteLine($"    {i + 1,2}. {patterns[i].Name}");
    }

    Console.WriteLine("\n     A. Executar TODOS em sequência");
    Console.WriteLine("     0. Sair");
}

static void RunPattern(Pattern pattern, int? index = null, int? total = null)
{
    var counter = index is not null ? $" [{index}/{total}]" : string.Empty;
    Console.WriteLine($"\n┌─ Executando: {pattern.Name}{counter} " + new string('─', 20));
    Console.WriteLine();

    try
    {
        var dllPath = Path.Combine(AppContext.BaseDirectory, pattern.Dll + ".dll");
        var assembly = Assembly.LoadFrom(dllPath);
        var entryPoint = assembly.EntryPoint
            ?? throw new InvalidOperationException("Ponto de entrada não encontrado.");

        // O Main gerado pelo top-level statement recebe string[] args.
        var args = entryPoint.GetParameters().Length == 0
            ? null
            : new object?[] { Array.Empty<string>() };

        entryPoint.Invoke(null, args);
    }
    catch (Exception ex)
    {
        // TargetInvocationException encapsula a exceção real do demo.
        var real = ex.InnerException ?? ex;
        Console.WriteLine($"  [erro ao executar '{pattern.Name}': {real.Message}]");
    }

    Console.WriteLine("\n└" + new string('─', 45));
}

static void Pause()
{
    Console.Write("\nPressione ENTER para voltar ao menu...");
    Console.ReadLine();
}

/// <summary>Item do menu: categoria, nome de exibição e nome do assembly (.dll).</summary>
internal sealed record Pattern(string Category, string Name, string Dll);
