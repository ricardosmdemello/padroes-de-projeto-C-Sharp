namespace DesignPatterns.Behavioral.Mediator;

/// <summary>Mediador: centraliza a comunicação entre os participantes.</summary>
public interface IChatMediator
{
    void Register(User user);
    void Send(string message, User sender);
}

/// <summary>
/// Padrão Mediator.
///
/// Define um objeto que encapsula como um conjunto de objetos interage. Em vez
/// de cada usuário referenciar todos os outros (acoplamento N-para-N), todos
/// falam apenas com o mediador, que coordena a entrega das mensagens.
/// </summary>
public sealed class ChatRoom : IChatMediator
{
    private readonly List<User> _users = new();

    public void Register(User user) => _users.Add(user);

    public void Send(string message, User sender)
    {
        foreach (var user in _users)
        {
            if (user != sender) // o remetente não recebe a própria mensagem
                user.Receive(message, sender.Name);
        }
    }
}

/// <summary>Colega: comunica-se com os demais somente através do mediador.</summary>
public sealed class User
{
    private readonly IChatMediator _mediator;
    public string Name { get; }

    public User(string name, IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
        _mediator.Register(this);
    }

    public void Send(string message)
    {
        Console.WriteLine($"{Name} envia: {message}");
        _mediator.Send(message, this);
    }

    public void Receive(string message, string from) =>
        Console.WriteLine($"   {Name} recebe de {from}: {message}");
}
