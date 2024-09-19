namespace Cadeteria2024MD.Models.DTOs
{
    public class PedidosDTO
    {
        public string Obs { get; set; } = string.Empty;
        public Clientes Cliente { get; set; } = new Clientes();
        public Estado Estado { get; set; }
        //aqui vamos a contener la lista de clientes
        public static List<Clientes> ListaClientes { get; set; } = new List<Clientes>();
        public Cadetes? Cadete { get; set; }
    }
}
