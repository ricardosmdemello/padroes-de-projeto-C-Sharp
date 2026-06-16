using DesignPatterns.Behavioral.Observer;

Console.WriteLine("=== Padrão Observer ===\n");

var station = new WeatherStation();

var phone = new PhoneDisplay();
var window = new WindowDisplay();

station.Subscribe(phone);
station.Subscribe(window);

Console.WriteLine("Nova leitura: 25.3 °C");
station.Temperature = 25.3f;

Console.WriteLine("\nO painel cancela a inscrição.");
station.Unsubscribe(window);

Console.WriteLine("\nNova leitura: 28.7 °C");
station.Temperature = 28.7f;
