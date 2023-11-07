using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

    public class CarreraService
    {
        private readonly IMongoCollection<Carrera> _carreraCollection;

        public CarreraService(
            IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                proyectoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                proyectoDatabaseSettings.Value.DatabaseName);

            _carreraCollection = mongoDatabase.GetCollection<Carrera>(
                proyectoDatabaseSettings.Value.CarreraCollectionName);
        }

        public async Task<List<Carrera>> GetAsync() =>
            await _carreraCollection.Find(_ => true).ToListAsync();

        public async Task<Carrera?> GetAsync(string id) =>
            await _carreraCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Carrera newCarrera) =>
            await _carreraCollection.InsertOneAsync(newCarrera);

        public async Task UpdateAsync(string id, Carrera updatedCarrera) =>
            await _carreraCollection.ReplaceOneAsync(x => x.Id == id, updatedCarrera);

        public async Task RemoveAsync(string id) =>
            await _carreraCollection.DeleteOneAsync(x => x.Id == id);
    }

