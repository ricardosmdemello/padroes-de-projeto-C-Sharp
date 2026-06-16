namespace Ddd.Domain;

/// <summary>Marca um evento de domínio: algo relevante que aconteceu no passado.</summary>
public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}

/// <summary>
/// DOMAIN EVENT.
///
/// Registra um fato de negócio (o pedido foi finalizado). Outras partes do
/// sistema podem reagir a ele de forma desacoplada (enviar e-mail, baixar
/// estoque), sem que o agregado conheça esses consumidores.
/// </summary>
public sealed record OrderPlacedEvent(Guid OrderId, Money Total) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
