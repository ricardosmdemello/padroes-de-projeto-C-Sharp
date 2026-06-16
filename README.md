# Padrões de Projeto em C# (.NET)

Catálogo prático dos **23 padrões de projeto clássicos do GoF** (*Gang of Four*),
todos implementados em C# com **.NET 8**. Cada padrão é um projeto executável,
independente e documentado, com um exemplo de uso real que pode ser rodado pelo
terminal.

O objetivo é servir como material de estudo e referência rápida: o código é
enxuto, comentado e cada projeto demonstra **um** padrão isoladamente.

> Estão implementados os **23 padrões** GoF: 5 criacionais, 7 estruturais e
> 11 comportamentais, com implementação idiomática em C#.

> **Padrões arquiteturais:** além dos padrões GoF (nível de classe), o diretório
> [`architecture/`](architecture/README.md) traz exemplos executáveis de
> **arquiteturas de aplicação** — Clean Architecture, CQRS + MediatR, Hexagonal
> (Ports & Adapters) e DDD tático. Veja o [README de arquitetura](architecture/README.md).

---

## Sumário

- [Requisitos](#requisitos)
- [Estrutura do repositório](#estrutura-do-repositório)
- [Como compilar e executar](#como-compilar-e-executar)
- [Os padrões](#os-padrões)
  - [Criacionais](#criacionais)
  - [Estruturais](#estruturais)
  - [Comportamentais](#comportamentais)
- [Como cada projeto está organizado](#como-cada-projeto-está-organizado)
- [Roadmap](#roadmap)

---

## Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) ou superior
  (a solução usa o formato `.slnx`, suportado pelo SDK 9+; para o SDK 8 use os
  projetos individualmente).

Verifique a instalação:

```bash
dotnet --version
```

---

## Estrutura do repositório

```
padroes-de-projeto-C-Sharp/
├── DesignPatterns.slnx          # Solução com todos os padrões GoF
├── README.md
├── .gitignore
├── architecture/                # Padrões arquiteturais (soluções independentes)
│   ├── CleanArchitecture/
│   ├── Cqrs/
│   ├── Hexagonal/
│   └── Ddd/
├── tools/
│   └── PatternMenu/             # Menu interativo que roda qualquer padrão
└── src/
    ├── Creational/              # Padrões criacionais
    │   ├── Singleton/
    │   ├── FactoryMethod/
    │   ├── AbstractFactory/
    │   ├── Builder/
    │   └── Prototype/
    ├── Structural/              # Padrões estruturais
    │   ├── Adapter/
    │   ├── Bridge/
    │   ├── Composite/
    │   ├── Decorator/
    │   ├── Facade/
    │   ├── Flyweight/
    │   └── Proxy/
    └── Behavioral/              # Padrões comportamentais
        ├── ChainOfResponsibility/
        ├── Command/
        ├── Interpreter/
        ├── Iterator/
        ├── Mediator/
        ├── Memento/
        ├── Observer/
        ├── State/
        ├── Strategy/
        ├── TemplateMethod/
        └── Visitor/
```

Cada pasta de padrão é um **projeto console independente** (`OutputType = Exe`),
com seu próprio `Program.cs` que demonstra o padrão em execução.

---

## Como compilar e executar

### Compilar a solução inteira

```bash
dotnet build DesignPatterns.slnx
```

### Menu interativo (recomendado)

A forma mais simples de explorar: um menu único de console que lista os 23
padrões por categoria e executa o que você escolher (ou todos em sequência).

```bash
dotnet run --project tools/PatternMenu
```

```
╔════════════════════════════════════════════╗
║       PADRÕES DE PROJETO (GoF) EM C#         ║
╚════════════════════════════════════════════╝

  Criacionais
     1. Singleton
     2. Factory Method
     ...
  Estruturais
     6. Adapter
     ...
  Comportamentais
    13. Chain of Responsibility
    ...

     A. Executar TODOS em sequência
     0. Sair
```

Cada padrão também pode ser executado isoladamente (abaixo).

### Executar um padrão específico

Use `dotnet run` apontando para a pasta do projeto desejado. Exemplos:

```bash
# Criacionais
dotnet run --project src/Creational/Singleton
dotnet run --project src/Creational/FactoryMethod
dotnet run --project src/Creational/AbstractFactory
dotnet run --project src/Creational/Builder
dotnet run --project src/Creational/Prototype

# Estruturais
dotnet run --project src/Structural/Adapter
dotnet run --project src/Structural/Decorator
dotnet run --project src/Structural/Facade
dotnet run --project src/Structural/Proxy
dotnet run --project src/Structural/Composite
dotnet run --project src/Structural/Bridge
dotnet run --project src/Structural/Flyweight

# Comportamentais
dotnet run --project src/Behavioral/Strategy
dotnet run --project src/Behavioral/Observer
dotnet run --project src/Behavioral/Command
dotnet run --project src/Behavioral/Mediator
dotnet run --project src/Behavioral/TemplateMethod
dotnet run --project src/Behavioral/State
dotnet run --project src/Behavioral/ChainOfResponsibility
dotnet run --project src/Behavioral/Iterator
dotnet run --project src/Behavioral/Visitor
dotnet run --project src/Behavioral/Memento
dotnet run --project src/Behavioral/Interpreter
```

---

## Os padrões

### Criacionais

Lidam com a **criação de objetos**, tornando o sistema independente de como os
objetos são criados, compostos e representados.

| Padrão | Intenção | Exemplo neste repositório |
|--------|----------|---------------------------|
| **Singleton** | Garantir uma única instância de uma classe e um ponto global de acesso. | `Logger` thread-safe via `Lazy<T>`. |
| **Factory Method** | Definir uma interface para criar objetos, deixando as subclasses escolherem a classe concreta. | `Logistics` criando `Truck` ou `Ship`. |
| **Abstract Factory** | Criar famílias de objetos relacionados sem especificar suas classes concretas. | Fábrica de UI (`Button` + `Checkbox`) para Windows/macOS. |
| **Builder** | Construir objetos complexos passo a passo, separando construção e representação. | `PizzaBuilder` fluente com um `Director`. |
| **Prototype** | Criar novos objetos clonando um protótipo existente (cópia profunda). | Clonagem de `Circle`/`Rectangle`. |

### Estruturais

Lidam com a **composição de classes e objetos**, formando estruturas maiores e
mantendo-as flexíveis e eficientes.

| Padrão | Intenção | Exemplo neste repositório |
|--------|----------|---------------------------|
| **Adapter** | Converter a interface de uma classe na interface esperada pelo cliente. | Adaptar um `LegacyGateway` para `IPaymentProcessor`. |
| **Bridge** | Desacoplar uma abstração de sua implementação para que variem independentemente. | `RemoteControl` x `IDevice` (TV/Rádio). |
| **Composite** | Tratar objetos individuais e composições de forma uniforme (árvores). | Árvore de arquivos e diretórios. |
| **Decorator** | Adicionar responsabilidades a um objeto dinamicamente. | Café com adicionais (leite, caramelo, chantilly). |
| **Facade** | Fornecer uma interface simplificada para um subsistema complexo. | `HomeTheaterFacade` orquestrando vários componentes. |
| **Flyweight** | Compartilhar estado comum entre muitos objetos para economizar memória. | Floresta com 1000 árvores e poucos "tipos". |
| **Proxy** | Fornecer um substituto que controla o acesso a outro objeto. | Carregamento tardio (*lazy*) de imagem em alta resolução. |

### Comportamentais

Lidam com **algoritmos e a atribuição de responsabilidades** entre objetos,
descrevendo padrões de comunicação.

| Padrão | Intenção | Exemplo neste repositório |
|--------|----------|---------------------------|
| **Chain of Responsibility** | Passar uma requisição por uma cadeia de handlers até que um a trate. | Escalonamento de chamados de suporte (N1 → N2 → Gerência). |
| **Command** | Encapsular uma requisição como objeto, permitindo desfazer (*undo*). | Editor de texto com histórico de comandos. |
| **Interpreter** | Avaliar sentenças de uma linguagem a partir de sua gramática. | Interpretador de expressões aritméticas (RPN). |
| **Iterator** | Acessar elementos de uma coleção sequencialmente sem expor sua estrutura. | Travessia *in-order* de uma árvore binária via `IEnumerable<T>`. |
| **Mediator** | Centralizar a comunicação entre objetos, reduzindo o acoplamento. | Sala de chat coordenando usuários. |
| **Memento** | Capturar e restaurar o estado interno de um objeto sem violar o encapsulamento. | Editor com checkpoints e desfazer. |
| **Observer** | Notificar automaticamente dependentes quando o estado muda. | Estação meteorológica com displays inscritos. |
| **State** | Alterar o comportamento de um objeto conforme seu estado interno. | Ciclo de vida de um pedido (Novo → Pago → Enviado → Entregue). |
| **Strategy** | Encapsular algoritmos intercambiáveis e selecioná-los em tempo de execução. | Formas de pagamento (cartão, Pix, boleto). |
| **Template Method** | Definir o esqueleto de um algoritmo e delegar passos às subclasses. | Pipeline de processamento de dados (CSV / linhas). |
| **Visitor** | Adicionar operações a uma hierarquia de objetos sem alterá-los. | Cálculo de área e perímetro de formas. |

---

## Como cada projeto está organizado

Todos os projetos seguem o mesmo padrão para facilitar a leitura:

1. **Arquivo(s) de implementação** (ex.: `Logger.cs`, `Coffee.cs`) — contêm as
   classes do padrão, com comentários XML (`///`) explicando o papel de cada
   participante (produto, fábrica, contexto, estratégia, etc.).
2. **`Program.cs`** — um exemplo de uso autocontido, escrito com *top-level
   statements*, que executa e imprime o resultado do padrão no console.

Convenções adotadas:

- `TargetFramework`: `net8.0`
- `Nullable` e `ImplicitUsings` habilitados
- Namespace por padrão: `DesignPatterns.<Categoria>.<Padrão>`
- Classes marcadas como `sealed` quando não há intenção de herança

---

## Roadmap

Os **23 padrões GoF** estão implementados. Próximos passos possíveis:

- [ ] Testes unitários (xUnit) cobrindo cada padrão.
- [ ] README individual por padrão com diagrama (Mermaid/UML).
- [ ] Exemplos de resiliência (Polly) e persistência real (EF Core).

Contribuições seguindo a mesma estrutura (um projeto por padrão, com `Program.cs`
demonstrativo) são bem-vindas.
