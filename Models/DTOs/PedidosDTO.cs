using Cadeteria2024MD.Models.Interfaces;
using System.Text.Json.Serialization;

namespace Cadeteria2024MD.Models.DTOs
{
    public class PedidosDTO
    {
        public string Obs { get; set; } = string.Empty;
        //uso un convertidor personalizado  para serializar y deserializar 

        [JsonConverter(typeof(ClienteConvert))]
        public Icliente Cliente { get; set; } = new ClientesDTO();
      
    }
}
