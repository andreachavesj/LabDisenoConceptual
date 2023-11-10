using LaboratorioComponentes.Models;

namespace LaboratorioComponentes.Builder
{
    public interface IBuilderAlumno
    {
        IBuilderAlumno SetNombre(String nombre);
        IBuilderAlumno SetCedula(String cedula);
        IBuilderAlumno SetTelefono(String telefono);
        IBuilderAlumno SetEmail(String email);
        IBuilderAlumno SetFechaNacimiento(String fechaNacimiento);
        IBuilderAlumno SetCarrera(String carrera);
        Alumno Build();
    }
}
