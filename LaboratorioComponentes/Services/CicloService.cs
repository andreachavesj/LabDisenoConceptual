using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

public class CicloService
{
    private readonly IMongoCollection<Ciclo> _cicloCollection;

    public CicloService(
        IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            proyectoDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            proyectoDatabaseSettings.Value.DatabaseName);

        _cicloCollection = mongoDatabase.GetCollection<Ciclo>(
            proyectoDatabaseSettings.Value.CicloCollectionName);
    }

    public async Task<List<Ciclo>> GetAsync() =>
        await _cicloCollection.Find(_ => true).ToListAsync();

    public async Task<Ciclo?> GetAsync(string id) =>
        await _cicloCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Ciclo newCiclo) =>
        await _cicloCollection.InsertOneAsync(newCiclo);

    public async Task UpdateAsync(string id, Ciclo updatedCiclo) =>
        await _cicloCollection.ReplaceOneAsync(x => x.Id == id, updatedCiclo);

    public async Task RemoveAsync(string id) =>
        await _cicloCollection.DeleteOneAsync(x => x.Id == id);
}