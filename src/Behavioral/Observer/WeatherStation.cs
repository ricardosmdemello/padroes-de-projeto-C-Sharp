namespace DesignPatterns.Behavioral.Observer;

/// <summary>Observador: reage às notificações do sujeito.</summary>
public interface IObserver
{
    void Update(float temperature);
}

/// <summary>Sujeito (observável): mantém e notifica seus observadores.</summary>
public interface ISubject
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Notify();
}

/// <summary>
/// Padrão Observer.
///
/// Define uma dependência um-para-muitos entre objetos: quando o sujeito muda
/// de estado, todos os observadores inscritos são notificados automaticamente.
/// O sujeito não conhece as classes concretas dos observadores, apenas a
/// interface — mantendo o acoplamento baixo.
/// </summary>
public sealed class WeatherStation : ISubject
{
    private readonly List<IObserver> _observers = new();
    private float _temperature;

    public float Temperature
    {
        get => _temperature;
        set
        {
            _temperature = value;
            Notify(); // qualquer mudança dispara a notificação
        }
    }

    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers)
            observer.Update(_temperature);
    }
}

public sealed class PhoneDisplay : IObserver
{
    public void Update(float temperature) =>
        Console.WriteLine($"[Celular] Temperatura agora: {temperature:F1} °C");
}

public sealed class WindowDisplay : IObserver
{
    public void Update(float temperature) =>
        Console.WriteLine($"[Painel] Atualizando vitrine para {temperature:F1} °C");
}
