namespace DesignPatterns.Structural.Facade;

// --- Subsistema complexo (vários componentes independentes) ------------------

public sealed class Amplifier
{
    public void On() => Console.WriteLine("Amplificador ligado.");
    public void SetVolume(int level) => Console.WriteLine($"Volume ajustado para {level}.");
    public void Off() => Console.WriteLine("Amplificador desligado.");
}

public sealed class Projector
{
    public void On() => Console.WriteLine("Projetor ligado.");
    public void WideScreenMode() => Console.WriteLine("Projetor em modo widescreen.");
    public void Off() => Console.WriteLine("Projetor desligado.");
}

public sealed class StreamingPlayer
{
    public void On() => Console.WriteLine("Player ligado.");
    public void Play(string movie) => Console.WriteLine($"Reproduzindo \"{movie}\".");
    public void Stop() => Console.WriteLine("Reprodução interrompida.");
    public void Off() => Console.WriteLine("Player desligado.");
}

public sealed class Lights
{
    public void Dim(int level) => Console.WriteLine($"Luzes reduzidas para {level}%.");
    public void On() => Console.WriteLine("Luzes acesas.");
}

/// <summary>
/// Padrão Facade.
///
/// Fornece uma interface única e simplificada para um subsistema complexo,
/// escondendo do cliente a coordenação entre os vários componentes. O cliente
/// chama apenas <see cref="WatchMovie"/> em vez de orquestrar tudo manualmente.
/// </summary>
public sealed class HomeTheaterFacade
{
    private readonly Amplifier _amp = new();
    private readonly Projector _projector = new();
    private readonly StreamingPlayer _player = new();
    private readonly Lights _lights = new();

    public void WatchMovie(string movie)
    {
        Console.WriteLine("Preparando para assistir ao filme...");
        _lights.Dim(10);
        _projector.On();
        _projector.WideScreenMode();
        _amp.On();
        _amp.SetVolume(8);
        _player.On();
        _player.Play(movie);
    }

    public void EndMovie()
    {
        Console.WriteLine("\nEncerrando a sessão...");
        _player.Stop();
        _player.Off();
        _amp.Off();
        _projector.Off();
        _lights.On();
    }
}
