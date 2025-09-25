using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class PessoaViewModel : INotifyPropertyChanged
    {
        private readonly PessoaService pessoaService;

        public PessoaViewModel()
        {
            pessoaService = new PessoaService();

            Pessoas = new ObservableCollection<Pessoa>(pessoaService.GetAll());

            SalvarCommand = new RelayCommand(Salvar);
            ExcluirCommand = new RelayCommand(Excluir);
        }

        // Campos da Pessoa
        private string nome;
        public string Nome
        {
            get => nome;
            set { nome = value; OnPropertyChanged(nameof(Nome)); }
        }

        private int cpf;
        public int Cpf
        {
            get => cpf;
            set { cpf = value; OnPropertyChanged(nameof(Cpf)); }
        }

        private string endereco;
        public string Endereco
        {
            get => endereco;
            set { endereco = value; OnPropertyChanged(nameof(Endereco)); }
        }

        // Lista de pessoas
        public ObservableCollection<Pessoa> Pessoas { get; set; }

        private Pessoa pessoaSelecionada;
        public Pessoa PessoaSelecionada
        {
            get => pessoaSelecionada;
            set { pessoaSelecionada = value; OnPropertyChanged(nameof(PessoaSelecionada)); }
        }
        public ICommand SalvarCommand { get; set; }
        public ICommand ExcluirCommand { get; set; }

        private void Salvar()
        {
            if (PessoaSelecionada == null)
            {
                var pessoa = new Pessoa
                {
                    Nome = this.Nome,
                    Cpf = this.Cpf,
                    Endereco = this.Endereco
                };

                pessoaService.AddPessoa(pessoa);
                Pessoas.Add(pessoa);
            }
            else
            {
                PessoaSelecionada.Nome = Nome;
                PessoaSelecionada.Cpf = Cpf;
                PessoaSelecionada.Endereco = Endereco;

                pessoaService.UpdatePessoa(PessoaSelecionada);
                var index = Pessoas.IndexOf(PessoaSelecionada);
                Pessoas[index] = PessoaSelecionada;
            }

            Nome = string.Empty;
            Cpf = 0;
            Endereco = string.Empty;
            PessoaSelecionada = null;
        }

        private void Excluir()
        {
            if (PessoaSelecionada != null)
            {
                pessoaService.RemovePessoa(PessoaSelecionada);
                Pessoas.Remove(PessoaSelecionada);

                Nome = string.Empty;
                Cpf = 0;
                Endereco = string.Empty;
                PessoaSelecionada = null;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
