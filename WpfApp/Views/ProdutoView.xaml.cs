using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Views
{
    /// <summary>
    /// Lógica interna para ProdutoView.xaml
    /// </summary>
    /// 

    //Criando os eventos dos botões Incluir, Editar, Salvar e Excluir
    public partial class ProdutoView : Window
    {
        private readonly ProdutoService _service;
        private Produto produtoAtual;

        public ProdutoView()
        {
            InitializeComponent();
            _service = new ProdutoService();
            CarregarProdutos();
        }

        private void CarregarProdutos()
        {
            dgProdutos.ItemsSource = null;
            dgProdutos.ItemsSource = _service.GetAll();
        }
        private void btnIncluir_Click(object sender, RoutedEventArgs e)
        {
            produtoAtual = new Produto()
            {
                Id = Guid.NewGuid(),
                Nome = txtNome.Text,
                Codigo = txtCodigo.Text,
                Valor = decimal.TryParse(txtValor.Text, out decimal valor) ? valor : 0
            };

            _service.Add(produtoAtual);
            CarregarProdutos();
            LimparCampos();
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProdutos.SelectedItem is Produto produto)
            {
                produtoAtual = produto;
                txtNome.Text = produto.Nome;
                txtCodigo.Text = produto.Codigo;
                txtValor.Text = produto.Valor.ToString();
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.");
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (produtoAtual != null)
            {
                produtoAtual.Nome = txtNome.Text;
                produtoAtual.Codigo = txtCodigo.Text;
                produtoAtual.Valor = decimal.TryParse(txtValor.Text, out decimal valor) ? valor : 0;

                _service.Update(produtoAtual);
                CarregarProdutos();
                LimparCampos();
                produtoAtual = null;
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (dgProdutos.SelectedItem is Produto produto)
            {
                _service.Delete(produto.Id);
                CarregarProdutos();
            }
            else
            {
                MessageBox.Show("Selecione um produto para excluir.");
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtValor.Text = string.Empty;
        }
    }
}
