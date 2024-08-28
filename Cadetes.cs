


public class Cadetes{
    private static int contadorId = 0;
    int Id;
   public string Nombre;
    string Direccion;
    string Telefono;

    List<Pedidos> ListaPedidos;
    public List<int> PedidosAsignados;



public Cadetes(string nombre,string direccion,string telefono )
{
 Id = ++contadorId;
Nombre=nombre;
Direccion=direccion;
Telefono=telefono;
ListaPedidos=new List<Pedidos>();

    
}


public void DarDeAltaPedido()
{
    Pedidos pedido=new Pedidos("es una obsevacion","jose luis","lujan 789","23484970","casa roja y linda");
    ListaPedidos.Add(pedido);
    foreach(Pedidos orden in ListaPedidos ){
        Console.WriteLine($"idPedidos: {orden.Nro}");
        Console.WriteLine($"Observacion: {orden.Obs}");
        Console.WriteLine($"Nombre cliente: {orden.Cliente.Nombre}");
        Console.WriteLine($"telefono Cliente: {orden.Cliente.Telefono}");
        Console.WriteLine();
    }


}




public void JornalACobrar(){

}
}
