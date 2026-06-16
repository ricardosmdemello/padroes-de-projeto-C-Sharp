using System.Collections.Concurrent;
using CleanArchitecture.Application;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Infrastructure;

/// <summary>
/// Adaptador de persistência (camada de infraestrutura).
///
/// Implementação concreta da porta <see cref="IProductRepository"/>. Aqui se
/// usaria Entity Framework, Dapper, etc.; para o exemplo, um armazenamento em
/// memória. Poderia ser trocada sem afetar domínio nem aplicação.
/// </summary>
public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _store = new();

    public Task AddAsync(Product product)
    {
        _store[product.Id] = product;
        return Task.CompletedTask;
    }

    public Task<Product?> GetByIdAsync(Guid id) =>
        Task.FromResult(_store.GetValueOrDefault(id));

    public Task<IReadOnlyList<Product>> GetAllAsync() =>
        Task.FromResult<IReadOnlyList<Product>>(_store.Values.ToList());
}
