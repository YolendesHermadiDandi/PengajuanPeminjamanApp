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

    const formattedData = objRoom.map(item => ({
        ...item,
        start: formatDate(item.start),
        end: formatDate(item.end)
    }));

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
        events: formattedData
    });

    calendar.render();

})


function formatDate(dateString) {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
}
