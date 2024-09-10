public class AccesoDatos
{

    public static string rutaCadeteFile = @"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\cadetes.csv";

    private static string rutaPedidos_csv = @"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\pedidos.csv";

    private static string rutaClientes_csv = @"C:\Users\diazs\Desktop\taller-2024\taller-2024-tps\data\clientes.csv";

    public AccesoDatos()
    {

    }



    //leer pedidos y devolver lista
    public static List<Pedidos> LeerDatosPedidos()
    {
        List<Pedidos> listaPedidos = new List<Pedidos>();

        using (StreamReader sr = new StreamReader(rutaPedidos_csv))
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
                                Cadete = valores.Length > 4 ? new Cadetes { Id = Convert.ToInt32(valores[4]) } : null
                            };

                            listaPedidos.Add(pedido);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al procesar la línea: {linea}. Detalles: {ex.Message}");
                        }
                    }

                }
                else
                {
                    Console.WriteLine($"Prosesando archivo Pedidos.csv");
                }
            }
        }

        return listaPedidos.Count > 0 ? listaPedidos : null;
    }




    //leer cadetes y devolver lista
    public static List<Cadetes> LeerDatosCadetes()
    {
        List<Cadetes> listaCadetes = new List<Cadetes>();

        using (StreamReader sr = new StreamReader(AccesoDatos.rutaCadeteFile))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');

                Cadetes ca = new Cadetes
                {
                    Id = Convert.ToInt32(valores[0]),
                    Nombre = valores[1],
                    Direccion = valores[2],
                    Telefono = valores[3]

                };

                listaCadetes.Add(ca);
            }
        }

        return listaCadetes;
    }



    //leer clientes y devolver lista
    public static List<Clientes> LeerDatosClientes()
    {
        List<Clientes> listaClientes = new List<Clientes>();

        using (StreamReader sr = new StreamReader(rutaClientes_csv))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                string[] valores = linea.Split(',');
                if (Guid.TryParse(valores[0], out Guid IDcli))
                    {

                        Guid clienteId = IDcli;
                        Clientes cliente = new Clientes
                        {
                            Id = clienteId,
                            Nombre = valores[1],
                            Direccion = valores[2],
                            Telefono = valores[3]
                        };

                        listaClientes.Add(cliente);
                    }else{
                        System.Console.WriteLine("no se pudo reconocer el id del cliente");
                    }
                

               
            }
        }

        return listaClientes.Count > 0 ? listaClientes : null;
    }










    public static int LectorIds(string rutaArchivo)
    {
        int ultimoId = 0;
        string[] lineas = File.ReadAllLines(rutaArchivo);

        // Verificar si el archivo no está vacío
        if (lineas.Length > 0)
        {
            // Obtener la última línea del archivo
            string ultimaLinea = lineas[lineas.Length - 1];

            // Verificar si la última línea no está vacía
            if (!string.IsNullOrWhiteSpace(ultimaLinea))
            {
                // Dividir la última línea en valores
                string[] valores = ultimaLinea.Split(',');

                // Verificar si el primer valor es un número válido
                if (valores.Length > 0 && int.TryParse(valores[0], out int id))
                {
                    ultimoId = id;
                }
            }
        }

        return ultimoId;
    }




}