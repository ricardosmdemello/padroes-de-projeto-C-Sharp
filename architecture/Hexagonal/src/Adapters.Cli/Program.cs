using Hexagonal.Adapters.Persistence;
using Hexagonal.Core.Application;
using Hexagonal.Core.Ports;

Console.WriteLine("=== Hexagonal (Ports & Adapters) ===\n");

// Este console é um ADAPTADOR DE ENTRADA. Ele "pluga" um adaptador de saída
// (persistência) no núcleo e interage somente pela porta de entrada.
IAccountRepository repository = new InMemoryAccountRepository();
IAccountService accounts = new AccountService(repository);

var id = accounts.OpenAccount("Ricardo", initialBalance: 100m);
Console.WriteLine($"Conta aberta. Saldo inicial: R$ {accounts.GetBalance(id):F2}");

accounts.Deposit(id, 250m);
Console.WriteLine($"Após depósito de R$ 250,00: R$ {accounts.GetBalance(id):F2}");

accounts.Withdraw(id, 80m);
Console.WriteLine($"Após saque de R$ 80,00: R$ {accounts.GetBalance(id):F2}");

Console.WriteLine("\nTentando sacar além do saldo:");
try
{
    accounts.Withdraw(id, 10_000m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Regra de domínio impediu: {ex.Message}");
}
