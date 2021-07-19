$(document).ready(function () {
    var events = [];
    var clientes = 0;

    $.ajax({
        type: "GET",
        url: "/Principal/GetAsistencias",
        success: function (data) {
            $.each(data, function (i, v) {

                events.push({
                    description: v.horaAsistencia,
                    title: v.cantidadAsistencias,
                    start: moment(v.fechaInicio),
                    end: v.fechaFinal != null ? moment(v.fechaFinal) : null,
                    clientes: v.listaCliente
                });
            })

            GenerateCalender(events);
        },
        error: function (error) {
            alert('failed');
        }
    })


    //var hora = "08:00";
    //var events = [
    //    { title: "Asistencias: ", description: "2", start: new Date("11/07/2021 18:00:00"), hora: hora, end: null, clientes: [{ id: 1, nombre: "Kendall" }, { id: 2, nombre: "Victoria" }]  },
    //    { title: "Asistencias: ", description: "1", start: new Date("2021-07-11 19:00:00"), hora: hora, end: null },
    //    { title: "Asistencias: ", description: "2", start: new Date("2021-07-13 18:00:00"), hora: hora, end: null, clientes: [{ id: 1, nombre: "Gerardo" }, { id: 2, nombre: "Marcela" }] },
    //];
    //    GenerateCalender(events);

    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay',
                close: 'fa-times',
                prev: 'fa-chevron-left',
                next: 'fa-chevron-right'
            },
            buttonText: {
                today: 'hoy',
                month: 'mes',
                week: 'semana',
                day: 'día'
            },
            eventColor: '#DAA520',
            eventLimit: true,
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                $('#myModal #eventTitle').text("Asistencias: " + calEvent.title);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>D&iacute;a de asistencia: </b>' + calEvent.start.format("DD-MM-YYYY") ));
                $description.append($('<p/>').html('<b>Hora de asistencia: </b>' + calEvent.description));
                if (calEvent.clientes != null) {
                    $description.append($('<p/>').html('<b>Clientes confirmados: </b>' + calEvent.title));
                    $.each(calEvent.clientes, function (i, v) {
                        clientes++;
                        $description.append($('<p/>').html('<b>' + clientes + '. </b>' + v.nombre + ' ' + v.apellidos));
                    })
                    clientes = 0;
                }
                $('#myModal #pDetails').empty().html($description);

                $('#myModal').modal();
            }
        })
    }
})