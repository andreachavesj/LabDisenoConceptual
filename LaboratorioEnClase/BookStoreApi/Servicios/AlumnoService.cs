using LaboratorioApi.Modelos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioApi.Servicios
{
    public class AlumnoService
    {
        private readonly IMongoCollection<Alumno> _booksCollection;

        public AlumnoService(
            IOptions<ProyectoDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Alumno>(
                bookStoreDatabaseSettings.Value.AlumnoCollectionName);
        }

        public async Task<List<Alumno>> GetAsync() =>
            await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Alumno?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Alumno newBook) =>
            await _booksCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Alumno updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}