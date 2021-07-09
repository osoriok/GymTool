using System;
using System.Collections.Generic;
using System.Linq;

namespace GymTool.Library
{
    public class LPaginador<T>
    {

        private int pagi_cuantos = 25;
        private int pagi_nav_num_enlaces = 3;
        private int pagi_actual;
        private String pagi_nav_anterior = "&laquo; Anterior ";
        private String pagi_nav_siguiente = "Siguiente &raquo; ";

        private String pagi_nav_primera = "&laquo; Primero ";
        private String pagi_nav_ultimo = "Último &raquo; ";
        private String pagi_navegacion = null;

        public object[] paginador(List<T> table, int pagina, int registros, String area, String controller,
            String action, String host)
        {
            pagi_actual = pagina == 0 ? 1 : pagina;
            if (registros > 0)
            {
                pagi_cuantos = registros;
            }
            int pagi_totalReg = table.Count;
            double valor1 = Math.Ceiling((double)pagi_totalReg / (double)pagi_cuantos);
            int pagi_totalPags = Convert.ToInt16(Math.Ceiling(valor1));
            if (pagi_actual != 1)
            {
                // Si no estamos en la p&aacute;gina 1. Ponemos el enlace "primera" 
                int pagi_url = 1; //ser&aacute; el n&uacute;mero de p&aacute;gina al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/"
                    + action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>"
                    + pagi_nav_primera + "</a>";

                // Si no estamos en la p&aacute;gina 1. Ponemos el enlace "anterior" 
                pagi_url = pagi_actual - 1; //ser&aacute; el n&uacute;mero de p&aacute;gina al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" + action
                    + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>"
                    + pagi_nav_anterior + " </a>";
            }
            // Si se defini&oacute; la variable pagi_nav_num_enlaces 
            // Calculamos el intervalo para restar y sumar a partir de la p&aacute;gina actual 
            double valor2 = (pagi_nav_num_enlaces / 2);
            int pagi_nav_intervalo = Convert.ToInt16(Math.Round(valor2));
            // Calculamos desde qu&eacute; n&uacute;mero de p&aacute;gina se mostrar&aacute; 
            int pagi_nav_desde = pagi_actual - pagi_nav_intervalo;
            // Calculamos hasta qu&eacute; n&uacute;mero de p&aacute;gina se mostrar&aacute; 
            int pagi_nav_hasta = pagi_actual + pagi_nav_intervalo;
            // Si pagi_nav_desde es un n&uacute;mero negativo
            if (pagi_nav_desde < 1)
            {
                // Le sumamos la cantidad sobrante al final para mantener
                //el n&uacute;mero de enlaces que se quiere mostrar.  
                pagi_nav_hasta -= (pagi_nav_desde - 1);
                // Establecemos pagi_nav_desde como 1. 
                pagi_nav_desde = 1;
            }
            // Si pagi_nav_hasta es un n&uacute;mero mayor que el total de p&aacute;ginas 
            if (pagi_nav_hasta > pagi_totalPags)
            {
                // Le restamos la cantidad excedida al comienzo para mantener 
                //el n&uacute;mero de enlaces que se quiere mostrar. 
                pagi_nav_desde -= (pagi_nav_hasta - pagi_totalPags);
                // Establecemos pagi_nav_hasta como el total de p&aacute;ginas. 
                pagi_nav_hasta = pagi_totalPags;
                // Hacemos el &uacute;ltimo ajuste verificando que al cambiar pagi_nav_desde 
                //no haya quedado con un valor no v&aacute;lido. 
                if (pagi_nav_desde < 1)
                {
                    pagi_nav_desde = 1;
                }
            }
            for (int pagi_i = pagi_nav_desde; pagi_i <= pagi_nav_hasta; pagi_i++)
            {
                //Desde p&aacute;gina 1 hasta &uacute;ltima p&aacute;gina (pagi_totalPags) 
                if (pagi_i == pagi_actual)
                {
                    // Si el n&uacute;mero de p&aacute;gina es la actual (pagi_actual). Se escribe el n&uacute;mero, pero sin enlace y en negrita. 
                    pagi_navegacion += "<span class='btn btn-default' disabled='disabled'>" + pagi_i + "</span>";
                }
                else
                {
                    // Si es cualquier otro. Se escribe el enlace a dicho n&uacute;mero de p&aacute;gina. 
                    pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" +
                        action + "?id=" + pagi_i + "&registros=" + pagi_cuantos + "&area=" + area + "'>" +
                        pagi_i + " </a>";
                }
            }
            if (pagi_actual < pagi_totalPags)
            {
                // Si no estamos en la &uacute;ltima p&aacute;gina. Ponemos el enlace "Siguiente" 
                int pagi_url = pagi_actual + 1; //ser&aacute; el n&uacute;mero de p&aacute;gina al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" +
                    action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" +
                    pagi_nav_siguiente + "</a>";

                // Si no estamos en la &uacute;ltima p&aacute;gina. Ponemos el enlace "Última" 
                pagi_url = pagi_totalPags; //ser&aacute; el n&uacute;mero de p&aacute;gina al que enlazamos 
                pagi_navegacion += "<a class='btn btn-default' href='" + host + "/" + controller + "/" +
                    action + "?id=" + pagi_url + "&registros=" + pagi_cuantos + "&area=" + area + "'>" +
                    pagi_nav_ultimo + "</a>";

            }
            /* 
       * Obtenci&oacute;n de los registros que se mostrar&aacute;n en la p&aacute;gina actual. 
       *------------------------------------------------------------------------ 
       */
            // Calculamos desde qu&eacute; registro se mostrar&aacute; en esta p&aacute;gina 
            // Recordemos que el conteo empieza desde CERO. 
            int pagi_inicial = (pagi_actual - 1) * pagi_cuantos;
            // Consulta SQL. Devuelve cantidad registros empezando desde pagi_inicial

            var query = table.Skip(pagi_inicial).Take(pagi_cuantos).ToList();


            /* 
        * Generaci&oacute;n de la informaci&oacute;n sobre los registros mostrados. 
        *------------------------------------------------------------------------ 
        */

            // N&uacute;mero del primer registro de la p&aacute;gina actual 
            int pagi_desde = pagi_inicial + 1;
            // N&uacute;mero del &uacute;ltimo registro de la p&aacute;gina actual 
            int pagi_hasta = pagi_inicial + pagi_cuantos;
            if (pagi_hasta > pagi_totalReg)
            {
                // Si estamos en la &uacute;ltima p&aacute;gina 
                // El &uacute;ltimo registro de la p&aacute;gina actual ser&aacute; igual al n&uacute;mero de registros. 
                pagi_hasta = pagi_totalReg;
            }

            String pagi_info = " del <b>" + pagi_actual + "</b> al <b>" + pagi_totalPags + "</b> de <b>" +
               pagi_totalReg + "</b> <b>/" + pagi_cuantos + " </b>";
            object[] data = { pagi_info, pagi_navegacion, query };
            return data;
        }
    }
}