$(document).ready(function () {
    GetAll();

})

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5010/api/Empleado/GetAll',
        dataType: 'JSON',
        success: function (result) {
            $.each(result.Empleados, function (i, empleado) {
                var fila = '<tr>' +
                    '<td class = "text-center" scope = "col" >' +
                    '<a class = "btn btn-warning glyphicon glyphicon-pencil" href = "#"></a>' +
                    '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.IdEmpleado + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.NumeroNomina + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.Nombre + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.ApellidoPaterno + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.ApellidoMaterno + '</td>' +
                    '<td class = "text-center" scope = "col">' + empleado.Estado + '</td>' +
                    '<tr>';

                $('#tblEmpleado tbody').append(fila);
            })
        }
    })
}