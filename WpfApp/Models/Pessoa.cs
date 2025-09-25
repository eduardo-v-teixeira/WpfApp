using System;

namespace WpfApp.Models
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
    }
}
