namespace Ddd.Domain;

/// <summary>
/// REPOSITORY (abstração).
///
/// Dá a ilusão de uma coleção em memória de agregados, escondendo a persistência
/// real. Trabalha sempre na granularidade do AGGREGATE ROOT — nunca de entidades
/// internas como <see cref="OrderItem"/>.
/// </summary>
public interface IOrderRepository
{
    void Save(Order order);
    Order? GetById(Guid id);
}
