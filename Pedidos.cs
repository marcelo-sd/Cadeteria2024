
public enum Estado{
    comenzado,
    enProceso,
  
    terminado
}

public class Pedidos{
  public int Nro=0;
   public string Obs;
   public Clientes Cliente;
  public Estado Estado;

public Pedidos(string obs,string nombreCli, string direccionCli, string telefonoCli,string datosRefCli)
{
    Nro++;
    Obs=obs;
    Estado=Estado.comenzado;
    Cliente=new Clientes(nombreCli,direccionCli,telefonoCli,datosRefCli);
    
}
  





















    public void VerDireccion(){

    }
       public void VerDatos(){
        
    }
}
