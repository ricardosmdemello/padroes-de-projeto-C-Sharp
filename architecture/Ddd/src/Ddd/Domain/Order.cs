namespace Ddd.Domain;

/// <summary>
/// ENTITY interna ao agregado: tem identidade própria (o produto), mas só é
/// acessada/modificada através da raiz do agregado <see cref="Order"/>.
/// </summary>
public sealed class OrderItem
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public Money UnitPrice { get; }
    public int Quantity { get; }

    public OrderItem(Guid productId, string productName, Money unitPrice, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public Money Subtotal => UnitPrice.Multiply(Quantity);
}

/// <summary>
/// AGGREGATE ROOT.
///
/// É a única porta de entrada para alterar o agregado: garante as invariantes
/// (não há item duplicado, pedido finalizado não muda) e mantém a consistência
/// do conjunto. Acumula eventos de domínio a serem publicados após persistir.
/// </summary>
public sealed class Order
{
    private readonly List<OrderItem> _items = new();
    private readonly List<IDomainEvent> _events = new();

    public Guid Id { get; }
    public bool IsPlaced { get; private set; }

    public IReadOnlyList<OrderItem> Items => _items;
    public IReadOnlyList<IDomainEvent> DomainEvents => _events;

    public Order(Guid id) => Id = id;

    public Money Total => _items.Aggregate(Money.Zero, (acc, item) => acc.Add(item.Subtotal));

    public void AddItem(Guid productId, string name, Money unitPrice, int quantity)
    {
        EnsureNotPlaced();
        if (_items.Any(i => i.ProductId == productId))
            throw new InvalidOperationException("O produto já está no pedido.");

        _items.Add(new OrderItem(productId, name, unitPrice, quantity));
    }

    /// <summary>Finaliza o pedido e registra o evento de domínio correspondente.</summary>
    public void Place()
    {
        EnsureNotPlaced();
        if (_items.Count == 0)
            throw new InvalidOperationException("Não é possível finalizar um pedido vazio.");

        IsPlaced = true;
        _events.Add(new OrderPlacedEvent(Id, Total));
    }

    public void ClearEvents() => _events.Clear();

    private void EnsureNotPlaced()
    {
        if (IsPlaced)
            throw new InvalidOperationException("O pedido já foi finalizado e não pode ser alterado.");
    }
}
