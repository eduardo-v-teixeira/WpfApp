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
        public string FormaPagamento { get; set; }
        public string Status { get; set; }
        public bool Finalizado { get; set; } = false;
    }

    public class ItemPedido
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal => ValorUnitario * Quantidade;
    }
}
