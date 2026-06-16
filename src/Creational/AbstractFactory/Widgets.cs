namespace DesignPatterns.Creational.AbstractFactory;

// --- Produtos abstratos -------------------------------------------------------

/// <summary>Produto abstrato: um botão.</summary>
public interface IButton
{
    string Render();
}

/// <summary>Produto abstrato: uma caixa de seleção.</summary>
public interface ICheckbox
{
    string Render();
}

// --- Família "Windows" --------------------------------------------------------

public sealed class WindowsButton : IButton
{
    public string Render() => "[ Botão estilo Windows ]";
}

public sealed class WindowsCheckbox : ICheckbox
{
    public string Render() => "[x] Checkbox estilo Windows";
}

// --- Família "macOS" ----------------------------------------------------------

public sealed class MacButton : IButton
{
    public string Render() => "( Botão estilo macOS )";
}

public sealed class MacCheckbox : ICheckbox
{
    public string Render() => "(x) Checkbox estilo macOS";
}

// --- Fábrica abstrata ---------------------------------------------------------

/// <summary>
/// Padrão Abstract Factory.
///
/// Permite produzir famílias de objetos relacionados (botão + checkbox) sem
/// acoplar o código cliente às classes concretas. Trocar a fábrica troca toda
/// a família de componentes de forma consistente.
/// </summary>
public interface IGuiFactory
{
    IButton CreateButton();
    ICheckbox CreateCheckbox();
}

/// <summary>Fábrica concreta que cria a família de widgets do Windows.</summary>
public sealed class WindowsFactory : IGuiFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ICheckbox CreateCheckbox() => new WindowsCheckbox();
}

/// <summary>Fábrica concreta que cria a família de widgets do macOS.</summary>
public sealed class MacFactory : IGuiFactory
{
    public IButton CreateButton() => new MacButton();
    public ICheckbox CreateCheckbox() => new MacCheckbox();
}

/// <summary>Código cliente: depende somente das abstrações.</summary>
public sealed class Application
{
    private readonly IButton _button;
    private readonly ICheckbox _checkbox;

    public Application(IGuiFactory factory)
    {
        _button = factory.CreateButton();
        _checkbox = factory.CreateCheckbox();
    }

    public void RenderUi()
    {
        Console.WriteLine(_button.Render());
        Console.WriteLine(_checkbox.Render());
    }
}
