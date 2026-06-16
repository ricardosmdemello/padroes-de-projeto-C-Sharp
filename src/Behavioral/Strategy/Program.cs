using DesignPatterns.Behavioral.Strategy;

Console.WriteLine("=== Padrão Strategy ===\n");

var cart = new ShoppingCart(new CreditCardPayment("1234567812345678"));
cart.Checkout(250.00m);

cart.SetStrategy(new PixPayment("ricardo@exemplo.com.br"));
cart.Checkout(99.90m);

cart.SetStrategy(new BoletoPayment());
cart.Checkout(1500.00m);
