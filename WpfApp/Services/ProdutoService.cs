using Newtonsoft.Json; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using WpfApp.Models;


//Já temos o arquivo JSON vazio (a base de dados).
// O ProductService já sabe manipular ele.
// Quando adicionarmos produtos pela tela, eles vão direto para o produtos.json.

namespace WpfApp.Services
{
    //Foi realizado Ações: Incluir, Editar, Salvar, Excluir
    public class ProdutoService
    {   //Ele já está preparado para ler e gravar automaticamente nesse JSON sempre que usar Add, Update ou Delete.
        private const string FilePath = "Data/produtos.json"; 
        private List<Produto> produtos;

        public ProdutoService()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                produtos = JsonConvert.DeserializeObject<List<Produto>>(json) ?? new List<Produto>();
            }
            else
            {
                produtos = new List<Produto>();
            }
        }

        public List<Produto> GetAll() => produtos;

        public void Add(Produto produto)
        {
            // gerar Id automático
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
