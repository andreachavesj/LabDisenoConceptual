using LaboratorioComponentes.IObserver;
using LaboratorioComponentes.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LaboratorioComponentes.Services;

    public class AlumnoService : ICicloObserver 
    {
        private readonly IMongoCollection<Alumno> _alumnoCollection;
        private readonly MongoDBContext _mongoDBContext;
        private readonly IMongoCollection<Ciclo> _cicloCollection;
        private readonly List<ICicloObserver> _cicloObservers = new List<ICicloObserver>();

    public AlumnoService(
        IOptions<ProyectoDatabaseSettings> proyectoDatabaseSettings,
        MongoDBContext mongoDBContext,
        CicloService cicloService)
    {
        _mongoDBContext = mongoDBContext;
        _alumnoCollection = _mongoDBContext.Database.GetCollection<Alumno>(
            proyectoDatabaseSettings.Value.AlumnoCollectionName);

        // Registrar este servicio como observador del servicio de ciclos
        cicloService.AgregarCicloObserver(this);
    }

    public async Task CrearNuevoCicloAsync(Ciclo nuevoCiclo)
    {
        await _cicloCollection.InsertOneAsync(nuevoCiclo);

        // Notificar a los observadores que se ha creado un nuevo ciclo
        NotificarCicloObservers(nuevoCiclo);
    }
    public void AgregarCicloObserver(ICicloObserver observer)
    {
        _cicloObservers.Add(observer);
    }

    public void EliminarCicloObserver(ICicloObserver observer)
    {
        _cicloObservers.Remove(observer);
    }

    private void NotificarCicloObservers(Ciclo ciclo)
    {
        foreach (var observer in _cicloObservers)
        {
            observer.NotificarNuevoCiclo(ciclo);
        }
    }

    public void NotificarNuevoCiclo(Ciclo ciclo)
    {
        // Lógica para notificar a los alumnos sobre el nuevo ciclo
        var alumnos = _alumnoCollection.Find(_ => true).ToList();

        foreach (var alumno in alumnos)
        {
            EnviarNotificacion(alumno, ciclo);
        }
    }

    private void EnviarNotificacion(Alumno alumno, Ciclo ciclo)
    {
        // Lógica para enviar una notificación al alumno sobre el nuevo ciclo
        // Ejemplo: aquí simplemente mostramos un mensaje en la consola
        Console.WriteLine($"Notificación enviada.");
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

