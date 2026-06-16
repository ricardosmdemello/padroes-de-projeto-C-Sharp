using DesignPatterns.Behavioral.ChainOfResponsibility;

Console.WriteLine("=== Padrão Chain of Responsibility ===\n");

// Monta a cadeia: Nível 1 -> Nível 2 -> Gerência.
var level1 = new Level1Support();
level1.SetNext(new Level2Support()).SetNext(new ManagerSupport());

level1.Handle(new SupportTicket("Senha esquecida", 1));
level1.Handle(new SupportTicket("Erro ao gerar relatório", 3));
level1.Handle(new SupportTicket("Sistema fora do ar", 5));
