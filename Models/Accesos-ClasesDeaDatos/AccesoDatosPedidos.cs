using Cadeteria2024MD.Models.Interfaces;
using System.Text.Json;

namespace Cadeteria2024MD.Models.Accesos_ClasesDeaDatos
{
    public class AccesoDatosPedidos
    {
        public List<Pedidos> ListaPedidos { get; set; }
        public bool res { get; set; }
        private Pedidos Pedido { get; set; }

        private string rutaPedidos_cvs = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\pedidos.csv";
        private string rutaPedidos_Json = @"C:\Users\diazs\Desktop\proyectos-c#\c#-vstudio\Cadeteria2024MD\Data\Json\pedidos.json";



        private static readonly ILogger<AccesoDatosPedidos> _logger;

        static AccesoDatosPedidos()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            _logger = loggerFactory.CreateLogger<AccesoDatosPedidos>();
        }







        // lee y devuelve una lista de pedidos desde file pedidos.csv
        public  List<Pedidos> Obtener()
        {

            List<Pedidos> listaPedidos = new List<Pedidos>();

            using (StreamReader sr = new StreamReader(rutaPedidos_cvs))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] valores = linea.Split(',');

                    if (int.TryParse(valores[0], out int nro))
                    {
                        if (Guid.TryParse(valores[2], out Guid clienteId))
                        {
                            try
                            {
                                Pedidos pedido = new Pedidos
                                {
                                    Nro = nro,
                                    Obs = valores[1],
                                    Cliente = new Clientes { Id = clienteId },
                                    Estado = (Estado)Enum.Parse(typeof(Estado), valores[3]),
                                    //   Cadete = valores.Length > 4 ? new Cadetes { Id = Convert.ToInt32(valores[4]) } : null,
                                    Cadete = int.Parse(valores[4]) != 00 ? new Cadetes { Id = Convert.ToInt32(valores[4]) } : null
                                };

                                listaPedidos.Add(pedido);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Ocurrio un error al leer el file CSV", ex);
                            }
                        }

                    }
                    else
                    {
                        _logger.LogInformation($"Prosesando archivo Pedidos.csv");
                    }
                }
            }

            return listaPedidos.Count > 0 ? listaPedidos : null;

        }














        public List<Pedidos> GuardarPedido(string linea)
        {
            // Asegúrate de que la ruta del archivo sea correcta y accesible
            if (string.IsNullOrEmpty(rutaPedidos_cvs))
            {
                throw new ArgumentException("La ruta del archivo no puede ser nula o vacía.");
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(rutaPedidos_cvs, true))
                {
                    sw.WriteLine(linea);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("No se ha podidos guardar el pedido");

            }
            return ListaPedidos;
        }





        public  List<Pedidos> LeerDatosPedidosJ()
        {
            // Lee el contenido del archivo JSON
            string jsonContent = File.ReadAllText(rutaPedidos_Json);

            List<PedidoIntermedio> pedidosIntermedios = JsonSerializer.Deserialize<List<PedidoIntermedio>>(jsonContent);

            List<Pedidos> listPedido = new List<Pedidos>();

            foreach (var pedidoIntermedio in pedidosIntermedios)
            {
                Pedidos pedido = new Pedidos
                {
                    Nro = pedidoIntermedio.Nro,
                    Obs = pedidoIntermedio.Obs,
                    Cliente = Guid.TryParse(pedidoIntermedio.ClienteId, out Guid IDcli) ? new Clientes { Id = IDcli } : null,
                    Estado = Enum.Parse<Estado>(pedidoIntermedio.Estado, true),
                    Cadete = pedidoIntermedio.CadeteId != 0 ? new Cadetes { Id = pedidoIntermedio.CadeteId } : null
                };
                listPedido.Add(pedido);
            }

            return listPedido;
        }

        public  List<PedidoIntermedio> LeerDatosPedidosIntermedioJ()
        {
            // Lee el contenido del archivo JSON
            string jsonContent = File.ReadAllText(rutaPedidos_Json);

            List<PedidoIntermedio> pedidosIntermedios = JsonSerializer.Deserialize<List<PedidoIntermedio>>(jsonContent);

            return pedidosIntermedios;
        }



        public void GuardarPedidoJson(int nro, string obs, Guid clienteId, Estado estado, int? cadeteId)
        {
            List<PedidoIntermedio> listPedidoIntermedio;

            // Leer el contenido existente del archivo JSON
            if (File.Exists(rutaPedidos_Json))
            {
                string jsonContent = File.ReadAllText(rutaPedidos_Json);
                listPedidoIntermedio = JsonSerializer.Deserialize<List<PedidoIntermedio>>(jsonContent) ?? new List<PedidoIntermedio>();
            }
            else
            {
                listPedidoIntermedio = new List<PedidoIntermedio>();
            }

            // Agregar el nuevo pedido a la lista
            listPedidoIntermedio.Add(new PedidoIntermedio
            {
                Nro = nro,
                Obs = obs,
                ClienteId = clienteId.ToString(),
                Estado = estado.ToString(),
                CadeteId = cadeteId ?? 0
            });

            // Serializar la lista de pedidos intermedios a JSON
            string jsonString = JsonSerializer.Serialize(listPedidoIntermedio, new JsonSerializerOptions { WriteIndented = true });

            // Guardar el JSON en el archivo
            File.WriteAllText(rutaPedidos_Json, jsonString);
        }



        public  void ModificarEstadosDePedidosJson(int nroPedido, string nuevoEstado, int? cadeteID)
        {
            try
            {
                string json = File.ReadAllText(rutaPedidos_Json);
                List<PedidoIntermedio> pedidosList = JsonSerializer.Deserialize<List<PedidoIntermedio>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Buscar y modificar el estado del pedido
                foreach (var pedido in pedidosList)
                {
                    if (pedido.Nro == nroPedido)
                    {
                        pedido.Estado = nuevoEstado;
                        pedido.CadeteId = cadeteID ?? pedido.CadeteId;
                        break;
                    }
                }

                // Reescribir el archivo JSON con los cambios
                string jsonActualizado = JsonSerializer.Serialize(pedidosList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(rutaPedidos_Json, jsonActualizado);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Ocurrió un error al modificar el estado del pedido en el archivo Json: {ex.Message}");
            }
        }
    }







    public class PedidoIntermedio
    {
        public int Nro { get; set; }
        public string Obs { get; set; } = string.Empty;
        public string ClienteId { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int CadeteId { get; set; }
    }





}
