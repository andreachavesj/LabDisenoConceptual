const apiUrl = 'https://localhost:44308/api/Alumno';

// Fetch Alumnos from the API
async function fetchAlumnos() {
    try {
        const response = await fetch(apiUrl);
        if (!response.ok) {
            throw new Error(`Error al obtener datos del servidor. Código: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error en la solicitud API:', error.message);
    }
}

// Create a new Alumno
async function createAlumno(alumnoData) {
    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(alumnoData),
        });

        if (!response.ok) {
            throw new Error(`Error al crear el alumno. Código: ${response.status}`);
        }

        const createdAlumno = await response.json();
        return createdAlumno;
    } catch (error) {
        console.error('Error en la solicitud API:', error.message);
        throw error;
    }
}

// Edit an existing Alumno
async function editAlumno(alumnoId, alumnoData) {
    try {
        console.log("ID alumno en edit" + alumnoId)
        const url = `${apiUrl}/${alumnoId}`;
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(alumnoData),
        });

        if (response.status === 204) {
            return { success: true };
        } else if (!response.ok) {
            throw new Error(`Error al editar el alumno. Código: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error al editar el alumno:', error.message);
        throw error;
    }
}

// Delete an Alumno
async function deleteAlumno(alumnoId) {
    console.log("ID alumno en delete" + alumnoId)
    try {
        const url = `${apiUrl}/${alumnoId}`;
        const response = await fetch(url, {
            method: 'DELETE',
        });

        if (response.status === 204) {
            return { success: true };
        } else if (!response.ok) {
            throw new Error(`Error al eliminar el alumno. Código: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error al eliminar el alumno:', error.message);
        throw error;
    }
}

// Update table with Alumnos data
async function updateTable() {
    const alumnos = await fetchAlumnos();
    const tableBody = document.querySelector('tbody');
    tableBody.innerHTML = '';

    alumnos.forEach((alumno, index) => {
        const row = `
        <tr>
            <th scope="row">${index + 1}</th>
            <td>${alumno.nombre}</td>
            <td>${alumno.cedula}</td>
            <td>${alumno.telefono}</td>
            <td>${alumno.email}</td>
            <td>${alumno.fecha_nacimiento}</td>
            <td>${alumno.carrera}</td>
        </tr>
    `;
        tableBody.innerHTML += row;
    });
}

// Event listeners and other functions (like openModalCreateClient, openModalEditClient, etc.) remain as they are
// Just make sure to adapt them to use the Alumno model properties

document.addEventListener('DOMContentLoaded', updateTable);

function openCreateAlumno() {
    Swal.fire({
        title: 'Crear Alumno',
        html: `
         <div class="edit-alumno">
            <label for="cedula">Cédula</label>
            <input id="cedula" class="swal2-input">
            </div>
             <div class="edit-alumno">
            <label for="nombre">Nombre</label>
            <input id="nombre" class="swal2-input">
            </div>
             <div class="edit-alumno">
            <label for="telefono">Teléfono</label>
            <input id="telefono" class="swal2-input">
            </div>
             <div class="edit-alumno">
            <label for="email">Email</label>
            <input id="email" class="swal2-input">
            </div>
             <div class="edit-alumno">
            <label for="carrera">Carrera</label>
            <input id="carrera" class="swal2-input">
            </div>
             <div class="edit-alumno">
            <label for="fechaNacimiento">Fecha de Nacimiento</label>
            <input id="fechaNacimiento" type="date" class="swal2-input">
            </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Guardar',
        cancelButtonText: 'Cancelar',
        preConfirm: () => {
            const cedula = Swal.getPopup().querySelector('#cedula').value;
            const nombre = Swal.getPopup().querySelector('#nombre').value;
            const telefono = Swal.getPopup().querySelector('#telefono').value;
            const email = Swal.getPopup().querySelector('#email').value;
            const carrera = Swal.getPopup().querySelector('#carrera').value;
            const fechaNacimiento = Swal.getPopup().querySelector('#fechaNacimiento').value;


            if (!cedula || !nombre || !telefono || !email || !fechaNacimiento || !carrera) {
                Swal.showValidationMessage('Por favor, complete todos los campos');
            }

            return { cedula, nombre, telefono, email, fecha_nacimiento: fechaNacimiento, carrera };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const { cedula, nombre, telefono, email, carrera, fecha_nacimiento  } = result.value;
            createAlumno({ cedula, nombre, telefono, email, carrera, fecha_nacimiento  })
                .then(async (createdAlumno) => {
                    await updateTable();
                    Swal.fire({
                        title: 'Alumno creado',
                        icon: 'success',
                        text: 'El alumno ha sido creado exitosamente.'
                    });
                })
                .catch((error) => {
                    console.error('Error al crear el alumno:', error.message);
                });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire({
                title: 'Acción cancelada',
                icon: 'info',
                text: 'La creación del alumno ha sido cancelada.'
            });
        }
    });
}

async function openEditAlumno() {
    const alumnos = await fetchAlumnos();
    if (!alumnos || alumnos.length === 0) {
        Swal.fire('No hay alumnos disponibles para editar');
        return;
    }

    const alumnoOptions = alumnos.map(alumno => `<option value="${alumno.Id}">${alumno.nombre}</option>`).join('');

    Swal.fire({
        title: 'Editar Alumno',
        html: `
           
            <label for="alumnoId">Escoja el alumno a editar: </label>
            <div class="edit-alumno">
            <select id="alumnoId" class="swal2-select">${alumnoOptions}</select>
            </div>
            <div class="edit-alumno">
            <label for="nombre">Nombre</label>
            <input id="nombre" class="swal2-input">
            </div>
            <div class="edit-alumno">
            <label for="cedula">Cédula</label>
            <input id="cedula" class="swal2-input">
            </div>
            <div class="edit-alumno">
            <label for="telefono">Teléfono</label>
            <input id="telefono" class="swal2-input">
            </div>
            <div class="edit-alumno">
            <label for="email">Email</label>
            <input id="email" class="swal2-input">
            </div>
            <div class="edit-alumno">
            <label for="fechaNacimiento">Fecha de Nacimiento</label>
            <input id="fechaNacimiento" type="date" class="swal2-input">
            </div>
            <div class="edit-alumno">
            <label for="carrera">Carrera</label>
            <input id="carrera" class="swal2-input">
            </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Guardar',
        cancelButtonText: 'Cancelar',
        didOpen: () => {
            // This function is triggered when the modal is fully displayed
            const alumnoIdSelect = Swal.getPopup().querySelector('#alumnoId');
            alumnoIdSelect.addEventListener('change', async (e) => {
                console.log('target value' + e.target.value)
                const selectedAlumnoId = e.target.value;
                const selectedAlumno = alumnos.find(alumno => alumno.Id === selectedAlumnoId);
                console.log("selectedAlumnoId" + selectedAlumnoId)
                if (selectedAlumno) {
                    Swal.getPopup().querySelector('#cedula').value = selectedAlumno.cedula;
                    Swal.getPopup().querySelector('#nombre').value = selectedAlumno.nombre;
                    Swal.getPopup().querySelector('#telefono').value = selectedAlumno.telefono;
                    Swal.getPopup().querySelector('#email').value = selectedAlumno.email;
                    Swal.getPopup().querySelector('#fechaNacimiento').value = selectedAlumno.fecha_nacimiento;
                    Swal.getPopup().querySelector('#carrera').value = selectedAlumno.carrera;
                }
            });
        },
        preConfirm: () => {
            const alumnoId = Swal.getPopup().querySelector('#alumnoId').value;
            const cedula = Swal.getPopup().querySelector('#cedula').value;
            const nombre = Swal.getPopup().querySelector('#nombre').value;
            const telefono = Swal.getPopup().querySelector('#telefono').value;
            const email = Swal.getPopup().querySelector('#email').value;
            const fechaNacimiento = Swal.getPopup().querySelector('#fechaNacimiento').value;
            const carrera = Swal.getPopup().querySelector('#carrera').value;

            if (!cedula || !nombre || !telefono || !email || !fechaNacimiento || !carrera) {
                Swal.showValidationMessage('Por favor, complete todos los campos');
            }

            return { alumnoId, nombre, cedula, telefono, email, fecha_nacimiento: fechaNacimiento, carrera };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const { alumnoId, nombre, cedula, telefono, email, fecha_nacimiento, carrera } = result.value;
            editAlumno(alumnoId, { nombre, cedula, telefono, email, fecha_nacimiento, carrera })
                .then(async () => {
                    await updateTable();
                    Swal.fire({
                        title: 'Alumno editado',
                        icon: 'success',
                        text: 'El alumno ha sido editado exitosamente.'
                    });
                })
                .catch((error) => {
                    console.error('Error al editar el alumno:', error.message);
                });
        }
    });
}


async function openDeleteAlumno() {

    const alumnos = await fetchAlumnos();
    if (!alumnos || alumnos.length === 0) {
        Swal.fire('No hay alumnos disponibles para borrar');
        return;
    }

    const alumnosList = alumnos.map(alumno => `<option value="${alumno.Id}">${alumno.cedula}</option>`).join('');

    Swal.fire({
        title: 'Eliminar Alumno',
        html: `
            <label for="idToDelete">Cédula del alumno a eliminar</label>
            <select id="idToDelete" class="swal2-select">${alumnosList}</select>
        `,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Siguiente',
        cancelButtonText: 'Cancelar',
        preConfirm: async () => {
            const idToDelete = Swal.getPopup().querySelector('#idToDelete').value;
            console.log("idToDelete" + idToDelete)
            if (!idToDelete) {
                Swal.showValidationMessage('Por favor, seleccione un ID');
            }

            const confirmResult = await Swal.fire({
                title: 'Confirmar eliminación',
                text: '¿Está seguro de que desea eliminar este alumno?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            });

            if (confirmResult.isConfirmed) {
                try {
                    await deleteAlumno(idToDelete);
                    await updateTable();

                    Swal.fire({
                        title: 'Alumno eliminado',
                        icon: 'success',
                        text: 'El alumno ha sido eliminado exitosamente.'
                    });
                } catch (error) {
                    console.error('Error al eliminar el alumno:', error.message);
                    Swal.fire({
                        title: 'Error',
                        icon: 'error',
                        text: 'Hubo un error al intentar eliminar el alumno.'
                    });
                }
            } else {
                Swal.fire({
                    title: 'Acción cancelada',
                    icon: 'info',
                    text: 'La eliminación del alumno ha sido cancelada.'
                });
            }
        }
    });
}

function generateDropdownOptions() {
    const tableRows = document.querySelectorAll('tbody tr');
    const options = Array.from(tableRows).map((row) => {
        const idCell = row.querySelector('th');
        return `<option value="${idCell.textContent}">${idCell.textContent}</option>`;
    });

    return options.join('');
}
