namespace DesignPatterns.Behavioral.TemplateMethod;

/// <summary>
/// Padrão Template Method.
///
/// Define o esqueleto de um algoritmo em um método (o "template"), delegando
/// passos específicos para as subclasses. A estrutura geral fica fixa na classe
/// base; as subclasses só preenchem as partes que variam.
/// </summary>
public abstract class DataProcessor
{
    /// <summary>
    /// O método-template: define a ordem fixa das etapas e não deve ser
    /// sobrescrito. Cada etapa variável é um método abstrato/virtual.
    /// </summary>
    public void Process(string source)
    {
        var raw = Read(source);
        var parsed = Parse(raw);
        var transformed = Transform(parsed);
        Save(transformed);
    }

    protected abstract string Read(string source);
    protected abstract IEnumerable<string> Parse(string raw);

    // Passo com implementação padrão que as subclasses podem reaproveitar.
    protected virtual IEnumerable<string> Transform(IEnumerable<string> data) =>
        data.Select(x => x.Trim().ToUpperInvariant());

    protected void Save(IEnumerable<string> data) =>
        Console.WriteLine($"Salvando: [{string.Join(", ", data)}]");
}

/// <summary>Implementa apenas as etapas específicas de CSV.</summary>
public sealed class CsvProcessor : DataProcessor
{
    protected override string Read(string source)
    {
        Console.WriteLine($"Lendo CSV de '{source}'.");
        return "ana, bruno, carla";
    }

    protected override IEnumerable<string> Parse(string raw) => raw.Split(',');
}

/// <summary>Implementa as etapas específicas de "linhas de texto".</summary>
public sealed class LineProcessor : DataProcessor
{
    protected override string Read(string source)
    {
        Console.WriteLine($"Lendo arquivo de linhas de '{source}'.");
        return "primeiro\nsegundo\nterceiro";
    }

    protected override IEnumerable<string> Parse(string raw) =>
        raw.Split('\n');
}
