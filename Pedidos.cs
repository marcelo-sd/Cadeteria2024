
public enum Estado{
    enProceso,
    medio,
    terminado
}

public class Pedidos{
  int Nro=0;
   string Obs;
    Clientes Cliente;
   Estado Estado;

public Pedidos(string obs,string nombreCli, string direccionCli, string telefonoCli,string datosRefCli)
{
    Nro++;
    Obs=obs;
    Cliente=new Clientes(nombreCli,direccionCli,telefonoCli,datosRefCli);
    
}
  




    public void VerDireccion(){

    }
       public void VerDatos(){
        
    }
}
