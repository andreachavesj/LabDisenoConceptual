using LaboratorioComponentes.Models;
using LaboratorioComponentes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioComponentes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase
    {
        private readonly CursoService _cursoService;

        public CursoController(CursoService cursoService) =>
            _cursoService = cursoService;

        [HttpGet]
        public async Task<List<Curso>> Get() =>
            await _cursoService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Curso>> Get(string id)
        {
            var curso = await _cursoService.GetAsync(id);

            if (curso is null)
            {
                return NotFound();
            }

            return curso;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Curso newCurso)
        {
            await _cursoService.CreateAsync(newCurso);

            return CreatedAtAction(nameof(Get), new { id = newCurso.Id }, newCurso);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Curso updatedCurso)
        {
            var curso = await _cursoService.GetAsync(id);

            if (curso is null)
            {
                return NotFound();
            }

            updatedCurso.Id = curso.Id;

            await _cursoService.UpdateAsync(id, updatedCurso);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var curso = await _cursoService.GetAsync(id);

            if (curso is null)
            {
                return NotFound();
            }

            await _cursoService.RemoveAsync(id);

            return NoContent();
        }
    }

