using Hexagonal.Core.Domain;
using Hexagonal.Core.Ports;

namespace Hexagonal.Core.Application;

/// <summary>
/// Serviço de aplicação: implementa a porta de ENTRADA usando a porta de SAÍDA.
///
/// É o coração executável do hexágono. Coordena domínio + persistência sem
/// conhecer NENHUM detalhe concreto: recebe um <see cref="IAccountRepository"/>
/// por injeção, então pode rodar com qualquer adaptador.
/// </summary>
public sealed class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository) => _repository = repository;

    public Guid OpenAccount(string owner, decimal initialBalance)
    {
        var account = new Account(Guid.NewGuid(), owner, initialBalance);
        _repository.Save(account);
        return account.Id;
    }

    public void Deposit(Guid accountId, decimal amount)
    {
        var account = Load(accountId);
        account.Deposit(amount);
        _repository.Save(account);
    }

    public void Withdraw(Guid accountId, decimal amount)
    {
        var account = Load(accountId);
        account.Withdraw(amount);
        _repository.Save(account);
    }

    public decimal GetBalance(Guid accountId) => Load(accountId).Balance;

    private Account Load(Guid id) =>
        _repository.FindById(id) ?? throw new InvalidOperationException("Conta não encontrada.");
}
