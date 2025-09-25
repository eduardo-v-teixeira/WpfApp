using System;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Views
{
    public partial class ProdutoWindow : Window
    {
        private readonly ProdutoService produtoService;
        private Produto? produtoSelecionado;

        public ProdutoWindow()
        {
            InitializeComponent();
            produtoService = new ProdutoService();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            var produtos = produtoService.GetAll();

            // Aplicar filtros apenas se os controles existirem
            if (txtFiltroNome != null && !string.IsNullOrEmpty(txtFiltroNome.Text))
            {
                produtos = produtos.Where(p => p.Nome.Contains(txtFiltroNome.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (txtFiltroCodigo != null && !string.IsNullOrEmpty(txtFiltroCodigo.Text))
            {
                produtos = produtos.Where(p => p.Codigo.Contains(txtFiltroCodigo.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (txtFiltroValorMin != null && decimal.TryParse(txtFiltroValorMin.Text, out decimal valorMin))
            {
                produtos = produtos.Where(p => p.Valor >= valorMin).ToList();
            }

            if (txtFiltroValorMax != null && decimal.TryParse(txtFiltroValorMax.Text, out decimal valorMax))
            {
                produtos = produtos.Where(p => p.Valor <= valorMax).ToList();
            }

            dgProdutos.ItemsSource = null;
            dgProdutos.ItemsSource = produtos;
        }

        private void BtnIncluir_Click(object sender, RoutedEventArgs e)
        {
            var novoProduto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = txtNome.Text,
                Codigo = txtCodigo.Text,
                Valor = decimal.TryParse(txtValor.Text, out var v) ? v : 0
            };

            produtoService.Add(novoProduto);
            CarregarProdutos();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            produtoSelecionado = dgProdutos.SelectedItem as Produto;
            if (produtoSelecionado != null)
            {
                txtNome.Text = produtoSelecionado.Nome;
                txtCodigo.Text = produtoSelecionado.Codigo;
                txtValor.Text = produtoSelecionado.Valor.ToString();
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (produtoSelecionado != null)
            {
                produtoSelecionado.Nome = txtNome.Text;
                produtoSelecionado.Codigo = txtCodigo.Text;
                produtoSelecionado.Valor = decimal.TryParse(txtValor.Text, out var v) ? v : 0;

                produtoService.Update(produtoSelecionado);
                produtoSelecionado = null;
                CarregarProdutos();
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (dgProdutos.SelectedItem is Produto produto)
            {
                produtoService.Delete(produto.Id);
                CarregarProdutos();
            }
        }

        // Eventos de filtro
        private void FiltroChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CarregarProdutos();
        }

        private void BtnLimparFiltros_Click(object sender, RoutedEventArgs e)
        {
            txtFiltroNome.Text = "";
            txtFiltroCodigo.Text = "";
            txtFiltroValorMin.Text = "";
            txtFiltroValorMax.Text = "";
            CarregarProdutos();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
