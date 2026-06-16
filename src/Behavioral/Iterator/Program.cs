using DesignPatterns.Behavioral.Iterator;

Console.WriteLine("=== Padrão Iterator ===\n");

var tree = new BinaryTree<int>();
foreach (var n in new[] { 50, 30, 70, 20, 40, 60, 80 })
    tree.Add(n, Comparer<int>.Default);

// O cliente percorre a árvore com foreach, sem conhecer sua estrutura interna.
Console.Write("Travessia em-ordem: ");
foreach (var value in tree)
    Console.Write($"{value} ");
Console.WriteLine();
