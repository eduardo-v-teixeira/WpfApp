using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class ProdutoService
    {
        private readonly string dataDirectory;
        private readonly string filePath;
        private List<Produto> produtos;

        public ProdutoService()
        {
            dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            filePath = Path.Combine(dataDirectory, "produtos.json");

            // garante que a pasta existe
            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            Load();
        }

        private void Load()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                produtos = JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
            }
            else
            {
                produtos = new List<Produto>();
            }
        }

        private void SaveChanges()
        {
            var json = JsonSerializer.Serialize(produtos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public List<Produto> GetAll() => produtos;

        public void Add(Produto produto)
        {
            produtos.Add(produto);
            SaveChanges();
        }

        public void Update(Produto produto)
        {
            var existing = produtos.FirstOrDefault(p => p.Id == produto.Id);
            if (existing != null)
            {
                existing.Nome = produto.Nome;
                existing.Codigo = produto.Codigo;
                existing.Valor = produto.Valor;
                SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                produtos.Remove(produto);
                SaveChanges();
            }
        }
    }
}
