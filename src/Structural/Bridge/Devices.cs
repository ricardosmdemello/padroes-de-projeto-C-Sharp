namespace DesignPatterns.Structural.Bridge;

/// <summary>
/// Implementação (a "ponte"). Define as operações de baixo nível dos
/// dispositivos. Pode variar independentemente da abstração que a usa.
/// </summary>
public interface IDevice
{
    bool IsEnabled { get; }
    int Volume { get; set; }
    void Enable();
    void Disable();
    string Name { get; }
}

public sealed class Tv : IDevice
{
    public bool IsEnabled { get; private set; }
    public int Volume { get; set; } = 30;
    public string Name => "TV";
    public void Enable() => IsEnabled = true;
    public void Disable() => IsEnabled = false;
}

public sealed class Radio : IDevice
{
    public bool IsEnabled { get; private set; }
    public int Volume { get; set; } = 20;
    public string Name => "Rádio";
    public void Enable() => IsEnabled = true;
    public void Disable() => IsEnabled = false;
}

/// <summary>
/// Padrão Bridge.
///
/// Desacopla uma abstração (o controle remoto) da sua implementação (o
/// dispositivo), de modo que ambas possam evoluir de forma independente.
/// Em vez de uma explosão de classes (TvRemote, RadioRemote, AdvancedTvRemote...)
/// combinamos livremente qualquer remoto com qualquer dispositivo.
/// </summary>
public class RemoteControl
{
    protected readonly IDevice Device;

    public RemoteControl(IDevice device) => Device = device;

    public void TogglePower()
    {
        if (Device.IsEnabled) Device.Disable();
        else Device.Enable();
        Console.WriteLine($"{Device.Name}: {(Device.IsEnabled ? "ligado" : "desligado")}.");
    }

    public void VolumeUp()
    {
        Device.Volume += 10;
        Console.WriteLine($"{Device.Name}: volume = {Device.Volume}.");
    }
}

/// <summary>Abstração refinada: estende o controle sem tocar nos dispositivos.</summary>
public sealed class AdvancedRemoteControl : RemoteControl
{
    public AdvancedRemoteControl(IDevice device) : base(device) { }

    public void Mute()
    {
        Device.Volume = 0;
        Console.WriteLine($"{Device.Name}: mudo.");
    }
}
