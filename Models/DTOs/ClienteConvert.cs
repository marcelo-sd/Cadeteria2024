using Cadeteria2024MD.Models.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cadeteria2024MD.Models.DTOs
{
    public class ClienteConvert : JsonConverter<Icliente>
    {
        // Sobrescribe el método Read para deserializar el JSON en un objeto Icliente
        public override Icliente Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parsea el JSON en un objeto JsonDocument
            var jsonObject = JsonDocument.ParseValue(ref reader);

            // Obtiene el texto JSON del objeto raíz
            var jsonObjectString = jsonObject.RootElement.GetRawText();

            // Deserializa el texto JSON en un objeto ClientesDTO y lo retorna
            return JsonSerializer.Deserialize<ClientesDTO>(jsonObjectString, options);
        }

        // Sobrescribe el método Write para serializar un objeto Icliente en JSON
        public override void Write(Utf8JsonWriter writer, Icliente value, JsonSerializerOptions options)
        {
            // Serializa el objeto Icliente (convertido a ClientesDTO) en JSON
            JsonSerializer.Serialize(writer, (ClientesDTO)value, options);
        }
    }
}
