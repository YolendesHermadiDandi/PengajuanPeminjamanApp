$(document).ready(function () {

    let objRoom;

    $.ajax({
        url: '/room/GetRoomDate',
        method: 'GET',
        dataType: 'json',
        async : false,
        success: function (data) {
            objRoom = data

        },
        error: function (error) {

        }
    });
    console.log(objRoom)

    for (var i = 0; i < objRoom.length; i++) {
        var startDate = new Date(objRoom[i].start);
        var endDate = new Date(objRoom[i].end);

        var formattedStartDate = startDate.toISOString().split('T')[0];
        var formattedEndDate = endDate.toISOString().split('T')[0];

        objRoom[i].start = formattedStartDate;
        objRoom[i].end = formattedEndDate;
    }

    var calendarEl = document.getElementById('calendar');
    var today = new Date();
    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'multiMonthYear,dayGridMonth'
        },
        initialView: 'dayGridMonth',
        initialDate: today.getFullYear() + '-' + String(today.getMonth() + 1).padStart(2, '0') + '-01',
        editable: false,
        selectable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        multiMonthMaxColumns: 100, // guarantee single column
        // showNonCurrentDates: true,
        // fixedWeekCount: false,
        // businessHours: true,
        // weekends: false,
        events: objRoom
    });

    calendar.render();

})
