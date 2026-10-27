# WpfApp

**Português** | [English](README.en.md)

Aplicação desktop em **WPF (.NET 8)** para cadastro de **pessoas**, **produtos** e **pedidos**, com os dados salvos em arquivos JSON. A janela principal ("Sistema") dá acesso às telas de cadastro, e os pedidos são criados a partir da pessoa selecionada.

## Funcionalidades

### Cadastro de Pessoas
- **Filtros** por Nome (sem diferenciar maiúsculas e minúsculas) e CPF, atualizados enquanto o usuário digita, e botão para limpar os filtros.
- **Grade** com Id, Nome, CPF e Endereço.
- **Ações**: Incluir, Editar (carrega os dados nos campos), Salvar, Excluir e Voltar.
- **Validação de CPF** ao incluir uma pessoa: formato, dígitos verificadores e rejeição de CPFs com todos os dígitos iguais. Um CPF inválido exibe um aviso e a pessoa não é salva.
- **Botão "Incluir Pedido"**: abre a tela de pedidos já vinculada à pessoa selecionada.
- **Pedidos da pessoa** em uma grade com Data da Venda, Valor Total, Forma de Pagamento e Status, com filtros Todos, Pendentes, Pagos e Entregues (status "Recebido").
- **Alteração de status** de um pedido finalizado com os botões Marcar como Pago, Marcar como Enviado e Marcar como Recebido.

### Cadastro de Produtos
- **Filtros** por Nome, Código e faixa de valor (mínimo e máximo), atualizados enquanto o usuário digita, e botão para limpar os filtros.
- **Grade** com Id, Nome, Código e Valor.
- **Ações**: Incluir, Editar, Salvar, Excluir e Voltar.

### Cadastro de Pedidos
- Aberto pelo botão **Incluir Pedido** na tela de Pessoas; o topo mostra o Nome e o CPF da pessoa.
- **Adição de produtos** com quantidade; adicionar de novo o mesmo produto soma a quantidade ao item existente.
- **Remover Item** selecionado na grade de itens (Produto, Valor Unitário, Quantidade e Total).
- **Valor total** recalculado a cada alteração e exibido em formato de moeda.
- **Forma de pagamento**: Dinheiro, Cartão ou Boleto.
- **Finalizar Pedido**: exige pelo menos um item, salva o pedido com status "Pendente", a data atual e o valor total, e fecha a tela.
- **Voltar** e **Cancelar** fecham a tela sem salvar; itens de um pedido não finalizado são descartados.

## Tecnologias

- **C#** / **.NET 8** (`net8.0-windows`)
- **WPF** (XAML com code-behind)
- **Newtonsoft.Json** 13.0.1 para persistência em arquivos JSON
- **LINQ** para filtros e totais

## Estrutura e arquitetura

```
WpfApp/
├── WpfApp.sln
└── WpfApp/
    ├── Models/       # Pessoa, Produto, Pedido, ItemPedido
    ├── Services/     # PessoaService, ProdutoService, PedidoService (regras e leitura/gravação dos JSON)
    ├── Views/        # PessoaWindow, ProdutoWindow, PedidoWindow (XAML + code-behind)
    ├── ViewModels/   # ProdutoViewModel (ainda não utilizado pelas telas)
    ├── Data/         # Pessoa.json, Produto.json, Pedido.json
    ├── App.xaml
    └── MainWindow.xaml
```

- **Models** representam as entidades.
- **Services** concentram as regras (validação de CPF, cálculo do total do pedido, alteração de status) e a persistência: cada serviço carrega o seu arquivo JSON ao ser criado e regrava o arquivo a cada alteração.
- **Views** tratam os eventos da interface no code-behind e chamam os serviços.

## Como executar

Requisitos: **Windows** e [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0). O Visual Studio 2022 é opcional.

Na raiz do repositório:

```bash
dotnet restore WpfApp.sln
dotnet build WpfApp.sln
dotnet run --project WpfApp/WpfApp.csproj
```

Também é possível abrir `WpfApp.sln` no Visual Studio e executar com F5.

Os dados ficam em `WpfApp/Data/*.json`. Os serviços localizam essa pasta subindo três níveis a partir da pasta do executável, o que corresponde à saída padrão `bin/Debug/net8.0-windows`.

## Como usar

1. Na janela principal, escolha **Cadastro de Pessoas** ou **Cadastro de Produtos**. O botão de pedidos apenas orienta a iniciar pela tela de Pessoas.
2. Cadastre os produtos: preencha Nome, Código e Valor e clique em **Incluir**.
3. Cadastre uma pessoa: preencha Nome, CPF (validado) e Endereço e clique em **Incluir**.
4. Selecione a pessoa na grade e clique em **Incluir Pedido**.
5. Na tela de pedidos, escolha um produto, informe a quantidade e clique em **Adicionar Produto**. Repita para outros itens, escolha a forma de pagamento e clique em **Finalizar Pedido**.
6. De volta à tela de Pessoas, selecione um pedido finalizado e use os botões **Marcar como Pago**, **Marcar como Enviado** ou **Marcar como Recebido** para atualizar o status.

⚠️ **Atenção**
- Se o usuário clicar em **Voltar** ou **Cancelar** antes de finalizar, o pedido é descartado.
- Um pedido recém-finalizado só aparece na grade da pessoa depois que a tela de Cadastro de Pessoas é fechada e aberta novamente.

## Limitações conhecidas

- Apenas o CPF é validado, e só na inclusão. Nome e campos de produto não são obrigatórios; um valor de produto inválido é salvo como 0.
- Não há banco de dados: os dados ficam nos arquivos JSON locais.
- Não há testes automatizados.
