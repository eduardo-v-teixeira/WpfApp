using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PessoaService
    {
        private readonly string FilePath;
        private List<Pessoa> pessoas;

        public PessoaService()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";
            var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));
            var dataDirectory = Path.Combine(projectDirectory, "Data");
            FilePath = Path.Combine(dataDirectory, "Pessoa.json");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                pessoas = JsonConvert.DeserializeObject<List<Pessoa>>(json) ?? new List<Pessoa>();
            }
            else
            {
                pessoas = new List<Pessoa>();
                SaveChanges();
            }
        }

        public List<Pessoa> GetAll() => pessoas;

        public void Add(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid();
            pessoas.Add(pessoa);
            SaveChanges();
        }

        public void Update(Pessoa pessoa)
        {
            var existing = pessoas.FirstOrDefault(p => p.Id == pessoa.Id);
            if (existing != null)
            {
                existing.Nome = pessoa.Nome;
                existing.CPF = pessoa.CPF;
                existing.Endereco = pessoa.Endereco;
                SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa != null)
            {
                pessoas.Remove(pessoa);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(pessoas, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}
