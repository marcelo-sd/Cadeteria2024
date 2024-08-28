



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

public void AsignarPedido(string nombreCadete,int numeroPedido ){
         foreach (Cadetes cadete in ListaCadetes)
        {
                if (cadete.Nombre == nombreCadete)
            {
              cadete.PedidosAsignados.Add(numeroPedido);
                  foreach(Pedidos pedidoY in ListaPedidos){
                    if(pedidoY.Nro==numeroPedido){
                        pedidoY.Estado=Estado.enProceso;
                    }
                  }

            }

        }
  Console.WriteLine($"Pedido: {numeroPedido} fue asignado al cadete: {nombreCadete}");

}

}