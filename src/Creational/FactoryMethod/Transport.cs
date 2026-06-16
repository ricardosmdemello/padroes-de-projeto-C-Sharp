namespace DesignPatterns.Creational.FactoryMethod;

/// <summary>Produto: define a interface dos objetos criados pela fábrica.</summary>
public interface ITransport
{
    string Deliver(string load);
}

/// <summary>Produto concreto entregue por terra.</summary>
public sealed class Truck : ITransport
{
    public string Deliver(string load) => $"Entregando '{load}' por estrada em um caminhão.";
}

/// <summary>Produto concreto entregue por mar.</summary>
public sealed class Ship : ITransport
{
    public string Deliver(string load) => $"Entregando '{load}' por mar em um navio.";
}

/// <summary>
/// Padrão Factory Method.
///
/// Define o método <see cref="CreateTransport"/> para criar objetos, mas deixa
/// as subclasses decidirem qual classe concreta instanciar. O código que usa a
/// logística trabalha apenas com a abstração <see cref="ITransport"/>.
/// </summary>
public abstract class Logistics
{
    /// <summary>O "factory method": cada subclasse escolhe o produto concreto.</summary>
    protected abstract ITransport CreateTransport();

    /// <summary>Lógica de negócio que depende apenas da abstração do produto.</summary>
    public string Plan(string load)
    {
        var transport = CreateTransport();
        return transport.Deliver(load);
    }
}

/// <summary>Cria caminhões.</summary>
public sealed class RoadLogistics : Logistics
{
    protected override ITransport CreateTransport() => new Truck();
}

/// <summary>Cria navios.</summary>
public sealed class SeaLogistics : Logistics
{
    protected override ITransport CreateTransport() => new Ship();
}
