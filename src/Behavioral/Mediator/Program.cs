using DesignPatterns.Behavioral.Mediator;

Console.WriteLine("=== Padrão Mediator ===\n");

var chat = new ChatRoom();

var ana = new User("Ana", chat);
var bruno = new User("Bruno", chat);
var carla = new User("Carla", chat);

ana.Send("Bom dia, pessoal!");
Console.WriteLine();
bruno.Send("Bom dia, Ana!");
