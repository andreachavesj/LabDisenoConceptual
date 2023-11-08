using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

public class CicloService
{
    private readonly IMongoCollection<Ciclo> _cicloCollection;
    private readonly MongoDBContext _mongoDBContext;

    public CicloService(
        IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings, MongoDBContext mongoDBContext)
    {
        _mongoDBContext = mongoDBContext;
        _cicloCollection = _mongoDBContext.Database.GetCollection<Ciclo>(
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