using LaboratorioComponentes.Models;
using LaboratorioComponentes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioComponentes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnoController : ControllerBase
{
    private readonly AlumnoService _alumnoService;

    public AlumnoController(AlumnoService alumnoService) =>
        _alumnoService = alumnoService;

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
    public async Task<IActionResult> Post(Alumno newAlumno)
    {
        await _alumnoService.CreateAsync(newAlumno);

        return CreatedAtAction(nameof(Get), new { id = newAlumno.Id }, newAlumno);
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