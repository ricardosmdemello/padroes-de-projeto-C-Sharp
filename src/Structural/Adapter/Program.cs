using DesignPatterns.Structural.Adapter;

Console.WriteLine("=== Padrão Adapter ===\n");

// O cliente conhece apenas a interface moderna IPaymentProcessor.
IPaymentProcessor processor = new LegacyGatewayAdapter(new LegacyGateway());

Console.WriteLine(processor.Pay(149.90m));
