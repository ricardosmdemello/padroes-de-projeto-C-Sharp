using CleanArchitecture.Domain;

namespace CleanArchitecture.Application;

/// <summary>
/// Porta de saída (abstração de persistência).
///
/// A interface é declarada na camada de aplicação, mas sua implementação vive
/// na infraestrutura. Isso inverte a dependência: o núcleo não conhece o banco;
/// a infraestrutura é que depende do núcleo (Dependency Inversion Principle).
/// </summary>
public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Product>> GetAllAsync();
}
