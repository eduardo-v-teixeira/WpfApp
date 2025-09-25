using System;
using System.Linq;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.Views
{
    public partial class PessoaWindow : Window
    {
        private readonly PessoaService pessoaService;
        private readonly PedidoService pedidoService;
        private Pessoa? pessoaSelecionada;
        private string filtroStatusPedido = "Todos";

        public PessoaWindow()
        {
            InitializeComponent();
            pessoaService = new PessoaService();
            pedidoService = new PedidoService();
            filtroStatusPedido = "Todos"; // Garantir inicialização
            CarregarPessoas();
        }

        private void CarregarPessoas()
        {
            var pessoas = pessoaService.GetAll();

            // Aplicar filtros apenas se os controles existirem
            if (txtFiltroNome != null && !string.IsNullOrEmpty(txtFiltroNome.Text))
            {
                pessoas = pessoas.Where(p => p.Nome.Contains(txtFiltroNome.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (txtFiltroCPF != null && !string.IsNullOrEmpty(txtFiltroCPF.Text))
            {
                pessoas = pessoas.Where(p => p.CPF.Contains(txtFiltroCPF.Text)).ToList();
            }

            dgPessoas.ItemsSource = null;
            dgPessoas.ItemsSource = pessoas;
        }

        private void CarregarPedidosPessoa()
        {
            if (pessoaSelecionada != null)
            {
                var pedidos = pedidoService.GetByPessoa(pessoaSelecionada.Id);

                // Aplicar filtro de status
                switch (filtroStatusPedido)
                {
                    case "Pendentes":
                        pedidos = pedidos.Where(p => p.Status == "Pendente").ToList();
                        break;
                    case "Pagos":
                        pedidos = pedidos.Where(p => p.Status == "Pago").ToList();
                        break;
                    case "Entregues":
                        pedidos = pedidos.Where(p => p.Status == "Recebido").ToList();
                        break;
                }

                dgPedidos.ItemsSource = null;
                dgPedidos.ItemsSource = pedidos;
            }
        }

        private void BtnIncluir_Click(object sender, RoutedEventArgs e)
        {
            var novaPessoa = new Pessoa
            {
                Nome = txtNome.Text,
                CPF = txtCPF.Text,
                Endereco = txtEndereco.Text
            };

            pessoaService.Add(novaPessoa);
            CarregarPessoas();
            LimparCampos();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            pessoaSelecionada = dgPessoas.SelectedItem as Pessoa;
            if (pessoaSelecionada != null)
            {
                txtNome.Text = pessoaSelecionada.Nome;
                txtCPF.Text = pessoaSelecionada.CPF;
                txtEndereco.Text = pessoaSelecionada.Endereco;
                CarregarPedidosPessoa();
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (pessoaSelecionada != null)
            {
                pessoaSelecionada.Nome = txtNome.Text;
                pessoaSelecionada.CPF = txtCPF.Text;
                pessoaSelecionada.Endereco = txtEndereco.Text;

                pessoaService.Update(pessoaSelecionada);
                pessoaSelecionada = null;
                CarregarPessoas();
                LimparCampos();
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (dgPessoas.SelectedItem is Pessoa pessoa)
            {
                pessoaService.Delete(pessoa.Id);
                CarregarPessoas();
                LimparCampos();
            }
        }

        private void BtnIncluirPedido_Click(object sender, RoutedEventArgs e)
        {
            if (dgPessoas.SelectedItem is Pessoa pessoa)
            {
                var pedidoWindow = new PedidoWindow(pessoa);
                pedidoWindow.ShowDialog();
                CarregarPedidosPessoa();
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa primeiro!");
            }
        }

        private void BtnMarcarPago_Click(object sender, RoutedEventArgs e)
        {
            AlterarStatusPedido("Pago");
        }

        private void BtnMarcarEnviado_Click(object sender, RoutedEventArgs e)
        {
            AlterarStatusPedido("Enviado");
        }

        private void BtnMarcarRecebido_Click(object sender, RoutedEventArgs e)
        {
            AlterarStatusPedido("Recebido");
        }

        private void AlterarStatusPedido(string novoStatus)
        {
            if (dgPedidos.SelectedItem is Pedido pedido)
            {
                if (!pedido.Finalizado)
                {
                    MessageBox.Show("Este pedido não foi finalizado e não pode ter o status alterado!");
                    return;
                }

                pedidoService.UpdateStatus(pedido.Id, novoStatus);
                CarregarPedidosPessoa();
                MessageBox.Show($"Status do pedido alterado para: {novoStatus}");
            }
            else
            {
                MessageBox.Show("Selecione um pedido primeiro!");
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtEndereco.Text = "";
            dgPedidos.ItemsSource = null;
        }

        // Eventos de filtro
        private void FiltroChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CarregarPessoas();
        }

        private void BtnLimparFiltros_Click(object sender, RoutedEventArgs e)
        {
            txtFiltroNome.Text = "";
            txtFiltroCPF.Text = "";
            CarregarPessoas();
        }

        private void BtnFiltroTodos_Click(object sender, RoutedEventArgs e)
        {
            filtroStatusPedido = "Todos";
            CarregarPedidosPessoa();
        }

        private void BtnFiltroPendentes_Click(object sender, RoutedEventArgs e)
        {
            filtroStatusPedido = "Pendentes";
            CarregarPedidosPessoa();
        }

        private void BtnFiltroPagos_Click(object sender, RoutedEventArgs e)
        {
            filtroStatusPedido = "Pagos";
            CarregarPedidosPessoa();
        }

        private void BtnFiltroEntregues_Click(object sender, RoutedEventArgs e)
        {
            filtroStatusPedido = "Entregues";
            CarregarPedidosPessoa();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Evento quando seleciona uma pessoa no grid
        private void DgPessoas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgPessoas.SelectedItem is Pessoa pessoa)
            {
                pessoaSelecionada = pessoa;
                CarregarPedidosPessoa();
            }
            else
            {
                pessoaSelecionada = null;
                dgPedidos.ItemsSource = null;
            }
        }
    }
}
