namespace DesignPatterns.Behavioral.State;

/// <summary>Estado: cada estado concreto sabe como reagir às ações.</summary>
public interface IOrderState
{
    string Name { get; }
    void Pay(Order order);
    void Ship(Order order);
    void Deliver(Order order);
}

/// <summary>
/// Padrão State.
///
/// Permite que um objeto altere seu comportamento quando seu estado interno
/// muda — como se trocasse de classe. Em vez de grandes blocos de if/switch
/// espalhados, cada estado vira uma classe que decide as transições válidas.
/// </summary>
public sealed class Order
{
    public IOrderState State { get; private set; }

    public Order() => State = new NewOrderState();

    public void SetState(IOrderState state)
    {
        State = state;
        Console.WriteLine($"  -> Pedido agora está: {state.Name}");
    }

    public void Pay() => State.Pay(this);
    public void Ship() => State.Ship(this);
    public void Deliver() => State.Deliver(this);
}

public sealed class NewOrderState : IOrderState
{
    public string Name => "Novo";
    public void Pay(Order order) => order.SetState(new PaidState());
    public void Ship(Order order) => Console.WriteLine("  ! Não é possível enviar: pedido não foi pago.");
    public void Deliver(Order order) => Console.WriteLine("  ! Não é possível entregar: pedido não foi enviado.");
}

public sealed class PaidState : IOrderState
{
    public string Name => "Pago";
    public void Pay(Order order) => Console.WriteLine("  ! Pedido já está pago.");
    public void Ship(Order order) => order.SetState(new ShippedState());
    public void Deliver(Order order) => Console.WriteLine("  ! Não é possível entregar: pedido não foi enviado.");
}

public sealed class ShippedState : IOrderState
{
    public string Name => "Enviado";
    public void Pay(Order order) => Console.WriteLine("  ! Pedido já está pago.");
    public void Ship(Order order) => Console.WriteLine("  ! Pedido já foi enviado.");
    public void Deliver(Order order) => order.SetState(new DeliveredState());
}

public sealed class DeliveredState : IOrderState
{
    public string Name => "Entregue";
    public void Pay(Order order) => Console.WriteLine("  ! Pedido já finalizado.");
    public void Ship(Order order) => Console.WriteLine("  ! Pedido já finalizado.");
    public void Deliver(Order order) => Console.WriteLine("  ! Pedido já foi entregue.");
}
