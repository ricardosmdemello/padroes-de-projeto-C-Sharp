using Ddd.Domain;
using Ddd.Infrastructure;

Console.WriteLine("=== DDD tático (Aggregate, Value Object, Domain Event, Repository) ===\n");

IOrderRepository repository = new InMemoryOrderRepository();

// Monta o agregado pela sua raiz, que garante as invariantes.
var order = new Order(Guid.NewGuid());
order.AddItem(Guid.NewGuid(), "Cadeira gamer", new Money(1200.00m), quantity: 2);
order.AddItem(Guid.NewGuid(), "Mesa", new Money(800.00m), quantity: 1);

Console.WriteLine("Itens do pedido:");
foreach (var item in order.Items)
    Console.WriteLine($"  - {item.Quantity}x {item.ProductName} ({item.UnitPrice}) = {item.Subtotal}");

Console.WriteLine($"\nTotal do pedido: {order.Total}");

// Finaliza: dispara o Domain Event.
order.Place();
repository.Save(order);

Console.WriteLine("\nEventos de domínio gerados:");
foreach (var e in order.DomainEvents)
{
    if (e is OrderPlacedEvent placed)
        Console.WriteLine($"  - OrderPlaced: pedido {placed.OrderId} no valor de {placed.Total} " +
                          $"em {placed.OccurredOn:HH:mm:ss}");
}

// Invariante: pedido finalizado não pode ser alterado.
Console.WriteLine("\nTentando alterar um pedido já finalizado:");
try
{
    order.AddItem(Guid.NewGuid(), "Item tardio", new Money(50m), 1);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Invariante do agregado impediu: {ex.Message}");
}

// Invariante do Value Object: dinheiro não pode ser negativo.
Console.WriteLine("\nTentando criar um valor monetário negativo:");
try
{
    _ = new Money(-1m);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"  Value Object impediu: {ex.Message}");
}
