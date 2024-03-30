document.addEventListener("DOMContentLoaded", function () {
    GetAll();
    GetAllEstado();
    ShowModal();
    Add();
    Update();
});

function GetAllEstado() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5010/api/Estado/GetAll',
        success: function (result) {
            $.each(result.estados, function (i, estado) {
                var opcion = '<option id="opcionEstado" value=' + estado.idEstado + '>' + estado.estado + '</option>'
                $('#floatingSelect').append(opcion);
            })
        }
    })
}

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5010/api/Empleado/GetAll',
        success: function (result) {
            $.each(result.empleados, function (i, empleado) {
                var fila = '<tr>' +
                    '<td>' +
                    '<a href="#" onclick="GetById(' + empleado.idEmpleado + ')"><i class="fa-solid fa-pen-to-square fa-xl" style="color: #f4e900;"></i></a>' +
                    '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.idEmpleado + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.numeroNomina + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.nombre + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.apellidoPaterno + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.apellidoMaterno + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.catEntidadFederativa.estado + '</td>' +
                    '<td class = "text-center" scope = "col" >' +
                    '<a href="#" onclick="Delete(' + empleado.idEmpleado + ')"><i class="fa-solid fa-trash-can fa-xl" style="color: #ff6666;"></i></a>' +
                    '</td>' +
                    '<tr>';

                $('#tblEmpleado tbody').append(fila);
            })
        },
        error: function (result) {
            alert("Error en la consulta.");
        }
    })
}

function GetById(idEmpleado) {
    /*    $(".btn-warning").click(function () {*/

    $.ajax({
        type: 'GET',
        url: 'http://localhost:5010/api/Empleado/GetById/' + idEmpleado,
        success: function (result) {
            // Llenar el formulario en el modal con la información del empleado
            $("#lblIdEmpleado").val(result.idEmpleado);
            $("#txtNombre").val(result.nombre);
            $("#txtApellidoPaterno").val(result.apellidoPaterno);
            $("#txtApellidoMaterno").val(result.apellidoMaterno);
            $("#floatingSelect").val(result.catEntidadFederativa.idEstado);

            // Abrir el modal
            $("#exampleModal").modal("show");
        },
        error: function (error) {
            alert("Error al obtener la información del empleado.");
        }
    });
    /*});*/
}

function Delete(idEmpleado) {
    $.ajax({
        type: 'DELETE',
        url: 'http://localhost:5010/api/Empleado/Delete/' + idEmpleado,
        success: function (result) {
            alert("Se ha eliminado el empleado");
            location.reload();
        },
        error: function (resul) {
            alert("No se pudo eliminar el empleado");
            location.reload();
        }
    })
}

function Add() {
    $("#btnAddEmpleado").click(function () {
        var nombretxt = $("#txtNombre").val();
        var apellidoPaternotxt = $("#txtApellidoPaterno").val();
        var apellidoMaternotxt = $("#txtApellidoMaterno").val();
        var idEstadotxt = $("#txtIdEstado").val();

        var empleado = {
            "nombre": nombretxt,
            "apellidoPaterno": apellidoPaternotxt,
            "apellidoMaterno": apellidoMaternotxt,
            "catEntidadFederativa": {
                "idEstado": idEstadotxt
            }
        }

        $.ajax({
            type: 'POST',
            url: 'http://localhost:5010/api/Empleado/Add',
            contentType: 'application/json', // Establece el tipo de contenido
            data: JSON.stringify(empleado), // Convierte el objeto a JSON
            success: function (result) {
                alert("Se ha agregado el empleado");
                LimpiarFormulario();
                $("#exampleModal").modal('hide');
                location.reload();
            },
            error: function (getEmpleado) {
                alert("No se pudo agregar el empleado")
            }
        });
    });
}

function Update() {
    $("#btnUpdateEmpleado").click(function () {
        var idEmpleadolbl = $("#lblIdEmpleado").val();
        var nombretxt = $("#txtNombre").val();
        var apellidoPaternotxt = $("#txtApellidoPaterno").val();
        var apellidoMaternotxt = $("#txtApellidoMaterno").val();
        var idEstadotxt = $("#txtIdEstado").val();

        var empleado = {
            "idEmpleado": idEmpleadolbl,
            "nombre": nombretxt,
            "apellidoPaterno": apellidoPaternotxt,
            "apellidoMaterno": apellidoMaternotxt,
            "catEntidadFederativa": {
                "idEstado": idEstadotxt
            }
        }

        $.ajax({
            type: 'PUT',
            url: 'http://localhost:5010/api/Empleado/Update',
            contentType: 'application/json', // Establece el tipo de contenido
            data: JSON.stringify(empleado), // Convierte el objeto a JSON
            success: function (result) {
                alert("Se ha actualizado el empleado");
                LimpiarFormulario();
                $("#exampleModal").modal('hide');
                location.reload();
            },
            error: function (getEmpleado) {
                alert("No se pudo actualizar el empleado")
            }
        });
    });
}

function LimpiarFormulario() {
    $("#txtNombre").val("");
    $("#txtApellidoPaterno").val("");
    $("#txtApellidoMaterno").val("");
    $("#txtIdEstado").val("");
}
function ShowModal() {
    $("#openModalButton").click(function () {
        $("#exampleModal").modal("show");
    });
}