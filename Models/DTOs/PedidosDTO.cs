using Cadeteria2024MD.Models.Interfaces;
using System.Text.Json.Serialization;

namespace Cadeteria2024MD.Models.DTOs
{
    public class PedidosDTO
    {
        public string Obs { get; set; } = string.Empty;
        //public ClientesDTO Cliente { get; set; } = new ClientesDTO();

        [JsonConverter(typeof(ClienteConvert))]
        public Icliente Cliente { get; set; } = new ClientesDTO();
      
    }
}
