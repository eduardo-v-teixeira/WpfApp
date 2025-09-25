using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PedidoViewModel : INotifyPropertyChanged
    {
        private readonly PedidoService pedidoService;
        private readonly PessoaService pessoaService;
        private readonly ProdutoService produtoService;

        public PedidoViewModel()
        {
            pedidoService = new PedidoService();
            pessoaService = new PessoaService();
            produtoService = new ProdutoService();

            Pessoas = new ObservableCollection<Pessoa>(pessoaService.GetAll());
            Produtos = new ObservableCollection<Produto>(produtoService.GetAll());
            ItensPedido = new ObservableCollection<ItemPedido>();

            AdicionarProdutoCommand = new RelayCommand(AdicionarProduto);
            FinalizarPedidoCommand = new RelayCommand(FinalizarPedido);
        }

        private Pessoa pessoaSelecionada;
        public Pessoa PessoaSelecionada
        {
            get => pessoaSelecionada;
            set { pessoaSelecionada = value; OnPropertyChanged(nameof(PessoaSelecionada)); }
        }

        private Produto produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get => produtoSelecionado;
            set { produtoSelecionado = value; OnPropertyChanged(nameof(ProdutoSelecionado)); }
        }

        private int quantidade = 1;
        public int Quantidade
        {
            get => quantidade;
            set { quantidade = value; OnPropertyChanged(nameof(Quantidade)); }
        }

        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<ItemPedido> ItensPedido { get; set; }

        public decimal ValorTotal => ItensPedido.Sum(i => i.Subtotal);

        // Comandos
        public ICommand AdicionarProdutoCommand { get; set; }
        public ICommand FinalizarPedidoCommand { get; set; }

        private void AdicionarProduto()
        {
            if (ProdutoSelecionado != null && Quantidade > 0)
            {
                var item = new ItemPedido
                {
                    Produto = ProdutoSelecionado,
                    Quantidade = Quantidade
                };
                ItensPedido.Add(item);
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        private void FinalizarPedido()
        {
            if (PessoaSelecionada == null || !ItensPedido.Any()) return;

            var pedido = new Pedido
            {
                Pessoa = PessoaSelecionada,
                Produtos = ItensPedido.ToList(),
                Status = Status.Pendente,
                DataVenda = DateTime.Now,
                FormaPagamento = "Dinheiro" 
            };

            pedidoService.AddPedido(pedido);

            // Limpar tela
            ItensPedido.Clear();
            PessoaSelecionada = null;
            ProdutoSelecionado = null;
            Quantidade = 1;
            OnPropertyChanged(nameof(ValorTotal));
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
