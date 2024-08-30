


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
public void ShowListaDeCadete(List<Pedidos> lisDelCadete){
    foreach(var p in lisDelCadete){
        System.Console.WriteLine("Numero de pedido: "+p.Nro);
        System.Console.WriteLine("Nombre cliente: "+p.Cliente.Nombre);
        System.Console.WriteLine("Observacion: "+p.Obs);
        System.Console.WriteLine("Estado de pedidos: "+p.Estado);
        System.Console.WriteLine();
        }

}


    public void JornalACobrar()
    {

    }
}
