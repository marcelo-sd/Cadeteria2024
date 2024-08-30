using System.Runtime.InteropServices;
using static System.Console;
using Colorful;
using System.Drawing;




ForegroundColor = ConsoleColor.Green; // Cambia el color del texto a rojo









Interfaz view = new Interfaz();

void GestionarRespuesta(int res)
{
    if (res == 1)
    {
        WriteLine("dime una observación del pedido: ");
        string observacion = ReadLine();
        WriteLine();
        WriteLine("nombre del cliente registrado: ");
        string nombreCli = ReadLine();

        var (listaPedi, resultado) = view.DarDeAltaPedido(observacion, nombreCli);
        while (!resultado)
        {
            System.Console.WriteLine("ingresa un nombre de cliente correcto");
            nombreCli = ReadLine();
            (listaPedi, resultado) = view.DarDeAltaPedido(observacion, nombreCli);
        }

        foreach (Pedidos ped in listaPedi)
        {
            WriteLine();
            WriteLine("numero de pedido: " + ped.Nro);
            WriteLine("nombre cliente: " + ped.Cliente.Nombre);
        }
    }
    else
    {
        WriteLine("dime una observación del pedido: ");
        string observacion = ReadLine();
        WriteLine();
        WriteLine("nombre del cliente nuevo: ");
        string? nombreCli = ReadLine();
        WriteLine();
        WriteLine("dirección: ");
        string direccionCli = ReadLine();
        WriteLine();
        WriteLine("teléfono: ");
        string telefonocli = ReadLine();
        WriteLine();
        WriteLine("datos de referencia: ");
        string datoDeREferencia = ReadLine();
        WriteLine();

        List<Pedidos> listaPedi = view.DarDeAltaPedido(observacion, nombreCli, direccionCli, telefonocli, datoDeREferencia);
        foreach (Pedidos ped in listaPedi)
        {
            WriteLine();
            WriteLine("numero de pedido: " + ped.Nro);
            WriteLine("nombre cliente: " + ped.Cliente.Nombre);
            WriteLine();
        }
    }
}


// asignar pedidos a cadetes

void AsignarPedidos()
{
    System.Console.WriteLine("ID de Cadete: ");

    Interfaz.ShowListCadetes();
    System.Console.WriteLine();
    int cadPar = int.Parse(ReadLine());
    System.Console.WriteLine("ID de Pedidos: ");
    Interfaz.ShowListPedidos();
    int pedPar = int.Parse(ReadLine());
    bool respuesta = view.AsignarPedidoAcadete(cadPar, pedPar);
    while (!respuesta)
    {
        System.Console.WriteLine("ingresa un id de cadete correcto");
        Interfaz.ShowListCadetes();
        cadPar = int.Parse(ReadLine());
        System.Console.WriteLine("ingresea un id del pedido correcto");
        Interfaz.ShowListPedidos();
        pedPar = int.Parse(ReadLine());
        respuesta = view.AsignarPedidoAcadete(cadPar, pedPar);
    }


    System.Console.WriteLine("el pedido se asigno correctamente");
    Interfaz.ShowCadete(cadPar);



}

void CrearCadetes()
{
    System.Console.WriteLine("ingrese el nombre del cadete");

    string nombreCa = ReadLine();
    System.Console.WriteLine("Ingrese la direccion del cadete");
    string direCa = ReadLine();
    System.Console.WriteLine("ingrese el telefono");
    string telCa = ReadLine();
    view.AnadirCadete(nombreCa, direCa, telCa);
}

//reasignar cadetes
void ReasinarPed()
{

    System.Console.WriteLine("numero de Cadete Anterior: ");
    Interfaz.ShowListCadetes();
    System.Console.WriteLine();
    int cadAnterior = int.Parse(ReadLine());

    System.Console.WriteLine("ingrese el id del pedido: ");
    Interfaz.ShowListPedidos();
    int pedPar = int.Parse(ReadLine());
    System.Console.WriteLine("numero del nuevo Cadete: ");

    Interfaz.ShowListCadetes(cadAnterior);
    int cadPar = int.Parse(ReadLine());
    bool respuesta = view.ResignarPedidoAcadete(cadPar, pedPar, cadAnterior);
    while (!respuesta)
    {
        System.Console.WriteLine("ID del  Cadete anterior correcto: ");
        Interfaz.ShowListCadetes();
        cadAnterior = int.Parse(ReadLine());
       
        System.Console.WriteLine("ingresa un ID del pedido correcto:");
        Interfaz.ShowListPedidos();
        pedPar = int.Parse(ReadLine());

         System.Console.WriteLine("ingresa un ID del nuevo cadete: ");
        Interfaz.ShowListCadetes();
        cadPar = int.Parse(ReadLine());
        
        respuesta = view.ResignarPedidoAcadete(cadPar, pedPar, cadAnterior);
    }

    System.Console.WriteLine("el pedido se asigno correctamente");
    Interfaz.ShowCadete(cadPar);

}

// cambiar estado
void CambiarEstado()
{
    System.Console.WriteLine("ingrese el id del pedido ");
    Interfaz.ShowListPedidos();
    int idPedido = int.Parse(ReadLine());
    bool respuesta = view.CambiarEstadoPedido(idPedido);
       while (!respuesta)
    {
        System.Console.WriteLine("ingresa in id de pedido correcto");
        Interfaz.ShowListPedidos();
        idPedido = int.Parse(ReadLine());
        respuesta = view.CambiarEstadoPedido(idPedido);
    }
Interfaz.ShowPedido(idPedido);
}





bool continuar = true;
while (continuar)
{
    WriteLine();
    WriteLine("1_ Dar de alta un pedido");
    WriteLine("2_ asignar pedidos");
    WriteLine("3_ Crear cadetes");
    WriteLine("4_ Cambiar de estado");
    WriteLine("5_ Reasignar pedido");
    WriteLine("6_ Salir");
    WriteLine("Selecciona una opción");
    string? opcion = ReadLine();

    switch (opcion)
    {
        case "1":
            WriteLine("es cliente registrado? 1_SI 2_NO");
            int respuesta = int.Parse(ReadLine());
            GestionarRespuesta(respuesta);

            break;
        case "2":
            WriteLine("asignar pedidos a cadtes");
            AsignarPedidos();
            break;
        case "3":
            WriteLine("Crear cadetes");
            CrearCadetes();

            break;
        case "4":
            CambiarEstado();
            break;
        case "5":
            ReasinarPed();
            break;
        case "6":
            System.Console.WriteLine("bye, bye");
            continuar = false;

            break;
        default:
            WriteLine("Selecciona una opción correcta");
            break;
    }
}



