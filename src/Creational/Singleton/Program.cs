using DesignPatterns.Creational.Singleton;

Console.WriteLine("=== Padrão Singleton ===\n");

// Em qualquer ponto da aplicação obtemos SEMPRE a mesma instância.
Logger.Instance.Log("Aplicação iniciada.");
Logger.Instance.Log("Processando pedido #1001.");

var a = Logger.Instance;
var b = Logger.Instance;

a.Log("Mensagem registrada via referência 'a'.");
b.Log("Mensagem registrada via referência 'b'.");

Console.WriteLine();
Console.WriteLine($"'a' e 'b' são a mesma instância? {ReferenceEquals(a, b)}");
Console.WriteLine($"Total de mensagens registradas: {Logger.Instance.Count}");
