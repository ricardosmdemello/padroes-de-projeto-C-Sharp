namespace Ddd.Domain;

/// <summary>
/// VALUE OBJECT.
///
/// Definido pelos seus atributos (não tem identidade) e é imutável. Dois
/// valores com os mesmos componentes são iguais — o <c>record struct</c> dá
/// essa igualdade estrutural automaticamente. Encapsula a regra: dinheiro não
/// pode ser negativo e operações preservam a moeda.
/// </summary>
public readonly record struct Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency = "BRL")
    {
        if (amount < 0)
            throw new ArgumentException("Valor monetário não pode ser negativo.");
        Amount = amount;
        Currency = currency;
    }

    public static Money Zero { get; } = new(0);

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Moedas diferentes não podem ser somadas.");
        return this with { Amount = Amount + other.Amount };
    }

    public Money Multiply(int factor) => this with { Amount = Amount * factor };

    public override string ToString() => $"{Currency} {Amount:F2}";
}
