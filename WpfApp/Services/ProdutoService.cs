using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class ProdutoService
    {
        private readonly string FilePath;
        private List<Produto> produtos;

        public ProdutoService()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";
            var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));
            var dataDirectory = Path.Combine(projectDirectory, "Data");
            FilePath = Path.Combine(dataDirectory, "Produto.json");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                produtos = JsonConvert.DeserializeObject<List<Produto>>(json) ?? new List<Produto>();
            }
            else
            {
                produtos = new List<Produto>();
                SaveChanges();
            }
        }

        public List<Produto> GetAll() => produtos;

        public void Add(Produto produto)
        {
            produto.Id = Guid.NewGuid();
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

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(produtos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }


    }
}
