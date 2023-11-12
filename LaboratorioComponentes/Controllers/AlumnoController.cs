using LaboratorioComponentes.Builder;
using LaboratorioComponentes.IBuilder;
using LaboratorioComponentes.IObserver;
using LaboratorioComponentes.Models;
using LaboratorioComponentes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioComponentes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnoController : ControllerBase, ICicloObserver
{
    private readonly AlumnoService _alumnoService;

    public AlumnoController(AlumnoService alumnoService, CicloService cicloService)
    {
        _alumnoService = alumnoService;
        cicloService.AgregarCicloObserver(this);
    }

    [ApiExplorerSettings(IgnoreApi = true)] // Ignorar este método en Swagger
    public void NotificarNuevoCiclo(Ciclo ciclo)
    {
        // Lógica específica para notificar a los alumnos sobre el nuevo ciclo
        // Puedes acceder a la lógica de notificación de alumnos desde _alumnoService.
        _alumnoService.NotificarNuevoCiclo(ciclo);
    }

    [HttpGet]
    public async Task<List<Alumno>> Get() =>
        await _alumnoService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Alumno>> Get(string id)
    {
        var alumno = await _alumnoService.GetAsync(id);

        if (alumno is null)
        {
            return NotFound();
        }

        return alumno;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Alumno alumno)
    {
        try
        {
            IBuilderAlumno alumnoBuilder = new AlumnoBuilder();
            Director director = new Director(alumnoBuilder);

            Alumno newAlumno = director.CrearAlumno(
                alumno.cedula,
                alumno.nombre,
                alumno.telefono,
                alumno.email,
                alumno.fecha_nacimiento,
                alumno.carrera
            );

            await _alumnoService.CreateAsync(newAlumno);

            return CreatedAtAction(nameof(Get), new { id = newAlumno.Id }, newAlumno);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Alumno updatedAlumno)
    {
        var alumno = await _alumnoService.GetAsync(id);

        if (alumno is null)
        {
            return NotFound();
        }

        updatedAlumno.Id = alumno.Id;

        await _alumnoService.UpdateAsync(id, updatedAlumno);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var alumno = await _alumnoService.GetAsync(id);

        if (alumno is null)
        {
            return NotFound();
        }

        await _alumnoService.RemoveAsync(id);

        return NoContent();
    }
}