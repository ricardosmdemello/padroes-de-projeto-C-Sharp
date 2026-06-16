using Hexagonal.Core.Domain;
using Hexagonal.Core.Ports;

namespace Hexagonal.Adapters.Persistence;

/// <summary>
/// ADAPTADOR DE SAÍDA: implementa a porta <see cref="IAccountRepository"/>.
///
/// Poderia ser substituído por um adaptador SQL, NoSQL ou de arquivo sem que o
/// núcleo precise mudar — é exatamente o ganho do Ports &amp; Adapters.
/// </summary>
public sealed class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<Guid, Account> _accounts = new();

    public void Save(Account account) => _accounts[account.Id] = account;
    public Account? FindById(Guid id) => _accounts.GetValueOrDefault(id);
}
