using System.Windows;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnAbrirPessoas_Click(object sender, RoutedEventArgs e)
        {
            var pessoaWindow = new PessoaWindow();
            pessoaWindow.Show();
        }

        private void BtnAbrirProdutos_Click(object sender, RoutedEventArgs e)
        {
            var produtoWindow = new ProdutoWindow();
            produtoWindow.Show();
        }

        private void BtnAbrirPedidos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Para criar pedidos, acesse o Cadastro de Pessoas e selecione uma pessoa.");
        }

    }
}