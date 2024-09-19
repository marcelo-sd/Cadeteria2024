using System.Text.Json;


public class AccesoJson : AccesoDatos
{

    public static List<Pedidos> LeerDatosPedidosJ()
    {



        // Lee el contenido del archivo JSON
        string jsonContent = File.ReadAllText(rutaPedidos_json);

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



    public static List<PedidoIntermedio> LeerDatosPedidosIntermedioJ()
    {
        // Lee el contenido del archivo JSON
        string jsonContent = File.ReadAllText(rutaPedidos_json);

        List<PedidoIntermedio> pedidosIntermedios = JsonSerializer.Deserialize<List<PedidoIntermedio>>(jsonContent);

      
        return pedidosIntermedios;
    }










    public void GuardarPedidoJson(int nro, string obs, Guid clienteId, Estado estado, int? cadeteId)
    {
        List<PedidoIntermedio> listPedidoIntermedio;

        // Leer el contenido existente del archivo JSON
        if (File.Exists(rutaPedidos_json))
        {
            string jsonContent = File.ReadAllText(rutaPedidos_json);
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
        File.WriteAllText(rutaPedidos_json, jsonString);
    }




      public static void ModificarEstadosDePedidosJson(int nroPedido, string nuevoEstado,int ? cadeteID)
    {
        try
        {
            string json = File.ReadAllText(rutaPedidos_json);
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
                    pedido.CadeteId=cadeteID ?? pedido.CadeteId;
                    break;
                }
            }

            // Reescribir el archivo JSON con los cambios
            string jsonActualizado = JsonSerializer.Serialize(pedidosList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaPedidos_json, jsonActualizado);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al modificar el estado del pedido en el archivo Json: {ex.Message}");
        }
    }

  




}












//tuve que crear un clas intermedia para usar solo los datos que son claves
public class PedidoIntermedio
{
    public int Nro { get; set; }
    public string Obs { get; set; } = string.Empty;
    public string ClienteId { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public int CadeteId { get; set; }
}

