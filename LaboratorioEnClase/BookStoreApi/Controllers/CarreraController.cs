using LaboratorioApi.Modelos;
using LaboratorioApi.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace LaboratorioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarreraController : ControllerBase
    {

        private readonly CarreraService _carreraService;

        public CarreraController(CarreraService carreraService) =>
            _carreraService = carreraService;

        [HttpGet]
        public async Task<List<Carrera>> Get() =>
            await _carreraService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Carrera>> Get(string id)
        {
            var carrera = await _carreraService.GetAsync(id);

            if (carrera is null)
            {
                return NotFound();
            }

            return carrera;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Carrera newCarrera)
        {
            await _carreraService.CreateAsync(newCarrera);

            return CreatedAtAction(nameof(Get), new { id = newCarrera.Id }, newCarrera);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Carrera updatedCarrera)
        {
            var carrera = await _carreraService.GetAsync(id);

            if (carrera is null)
            {
                return NotFound();
            }

            updatedCarrera.Id = carrera.Id;

            await _carreraService.UpdateAsync(id, updatedCarrera);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var carrera = await _carreraService.GetAsync(id);

            if (carrera is null)
            {
                return NotFound();
            }

            await _carreraService.RemoveAsync(id);

            return NoContent();
        }
    }
}
