
public class AccesoCsv : AccesoDatos
{

    private static readonly ILogger<AccesoCsv> _logger;

    static AccesoCsv()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        _logger = loggerFactory.CreateLogger<AccesoCsv>();
    }



    //lee Pedidos.csv y devuelve list
    public static List<Pedidos> LeerDatosPedidosC()
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


    //leer cadetes y devolver lista
    public static List<Cadetes> LeerDatosCadetesC()
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
    public static List<Clientes> LeerDatosClientesC()
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
                }
                else
                {
                    throw new ArgumentException("no se pudo reconocer el id del cliente");
                }



            }
        }

        return listaClientes.Count > 0 ? listaClientes : null;
    }







}

