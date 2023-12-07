const apiUrl = 'https://localhost:44308/api/Curso';

// Fetch Cursos from the API
async function fetchCursos() {
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

// Create a new Curso
async function createCurso(CursoData) {
    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(CursoData),
        });

        if (!response.ok) {
            throw new Error(`Error al crear el curso. Código: ${response.status}`);
        }

        const createdCurso = await response.json();
        return createdCurso;
    } catch (error) {
        console.error('Error en la solicitud API:', error.message);
        throw error;
    }
}

// Edit an existing Curso
async function editCurso(CursoId, CursoData) {
    try {
        console.log("ID curso en edit" + CursoId)
        const url = `${apiUrl}/${CursoId}`;
        const response = await fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(CursoData),
        });

        if (response.status === 204) {
            return { success: true };
        } else if (!response.ok) {
            throw new Error(`Error al editar el curso. Código: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error al editar el curso:', error.message);
        throw error;
    }
}

// Delete an Curso
async function deleteCurso(CursoId) {
    console.log("ID curso en delete" + CursoId)
    try {
        const url = `${apiUrl}/${CursoId}`;
        const response = await fetch(url, {
            method: 'DELETE',
        });

        if (response.status === 204) {
            return { success: true };
        } else if (!response.ok) {
            throw new Error(`Error al eliminar el curso. Código: ${response.status}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error al eliminar el curso:', error.message);
        throw error;
    }
}

// Update table with Cursos data
async function updateTable() {
    const cursos = await fetchCursos();
    const tableBody = document.querySelector('tbody');
    tableBody.innerHTML = '';

    cursos.forEach((curso, index) => {
        const row = `
        <tr>
            <th scope="row">${index + 1}</th>
            <td>${curso.Nombre}</td>
            <td>${curso.Creditos}</td>
            <td>${curso.HorasSemanales}</td>
            <td>${curso.codigo_carrera}</td>
        </tr>
    `;
        tableBody.innerHTML += row;
    });
}

// Event listeners and other functions (like openModalCreateClient, openModalEditClient, etc.) remain as they are
// Just make sure to adapt them to use the Curso model properties

document.addEventListener('DOMContentLoaded', updateTable);

function openCreateCurso() {
    Swal.fire({
        title: 'Crear Curso',
        html: `
         <div class="edit-curso">
            <label for="codigo">Codigo</label>
            <input id="codigo" class="swal2-input">
            </div>
             <div class="edit-curso">
            <label for="nombre">Nombre</label>
            <input id="nombre" class="swal2-input">
            </div>
             <div class="edit-curso">
            <label for="creditos">Creditos</label>
            <input id="creditos" class="swal2-input">
            </div>
             <div class="edit-curso">
            <label for="horas">Horas semanales</label>
            <input id="horas" class="swal2-input">
            </div>
             <div class="edit-curso">
            <label for="codigoCarrera">Codigo Carrera</label>
            <input id="codigoCarrera" class="swal2-input">
            </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Guardar',
        cancelButtonText: 'Cancelar',
        preConfirm: () => {
            const codigo = Swal.getPopup().querySelector('#codigo').value;
            const nombre = Swal.getPopup().querySelector('#nombre').value;
            const creditos = Swal.getPopup().querySelector('#creditos').value;
            const horas = Swal.getPopup().querySelector('#horas').value;
            const carrera = Swal.getPopup().querySelector('#codigoCarrera').value;


            if (!codigo || !nombre || !creditos || !horas || !fechaNacimiento || !carrera) {
                Swal.showValidationMessage('Por favor, complete todos los campos');
            }

            return { codigo, nombre, creditos, horas, carrera };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const { codigo, nombre, creditos, horas, carrera } = result.value;
            createCurso({ codigo, nombre, creditos, horas, carrera })
                .then(async (createdCurso) => {
                    await updateTable();
                    Swal.fire({
                        title: 'Curso creado',
                        icon: 'success',
                        text: 'El curso ha sido creado exitosamente.'
                    });
                })
                .catch((error) => {
                    console.error('Error al crear el curso:', error.message);
                });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire({
                title: 'Acción cancelada',
                icon: 'info',
                text: 'La creación del curso ha sido cancelada.'
            });
        }
    });
}

async function openEditCurso() {
    const cursos = await fetchCursos();
    if (!cursos || cursos.length === 0) {
        Swal.fire('No hay cursos disponibles para editar');
        return;
    }

    const CursoOptions = cursos.map(curso => `<option value="${curso.Id}">${curso.nombre}</option>`).join('');

    Swal.fire({
        title: 'Editar Curso',
        html: `
           
            <label for="CursoId">Escoja el curso a editar: </label>
            <div class="edit-curso">
            <select id="CursoId" class="swal2-select">${CursoOptions}</select>
            </div>
            <div class="edit-curso">
            <label for="nombre">Nombre</label>
            <input id="nombre" class="swal2-input">
            </div>
            <div class="edit-curso">
            <label for="creditos">Creditos</label>
            <input id="creditos" class="swal2-input">
            </div>
            <div class="edit-curso">
            <label for="horas">Horas semanales</label>
            <input id="horas" class="swal2-input">
            </div>
            <div class="edit-curso">
            <label for="codigoCarrera">Codigo Carrera</label>
            <input id="codigoCarrera" class="swal2-input">
            </div>
        `,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: 'Guardar',
        cancelButtonText: 'Cancelar',
        didOpen: () => {
            // This function is triggered when the modal is fully displayed
            const cursoIdSelect = Swal.getPopup().querySelector('#CursoId');
            cursoIdSelect.addEventListener('change', async (e) => {
                console.log('target value' + e.target.value)
                const selectedCursoId = e.target.value;
                const selectedCurso = cursos.find(curso => curso.Id === selectedCursoId);
                console.log("selectedCursoId" + selectedCursoId)
                if (selectedCurso) {
                    Swal.getPopup().querySelector('#CursoId').value = selectedCurso.Id;
                    Swal.getPopup().querySelector('#nombre').value = selectedCurso.nombre;
                    Swal.getPopup().querySelector('#creditos').value = selectedCurso.creditos;
                    Swal.getPopup().querySelector('#horas').value = selectedCurso.horas;
                    Swal.getPopup().querySelector('#codigoCarrera').value = selectedCurso.codigo_carrera;
                }
            });
        },
        preConfirm: () => {
            const CursoId = Swal.getPopup().querySelector('#CursoId').value;
            const nombre = Swal.getPopup().querySelector('#nombre').value;
            const creditos = Swal.getPopup().querySelector('#creditos').value;
            const horas = Swal.getPopup().querySelector('#horas').value;
            const codigoCarrera = Swal.getPopup().querySelector('#codigoCarrera').value;

            if (!nombre || !creditos || !horas || !codigoCarrera) {
                Swal.showValidationMessage('Por favor, complete todos los campos');
            }

            return { CursoId, nombre, creditos, horas, codigoCarrera };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            const { CursoId, nombre, creditos, horas, codigoCarrera } = result.value;
            editCurso(CursoId, { nombre, creditos, horas, email, codigoCarrera })
                .then(async () => {
                    await updateTable();
                    Swal.fire({
                        title: 'Curso editado',
                        icon: 'success',
                        text: 'El curso ha sido editado exitosamente.'
                    });
                })
                .catch((error) => {
                    console.error('Error al editar el curso:', error.message);
                });
        }
    });
}


async function openDeleteCurso() {

    const cursos = await fetchCursos();
    if (!cursos || cursos.length === 0) {
        Swal.fire('No hay cursos disponibles para borrar');
        return;
    }

    const CursosList = cursos.map(curso => `<option value="${curso.Id}">${curso.codigo}</option>`).join('');

    Swal.fire({
        title: 'Eliminar Curso',
        html: `
            <label for="idToDelete">Codigo del curso a eliminar</label>
            <select id="idToDelete" class="swal2-select">${CursosList}</select>
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
                text: '¿Está seguro de que desea eliminar este curso?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            });

            if (confirmResult.isConfirmed) {
                try {
                    await deleteCurso(idToDelete);
                    await updateTable();

                    Swal.fire({
                        title: 'Curso eliminado',
                        icon: 'success',
                        text: 'El curso ha sido eliminado exitosamente.'
                    });
                } catch (error) {
                    console.error('Error al eliminar el curso:', error.message);
                    Swal.fire({
                        title: 'Error',
                        icon: 'error',
                        text: 'Hubo un error al intentar eliminar el curso.'
                    });
                }
            } else {
                Swal.fire({
                    title: 'Acción cancelada',
                    icon: 'info',
                    text: 'La eliminación del curso ha sido cancelada.'
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
