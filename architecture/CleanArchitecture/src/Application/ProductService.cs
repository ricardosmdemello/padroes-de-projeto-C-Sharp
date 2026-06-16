using CleanArchitecture.Domain;

namespace CleanArchitecture.Application;

/// <summary>
/// Caso de uso (camada de aplicação).
///
/// Orquestra o fluxo: recebe DTOs, aciona as regras do domínio e usa a porta de
/// repositório para persistir/recuperar. Não conhece detalhes de infraestrutura
/// — trabalha apenas com a abstração <see cref="IProductRepository"/>.
/// </summary>
public sealed class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) => _repository = repository;

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product(Guid.NewGuid(), request.Name, request.Price);
        await _repository.AddAsync(product);
        return Map(product);
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);
        return product is null ? null : Map(product);
    }

    public async Task<IReadOnlyList<ProductResponse>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(Map).ToList();
    }

    private static ProductResponse Map(Product p) => new(p.Id, p.Name, p.Price);
}
