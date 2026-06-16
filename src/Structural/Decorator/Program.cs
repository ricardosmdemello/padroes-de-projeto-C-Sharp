using DesignPatterns.Structural.Decorator;

Console.WriteLine("=== Padrão Decorator ===\n");

// Empilhamos comportamentos em tempo de execução.
ICoffee order = new SimpleCoffee();
order = new MilkDecorator(order);
order = new CaramelDecorator(order);
order = new WhipDecorator(order);

Console.WriteLine($"Pedido: {order.Description}");
Console.WriteLine($"Total:  R$ {order.Cost:F2}");
