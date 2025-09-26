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

        public PedidoService() //objetivo inicializar o FilePath e a lista pedidos
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location; //vai inferir onde o assembly está localizado
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? ""; // ter a pasta base da assembly para montar o caminho do projeto
            var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));// vai subir três niveis para chegar na raiz do projeto
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

        public List<Pedido> GetAll() => pedidos; // vai retonar todos os pedidos

        public List<Pedido> GetByPessoa(Guid pessoaId) => pedidos.Where(p => p.PessoaId == pessoaId).ToList(); // retonar os pedidos da pessoa

        public void Add(Pedido pedido) //vai adicionar, caulcularTotal, definir a data e finalizar
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
