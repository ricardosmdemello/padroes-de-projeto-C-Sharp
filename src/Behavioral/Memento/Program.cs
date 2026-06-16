using DesignPatterns.Behavioral.Memento;

Console.WriteLine("=== Padrão Memento ===\n");

var editor = new Editor();
var history = new History();

editor.Type("Olá");
history.Push(editor.Save());          // checkpoint 1
Console.WriteLine($"Digitou: {editor}");

editor.Type(", mundo");
history.Push(editor.Save());          // checkpoint 2
Console.WriteLine($"Digitou: {editor}");

editor.Type("!!! (rascunho)");
Console.WriteLine($"Digitou: {editor}");

Console.WriteLine("\nDesfazendo até o último checkpoint:");
editor.Restore(history.Pop()!);
Console.WriteLine($"Estado: {editor}");

Console.WriteLine("\nDesfazendo novamente:");
editor.Restore(history.Pop()!);
Console.WriteLine($"Estado: {editor}");
