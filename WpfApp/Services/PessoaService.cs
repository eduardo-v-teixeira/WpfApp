using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Services
{
    class PessoaService
    {
        private readonly string FilePath;
        private List<Pessoa> pessoas;

        public PessoaService()
        {
            // Caminho do arquivo JSON
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation) ?? "";
            var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", ".."));
            var dataDirectory = Path.Combine(projectDirectory, "data");
            FilePath = Path.Combine(dataDirectory, "pessoas.json");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                pessoas = System.Text.Json.JsonSerializer.Deserialize<List<Pessoa>>(json) ?? new List<Pessoa>();
            }
            else
            {
                pessoas = new List<Pessoa>();
                SaveChanges();
            }


        }
        // salvar os dados no json
        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(Pessoa, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void AddPessoa(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            SaveChanges();
        }
        public void UpdatePessoa(Pessoa pessoa)
        {
            var index = pessoas.FindIndex(p => p.Id == pessoa.Id);
            if (index >= 0)
            {
                pessoas[index] = pessoa;
                SaveChanges();
            }
        }

        public void RemovePessoa(Pessoa pessoa)
        {
            pessoas.RemoveAll(p => p.Id == pessoa.Id);
            SaveChanges();
        }
    }

}
