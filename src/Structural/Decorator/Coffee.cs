namespace DesignPatterns.Structural.Decorator;

/// <summary>Componente: a interface comum a objetos simples e decorados.</summary>
public interface ICoffee
{
    string Description { get; }
    decimal Cost { get; }
}

/// <summary>Componente concreto: o café base.</summary>
public sealed class SimpleCoffee : ICoffee
{
    public string Description => "Café";
    public decimal Cost => 5.00m;
}

/// <summary>
/// Padrão Decorator.
///
/// Permite adicionar comportamentos/responsabilidades a um objeto
/// dinamicamente, envolvendo-o (wrapping). Cada decorador implementa a mesma
/// interface do componente e delega a ele, somando o seu próprio efeito.
/// É uma alternativa flexível à herança.
/// </summary>
public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee Inner;

    protected CoffeeDecorator(ICoffee inner) => Inner = inner;

    public abstract string Description { get; }
    public abstract decimal Cost { get; }
}

public sealed class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee inner) : base(inner) { }
    public override string Description => $"{Inner.Description} + leite";
    public override decimal Cost => Inner.Cost + 1.50m;
}

public sealed class WhipDecorator : CoffeeDecorator
{
    public WhipDecorator(ICoffee inner) : base(inner) { }
    public override string Description => $"{Inner.Description} + chantilly";
    public override decimal Cost => Inner.Cost + 2.00m;
}

public sealed class CaramelDecorator : CoffeeDecorator
{
    public CaramelDecorator(ICoffee inner) : base(inner) { }
    public override string Description => $"{Inner.Description} + caramelo";
    public override decimal Cost => Inner.Cost + 2.50m;
}
