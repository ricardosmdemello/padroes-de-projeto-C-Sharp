namespace Cqrs.Domain;

/// <summary>Entidade simples usada pelos lados de escrita e leitura.</summary>
public sealed class Product
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
}

/// <summary>
/// Armazenamento em memória compartilhado. Em um sistema real o CQRS costuma
/// ter até bancos distintos para escrita (normalizado) e leitura (otimizado);
/// aqui usamos um único store para manter o exemplo enxuto.
/// </summary>
public sealed class ProductStore
{
    private readonly Dictionary<Guid, Product> _items = new();

    public void Add(Product product) => _items[product.Id] = product;
    public Product? GetById(Guid id) => _items.GetValueOrDefault(id);
    public IReadOnlyList<Product> GetAll() => _items.Values.ToList();
}
