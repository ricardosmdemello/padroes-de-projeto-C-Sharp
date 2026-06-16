using DesignPatterns.Creational.FactoryMethod;

Console.WriteLine("=== Padrão Factory Method ===\n");

Logistics logistics = new RoadLogistics();
Console.WriteLine(logistics.Plan("móveis"));

logistics = new SeaLogistics();
Console.WriteLine(logistics.Plan("contêiner de eletrônicos"));
