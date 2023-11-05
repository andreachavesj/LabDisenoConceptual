using LaboratorioApi.Modelos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioApi.Servicios
{
    public class CursoService
    {
        private readonly IMongoCollection<Curso> _cursoCollection;

        public CursoService(
            IOptions<ProyectoDatabaseSettings> proyectoDbSettings)
        {
            var mongoClient = new MongoClient(
                proyectoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                proyectoDbSettings.Value.DatabaseName);

            _cursoCollection = mongoDatabase.GetCollection<Curso>(
                proyectoDbSettings.Value.CursoCollectionName);
        }

        public async Task<List<Curso>> GetAsync() =>
            await _cursoCollection.Find(_ => true).ToListAsync();

        public async Task<Curso?> GetAsync(string id) =>
            await _cursoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Curso newCurso) =>
            await _cursoCollection.InsertOneAsync(newCurso);

        public async Task UpdateAsync(string id, Curso updatedCurso) =>
            await _cursoCollection.ReplaceOneAsync(x => x.Id == id, updatedCurso);

        public async Task RemoveAsync(string id) =>
            await _cursoCollection.DeleteOneAsync(x => x.Id == id);
    }
}
