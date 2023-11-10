using LaboratorioComponentes.Builder;
using LaboratorioComponentes.Models;

namespace LaboratorioComponentes.IBuilder
{
    public class AlumnoBuilder : IBuilderAlumno
    {
        private Alumno alumno = new Alumno();

        public IBuilderAlumno SetCarrera(string carrera)
        {
            alumno.carrera = carrera;
            return this;
        }

        public IBuilderAlumno SetCedula(string cedula)
        {
            alumno.cedula = cedula;
            return this;
        }

        public IBuilderAlumno SetEmail(string email)
        {
            alumno.email = email;
            return this;
        }

        public IBuilderAlumno SetFechaNacimiento(string fechaNacimiento)
        {
            alumno.fecha_nacimiento = fechaNacimiento;
            return this;
        }

        public IBuilderAlumno SetNombre(string nombre)
        {
            alumno.nombre = nombre;
            return this;
        }

        public IBuilderAlumno SetTelefono(string telefono)
        {
            alumno.telefono = telefono; 
            return this;
        }

        Alumno IBuilderAlumno.Build()
        {
            return alumno;
        }
    }
}
