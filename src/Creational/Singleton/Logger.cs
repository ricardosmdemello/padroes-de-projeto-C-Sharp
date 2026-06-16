namespace DesignPatterns.Creational.Singleton;

/// <summary>
/// Padrão Singleton.
///
/// Garante que uma classe tenha apenas uma única instância em toda a aplicação
/// e fornece um ponto global de acesso a ela.
///
/// Esta implementação usa <see cref="Lazy{T}"/>, que é a forma idiomática e
/// thread-safe de implementar o Singleton em C#: a instância só é criada no
/// primeiro acesso e o próprio runtime garante a segurança em ambiente
/// concorrente.
/// </summary>
public sealed class Logger
{
    // O construtor privado impede que a classe seja instanciada de fora.
    private readonly List<string> _entries = new();

    private Logger()
    {
        // Simula uma inicialização "cara" (abrir arquivo, conexão, etc.).
        Log("Logger inicializado.");
    }

    // Lazy<T> assegura criação única, sob demanda e thread-safe.
    private static readonly Lazy<Logger> _instance =
        new(() => new Logger(), LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>Ponto único e global de acesso à instância.</summary>
    public static Logger Instance => _instance.Value;

    /// <summary>Registra uma mensagem com data/hora.</summary>
    public void Log(string message)
    {
        var entry = $"[{DateTime.Now:HH:mm:ss}] {message}";
        _entries.Add(entry);
        Console.WriteLine(entry);
    }

    /// <summary>Quantidade total de mensagens já registradas.</summary>
    public int Count => _entries.Count;
}
