using DesignPatterns.Behavioral.Interpreter;

Console.WriteLine("=== Padrão Interpreter ===\n");

// Construção manual da AST para "(5 + 3) * 2":
IExpression manual = new MultiplyExpression(
    new AddExpression(new NumberExpression(5), new NumberExpression(3)),
    new NumberExpression(2));
Console.WriteLine($"AST montada à mão (5 + 3) * 2 = {manual.Interpret()}");

// Avaliando expressões em notação polonesa reversa (RPN):
string[] expressions =
{
    "5 3 +",        // 8
    "5 3 + 2 *",    // 16
    "10 4 - 3 *"    // 18
};

Console.WriteLine();
foreach (var rpn in expressions)
{
    var ast = RpnParser.Parse(rpn);
    Console.WriteLine($"RPN \"{rpn}\" = {ast.Interpret()}");
}
