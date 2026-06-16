namespace CleanArchitecture.Domain;

/// <summary>
/// Entidade de domínio.
///
/// É o núcleo da Clean Architecture: a camada mais interna, sem nenhuma
/// dependência de frameworks, banco de dados ou UI. Concentra as regras de
/// negócio invariantes da entidade (ex.: nome obrigatório, preço não negativo).
/// </summary>
public sealed class Product
{
    public Guid Id { get; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Product(Guid id, string name, decimal price)
    {
        if (id == Guid.Empty)
            throw new DomainException("O id do produto é obrigatório.");

        Id = id;
        Rename(name);
        ChangePrice(price);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("O nome do produto é obrigatório.");
        Name = name.Trim();
    }

    public void ChangePrice(decimal price)
    {
        if (price < 0)
            throw new DomainException("O preço não pode ser negativo.");
        Price = price;
    }
}
