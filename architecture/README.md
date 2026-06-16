# Padrões Arquiteturais em C# (.NET)

Exemplos executáveis das principais **arquiteturas de aplicação** usadas no
ecossistema .NET. Diferente dos padrões GoF (em [`../src`](../src)), que atuam no
nível de classes, estes padrões definem a **estrutura macro** de um sistema:
como as camadas se organizam, para onde apontam as dependências e como o domínio
é protegido de detalhes de infraestrutura.

Cada arquitetura é uma **solução `.slnx` independente**, com um aplicativo de
console que demonstra o fluxo de ponta a ponta. Todas usam **.NET 8** e
armazenamento em memória para focar na estrutura, não na tecnologia de banco.

---

## Conteúdo

| Pasta | Arquitetura | Domínio de exemplo |
|-------|-------------|--------------------|
| [`CleanArchitecture/`](#1-clean-architecture) | Clean Architecture | CRUD de produtos |
| [`Cqrs/`](#2-cqrs--mediatr) | CQRS + MediatR | Produtos (comandos/consultas) |
| [`Hexagonal/`](#3-hexagonal-ports--adapters) | Hexagonal (Ports & Adapters) | Conta bancária |
| [`Ddd/`](#4-ddd-tático) | DDD tático | Pedido (Order) |

---

## 1. Clean Architecture

**Ideia central:** organizar o código em camadas concêntricas onde a *regra de
dependência* aponta sempre para dentro. O domínio não conhece ninguém; a
infraestrutura conhece o domínio — nunca o contrário.

```
ConsoleApp (composition root)
      │  injeta as implementações
      ▼
Infrastructure  ──▶  Application  ──▶  Domain
(adaptadores)        (casos de uso)    (regras de negócio puras)
```

| Projeto | Responsabilidade |
|---------|------------------|
| `Domain` | Entidade `Product` e suas invariantes. Sem dependências. |
| `Application` | Casos de uso (`ProductService`), DTOs e a porta `IProductRepository`. |
| `Infrastructure` | `InMemoryProductRepository` (implementação da porta). |
| `ConsoleApp` | *Composition root*: escolhe as implementações e as injeta. |

**Executar:**
```bash
dotnet run --project CleanArchitecture/src/ConsoleApp
```

---

## 2. CQRS + MediatR

**Ideia central:** *Command Query Responsibility Segregation* — separar as
operações que **alteram** estado (comandos) das que apenas **leem** (consultas).
A biblioteca [MediatR](https://github.com/jbogard/MediatR) entrega cada mensagem
ao seu handler, desacoplando quem envia de quem trata.

```
mediator.Send(command) ─▶ CreateProductHandler  (escrita)
mediator.Send(query)   ─▶ GetAllProductsHandler  (leitura)
```

| Arquivo | Papel |
|---------|-------|
| `CreateProductCommand` | Comando + handler de escrita. |
| `ProductQueries` | Consultas + handlers de leitura (read models). |
| `Program.cs` | Configura DI/MediatR e envia comandos e consultas. |

**Executar:**
```bash
dotnet run --project Cqrs/src/Cqrs
```

> Usa o pacote `MediatR` 12.x (versão de licença gratuita) e
> `Microsoft.Extensions.DependencyInjection`.

---

## 3. Hexagonal (Ports & Adapters)

**Ideia central:** isolar o núcleo de negócio atrás de **portas** (interfaces) e
conectá-lo ao mundo externo por **adaptadores** (implementações). Portas de
entrada expõem o que o núcleo faz; portas de saída descrevem o que ele precisa.

```
Adapters.Cli  ──▶ [ Porta de entrada ]  Core  [ Porta de saída ] ◀── Adapters.Persistence
(driving adapter)     IAccountService    (domínio)  IAccountRepository    (driven adapter)
```

| Projeto | Papel |
|---------|-------|
| `Core` | Domínio (`Account`), portas (`IAccountService`, `IAccountRepository`) e o serviço de aplicação. |
| `Adapters.Persistence` | Adaptador de saída: `InMemoryAccountRepository`. |
| `Adapters.Cli` | Adaptador de entrada: console que pluga tudo e usa a porta de entrada. |

**Executar:**
```bash
dotnet run --project Hexagonal/src/Adapters.Cli
```

---

## 4. DDD tático

**Ideia central:** modelar o domínio com os blocos de construção do
*Domain-Driven Design*, mantendo as regras de negócio dentro do próprio modelo.

| Bloco | Exemplo |
|-------|---------|
| **Aggregate Root** | `Order` — única porta de entrada para alterar o pedido; garante as invariantes. |
| **Entity** | `OrderItem` — identidade própria, acessada só pela raiz. |
| **Value Object** | `Money` — imutável, igualdade estrutural, valida valor não negativo. |
| **Domain Event** | `OrderPlacedEvent` — registra que o pedido foi finalizado. |
| **Repository** | `IOrderRepository` — coleção de agregados (granularidade da raiz). |

**Executar:**
```bash
dotnet run --project Ddd/src/Ddd
```

---

## Observação

Estes exemplos são **didáticos** e propositalmente enxutos: usam persistência em
memória e omitem detalhes de produção (transações, logging, migrações, API HTTP).
O foco é deixar evidente a **organização das dependências** de cada arquitetura.
Padrões puramente operacionais (microservices, serverless, event-driven com
broker real) dependem de infraestrutura externa e ficam fora do escopo destes
projetos autocontidos.
