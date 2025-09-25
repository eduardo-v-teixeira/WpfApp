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


        public PedidoService() // construtor é execultado quando a classe é instanciada
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
        // Salva os pedidos no arquivo JSON
        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(pedidos, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        //Criando o Crud de pedidos
        // Adiciona um novo pedido
        public void AddPedido(Pedido pedido)
        {
            pedido.Id = Guid.NewGuid();
            pedido.Finalizado = true;
            pedidos.Add(pedido);
            SaveChanges();
        }

        // Atualiza um pedido existente
        public void UpdatePedido(Pedido pedido)
        {
            var index = pedidos.FindIndex(p => p.Id == pedido.Id);
            if (index >= 0)
            {
                pedidos[index] = pedido;
                SaveChanges();
            }
        }

        // Remove um pedido
        public void RemovePedido(Pedido pedido)
        {
            pedidos.RemoveAll(p => p.Id == pedido.Id);
            SaveChanges();
        }

        // Filtrar pedidos por status
        public List<Pedido> GetByStatus(Status status)
        {
            return pedidos.Where(p => p.Status == status).ToList();
        }

        // Filtrar pedidos de uma pessoa específica
        public List<Pedido> GetByPessoa(Guid pessoaId)
        {
            return pedidos.Where(p => p.Pessoa != null && p.Pessoa.Id == pessoaId).ToList();
        }
    }
}