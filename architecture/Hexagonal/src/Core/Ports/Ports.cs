using Hexagonal.Core.Domain;

namespace Hexagonal.Core.Ports;

/// <summary>
/// PORTA DE ENTRADA (driving port).
///
/// Define o que o mundo externo pode pedir ao núcleo. Os adaptadores de entrada
/// (CLI, API REST, testes) dependem desta interface, não da implementação.
/// </summary>
public interface IAccountService
{
    Guid OpenAccount(string owner, decimal initialBalance);
    void Deposit(Guid accountId, decimal amount);
    void Withdraw(Guid accountId, decimal amount);
    decimal GetBalance(Guid accountId);
}

/// <summary>
/// PORTA DE SAÍDA (driven port).
///
/// Define o que o núcleo precisa do mundo externo (persistência). Os adaptadores
/// de saída (banco em memória, SQL, etc.) implementam esta interface.
/// </summary>
public interface IAccountRepository
{
    void Save(Account account);
    Account? FindById(Guid id);
}
