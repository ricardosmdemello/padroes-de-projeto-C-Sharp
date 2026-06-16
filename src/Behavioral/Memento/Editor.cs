namespace DesignPatterns.Behavioral.Memento;

/// <summary>
/// Memento: instantâneo imutável do estado do originador.
///
/// Expõe o estado apenas para quem o criou (o originador). O "caretaker" guarda
/// o memento sem conhecer seu conteúdo — preservando o encapsulamento.
/// </summary>
public sealed class EditorMemento
{
    // Propriedades internas: só o originador (mesmo assembly) deveria ler/criar.
    internal string Content { get; }
    internal int CursorPosition { get; }

    internal EditorMemento(string content, int cursorPosition)
    {
        Content = content;
        CursorPosition = cursorPosition;
    }
}

/// <summary>
/// Padrão Memento.
///
/// Permite capturar e externalizar o estado interno de um objeto (o originador)
/// sem violar o encapsulamento, de modo que ele possa ser restaurado depois.
/// Aqui o editor salva e restaura conteúdo e posição do cursor.
/// </summary>
public sealed class Editor
{
    public string Content { get; private set; } = string.Empty;
    public int CursorPosition { get; private set; }

    public void Type(string text)
    {
        Content += text;
        CursorPosition = Content.Length;
    }

    /// <summary>Cria um memento com o estado atual.</summary>
    public EditorMemento Save() => new(Content, CursorPosition);

    /// <summary>Restaura o estado a partir de um memento.</summary>
    public void Restore(EditorMemento memento)
    {
        Content = memento.Content;
        CursorPosition = memento.CursorPosition;
    }

    public override string ToString() => $"\"{Content}\" (cursor em {CursorPosition})";
}

/// <summary>
/// Caretaker: mantém o histórico de mementos e coordena o desfazer.
/// Não inspeciona o conteúdo dos mementos.
/// </summary>
public sealed class History
{
    private readonly Stack<EditorMemento> _snapshots = new();

    public void Push(EditorMemento memento) => _snapshots.Push(memento);

    public EditorMemento? Pop() => _snapshots.Count > 0 ? _snapshots.Pop() : null;

    public int Count => _snapshots.Count;
}
