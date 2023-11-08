using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

    public class AlumnoService
    {
        private readonly IMongoCollection<Alumno> _alumnoCollection;
        private readonly MongoDBContext _mongoDBContext;

    public AlumnoService(
        IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings, MongoDBContext mongoDBContext)
    {
        _mongoDBContext = mongoDBContext; // Asigna la instancia de MongoDBContext
        _alumnoCollection = _mongoDBContext.Database.GetCollection<Alumno>(
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

