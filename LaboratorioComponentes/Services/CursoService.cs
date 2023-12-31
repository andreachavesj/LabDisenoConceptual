﻿using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

    public class CursoService
    {
        private readonly IMongoCollection<Curso> _cursoCollection;
        private readonly MongoDBContext _mongoDBContext;

    public CursoService(
            IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings, MongoDBContext mongoDBContext)
    {
        _mongoDBContext = mongoDBContext;
        _cursoCollection = _mongoDBContext.Database.GetCollection<Curso>(
        proyectoDatabaseSettings.Value.CursoCollectionName);
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

