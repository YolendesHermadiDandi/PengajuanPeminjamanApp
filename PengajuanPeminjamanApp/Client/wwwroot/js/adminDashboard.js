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
                            <a href="activities.html">View all Notifications</a>
                        </div>
                    </div>`
        );
        let index = 0;
        $.each(result, function (key, val) {
            $('ul.notification-list').append(` <li class="notification-message" id=${key} >
                                    <a href="#" onclick="UpdateNotif('${result.notifications[index].guid}')" data-bs-toggle="modal" data-bs-target="#notifModal">
                                        <div class="media d-flex">
                                            <div class="media-body flex-grow-1">
                                                <p class="noti-details">
                                                    <span class="noti-title">${result.employees[index].firstName + " " + result.employees[index].lastName}e</span> Melakukan Request <span class="noti-title"> ${result.notifications[index].message}</span>
                                                </p>
                                            </div>
                                        </div>
                                    </a>
                                </li>`
            );
            index++;
        });
    }).fail((error) => {
    });
});
function UpdateNotif(guid) {
    $.ajax({
        url: "/unreadNotification/b0d1d139-ee52-4ca4-4ee7-08dbd232b40c",
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => { }).fail((error) => { });
}