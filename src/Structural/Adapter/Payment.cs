namespace DesignPatterns.Structural.Adapter;

/// <summary>Interface esperada pelo nosso sistema (o "Target").</summary>
public interface IPaymentProcessor
{
    string Pay(decimal amount);
}

/// <summary>
/// Serviço legado/externo com uma interface incompatível (o "Adaptee").
/// Não podemos (ou não queremos) alterá-lo.
/// </summary>
public sealed class LegacyGateway
{
    public string ExecuteTransaction(int amountInCents, string currency)
        => $"Transação de {amountInCents} centavos ({currency}) executada pelo gateway legado.";
}

/// <summary>
/// Padrão Adapter.
///
/// Converte a interface de uma classe em outra interface que o cliente espera.
/// Aqui o adapter implementa <see cref="IPaymentProcessor"/> e traduz a chamada
/// para o formato esperado pelo <see cref="LegacyGateway"/> (centavos + moeda).
/// </summary>
public sealed class LegacyGatewayAdapter : IPaymentProcessor
{
    private readonly LegacyGateway _gateway;

    public LegacyGatewayAdapter(LegacyGateway gateway) => _gateway = gateway;

    public string Pay(decimal amount)
    {
        // Adapta o contrato moderno (decimal em reais) para o legado.
        var cents = (int)(amount * 100);
        return _gateway.ExecuteTransaction(cents, "BRL");
    }
}
