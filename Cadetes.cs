


public class Cadetes
{

    public int Id = 0;
    public string Nombre;
    string Direccion;
    public string Telefono;

    public List<Pedidos>? ListaPedidos;



    public Cadetes(string nombre, string direccion, string telefono, List<Cadetes> lisCadetes)
    {
        if (lisCadetes == null || lisCadetes.Count == 0)
        {
            Id = 1;
        }
        else
        {
            Id = lisCadetes.Count + 1;
        }
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        ListaPedidos = new List<Pedidos>();

    }

    //es para mostrar la lista de pedidos asiganados a cada cadete
    public void ShowListaDeCadete(List<Pedidos> lisDelCadete)
    {
        foreach (var p in lisDelCadete)
        {
            System.Console.WriteLine("Numero de pedido: " + p.Nro);
            System.Console.WriteLine("Nombre cliente: " + p.Cliente.Nombre);
            System.Console.WriteLine("Observacion: " + p.Obs);
            System.Console.WriteLine("Estado de pedidos: " + p.Estado);
            System.Console.WriteLine();
        }

    }


    public double JornalACobrar(List<Pedidos> listaPedidosPara)
    {
        double jornal = listaPedidosPara.Count() * 500;
        return jornal;

    }


    public void CantidadEnvios(Cadetes ca)
    {
        int pedidosRealizados = 0;
        if (ca.ListaPedidos != null && ca.ListaPedidos.Any())
        {
            pedidosRealizados = ca.ListaPedidos.Count(p => p.Estado == Estado.terminado);
            System.Console.WriteLine("cantidad de pedidos reaizados: " + pedidosRealizados);
        }

        System.Console.WriteLine("cadete: " + ca.Nombre);
        System.Console.WriteLine("este cadete no tiene una lista de pedidos vacios");
    }

    public void MontoGanado(Cadetes ca)
    {
         int peRealizados = ca.ListaPedidos.Count(p => p.Estado == Estado.terminado);
        System.Console.WriteLine("monto ganado: " + (peRealizados * 500));
    }

}
