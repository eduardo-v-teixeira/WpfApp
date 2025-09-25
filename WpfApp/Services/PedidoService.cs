using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PedidoService
    {
        private readonly string FilePath;
        private List<Pedido> pedidos;

        public PedidoService()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";
            var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));
            var dataDirectory = Path.Combine(projectDirectory, "Data");
            FilePath = Path.Combine(dataDirectory, "Pedido.json");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json) ?? new List<Pedido>();
            }
            else
            {
                pedidos = new List<Pedido>();
                SaveChanges();
            }
        }

        public List<Pedido> GetAll() => pedidos;

        public List<Pedido> GetByPessoa(Guid pessoaId) => pedidos.Where(p => p.PessoaId == pessoaId).ToList();

        public void Add(Pedido pedido)
        {
            pedido.Id = Guid.NewGuid();
            pedido.DataVenda = DateTime.Now;
            pedido.ValorTotal = pedido.Produtos.Sum(p => p.ValorTotal);
            pedido.Finalizado = true;
            pedidos.Add(pedido);
            SaveChanges();
        }

        public void UpdateStatus(Guid id, string novoStatus)
        {
            var existing = pedidos.FirstOrDefault(p => p.Id == id);
            if (existing != null)
            {
                existing.Status = novoStatus;
                SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
            if (pedido != null)
            {
                pedidos.Remove(pedido);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}
