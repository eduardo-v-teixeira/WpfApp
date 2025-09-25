using System;
using System.Collections.Generic;

namespace WpfApp.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public Guid PessoaId { get; set; }
        public List<ItemPedido> Produtos { get; set; } = new List<ItemPedido>();
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
        public string Status { get; set; } = "Pendente";
        public bool Finalizado { get; set; } = false;
    }

    public class ItemPedido
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal => ValorUnitario * Quantidade;
    }
}
