using System.Text;

namespace DesignPatterns.Creational.Builder;

/// <summary>Produto complexo construído passo a passo.</summary>
public sealed class Pizza
{
    public string Size { get; init; } = "média";
    public string Dough { get; init; } = "tradicional";
    public IReadOnlyList<string> Toppings { get; init; } = Array.Empty<string>();

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Pizza {Size}, massa {Dough}");
        sb.Append("Ingredientes: ");
        sb.Append(Toppings.Count == 0 ? "(nenhum)" : string.Join(", ", Toppings));
        return sb.ToString();
    }
}

/// <summary>
/// Padrão Builder.
///
/// Separa a construção de um objeto complexo da sua representação, permitindo
/// montar o objeto em etapas. A API fluente (cada método retorna o próprio
/// builder) torna a montagem legível e evita construtores com muitos parâmetros.
/// </summary>
public sealed class PizzaBuilder
{
    private string _size = "média";
    private string _dough = "tradicional";
    private readonly List<string> _toppings = new();

    public PizzaBuilder WithSize(string size)
    {
        _size = size;
        return this;
    }

    public PizzaBuilder WithDough(string dough)
    {
        _dough = dough;
        return this;
    }

    public PizzaBuilder AddTopping(string topping)
    {
        _toppings.Add(topping);
        return this;
    }

    /// <summary>Monta o produto final a partir das etapas configuradas.</summary>
    public Pizza Build() => new()
    {
        Size = _size,
        Dough = _dough,
        Toppings = _toppings.ToArray()
    };
}

/// <summary>
/// "Director" opcional: encapsula receitas de montagem reutilizáveis,
/// escondendo do cliente a sequência de passos de um produto comum.
/// </summary>
public sealed class PizzaDirector
{
    public Pizza MakeMargherita() => new PizzaBuilder()
        .WithSize("grande")
        .WithDough("fina")
        .AddTopping("molho de tomate")
        .AddTopping("muçarela")
        .AddTopping("manjericão")
        .Build();
}
