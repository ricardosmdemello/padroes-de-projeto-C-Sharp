namespace DesignPatterns.Structural.Composite;

/// <summary>
/// Padrão Composite.
///
/// Compõe objetos em estruturas de árvore para representar hierarquias
/// "parte-todo". O cliente trata objetos individuais (arquivos) e composições
/// (pastas) de maneira uniforme através do mesmo componente.
/// </summary>
public abstract class FileSystemItem
{
    public string Name { get; }

    protected FileSystemItem(string name) => Name = name;

    /// <summary>Tamanho em KB — folhas retornam o próprio; composições somam os filhos.</summary>
    public abstract long GetSize();

    /// <summary>Imprime a árvore com indentação.</summary>
    public abstract void Print(string indent = "");
}

/// <summary>Folha: não possui filhos.</summary>
public sealed class FileLeaf : FileSystemItem
{
    private readonly long _sizeKb;

    public FileLeaf(string name, long sizeKb) : base(name) => _sizeKb = sizeKb;

    public override long GetSize() => _sizeKb;

    public override void Print(string indent = "") =>
        Console.WriteLine($"{indent}- {Name} ({_sizeKb} KB)");
}

/// <summary>Composição: contém outros itens (folhas ou composições).</summary>
public sealed class DirectoryComposite : FileSystemItem
{
    private readonly List<FileSystemItem> _children = new();

    public DirectoryComposite(string name) : base(name) { }

    public DirectoryComposite Add(FileSystemItem item)
    {
        _children.Add(item);
        return this;
    }

    public override long GetSize() => _children.Sum(c => c.GetSize());

    public override void Print(string indent = "")
    {
        Console.WriteLine($"{indent}+ {Name}/ ({GetSize()} KB)");
        foreach (var child in _children)
            child.Print(indent + "  ");
    }
}
