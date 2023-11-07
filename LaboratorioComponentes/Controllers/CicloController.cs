using LaboratorioComponentes.Models;
using LaboratorioComponentes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioComponentes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CicloController : ControllerBase
{
    private readonly CicloService _cicloService;

    public CicloController(CicloService cicloService) =>
        _cicloService = cicloService;

    [HttpGet]
    public async Task<List<Ciclo>> Get() =>
        await _cicloService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Ciclo>> Get(string id)
    {
        var ciclo = await _cicloService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        return ciclo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Ciclo newCiclo)
    {
        await _cicloService.CreateAsync(newCiclo);

        return CreatedAtAction(nameof(Get), new { id = newCiclo.Id }, newCiclo);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Ciclo updatedCiclo)
    {
        var ciclo = await _cicloService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        updatedCiclo.Id = ciclo.Id;

        await _cicloService.UpdateAsync(id, updatedCiclo);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var ciclo = await _cicloService.GetAsync(id);

        if (ciclo is null)
        {
            return NotFound();
        }

        await _cicloService.RemoveAsync(id);

        return NoContent();
    }
}


