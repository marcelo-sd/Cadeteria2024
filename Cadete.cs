


public class Cadete{
    int Id;
    string Nombre;
    string Direccion;
    string Telefono;
    
List<Pedidos> ListaPedidos;


public Cadete(string nombre,string direccion,string telefono )
{
Id++;
Nombre=nombre;
Direccion=direccion;
Telefono=telefono;
ListaPedidos=new List<Pedidos>();
    
}



public void JornalACobrar(){

}
}
