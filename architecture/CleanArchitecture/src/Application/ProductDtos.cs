namespace CleanArchitecture.Application;

/// <summary>Dados de entrada para criar um produto (desacopla a UI do domínio).</summary>
public sealed record CreateProductRequest(string Name, decimal Price);

/// <summary>Representação de saída de um produto.</summary>
public sealed record ProductResponse(Guid Id, string Name, decimal Price);
