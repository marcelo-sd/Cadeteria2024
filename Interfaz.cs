using System.ComponentModel.Design;
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

//jornal de cada cadete
public (bool,double) JornalCa(int idCad){
    bool res =true;
    cadete=ListaCadetes.FirstOrDefault(ca=> ca.Id==idCad);
      if (cadete == null)
        {
            System.Console.WriteLine("Error: cadete no encontrado.");
            return (false,0);
        }



    double cobro = cadete.JornalACobrar(cadete.ListaPedidos);
   return (res,cobro);

}

//informe cadete
public void InformeCadete(){

foreach(var ca in ListaCadetes){
    System.Console.WriteLine("Nombre cadete"+ca.Nombre);
    cadete.CantidadEnvios(ca);
    cadete.MontoGanado(ca);
}
System.Console.WriteLine("Promedio: "+(ListaPedidos.Count()/ListaCadetes.Count()*100));
}






}



