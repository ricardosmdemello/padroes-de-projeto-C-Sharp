namespace CleanArchitecture.Domain;

/// <summary>Erro de violação de uma regra de negócio do domínio.</summary>
public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
