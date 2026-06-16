namespace DesignPatterns.Behavioral.Interpreter;

/// <summary>
/// Cliente da gramática: monta a árvore de expressões (AST) a partir de uma
/// string em notação polonesa reversa (RPN), ex.: "5 3 + 2 *".
///
/// O parser apenas constrói os nós; a avaliação fica a cargo do
/// <see cref="IExpression.Interpret"/> de cada nó.
/// </summary>
public static class RpnParser
{
    public static IExpression Parse(string expression)
    {
        var stack = new Stack<IExpression>();

        foreach (var token in expression.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (int.TryParse(token, out var number))
            {
                stack.Push(new NumberExpression(number));
                continue;
            }

            // Operador: consome os dois operandos do topo da pilha.
            var right = stack.Pop();
            var left = stack.Pop();

            stack.Push(token switch
            {
                "+" => new AddExpression(left, right),
                "-" => new SubtractExpression(left, right),
                "*" => new MultiplyExpression(left, right),
                _ => throw new FormatException($"Token inválido: '{token}'.")
            });
        }

        return stack.Pop();
    }
}
