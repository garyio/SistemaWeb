//var url = document.location.host + '/api/Products';
var url = 'http://localhost:31442/api/ArticulosApi'

$(document).ready(function () {
    $.getJSON(url).done(function (data) {
        var trHtml
        $.each(data, function (key, item) {
            trHtml += '<tr ondblclick="find(' + item.ProductId + ');"><td>' + item.ProductId + '</td>'
            + '<td>' + item.Name + '</td>'
            + '<td>' + item.Price + '</td>'
            + '<td>' + item.DueDate + '</td>'
            + '<td><span class="btn btn-flat glyphicon glyphicon-trash" onclick="Delete(' + item.ProductId + ');"></span></td></tr>'
        });
        $('#BodyProducts').append(trHtml);
        console.log(data);
    });
});

function insert(id) {

    if (id == null) {
        var data = {
            Name: $("#txtNombre").val(),
            Price: $('#txtPrecio').val(),
            DueDate: $('#datepicker').val()
        };

        var json = JSON.stringify(data);
        console.log(json);

        $.ajax({
            url: url,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: json,
            success: function (result) {
                $('#myModal').modal('hide')
                InitCtrls();
            }
        })
    } else {
        var data = {
            ProductId: id,
            Name: $("#txtNombre").val(),
            Price: $('#txtPrecio').val(),
            DueDate: $('#datepicker').val()
        };

        var json = JSON.stringify(data);
        console.log(json);

        $.ajax({
            url: url + '/' + id,
            type: 'PUT',
            contentType: "application/json; charset=utf-8",
            data: json,
            success: function (result) {
                $('#myModal').modal('hide')
                InitCtrls();
            }
        })
    }
};

// Este contro lleno de nuevo el Grid para que no haga refresh a la pagina
function InitCtrls() {
    $('#btnGuardar').attr("onclick", "insert(null);");
    $("#txtNombre").val('');
    $('#txtPrecio').val('');
    $('#datepicker').val('');

    $.getJSON(url).done(function (data) {
        $('#BodyProducts').empty();
        var trHtml
        $.each(data, function (key, item) {
            trHtml += '<tr ondblclick="find(' + item.ProductId + ');"><td>' + item.ProductId + '</td>'
            + '<td>' + item.Name + '</td>'
            + '<td>' + item.Price + '</td>'
            + '<td>' + item.DueDate + '</td>'
            + '<td><span class="btn btn-flat glyphicon glyphicon-trash" onclick="Delete(' + item.ProductId + ');"></span></td></tr>'
        });
        $('#BodyProducts').append(trHtml);
        console.log(data);
    });
}

function find(id) {

    $.getJSON(url + '/' + id)
        .done(function (data) {
            $('#myModal').modal('show');
            $('#btnGuardar').attr("onclick", "insert(" + id + ");");
            $("#txtNombre").val(data.Name);
            $('#txtPrecio').val(data.Price);
            $('#datepicker').val(data.DueDate);
            console.log(data);
        })
    .fail(function (error, status, err) {
        alert(error + ' - ' + status + ' - ' + err);
    })
}

function Delete(id) {
    $.ajax({
        url: url + '/' + id,
        type: 'DELETE',
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            alert('Producto Eliminado Exitosamente.!');
            InitCtrls();
        }
    })
}

