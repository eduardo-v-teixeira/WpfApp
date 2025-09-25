using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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