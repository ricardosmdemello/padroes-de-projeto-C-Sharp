using CleanArchitecture.Application;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure;

Console.WriteLine("=== Clean Architecture ===\n");

// Composition Root: a camada mais externa escolhe as implementações concretas
// e as injeta no caso de uso. Trocar 'InMemoryProductRepository' por uma
// implementação com banco não exigiria alterar Domain nem Application.
IProductRepository repository = new InMemoryProductRepository();
var service = new ProductService(repository);

// Caso de uso: criar produtos.
var notebook = await service.CreateAsync(new CreateProductRequest("Notebook", 4500.00m));
var mouse = await service.CreateAsync(new CreateProductRequest("Mouse", 120.00m));
Console.WriteLine($"Criado: {notebook.Name} (R$ {notebook.Price:F2}) - id {notebook.Id}");
Console.WriteLine($"Criado: {mouse.Name} (R$ {mouse.Price:F2}) - id {mouse.Id}");

// Caso de uso: listar.
Console.WriteLine("\nProdutos cadastrados:");
foreach (var p in await service.GetAllAsync())
    Console.WriteLine($"  - {p.Name}: R$ {p.Price:F2}");

// Caso de uso: buscar por id.
var found = await service.GetByIdAsync(notebook.Id);
Console.WriteLine($"\nBusca por id: {found?.Name ?? "(não encontrado)"}");

// As regras de negócio do domínio são respeitadas mesmo vindas da borda.
Console.WriteLine("\nTentando criar produto inválido (preço negativo):");
try
{
    await service.CreateAsync(new CreateProductRequest("Inválido", -10m));
}
catch (DomainException ex)
{
    Console.WriteLine($"  Regra de domínio impediu: {ex.Message}");
}
