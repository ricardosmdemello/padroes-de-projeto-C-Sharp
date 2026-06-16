using DesignPatterns.Structural.Proxy;

Console.WriteLine("=== Padrão Proxy ===\n");

Console.WriteLine("Criando o proxy (nada é carregado do disco ainda):");
IImage image = new ImageProxy("foto-4k.png");

Console.WriteLine("\nPrimeira exibição (dispara o carregamento real):");
image.Display();

Console.WriteLine("\nSegunda exibição (reutiliza o objeto já carregado):");
image.Display();
