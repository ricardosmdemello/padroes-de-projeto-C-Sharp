using DesignPatterns.Structural.Bridge;

Console.WriteLine("=== Padrão Bridge ===\n");

// Mesma abstração (remoto) combinada com implementações diferentes (dispositivos).
var tvRemote = new RemoteControl(new Tv());
tvRemote.TogglePower();
tvRemote.VolumeUp();

Console.WriteLine();

var radioRemote = new AdvancedRemoteControl(new Radio());
radioRemote.TogglePower();
radioRemote.VolumeUp();
radioRemote.Mute();
