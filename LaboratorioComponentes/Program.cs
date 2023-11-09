using LaboratorioComponentes.Models;
using LaboratorioComponentes.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ProyectoDatabaseSettings>(
    builder.Configuration.GetSection("ProyectoDatabase"));

// Registro de MongoDBContext
builder.Services.AddSingleton(provider =>
{
    var settings = provider.GetRequiredService<IOptions<ProyectoDatabaseSettings>>().Value;
    return MongoDBContext.GetInstance(settings.ConnectionString, settings.DatabaseName);
});

// Registra los servicios que dependen de MongoDBContext
builder.Services.AddSingleton<AlumnoService>();
builder.Services.AddSingleton<CursoService>();
builder.Services.AddSingleton<CarreraService>();
builder.Services.AddSingleton<CicloService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Añade Swagger para documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

//Iterator demo
var persons = new Aggregate<Person>();
persons[0] = new Person("Johnn", 30);
persons[0] = new Person("Jane", 20);
persons[0] = new Person("Michael", 10);

var iterator = persons.Iterator;
while (iterator.IsLeft())
{
    Console.WriteLine(iterator.Current);
    iterator.Next();
}
