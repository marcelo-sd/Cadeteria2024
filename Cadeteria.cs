



public class Cadeteria
{
    string Nombre;
    string Telefono;

    public Pedidos PedidoA;
    public Cadetes cadete;


    public static List<Pedidos> ListaPedidos;
    // aqui vamos a manejar la lista de pedidos
    public static List<Cadetes> ListaCadetes;
    // aqui vamos a manejar la lista de pedidos



    public Cadeteria(string nombre, string telefono, List<Cadetes> listaCadetesParametro, List<Pedidos> listaPedidosParametro)
    {
        Nombre = nombre;
        Telefono = telefono;
        ListaCadetes = listaCadetesParametro;
        ListaPedidos = listaPedidosParametro;
    }
    public Cadeteria()
    {
        ListaPedidos = new List<Pedidos>();
        ListaCadetes = new List<Cadetes>();

    }





    public void JornalCobrar(int idcadete)
    {
        System.Console.WriteLine("este es el metodo jornal a cobrar");

    }







    //dar de alta un pedido
    public List<Pedidos> DarDeAltaPedido(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli)
    {

        PedidoA = new Pedidos(obs, nombreCli, direccionCli, telefonoCli, datosRefCli, ListaPedidos.Count());

        ListaPedidos.Add(PedidoA);
        return ListaPedidos;

    }


    //si el cliente esta registrado anteriormente
    public (List<Pedidos>, bool) DarDeAltaPedido(string obs, string nombreCliente)
    {
        bool res = false;

        foreach (var p in ListaPedidos)
        {
            if (p.Cliente.Nombre == nombreCliente)
            {
                PedidoA = new Pedidos(obs, nombreCliente, ListaPedidos);
                ListaPedidos.Add(PedidoA);
                res = true;
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
        return true;
    }

    //añadir cadete
    public void AnadirCadete(string nombreCa, string direCa, string telCa)

    {

        cadete = new Cadetes(nombreCa, direCa, telCa, ListaCadetes);
        ListaCadetes.Add(cadete);
        ShowListCadetes();

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
            System.Console.WriteLine("Nombre del Cliente: " + i.Cliente.Nombre);

            System.Console.WriteLine("Cadete asignado: " + (i.Cadete.Nombre ?? "null"));

            System.Console.WriteLine("Estado: " + i.Estado);

            System.Console.WriteLine();
        }
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
                System.Console.WriteLine("ID: " + c.Id);
                System.Console.WriteLine("nombre de Cadete: " + c.Nombre);
                var pedidosDelCadete = ListaPedidos
       .Where(p => p.Cadete != null && p.Cadete.Id == c.Id)
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
                System.Console.WriteLine("cliente: " + p.Cliente.Nombre);
                System.Console.WriteLine("Cadete asignado: " + (p.Cadete.Nombre ?? "null"));

            }
        }
    }





}