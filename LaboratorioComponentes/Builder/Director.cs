using LaboratorioComponentes.IBuilder;
using LaboratorioComponentes.Models;

namespace LaboratorioComponentes.Builder
{
    public class Director
    {
        private IBuilderAlumno alumnoBuilder;

        public Director(IBuilderAlumno alumnoBuilder) 
        {
            this.alumnoBuilder = alumnoBuilder;
        }

        public Alumno CrearAlumno(string cedula, string nombre, string telefono, string email, string fechaNacimiento, string carrera)
        {
            return alumnoBuilder
                .SetCarrera(carrera)
                .SetEmail(email)
                .SetCedula(cedula)
                .SetTelefono(telefono)
                .SetFechaNacimiento(fechaNacimiento)
                .SetNombre(nombre)
                .Build();
        }
    }
}
