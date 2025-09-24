using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;//vai ocorrer quando uma propriedade mudar
                                                                  //Manipular o evento NotifyPropertyChanged
        public Produto? Produto { get; set; }
        public void NotifyPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
