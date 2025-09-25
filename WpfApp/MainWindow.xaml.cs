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
            Main.Content = new ProdutoWindow();
            Main.Content = new ();
            Main.Content = new ();

        }
        //Botão para abrir a janela de produtos
        private void BtnAbrirProduto_Click(object sender, RoutedEventArgs e)
        {
            var produtoView = new ProdutoWindow();
            produtoView.Show();
        }


    }
}