using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

    public class AlumnoService
    {
        private readonly IMongoCollection<Alumno> _alumnoCollection;

        public AlumnoService(
            IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                proyectoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                proyectoDatabaseSettings.Value.DatabaseName);

            _alumnoCollection = mongoDatabase.GetCollection<Alumno>(
                proyectoDatabaseSettings.Value.AlumnoCollectionName);
        }

        public async Task<List<Alumno>> GetAsync() =>
            await _alumnoCollection.Find(_ => true).ToListAsync();

        public async Task<Alumno?> GetAsync(string id) =>
            await _alumnoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Alumno newAlumno) =>
            await _alumnoCollection.InsertOneAsync(newAlumno);

        public async Task UpdateAsync(string id, Alumno updatedAlumno) =>
            await _alumnoCollection.ReplaceOneAsync(x => x.Id == id, updatedAlumno);

        public async Task RemoveAsync(string id) =>
            await _alumnoCollection.DeleteOneAsync(x => x.Id == id);
    }

