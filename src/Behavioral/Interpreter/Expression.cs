namespace DesignPatterns.Behavioral.Interpreter;

/// <summary>
/// Expressão abstrata (a "gramática").
///
/// Padrão Interpreter: para uma linguagem simples, representa cada regra
/// gramatical como uma classe e define uma operação <see cref="Interpret"/> que
/// avalia a expressão. Os nós compõem uma árvore sintática abstrata (AST).
/// </summary>
public interface IExpression
{
    int Interpret();
}

/// <summary>Expressão terminal: um número literal.</summary>
public sealed class NumberExpression : IExpression
{
    private readonly int _value;
    public NumberExpression(int value) => _value = value;
    public int Interpret() => _value;
}

/// <summary>Expressão não-terminal: soma de duas subexpressões.</summary>
public sealed class AddExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;
    public AddExpression(IExpression left, IExpression right) => (_left, _right) = (left, right);
    public int Interpret() => _left.Interpret() + _right.Interpret();
}

/// <summary>Expressão não-terminal: subtração.</summary>
public sealed class SubtractExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;
    public SubtractExpression(IExpression left, IExpression right) => (_left, _right) = (left, right);
    public int Interpret() => _left.Interpret() - _right.Interpret();
}

/// <summary>Expressão não-terminal: multiplicação.</summary>
public sealed class MultiplyExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;
    public MultiplyExpression(IExpression left, IExpression right) => (_left, _right) = (left, right);
    public int Interpret() => _left.Interpret() * _right.Interpret();
}
