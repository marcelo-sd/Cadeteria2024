using System.Security.Cryptography;

public class Interfaz
{
    public Pedidos PedidoA;
    public static List<Pedidos> ListaPedidos;
    // aqui vamos a manejar la lista de pedidos
    public Cadetes cadete;
    public static List<Cadetes> ListaCadetes;
    //aqui vamos a manejar la lista de cadetes


    public Interfaz()
    {
        ListaPedidos = new List<Pedidos>();
        ListaCadetes = new List<Cadetes>();
    }

    public List<Pedidos> DarDeAltaPedido(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli)
    {

        PedidoA = new Pedidos(obs, nombreCli, direccionCli, telefonoCli, datosRefCli, ListaPedidos.Count());
        ListaPedidos.Add(PedidoA);
        return ListaPedidos;

    }



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


    public void AnadirCadete(string nombreCa, string direCa, string telCa)

    {

        cadete = new Cadetes(nombreCa, direCa, telCa, ListaCadetes);
        ListaCadetes.Add(cadete);
        ShowListCadetes();

    }

public bool AsignarPedidoAcadete(int idCadete, int idPedido)
{
    bool res = false;
    Cadetes cadeteEncontrado = null;
    Pedidos pedidoEncontrado = null;

    foreach (var c in ListaCadetes)
    {
        if (c.Id == idCadete)
        {
            cadeteEncontrado = c;
            break;
        }
    }

    if (cadeteEncontrado == null)
    {
        System.Console.WriteLine("Cadete no encontrado");
        return res;
    }

    foreach (var p in ListaPedidos)
    {
        if (p.Nro == idPedido)
        {
            pedidoEncontrado = p;
            break;
        }
    }

    if (pedidoEncontrado == null)
    {
        System.Console.WriteLine("Pedido no encontrado");
        return res;
    }

    cadeteEncontrado.ListaPedidos.Add(pedidoEncontrado);
    res = true;
    return res;
}













    public static void ShowListPedidos()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("lista de pedidos");
        System.Console.WriteLine();
        foreach (var i in ListaPedidos)
        {
            System.Console.WriteLine(i.Nro);
            System.Console.WriteLine(i.Obs);
            System.Console.WriteLine(i.Cliente.Nombre);
            System.Console.WriteLine();
        }
    }
    public static void ShowListCadetes()
    {

        System.Console.WriteLine("esta es la lista de cadetes actual");
        foreach (var i in ListaCadetes)
        {
            System.Console.WriteLine("id: "+i.Id);
            System.Console.WriteLine("nombre: " + i.Nombre);
            System.Console.WriteLine("Telefono: " + i.Telefono);
        }
    }
    public static void ShowCadete(int idCadete){
        System.Console.WriteLine("cadete:");
        foreach(var c in ListaCadetes){
            if(c.Id==idCadete){
                System.Console.WriteLine(c.Nombre);

                c.ShowListaDeCadete(c.ListaPedidos);
            }
        }
    }


}



