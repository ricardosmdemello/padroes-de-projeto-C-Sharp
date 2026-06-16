namespace Hexagonal.Core.Domain;

/// <summary>
/// Domínio (centro do hexágono).
///
/// Contém as regras de negócio puras de uma conta bancária. Não depende de
/// nenhuma tecnologia externa — bancos, HTTP, filas ficam todos fora do núcleo,
/// acessíveis apenas através de portas.
/// </summary>
public sealed class Account
{
    public Guid Id { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }

    public Account(Guid id, string owner, decimal initialBalance = 0)
    {
        Id = id;
        Owner = owner;
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new InvalidOperationException("O valor do depósito deve ser positivo.");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new InvalidOperationException("O valor do saque deve ser positivo.");
        if (amount > Balance) throw new InvalidOperationException("Saldo insuficiente.");
        Balance -= amount;
    }
}
