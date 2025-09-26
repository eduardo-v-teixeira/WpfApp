#WpfApp

## Projeto de Cadastro e Manipulação de Dados em WPF (.NET Framework 4.6)

## Funcionalidades Implementadas

### 1. Cadastro de Pessoas
- **Filtros**: Pesquisa por Nome e CPF.
- **Grid de Registros**: Exibe todas as pessoas cadastradas.
- **Ações**: Incluir, Editar, Salvar, Excluir pessoas.
- **Botão "Incluir Pedido"**: Abre a tela de Pedidos, já vinculada à pessoa selecionada.
- **Grelha de Pedidos da Pessoa**: Exibe os pedidos feitos pela pessoa selecionada, com filtros por status (Entregues, Pagos, Pendentes).
- **Ações por Pedido**: Permite alterar o status do pedido (Marcar como Pago, Enviado, Recebido).
- **Validação**: Validação de CPF (formato e dígitos verificadores) e campos obrigatórios.

### 2. Cadastro de Produtos
- **Filtros**: Pesquisa por Nome, Código e Faixa de Valor (valor inicial e final).
- **Grid de Registros**: Exibe todos os produtos cadastrados.
- **Ações**: Incluir, Editar, Salvar, Excluir produtos.
- **Validação**: Campos obrigatórios.

### 3. Cadastro de Pedidos
- **Para vincular pedidos a pessoas, tem que estar na tela de Pessoas e clicar no botão "Incluir Pedido".
- **Seleção de Pessoa**: Permite vincular o pedido a uma pessoa existente.
- **Adição de Produtos**: Permite adicionar múltiplos produtos ao pedido, especificando a quantidade de cada um.
- **Cálculo de Valor Total**: Calculado automaticamente conforme os produtos são aumentados/removidos.
- **Forma de Pagamento**: Seleção entre Dinheiro, Cartão, Boleto.
- **Botão "Finalizar Pedido"**: Salva o pedido, define o status como "Pendente" e bloqueia a edição.
- **Descarte de Pedido**: Pedidos não finalizados são descartados ao sair da tela.

## Arquitetura e Tecnologias
- **Linguagem**: C#
- **Interface Gráfica**: WPF (Windows Presentation Foundation)
- **Padrão de Projeto**: MVVM (Model-View-ViewModel) para separação de responsabilidades e testabilidade.
- **Persistência de Dados**: Arquivos JSON, utilizando a biblioteca `Newtonsoft.Json`.
- **Manipulação de Dados**: LINQ (Language Integrated Query) para consultas e operações nas coleções de dados.
- **Organização de Código**: Utilização de `Services` para lógica de negócio e persistência, `ViewModels` para lógica de apresentação, e `Models` para entidades de domínio.


## Instruções de execução
- ** Ao iniciar o projeto, a tela principal apresenta três opções: "Cadastro de Pessoas", "Cadastro de Produtos" e "Cadastro de Pedidos".

## Instruções Cadastro de Pessoas
- ** Na tela de Cadastro de Pessoas, no canto superior esquerdo estão disponíveis os filtros de pesquisa por Nome e CPF, acompanhados de um botão para limpar os filtros aplicados.
Logo abaixo encontra-se a área de cadastro, onde devem ser preenchidos os campos: Nome (obrigatório), CPF (obrigatório, com validação) e Endereço (opcional).

- ** Ainda nessa tela, estão disponíveis os botões de ação: Voltar para a tela anterior, Incluir, Editar, Salvar, Excluir e Incluir Pedido.

- ** Abaixo está uma grade com os registros das pessoas cadastradas, mostrando ID, Nome, CPF e Endereço.
Quando uma pessoa é selecionada, a parte inferior da tela exibe os pedidos a ela, em uma grade que apresenta os campos Dados de Venda, Valor Total, Forma de Pagamento e Status.

- **Os pedidos podem ser filtrados por status, sendo possível visualizar Todos, Pendentes, Pagos ou Entregues.
Logo abaixo da grade, há botões que permitem alterar o status de um pedido para Pago, Enviado ou Recebido, de acordo com o andamento da operação.

## Instruções Cadastro de Produtos

- ** Na tela de Cadastro de Produtos, na parte superior esquerda, estão disponíveis os filtros de pesquisa por Nome,
Código, Valor Mínimo e Valor Máximo, acompanhados de um botão para limpar os filtros aplicados.

- ** Logo abaixo encontra-se a área de cadastro, onde devem ser preenchidos os campos: Nome, Código e Valor.

- ** Por fim, a tela exibe uma grade que apresenta os produtos cadastrados, listando as colunas ID, Nome, Código e Valor.

## Instruções Cadastro de Pedidos

- ** Para acessar a tela de Cadastro de Pedidos, é necessário primeiro selecionar uma pessoa dentro da tela de Cadastro de Pessoas.
Após criar ou selecionar a pessoa desejada, basta clicar em Incluir Pedido, o que direciona para a tela de pedidos.

- ** Na tela de Cadastro de Pedidos, no topo é exibido a pessoa selecionada, mostrando seu Nome e CPF.

- **Logo abaixo, há a opção de escolha dos produtos cadastrados. Após selecionar um produto e informar a quantidade desejada,
basta clicar em Adicionar Produto. O item será exibido na grade da tela, contendo as informações de Nome do Produto,
Valor Unitário, Quantidade e Total.

- ** Abaixo da grade existe o botão Remover Item, que exige a seleção de um item na grade para ser excluído.

- ** Em seguida, há o campo de seleção da Forma de Pagamento, que pode ser Dinheiro, Cartão ou Boleto,
acompanhado do Valor Total calculado automaticamente.

- ** Na parte inferior da tela estão os botões: Voltar, Finalizar Pedido e Cancelar.

⚠️Atenção:

- Se um produto for adicionado, mas o usuário clicar em Voltar antes de finalizar o pedido, ele será descartado.
- Se o usuário clicar em Cancelar, o pedido também será cancelado.
- Após finalizar o pedido, é necessário sair da tela de Cadastro de Pedidos e retornar à tela de Cadastro de Pessoas.
Somente assim o pedido será enviado na grade de pedidos à pessoa selecionada.
