namespace DesignPatterns.Behavioral.Strategy;

/// <summary>Estratégia: a interface comum a todos os algoritmos intercambiáveis.</summary>
public interface IPaymentStrategy
{
    string Pay(decimal amount);
}

public sealed class CreditCardPayment : IPaymentStrategy
{
    private readonly string _number;
    public CreditCardPayment(string number) => _number = number;
    public string Pay(decimal amount) =>
        $"Pagando R$ {amount:F2} no cartão final {_number[^4..]}.";
}

public sealed class PixPayment : IPaymentStrategy
{
    private readonly string _key;
    public PixPayment(string key) => _key = key;
    public string Pay(decimal amount) => $"Pagando R$ {amount:F2} via Pix para a chave '{_key}'.";
}

public sealed class BoletoPayment : IPaymentStrategy
{
    public string Pay(decimal amount) =>
        $"Gerando boleto de R$ {amount:F2} com vencimento em 3 dias.";
}

/// <summary>
/// Padrão Strategy.
///
/// Define uma família de algoritmos, encapsula cada um e os torna
/// intercambiáveis. O contexto (o carrinho) delega o cálculo à estratégia
/// escolhida, podendo trocá-la em tempo de execução sem alterar o seu código.
/// </summary>
public sealed class ShoppingCart
{
    private IPaymentStrategy _strategy;

    public ShoppingCart(IPaymentStrategy strategy) => _strategy = strategy;

    /// <summary>Permite trocar o algoritmo em tempo de execução.</summary>
    public void SetStrategy(IPaymentStrategy strategy) => _strategy = strategy;

    public void Checkout(decimal amount) => Console.WriteLine(_strategy.Pay(amount));
}
