using LaboratorioComponentes.Models;
using LaboratorioComponentes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ProyectoDatabaseSettings>(
    builder.Configuration.GetSection("ProyectoDatabase"));

builder.Services.AddSingleton<AlumnoService>();
builder.Services.AddSingleton<CursoService>();
builder.Services.AddSingleton<CarreraService>();
builder.Services.AddSingleton<CicloService>();

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
