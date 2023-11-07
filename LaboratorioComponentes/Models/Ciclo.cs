using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LaboratorioComponentes.Models;


public class Ciclo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int anio { get; set; }
    public int numero { get; set; }
    public string fecha_inicio { get; set; } = null!;
    public string fecha_finalizacion { get; set; } = null!;

}
