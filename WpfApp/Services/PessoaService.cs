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

        public List<Pessoa> GetAll() => pessoas; //vai retornar a lista de pessoas

        public void Add(Pessoa pessoa) // vai fazer a verificação do cpf e adicionar a pessoa
        {
            if (!Validar(pessoa.CPF))
            {
                throw new ArgumentException("CPF inválido.");
            }

            pessoa.Id = Guid.NewGuid();
            pessoas.Add(pessoa);
            SaveChanges();
        }

        public void Update(Pessoa pessoa) // vai atualizar as informações da pessoa
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
            var pessoa = pessoas.FirstOrDefault(p => p.Id == id); //vai deletar a pessoa pelo id
            if (pessoa != null)
            {
                pessoas.Remove(pessoa);
                SaveChanges();
            }
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(pessoas, Formatting.Indented); // vai salvar as mudanças no arquivo json
            File.WriteAllText(FilePath, json);
        }

        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
            {
                return false;
            }


            if (new string(cpf[0], cpf.Length) == cpf) //descarta cpf repetidos
            {
                return false;
            }

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;


            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}