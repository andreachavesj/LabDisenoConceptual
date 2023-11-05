using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace LaboratorioComponentes.Models;

    public class Carrera
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string codigo { get; set; } = null!;

        public string nombre { get; set; } = null!;

        public string titulo { get; set; } = null!;

        public CursoCarrera[]? cursos { get; set; } = null!;
    }

    public class CursoCarrera
    {
        public int codigo_curso { get; set; } 
    }


