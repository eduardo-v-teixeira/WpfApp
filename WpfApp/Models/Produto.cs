using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal Valor { get; set; }

        public List<Produto> ObterProdutos()
        {
            List<Produto> listaProdutos = new List<Produto>
            {
                new Produto { Id = Guid.NewGuid(), Nome = "Produto A", Codigo = "A001", Valor = 10.0m },
                new Produto { Id = Guid.NewGuid(), Nome = "Produto B", Codigo = "B002", Valor = 20.0m },
                new Produto { Id = Guid.NewGuid(), Nome = "Produto C", Codigo = "C003", Valor = 30.0m },
            };
            return listaProdutos;
        }

    }
}
