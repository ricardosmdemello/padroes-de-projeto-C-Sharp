namespace DesignPatterns.Structural.Flyweight;

/// <summary>
/// Flyweight: contém o estado INTRÍNSECO, isto é, os dados pesados que podem
/// ser compartilhados entre muitos objetos (textura, cor, nome da espécie).
/// </summary>
public sealed class TreeType
{
    public string Name { get; }
    public string Color { get; }
    public string Texture { get; }

    public TreeType(string name, string color, string texture)
    {
        Name = name;
        Color = color;
        Texture = texture;
    }

    public void Draw(int x, int y) =>
        Console.WriteLine($"Desenhando {Name} ({Color}) em ({x}, {y}).");
}

/// <summary>
/// Padrão Flyweight (fábrica).
///
/// Reduz o uso de memória compartilhando o máximo de estado entre objetos
/// semelhantes. A fábrica garante que cada combinação de estado intrínseco
/// exista apenas uma vez e seja reutilizada.
/// </summary>
public sealed class TreeTypeFactory
{
    private readonly Dictionary<string, TreeType> _cache = new();

    public TreeType GetTreeType(string name, string color, string texture)
    {
        var key = $"{name}|{color}|{texture}";
        if (!_cache.TryGetValue(key, out var type))
        {
            type = new TreeType(name, color, texture);
            _cache[key] = type;
            Console.WriteLine($"[criado] novo flyweight: {key}");
        }
        return type;
    }

    public int CreatedCount => _cache.Count;
}

/// <summary>
/// Contexto: guarda o estado EXTRÍNSECO (posição), que é único por instância,
/// e referencia o flyweight compartilhado.
/// </summary>
public sealed class Tree
{
    private readonly int _x;
    private readonly int _y;
    private readonly TreeType _type;

    public Tree(int x, int y, TreeType type)
    {
        _x = x;
        _y = y;
        _type = type;
    }

    public void Draw() => _type.Draw(_x, _y);
}
