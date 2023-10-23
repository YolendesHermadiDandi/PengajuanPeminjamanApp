$(document).ready(function () {
    $.ajax({
        url: "/unreadNotification/b0d1d139-ee52-4ca4-4ee7-08dbd232b40c",
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        console.log(result);
        $('li.notification').html(
            `<a href="javascript:void(0);" class="dropdown-toggle nav-link" data-bs-toggle="dropdown">
                        <img src="/assets/img/icons/notification-bing.svg" alt="img"> <span class="badge rounded-pill">${result.notifications.length}</span>
                    </a>
                    <div class="dropdown-menu notifications">
                        <div class="topnav-dropdown-header">
                            <span class="notification-title">Notifications</span>
                        </div>
                        <div class="noti-content">
                            <ul class="notification-list">
                            </ul>
                        </div>
                        <div class="topnav-dropdown-footer">
                            <a href="/admin/notifications">View all Notifications</a>
                        </div>
                    </div>`
        );
        let index = 0;
        for (let i = 0; i < result.notifications.length; i++) {
            $('ul.notification-list').append(` <li class="notification-message" id=${i} >
                                    <a onclick="UpdateNotif('${result.notifications[i].guid}')" data-bs-toggle="modal" data-bs-target="#notifModal">
                                        <div class="media d-flex">
                                            <div class="media-body flex-grow-1">
                                                <p class="noti-details">
                                                    <span class="noti-title">${result.employees[i].firstName + " " + result.employees[i].lastName}e</span> Melakukan Request <span class="noti-title"> ${result.notifications[i].message}</span>
                                                </p>
                                            </div>
                                        </div>
                                    </a>
                                </li>`
            );
        }
      
    }).fail((error) => {
    });
});
function UpdateNotif(guid) {
    $.ajax({
        url: "/updateNotification/"+guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        $.ajax({
            url: "/getNotification/" + guid,
            dataSrc: "data",
            dataType: "JSON"
        }).done((result) => {
            $('.modal-header > .page-title').html(`<h4>${result.employees.firstName + " " + result.employees.lastName}<h4>
                                               <h6>${result.employees.email}<h6>
                                               `);
            $('#modal-message').html(`<p>Melakukan Request Terhadap ${result.notifications.message}</p>`);
            console.log(result);
        }).fail((error) => { });
    }).fail((error) => { });
}

$('#updateNotif').on('click', (e) => {
    location.reload();
})

$('button#close-modal').on('click', (e) => {
    location.reload();
});
$('button#rejected-modal').on('click', (e) => {
    location.reload();
});
$('button#approved-modal').on('click', (e) => {
    location.reload();
})
$('div#notifModal').on('click', (e) => {
    location.reload();
})