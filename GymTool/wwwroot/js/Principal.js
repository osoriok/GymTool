﻿class Principal {
    userLink(URLactual) {
        let url = "";
        let cadena = URLactual.split("/");
        for (var i = 0; i < cadena.length; i++) {
            if (cadena[i] != "Index") {
                url += cadena[i];
            }
        }

        switch (url) {
            case "ClientesRegistrar":
                document.getElementById('files').addEventListener('change', imageCustomer, false);
                break;

        }
    }

    
}