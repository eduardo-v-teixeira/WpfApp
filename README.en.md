# WpfApp

[Português](README.md) | **English**

A **WPF (.NET 8)** desktop application to register **people**, **products** and **orders**, with the data saved as JSON files. The main window ("Sistema") opens the registration screens, and orders are created from the selected person.

The user interface is in Portuguese; screen and button names below are given in Portuguese with a translation.

## Features

### People (Cadastro de Pessoas)
- **Filters** by name (case-insensitive) and CPF (Brazilian taxpayer ID), applied as the user types, plus a button to clear them.
- **Grid** with Id, Name, CPF and Address.
- **Actions**: Incluir (add), Editar (load the selected person into the fields), Salvar (save), Excluir (delete) and Voltar (back).
- **CPF validation** when adding a person: format, check digits and rejection of CPFs with all digits equal. An invalid CPF shows a warning and the person is not saved.
- **"Incluir Pedido" button**: opens the order screen already linked to the selected person.
- **The person's orders** in a grid with sale date, total, payment method and status, with filters Todos (all), Pendentes (pending), Pagos (paid) and Entregues (delivered, status "Recebido").
- **Status change** of a finalized order with the buttons Marcar como Pago (paid), Marcar como Enviado (shipped) and Marcar como Recebido (received).

### Products (Cadastro de Produtos)
- **Filters** by name, code and value range (minimum and maximum), applied as the user types, plus a button to clear them.
- **Grid** with Id, Name, Code and Value.
- **Actions**: add, edit, save, delete and back.

### Orders (Cadastro de Pedidos)
- Opened with **Incluir Pedido** on the People screen; the header shows the person's name and CPF.
- **Add products** with a quantity; adding the same product again adds to the existing item's quantity.
- **Remover Item** removes the item selected in the items grid (product, unit price, quantity and total).
- **Order total** recalculated on every change and shown as currency.
- **Payment method**: Dinheiro (cash), Cartão (card) or Boleto (Brazilian bank slip).
- **Finalizar Pedido** (finalize): requires at least one item, saves the order with status "Pendente" (pending), the current date and the total, and closes the screen.
- **Voltar** (back) and **Cancelar** (cancel) close the screen without saving; items of an unfinalized order are discarded.

## Tech stack

- **C#** / **.NET 8** (`net8.0-windows`)
- **WPF** (XAML with code-behind)
- **Newtonsoft.Json** 13.0.1 for JSON file persistence
- **LINQ** for filters and totals

## Structure and architecture

```
WpfApp/
├── WpfApp.sln
└── WpfApp/
    ├── Models/       # Pessoa, Produto, Pedido, ItemPedido
    ├── Services/     # PessoaService, ProdutoService, PedidoService (rules and JSON read/write)
    ├── Views/        # PessoaWindow, ProdutoWindow, PedidoWindow (XAML + code-behind)
    ├── ViewModels/   # ProdutoViewModel (not used by the screens yet)
    ├── Data/         # Pessoa.json, Produto.json, Pedido.json
    ├── App.xaml
    └── MainWindow.xaml
```

- **Models** are the entities.
- **Services** hold the rules (CPF validation, order total, status change) and the persistence: each service loads its JSON file when it is created and rewrites the file on every change.
- **Views** handle the UI events in code-behind and call the services.

## How to run

Requirements: **Windows** and the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0). Visual Studio 2022 is optional.

From the repository root:

```bash
dotnet restore WpfApp.sln
dotnet build WpfApp.sln
dotnet run --project WpfApp/WpfApp.csproj
```

You can also open `WpfApp.sln` in Visual Studio and press F5.

Data is stored in `WpfApp/Data/*.json`. The services find that folder by going three levels up from the executable's folder, which matches the default `bin/Debug/net8.0-windows` output.

## How to use

1. In the main window, choose **Cadastro de Pessoas** (people) or **Cadastro de Produtos** (products). The orders button only tells you to start from the People screen.
2. Add products: fill in name, code and value and click **Incluir**.
3. Add a person: fill in name, CPF (validated) and address and click **Incluir**.
4. Select the person in the grid and click **Incluir Pedido**.
5. On the order screen, pick a product, enter the quantity and click **Adicionar Produto**. Repeat for other items, choose the payment method and click **Finalizar Pedido**.
6. Back on the People screen, select a finalized order and use **Marcar como Pago**, **Marcar como Enviado** or **Marcar como Recebido** to update its status.

⚠️ **Note**
- If you click **Voltar** or **Cancelar** before finalizing, the order is discarded.
- A newly finalized order only shows in the person's grid after the People screen is closed and opened again.

## Known limitations

- Only the CPF is validated, and only when adding a person. Name and product fields are not required; an invalid product value is saved as 0.
- There is no database: data lives in local JSON files.
- There are no automated tests.
