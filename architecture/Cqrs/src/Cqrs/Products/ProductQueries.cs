using Cqrs.Domain;
using MediatR;

namespace Cqrs.Products;

/// <summary>Modelo de leitura (read model), separado da entidade de escrita.</summary>
public sealed record ProductView(Guid Id, string Name, decimal Price);

/// <summary>
/// CONSULTA (lado da leitura).
///
/// No CQRS, consultas NÃO alteram estado e retornam dados moldados para a tela.
/// Cada consulta tem seu próprio handler, permitindo otimizar leitura e escrita
/// de forma independente.
/// </summary>
public sealed record GetAllProductsQuery : IRequest<IReadOnlyList<ProductView>>;

public sealed class GetAllProductsHandler
    : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductView>>
{
    private readonly ProductStore _store;

    public GetAllProductsHandler(ProductStore store) => _store = store;

    public Task<IReadOnlyList<ProductView>> Handle(
        GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<ProductView> result = _store.GetAll()
            .Select(p => new ProductView(p.Id, p.Name, p.Price))
            .ToList();
        return Task.FromResult(result);
    }
}

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductView?>;

public sealed class GetProductByIdHandler
    : IRequestHandler<GetProductByIdQuery, ProductView?>
{
    private readonly ProductStore _store;

    public GetProductByIdHandler(ProductStore store) => _store = store;

    public Task<ProductView?> Handle(
        GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var p = _store.GetById(request.Id);
        return Task.FromResult(p is null ? null : new ProductView(p.Id, p.Name, p.Price));
    }
}
