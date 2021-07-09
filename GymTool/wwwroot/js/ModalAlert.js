function confirmar(accion,crud) {
    var cancelado = "¡Se cancel&oacute; la solicitud!";
    var modulo="";
    if (accion == "Eliminar") {
        var pregunta = "¿Seguro que deseas eliminar el registro?";
        var confirmado = "¡Se elimin&oacute; el registro correctamente!"
    } else {//o la accion es editar
        var pregunta = "¿Seguro que deseas modificar el registro?";
        var confirmado = "¡Se modific&oacute; el registro correctamente!"
    }

    if (crud == "Cliente") {
        modulo = "accCliente";
    } else if (crud == "Usuario") {
        modulo = "accUsuario";
    } else if (crud == "Membresia") {
        modulo = "accMembresia";
    }

    var bool = confirm(pregunta);
    if (bool) {
        document.getElementById(modulo).value = "true";
        //alert(confirmado);
    } else {
        document.getElementById(modulo).value = "false";
        //alert(cancelado);
    }
}



