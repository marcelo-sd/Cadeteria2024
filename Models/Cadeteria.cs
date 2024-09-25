



using Cadeteria2024MD.Models.Accesos_ClasesDeaDatos;
using System;
using System.Data.Common;
public class Cadeteria
{
    string Nombre;
    string Telefono;

    public Pedidos PedidoA;

    public static Pedidos PedidoB = new Pedidos();

    public Cadetes cadete;






    public static List<Pedidos> ListaPedidos;
    // aqui vamos a manejar la lista de pedidos
    public static List<Cadetes> ListaCadetes;
    // aqui vamos a manejar la lista de pedidos
    AccesoDatosPedidos acDped;
    AccesoDatosCadetes acDcad;

    private readonly ILogger<Cadeteria> _logger;


    public Cadeteria(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;

    }



    public Cadeteria(ILogger<Cadeteria> logger)
    {
        acDped = new AccesoDatosPedidos();
        acDcad=new AccesoDatosCadetes();

        _logger = logger;

        ListaPedidos = acDped.Obtener() ?? new List<Pedidos>();
        // ListaPedidos = AccesoJson.LeerDatosPedidosJ();

        ListaCadetes = acDcad.ObtenerListaCadetes() ?? new List<Cadetes>();
        //ListaCadetes = AccesoCsv.LeerDatosCadetesC() ?? new List<Cadetes>();


    }

    public Cadeteria()
    {
        acDped=new AccesoDatosPedidos();
        acDcad= new AccesoDatosCadetes();

        ListaPedidos = acDped.Obtener() ?? new List<Pedidos>();
        // ListaPedidos = AccesoJson.LeerDatosPedidosJ();
        ListaCadetes = acDcad.ObtenerListaCadetes() ?? new List<Cadetes>();

    }









    //dar de alta un pedido
    public bool DarDeAltaPedido(string obs, string nombreCli, string direccionCli, string telefonoCli, string datosRefCli)
    {
        bool result = false;
        PedidoA = new Pedidos(obs, nombreCli, direccionCli, telefonoCli, datosRefCli);

        ListaPedidos.Add(PedidoA);

        result = PedidoA.GuardarPedido();

        acDped.GuardarPedidoJson(PedidoA.Nro, obs, PedidoA.Cliente.Id, Estado.comenzado, null);
        return result;


    }



    //si el cliente esta registrado anteriormente
    public  bool DarDeAltaPedido(string obs, string nombreCli)
    {
        bool res = false;
        Guid IdCliente = Pedidos.ListaClientes
            .Where(cli => cli.Nombre == nombreCli)
            .Select(cli => cli.Id)
            .FirstOrDefault();

        foreach (var p in ListaPedidos)
        {
            if (p.Cliente.Id == IdCliente)
            {
                PedidoA = new Pedidos(obs, nombreCli);
                ListaPedidos.Add(PedidoA);
                PedidoA.GuardarPedido();
                acDped.GuardarPedidoJson(PedidoA.Nro, obs, PedidoA.Cliente.Id, Estado.comenzado, null);
                return  res =true;
            }
        }

        // Si no se encontró el cliente en la lista
        if (!res)
        {
            _logger.LogInformation("Este cliente no esta registrado");
        }

        return  res;
    }




    //asignar cadete a pedido
    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        bool respuesta = true;
        Pedidos? pedidoEncontrado = ListaPedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedidoEncontrado == null)
        {
            _logger.LogWarning("Pedido no encontrado: {IdPedido}", idPedido);
            return false;

        }
        Cadetes? cadeteEncontrado = ListaCadetes.FirstOrDefault(c => c.Id == idCadete);
        if (cadeteEncontrado == null)
        {
            _logger.LogWarning("cadete no encontrado");
            return false;
        }
        pedidoEncontrado.Cadete = cadeteEncontrado;

        Pedidos.AsignarPedidoAcadete(idCadete, idPedido);
        Pedidos.ModificarPedidosEstado(idPedido, Estado.enProceso);

        return true;
    }



    //añadir cadete
    public void AnadirCadete(string nombreCa, string direCa, string telCa)

    {
        cadete = new Cadetes(nombreCa, direCa, telCa);
        ListaCadetes.Add(cadete);
        cadete.GuardarCadeteCsv();
    }


    //reasignar cadetes
    public bool ResignarPedidoAcadete(int idPedido, int idCadet, int cadAnterior)
    {
        if (ListaPedidos == null || ListaCadetes == null || !ListaPedidos.Any() || !ListaCadetes.Any())
        {
            _logger.LogWarning(" Lista de pedidos o cadetes está vacía o no inicializada.");
            return false;
        }

        var pedido = ListaPedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
        {
            _logger.LogWarning("Error: Pedido no encontrado.");
            return false;
        }

        var cadeteAnterior = ListaCadetes.FirstOrDefault(ca => ca.Id == cadAnterior);

        var nuevoCadete = ListaCadetes.FirstOrDefault(c => c.Id == idCadet);

        if (cadeteAnterior == null)
        {
            _logger.LogWarning("Error: Cadete anterior no encontrado.");
            return false;
        }

        if (nuevoCadete == null)
        {
            _logger.LogWarning("Error: Nuevo cadete no encontrado.");
            return false;
        }


        pedido.Cadete = nuevoCadete;
        Pedidos.AsignarPedidoAcadete(nuevoCadete.Id, pedido.Nro);
        acDped.ModificarEstadosDePedidosJson(pedido.Nro, Estado.enProceso.ToString(), nuevoCadete.Id);

        _logger.LogInformation("El pedido ha sido reasignado correctamente.");

        return true;
    }


    //cambiar estado de pedido
    public bool CambiarEstadoPedido(int idPedido)
    {
        bool res = false;
        foreach (var p in ListaPedidos)
        {
            if (p.Nro == idPedido)
            {
                p.Estado = Estado.terminado;
                _logger.LogInformation("El estado de pedido fue cambiado correctamente a 'TERMINADO'");

                Pedidos.ModificarPedidosEstado(p.Nro, Estado.terminado);
                acDped.ModificarEstadosDePedidosJson(p.Nro, Estado.terminado.ToString(), null);
                res = true;
                break; // Salimos del bucle ya que encontramos el pedido
            }
        }
        return res;
    }

   

    //jornal de cada cadete
    public (bool, double) JornalCobrar(int idCadete)
    {
        bool res = true;
        double cobro = 0;

        var contador = ListaPedidos.Count(p => p.Cadete != null && p.Cadete.Id == idCadete && p.Estado == Estado.terminado);
        if (contador == null)
        {
            res = false;
            return (res, cobro);

        }
        cobro = contador * 500;

        return (res, cobro);

    }




 
    //informe integral de la cadeteria

    public object InformeIntegral()
    {
        int cantPedidos=ListaPedidos.Count();
        int pedidosConCadete = ListaPedidos.Where(p => p.Cadete != null).Count();
        int PeddosTerminados=ListaPedidos.Where(p=>p.Estado == Estado.terminado).Count();
        int PedidosEnProceso=ListaPedidos.Where(p=>p.Estado==Estado.enProceso).Count();
        double TotalDinero = PeddosTerminados * 500;
        int PedidosSinAsignar = ListaPedidos.Where(p => p.Cadete == null).Count();
        int CantidadDeCadetes=ListaCadetes.Count();



        return new
        {
            Pedidos = cantPedidos,
            PedidosConCadetes = pedidosConCadete,
            PedidosTerminados = PeddosTerminados,
            PedidosEnProceso = PedidosEnProceso,
            PedidosSinAsignar =PedidosSinAsignar,
            TotalDinero = TotalDinero,
            CantidadDeCadetes=CantidadDeCadetes,


        };
    }






}
