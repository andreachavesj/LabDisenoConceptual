using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LaboratorioApi.Modelos
{
    public class Curso
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int Codigo { get; set; } 

        public string Nombre { get; set; }

        public int Creditos { get; set; } 

        public int HorasSemanales { get; set; }
        
        public string codigo_carrera { get; set; }
    }
}
