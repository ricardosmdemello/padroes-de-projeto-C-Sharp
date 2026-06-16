using DesignPatterns.Behavioral.State;

Console.WriteLine("=== Padrão State ===\n");

var order = new Order();
Console.WriteLine($"Estado inicial: {order.State.Name}\n");

Console.WriteLine("Tentando enviar antes de pagar:");
order.Ship();

Console.WriteLine("\nPagando:");
order.Pay();

Console.WriteLine("\nEnviando:");
order.Ship();

Console.WriteLine("\nEntregando:");
order.Deliver();

Console.WriteLine("\nTentando pagar novamente:");
order.Pay();
