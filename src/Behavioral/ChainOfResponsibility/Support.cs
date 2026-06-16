namespace DesignPatterns.Behavioral.ChainOfResponsibility;

/// <summary>Representa o chamado a ser tratado.</summary>
public sealed record SupportTicket(string Description, int Severity);

/// <summary>
/// Padrão Chain of Responsibility.
///
/// Permite passar uma requisição por uma cadeia de handlers. Cada handler
/// decide se trata a requisição ou a repassa ao próximo. O remetente não sabe
/// qual handler vai atender — apenas envia para o início da cadeia.
/// </summary>
public abstract class SupportHandler
{
    private SupportHandler? _next;

    /// <summary>Encadeia o próximo handler e retorna-o para permitir o fluent setup.</summary>
    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    public void Handle(SupportTicket ticket)
    {
        if (CanHandle(ticket))
            Resolve(ticket);
        else if (_next is not null)
            _next.Handle(ticket);
        else
            Console.WriteLine($"Nenhum nível conseguiu tratar: \"{ticket.Description}\".");
    }

    protected abstract bool CanHandle(SupportTicket ticket);
    protected abstract void Resolve(SupportTicket ticket);
}

public sealed class Level1Support : SupportHandler
{
    protected override bool CanHandle(SupportTicket ticket) => ticket.Severity <= 1;
    protected override void Resolve(SupportTicket ticket) =>
        Console.WriteLine($"[Nível 1] Resolvendo: \"{ticket.Description}\".");
}

public sealed class Level2Support : SupportHandler
{
    protected override bool CanHandle(SupportTicket ticket) => ticket.Severity <= 3;
    protected override void Resolve(SupportTicket ticket) =>
        Console.WriteLine($"[Nível 2] Resolvendo: \"{ticket.Description}\".");
}

public sealed class ManagerSupport : SupportHandler
{
    protected override bool CanHandle(SupportTicket ticket) => true; // último recurso
    protected override void Resolve(SupportTicket ticket) =>
        Console.WriteLine($"[Gerência] Tratando caso crítico: \"{ticket.Description}\".");
}
