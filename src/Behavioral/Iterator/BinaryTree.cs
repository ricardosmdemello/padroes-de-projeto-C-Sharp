using System.Collections;

namespace DesignPatterns.Behavioral.Iterator;

/// <summary>
/// Padrão Iterator.
///
/// Fornece uma forma de acessar sequencialmente os elementos de uma coleção
/// sem expor sua representação interna. Em C# isso é idiomático ao implementar
/// <see cref="IEnumerable{T}"/>; o uso de <c>yield return</c> deixa o runtime
/// gerar o iterador (o objeto que mantém a posição da travessia).
/// </summary>
public sealed class BinaryTree<T> : IEnumerable<T>
{
    private sealed class Node
    {
        public T Value = default!;
        public Node? Left;
        public Node? Right;
    }

    private Node? _root;

    public void Add(T value, IComparer<T> comparer) => _root = Insert(_root, value, comparer);

    private static Node Insert(Node? node, T value, IComparer<T> comparer)
    {
        if (node is null) return new Node { Value = value };
        if (comparer.Compare(value, node.Value) < 0)
            node.Left = Insert(node.Left, value, comparer);
        else
            node.Right = Insert(node.Right, value, comparer);
        return node;
    }

    /// <summary>Travessia em-ordem (in-order): produz os elementos ordenados.</summary>
    public IEnumerator<T> GetEnumerator() => InOrder(_root).GetEnumerator();

    private static IEnumerable<T> InOrder(Node? node)
    {
        if (node is null) yield break;
        foreach (var v in InOrder(node.Left)) yield return v;
        yield return node.Value;
        foreach (var v in InOrder(node.Right)) yield return v;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
