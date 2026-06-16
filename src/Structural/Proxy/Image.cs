namespace DesignPatterns.Structural.Proxy;

/// <summary>Interface comum ao objeto real e ao proxy.</summary>
public interface IImage
{
    void Display();
}

/// <summary>
/// Objeto real cuja criação é cara (simula carregar do disco). É justamente o
/// que queremos adiar até ser realmente necessário.
/// </summary>
public sealed class HighResolutionImage : IImage
{
    private readonly string _fileName;

    public HighResolutionImage(string fileName)
    {
        _fileName = fileName;
        LoadFromDisk();
    }

    private void LoadFromDisk() =>
        Console.WriteLine($"[custo alto] Carregando '{_fileName}' do disco...");

    public void Display() => Console.WriteLine($"Exibindo '{_fileName}'.");
}

/// <summary>
/// Padrão Proxy (variação "virtual proxy" / lazy loading).
///
/// Funciona como substituto/representante de outro objeto, controlando o acesso
/// a ele. Aqui o objeto real só é criado no primeiro <see cref="Display"/>,
/// evitando o custo de carregamento enquanto a imagem não for usada.
/// </summary>
public sealed class ImageProxy : IImage
{
    private readonly string _fileName;
    private HighResolutionImage? _realImage;

    public ImageProxy(string fileName) => _fileName = fileName;

    public void Display()
    {
        // Inicialização tardia: cria o objeto real apenas quando necessário.
        _realImage ??= new HighResolutionImage(_fileName);
        _realImage.Display();
    }
}
