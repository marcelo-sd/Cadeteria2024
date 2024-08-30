



public class Cadeteria{
    string Nombre;
    string Telefono;
    List<Cadetes> ListaCadetes;
    List<Pedidos> ListaPedidos;


public Cadeteria(string nombre,string telefono,List<Cadetes> listaCadetesParametro,List<Pedidos> listaPedidosParametro)
{
    Nombre=nombre;
    Telefono=telefono;
    ListaCadetes =listaCadetesParametro;  
    ListaPedidos =listaPedidosParametro;  
}


}