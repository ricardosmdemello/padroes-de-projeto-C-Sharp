using Cqrs.Domain;
using MediatR;

namespace Cqrs.Products;

/// <summary>
/// COMANDO (lado da escrita).
///
/// No CQRS, comandos representam intenções de ALTERAR o estado e normalmente
/// retornam pouco (aqui, o id criado). São tratados por um único handler.
/// </summary>
public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;

/// <summary>Handler do comando: contém a lógica de escrita.</summary>
public sealed class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly ProductStore _store;

    public CreateProductHandler(ProductStore store) => _store = store;

    public Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("O nome do produto é obrigatório.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Price = request.Price
        };
        _store.Add(product);
        return Task.FromResult(product.Id);
    }
}
