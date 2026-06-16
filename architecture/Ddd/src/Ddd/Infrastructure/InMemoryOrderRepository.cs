using Ddd.Domain;

namespace Ddd.Infrastructure;

/// <summary>Implementação de infraestrutura do repositório do agregado Order.</summary>
public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly Dictionary<Guid, Order> _orders = new();

    public void Save(Order order) => _orders[order.Id] = order;
    public Order? GetById(Guid id) => _orders.GetValueOrDefault(id);
}
