using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Views
{
    public partial class PedidoWindow : Window
    {
        private readonly ProdutoService produtoService;
        private readonly PedidoService pedidoService;
        private readonly Pessoa pessoa;
        private List<ItemPedido> itensPedido;

        public PedidoWindow(Pessoa pessoa)
        {
            InitializeComponent();
            this.pessoa = pessoa;
            produtoService = new ProdutoService();
            pedidoService = new PedidoService();
            itensPedido = new List<ItemPedido>();

            InicializarTela();
        }

        private void InicializarTela()
        {
            lblPessoa.Content = $"{pessoa.Nome} - {pessoa.CPF}";
            cbProdutos.ItemsSource = produtoService.GetAll();
            cbFormaPagamento.SelectedIndex = 0;
            AtualizarGrid();
        }

        private void BtnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (cbProdutos.SelectedItem is Produto produto && int.TryParse(txtQuantidade.Text, out int quantidade) && quantidade > 0)
            {
                var itemExistente = itensPedido.FirstOrDefault(i => i.ProdutoId == produto.Id);

                if (itemExistente != null)
                {
                    itemExistente.Quantidade += quantidade;
                }
                else
                {
                    var novoItem = new ItemPedido
                    {
                        ProdutoId = produto.Id,
                        NomeProduto = produto.Nome,
                        ValorUnitario = produto.Valor,
                        Quantidade = quantidade
                    };
                    itensPedido.Add(novoItem);
                }

                txtQuantidade.Text = "";
                AtualizarGrid();
                AtualizarTotal();
            }
            else
            {
                MessageBox.Show("Selecione um produto e informe uma quantidade válida!");
            }
        }

        private void BtnRemoverItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgItensPedido.SelectedItem is ItemPedido item)
            {
                itensPedido.Remove(item);
                AtualizarGrid();
                AtualizarTotal();
            }
            else
            {
                MessageBox.Show("Selecione um item para remover!");
            }
        }

        private void BtnFinalizarPedido_Click(object sender, RoutedEventArgs e)
        {
            if (!itensPedido.Any())
            {
                MessageBox.Show("Adicione pelo menos um produto ao pedido!");
                return;
            }

            if (cbFormaPagamento.SelectedItem is ComboBoxItem formaPagamento)
            {
                var novoPedido = new Pedido
                {
                    PessoaId = pessoa.Id,
                    Produtos = new List<ItemPedido>(itensPedido),
                    FormaPagamento = formaPagamento.Content.ToString() ?? "",
                    Status = "Pendente"
                };

                pedidoService.Add(novoPedido);
                MessageBox.Show("Pedido finalizado com sucesso!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Selecione uma forma de pagamento!");
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AtualizarGrid()
        {
            dgItensPedido.ItemsSource = null;
            dgItensPedido.ItemsSource = itensPedido;
        }

        private void AtualizarTotal()
        {
            var total = itensPedido.Sum(i => i.ValorTotal);
            lblTotal.Content = total.ToString("C");
        }
    }
}
