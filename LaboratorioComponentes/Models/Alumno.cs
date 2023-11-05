using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LaboratorioComponentes.Models;

public class Alumno
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string cedula { get; set; } = null!;

    public string nombre { get; set; } = null!;

    public string telefono { get; set; } = null!;

    public string email { get; set; } = null!;

    public string fecha_nacimiento { get; set; } = null!;

    public string carrera { get; set; } = null!;


}