using DesignPatterns.Behavioral.Command;

Console.WriteLine("=== Padrão Command ===\n");

var document = new TextDocument();
var manager = new CommandManager();

manager.Run(new TypeTextCommand(document, "Olá"));
manager.Run(new TypeTextCommand(document, ", mundo"));
manager.Run(new TypeTextCommand(document, "!!!"));
Console.WriteLine($"Conteúdo: \"{document.Content}\"");

manager.Undo();
Console.WriteLine($"Após 1 undo: \"{document.Content}\"");

manager.Undo();
Console.WriteLine($"Após 2 undos: \"{document.Content}\"");
