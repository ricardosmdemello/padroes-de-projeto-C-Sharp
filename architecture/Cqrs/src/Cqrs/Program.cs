using Cqrs.Domain;
using Cqrs.Products;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("=== CQRS + MediatR ===\n");

// Configuração da injeção de dependência e do MediatR.
var services = new ServiceCollection();
services.AddSingleton<ProductStore>();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

// --- Lado da ESCRITA: enviamos comandos. O remetente não conhece o handler. ---
Console.WriteLine("Enviando comandos (escrita):");
var id1 = await mediator.Send(new CreateProductCommand("Teclado mecânico", 350.00m));
var id2 = await mediator.Send(new CreateProductCommand("Monitor 27\"", 1800.00m));
Console.WriteLine($"  Produto criado: {id1}");
Console.WriteLine($"  Produto criado: {id2}");

// --- Lado da LEITURA: enviamos consultas. ---
Console.WriteLine("\nEnviando consultas (leitura):");
var all = await mediator.Send(new GetAllProductsQuery());
foreach (var p in all)
    Console.WriteLine($"  - {p.Name}: R$ {p.Price:F2}");

var one = await mediator.Send(new GetProductByIdQuery(id1));
Console.WriteLine($"\nConsulta por id: {one?.Name ?? "(não encontrado)"}");
