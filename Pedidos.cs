
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

public enum Estado
{
    comenzado,
    enProceso,

    terminado
}

public class Pedidos
{
    public int Nro = 0;
    public string Obs = string.Empty;
    public Clientes Cliente = new Clientes();
    public Estado Estado;
    public static List<Clientes> ListaClientes { get; set; } = new List<Clientes>();
    public Cadetes? Cadete;
    //aqui vamos a contener la lista de clientes

    public Pedidos(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli, int IdPedido)
    {
        Nro = IdPedido + 1;
        Obs = obs;
        Estado = Estado.comenzado;
        Cliente = new Clientes(nombreCli, direccionCli, telefonoCli, datosRefCli);
        ListaClientes.Add(Cliente);
       // ShowList();
    }

    public Pedidos(string obs, string parNombre, List<Pedidos> listaPara)
    {
        foreach (Clientes cli in ListaClientes)
        {
            if (cli.Nombre == parNombre)
            {
                Cliente = cli;

                if (listaPara.Count > 0)
                {
                    Nro = listaPara.Count + 1;
                }


                Obs = obs;
                Estado = Estado.comenzado;
            }
            else
            {
                Console.WriteLine("no es un cliente registrado");
            }
        }
        ShowList();


    }





    public static void ShowList()
    {
        System.Console.WriteLine("");
        Console.WriteLine("Esta es la lista de clientes actual: ");
        foreach (Clientes c in ListaClientes)
        {
            Console.WriteLine(c.Nombre);

        }
    }













    public void VerDireccion()
    {

    }
    public void VerDatos()
    {

    }
}
