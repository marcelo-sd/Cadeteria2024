namespace Cadeteria2024MD.Models.DTOs
{
    public class PedidosDTO
    {
        public string Obs { get; set; } = string.Empty;
        public ClientesDTO Cliente { get; set; } = new ClientesDTO();
      
    }
}
