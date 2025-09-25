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
    }
