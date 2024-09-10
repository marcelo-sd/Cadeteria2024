



using System.Data.Common;

public class Cadeteria
{
    string Nombre;
    string Telefono;

    public Pedidos PedidoA;
    public static Pedidos PedidoB = new Pedidos();
    public Cadetes cadete;


    public static List<Pedidos> ListaPedidos;
    // aqui vamos a manejar la lista de pedidos
    public static List<Cadetes> ListaCadetes;
    // aqui vamos a manejar la lista de pedidos



    public Cadeteria(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;

    }



    public Cadeteria()
    {
        ListaPedidos = AccesoDatos.LeerDatosPedidos() ?? new List<Pedidos>();
        ListaCadetes = AccesoDatos.LeerDatosCadetes() ?? new List<Cadetes>();

    }




    //jornal de cada cadete
    public (bool, double) JornalCobrar(int idCadete)
    {
        bool res = true;
        double cobro = 0;

        var contador = ListaPedidos.Count(p => p.Cadete != null && p.Cadete.Id == idCadete && p.Estado == Estado.terminado);
        if (contador == null)
        {
            res = false;
            return (res, cobro);

        }
        cobro = contador * 500;

        return (res, cobro);

    }






    //dar de alta un pedido
    public List<Pedidos> DarDeAltaPedido(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli)
    {
        PedidoA = new Pedidos(obs, nombreCli, direccionCli, telefonoCli, datosRefCli);

        ListaPedidos.Add(PedidoA);
        PedidoA.GuardarPedido();
        return ListaPedidos;

    }



    //si el cliente esta registrado anteriormente
    public (List<Pedidos>, bool) DarDeAltaPedido(string obs, string nombreCli)
    {
        bool res = false;
        Guid IDCliente = Pedidos.ListaClientes
            .Where(cli => cli.Nombre == nombreCli)
            .Select(cli => cli.Id)
            .FirstOrDefault();

        foreach (var p in ListaPedidos)
        {
            if (p.Cliente.Id ==IDCliente)
            {
                PedidoA = new Pedidos(obs, nombreCli);
                ListaPedidos.Add(PedidoA);
                res = true;
                PedidoA.GuardarPedido();
                return (ListaPedidos, res);
            }
        }

        // Si no se encontró el cliente en la lista
        if (!res)
        {
            System.Console.WriteLine("este cliente no esta registrado ");
        }

        return (ListaPedidos, res);
    }


    //asignar cadete a pedido
    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        bool respuesta = true;
        var pedidoEncontrado = ListaPedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedidoEncontrado == null)
        {
            System.Console.WriteLine("Pedido no encontrado");
            return false;

        }
        var cadeteEncontrado = ListaCadetes.FirstOrDefault(c => c.Id == idCadete);
        if (cadeteEncontrado == null)
        {
            System.Console.WriteLine("cadete no encontrado");
            return false;
        }
        pedidoEncontrado.Cadete = cadeteEncontrado;

        Pedidos.AsignarPedidoAcadete(idCadete, idPedido);
        Pedidos.ModificarPedidosEstado(idPedido, Estado.enProceso);
        return true;
    }



    //añadir cadete
    public void AnadirCadete(string nombreCa, string direCa, string telCa)

    {
        cadete = new Cadetes(nombreCa, direCa, telCa);
        ListaCadetes.Add(cadete);
        cadete.GuardarCadeteCsv();
        ShowListCadetes();
    }


    //reasignar cadetes
    public bool ResignarPedidoAcadete(int idPedido, int idCadet, int cadAnterior)
    {
        if (ListaPedidos == null || ListaCadetes == null || !ListaPedidos.Any() || !ListaCadetes.Any())
        {
            System.Console.WriteLine("Error: Lista de pedidos o cadetes está vacía o no inicializada.");
            return false;
        }

        var pedido = ListaPedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
        {
            System.Console.WriteLine("Error: Pedido no encontrado.");
            return false;
        }

        var cadeteAnterior = ListaCadetes.FirstOrDefault(pe => pe.Id == cadAnterior);
        var nuevoCadete = ListaCadetes.FirstOrDefault(c => c.Id == idCadet);

        if (cadeteAnterior == null)
        {
            System.Console.WriteLine("Error: Cadete anterior no encontrado.");
            return false;
        }

        if (nuevoCadete == null)
        {
            System.Console.WriteLine("Error: Nuevo cadete no encontrado.");
            return false;
        }


        pedido.Cadete = nuevoCadete;
        Pedidos.AsignarPedidoAcadete(pedido.Nro, nuevoCadete.Id);

        System.Console.WriteLine("El pedido ha sido reasignado correctamente.");

        return true;
    }

    //cambiar estado de pedido
    public bool CambiarEstadoPedido(int idPedido)
    {
        bool res = false;
        foreach (var p in ListaPedidos)
        {
            if (p.Nro == idPedido)
            {
                p.Estado = Estado.terminado;
                System.Console.WriteLine("El estado de pedido fue cambiado correctamente a 'TERMINADO'");
                Pedidos.ModificarPedidosEstado(p.Nro, Estado.terminado);
                res = true;
                break; // Salimos del bucle ya que encontramos el pedido
            }
        }
        return res;
    }















    //lista de pedidos
    public static void ShowListPedidos()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Lista de pedidos Actual: ");
        foreach (var i in ListaPedidos)
        {
            System.Console.WriteLine("id del pedido: " + i.Nro);
            System.Console.WriteLine("Observacion: " + i.Obs);
            System.Console.WriteLine("ID del Cliente: " + i.Cliente.Id);

            if (i.Cadete != null)
            {
                Console.WriteLine("Cadete asignado ID: " + i.Cadete.Id);
            }
            else
            {
                Console.WriteLine("Cadete asignado: null");
            }

            System.Console.WriteLine("Estado: " + i.Estado);

            System.Console.WriteLine();
        }
    }


    public static void ShowListaClientes()
    {
        PedidoB.MostrarListaClientes();
    }









    //mostrar lista de cadetes
    public static void ShowListCadetes()
    {

        System.Console.WriteLine("Esta es la lista de cadetes actual:");
        foreach (var i in ListaCadetes)
        {
            System.Console.WriteLine("id Del Cadete: " + i.Id);
            System.Console.WriteLine("nombre: " + i.Nombre);
            System.Console.WriteLine("Telefono: " + i.Telefono);
            var pedidosDelCadete = ListaPedidos
            .Where(p => p.Cadete != null && p.Cadete.Id == i.Id)
            .Select(p => new { p.Nro, p.Obs })
            .ToList();

            System.Console.WriteLine("Pedidos asignados a este cadete:");
            foreach (var pedido in pedidosDelCadete)
            {
                System.Console.WriteLine($"Nro:  {pedido.Nro}, Obs:  {pedido.Obs}");
            }

            System.Console.WriteLine();
        }
    }

    // esta es pra que muestre los cadetes que no estan dentro del parametro Cad(es el cadete anterior)
    public static void ShowListCadetes(int cadAnterior)
    {
        System.Console.WriteLine("Esta es la lista de cadetes actual sin el cadete anterior seleccionado");
        foreach (var i in ListaCadetes)
        {
            if (i.Id != cadAnterior)
            {
                System.Console.WriteLine("id Del Cadete: " + i.Id);
                System.Console.WriteLine("nombre: " + i.Nombre);
                System.Console.WriteLine("Telefono: " + i.Telefono);
                var pedidosDelCadete = ListaPedidos
        .Where(p => p.Cadete != null && p.Cadete.Id == i.Id)
        .Select(p => new { p.Nro, p.Obs })
        .ToList();

                System.Console.WriteLine("Pedidos asignados a este cadete:");
                foreach (var pedido in pedidosDelCadete)
                {
                    System.Console.WriteLine($"Nro:  {pedido.Nro}, Obs:  {pedido.Obs}");
                }

                System.Console.WriteLine();


            }

        }
    }




    //mostrar cadete solo
    public static void ShowCadete(int idCadete)
    {
        System.Console.WriteLine("cadete:");
        foreach (var c in ListaCadetes)
        {
            if (c.Id == idCadete)
            {
                System.Console.WriteLine("ID del cadete: " + c.Id);
                System.Console.WriteLine("nombre de Cadete: " + c.Nombre);
                var pedidosDelCadete = ListaPedidos
       .Where(p => p.Cadete != null && p.Cadete.Id == c.Id)
       .Select(p => new { p.Nro, p.Obs })
       .ToList();

                System.Console.WriteLine("Pedidos asignados a este cadete:");
                foreach (var pedido in pedidosDelCadete)
                {
                    System.Console.WriteLine($"ID del pedido:  {pedido.Nro}, Obs:  {pedido.Obs}");
                }

                System.Console.WriteLine();


            }
        }
    }

    //mostrar pedido solo 

    public static void ShowPedido(int idPedido)
    {
        System.Console.WriteLine("Pedido:");
        foreach (var p in ListaPedidos)
        {
            if (p.Nro == idPedido)
            {

                System.Console.WriteLine("Pedido numero: " + p.Nro);
                System.Console.WriteLine("Observacion: " + p.Obs);
                System.Console.WriteLine("Estado de pedido: " + p.Estado);
                System.Console.WriteLine("cliente ID: " + p.Cliente.Id ?? "NULL");
                System.Console.WriteLine("Cadete asignado ID: " + (p.Cadete.Id.ToString() ?? "NULL"));

            }
        }
    }



    //informe cadete
    public void InformeCadete()
    {
        var pedidosConCadete = ListaPedidos.Where(p => p.Cadete != null).ToList();
        foreach (var pe in pedidosConCadete)
        {

            System.Console.WriteLine("Nombre cadete" + pe.Cadete.Nombre);

            int cantidadEnvios = ListaPedidos.Count(p => p.Cadete != null && p.Cadete.Id == pe.Cadete.Id && p.Estado == Estado.terminado);
            Console.WriteLine($"Cantidad de envíos: {cantidadEnvios}");

            int montoGanado = cantidadEnvios * 500;
            Console.WriteLine($"Monto Ganado: {montoGanado}");

            double promedioEnvios = (double)cantidadEnvios / pedidosConCadete.Count(p => p.Cadete.Id == pe.Cadete.Id);
            Console.WriteLine($"Promedio de pedidos realizados por este cadete: {promedioEnvios:F2}");



        }
    }






}