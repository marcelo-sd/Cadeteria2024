


public class Cadetes
{

    public int Id = 0;
    public string Nombre;
    string Direccion;
    public string Telefono;




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

    }

    


    public double JornalACobrar(List<Pedidos> listaPedidosPara)
    {
        double jornal = listaPedidosPara.Count() * 500;
        return jornal;

    }


/*     public void CantidadEnvios(Cadetes ca)
    {
        int pedidosRealizados = 0;
        if (ca.ListaPedidos != null && ca.ListaPedidos.Any())
        {
            pedidosRealizados = ca.ListaPedidos.Count(p => p.Estado == Estado.terminado);
            System.Console.WriteLine("cantidad de pedidos reaizados: " + pedidosRealizados);
        }
        System.Console.WriteLine("este cadete no tiene una lista de pedidos vacios");
    }

    public void MontoGanado(Cadetes ca){
       System.Console.WriteLine("monto ganado: "+(ca.ListaPedidos.Count()*500));
    } */
    
}
