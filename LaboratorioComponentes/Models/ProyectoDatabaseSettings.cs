namespace LaboratorioComponentes.Models
{
    public class ProyectoDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string AlumnoCollectionName { get; set; } = null!;

        public string CarreraCollectionName { get; set; } = null!;

        public string CursoCollectionName { get; set; } = null!;

        public string CicloCollectionName { get; set; } = null!;
    }
}