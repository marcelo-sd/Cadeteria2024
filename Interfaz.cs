using System.Diagnostics;
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
        pedidoEncontrado.Estado = Estado.enProceso;
        res = true;
        return res;
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

        cadeteAnterior.ListaPedidos.Remove(pedido);
        nuevoCadete.ListaPedidos.Add(pedido);
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
                res = true;
                break; // Salimos del bucle ya que encontramos el pedido
            }
        }
        return res;
    }







    public static void ShowListPedidos()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("Lista de pedidos Actual: ");
        foreach (var i in ListaPedidos)
        {
            System.Console.WriteLine("id del pedido: " + i.Nro);
            System.Console.WriteLine("Observacion: " + i.Obs);
            System.Console.WriteLine("Nombre del Cliente: " + i.Cliente.Nombre);
            bool cadeteAsignado = false;
            foreach (var c in ListaCadetes)
            {
                if (c.ListaPedidos != null && c.ListaPedidos.Contains(i))
                {
                    System.Console.WriteLine($"Cadete asignado: {c.Nombre}");
                    cadeteAsignado = true;
                    break;

                }

            }
            if (!cadeteAsignado)
            {
                System.Console.WriteLine("Cadete asignado: NULL");
            }
            System.Console.WriteLine();
        }
    }

    public static void ShowListCadetes()
    {

        System.Console.WriteLine("Esta es la lista de cadetes actual:");
        foreach (var i in ListaCadetes)
        {
            System.Console.WriteLine("id Del Cadete: " + i.Id);
            System.Console.WriteLine("nombre: " + i.Nombre);
            System.Console.WriteLine("Telefono: " + i.Telefono);
            if (i.ListaPedidos != null)
            {
                foreach (var p in i.ListaPedidos)
                {
                    System.Console.WriteLine("Esta es la lista de Pedidos del cadete: ");
                    System.Console.WriteLine("id de pedido: " + p.Nro);
                    System.Console.WriteLine("nombre del cliente: " + p.Cliente.Nombre);
                    System.Console.WriteLine("Observacion: " + p.Obs);
                    System.Console.WriteLine("Estado: " + p.Estado);

                }
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
                if (i.ListaPedidos != null)
                {
                    foreach (var p in i.ListaPedidos)
                    {
                        System.Console.WriteLine("Esta es la lista de pedidos asignados al cadete:");
                        System.Console.WriteLine("id de pedido: " + p.Nro);
                        System.Console.WriteLine("nombre del cliente: " + p.Cliente.Nombre);
                        System.Console.WriteLine("Observacion: " + p.Obs);
                        System.Console.WriteLine("Estado: " + p.Estado);

                    }
                }

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

                c.ShowListaDeCadete(c.ListaPedidos);
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

            }
        }
    }



}



