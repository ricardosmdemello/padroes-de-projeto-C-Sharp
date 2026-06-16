using DesignPatterns.Behavioral.TemplateMethod;

Console.WriteLine("=== Padrão Template Method ===\n");

DataProcessor processor = new CsvProcessor();
processor.Process("contatos.csv");

Console.WriteLine();

processor = new LineProcessor();
processor.Process("itens.txt");
