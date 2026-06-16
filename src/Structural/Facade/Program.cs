using DesignPatterns.Structural.Facade;

Console.WriteLine("=== Padrão Facade ===\n");

var homeTheater = new HomeTheaterFacade();
homeTheater.WatchMovie("Interestelar");
homeTheater.EndMovie();
