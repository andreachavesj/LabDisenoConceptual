using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace LaboratorioApi.Modelos
{
    public class Carrera
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string codigo { get; set; }

        public string nombre { get; set; }

        public string titulo { get; set; }

        public CursoCarrera[]? cursos { get; set; } = null!;
    }

    public class CursoCarrera
    {
        public int codigo_curso { get; set; }
    }

}
