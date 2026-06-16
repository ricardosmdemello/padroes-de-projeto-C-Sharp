using System.Text;

namespace DesignPatterns.Behavioral.Command;

/// <summary>Receptor: sabe executar a operação de fato.</summary>
public sealed class TextDocument
{
    private readonly StringBuilder _content = new();

    public string Content => _content.ToString();
    public void Append(string text) => _content.Append(text);
    public void RemoveFromEnd(int length) =>
        _content.Remove(_content.Length - length, length);
}

/// <summary>
/// Padrão Command.
///
/// Encapsula uma requisição como um objeto, permitindo parametrizar clientes,
/// enfileirar operações e — principalmente — suportar desfazer (undo). Cada
/// comando sabe executar a si mesmo e reverter o seu efeito.
/// </summary>
public interface ICommand
{
    void Execute();
    void Undo();
}

/// <summary>Comando concreto: digitar um texto.</summary>
public sealed class TypeTextCommand : ICommand
{
    private readonly TextDocument _document;
    private readonly string _text;

    public TypeTextCommand(TextDocument document, string text)
    {
        _document = document;
        _text = text;
    }

    public void Execute() => _document.Append(_text);
    public void Undo() => _document.RemoveFromEnd(_text.Length);
}

/// <summary>
/// Invocador: dispara comandos e mantém o histórico para permitir o undo.
/// Não conhece os detalhes de cada operação, apenas a interface ICommand.
/// </summary>
public sealed class CommandManager
{
    private readonly Stack<ICommand> _history = new();

    public void Run(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void Undo()
    {
        if (_history.Count == 0) return;
        _history.Pop().Undo();
    }
}
