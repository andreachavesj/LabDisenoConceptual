using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStoreApi.Modelos;

public class Alumno
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? _id { get; set; }


    [BsonElement("cedula")]
    [JsonPropertyName("cedula")]
    public string cedula { get; set; } = null!;

    public decimal nombre { get; set; }

    public string telefono { get; set; } = null!;

    public string email { get; set; } = null!;
    
    public string fecha_nacimiento { get; set; } = null!;
    
    public string carrera { get; set; } = null!;


}