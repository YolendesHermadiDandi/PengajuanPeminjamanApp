$(document).ready(function () {
    $.ajax({
        url: "/allNotification/b0d1d139-ee52-4ca4-4ee7-08dbd232b40c",
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        console.log(result);
        for (let i = 0; i < result.notifications.length; i++) {
            $("ul#list-notification").append(`
                            <li>
                                <div class="activity-user">
                                    <a  title="" data-toggle="tooltip">
                                    <img src="https://source.unsplash.com/random/user"
                                             class="img-fluid">
                                    </a>
                                </div>
                                <div class="activity-content">
                                    <div class="timeline-content">
                                        <a href="#" class="name">${result.employees[i].firstName + " " + result.employees[i].lastName}</a> Melakukan Request ${result.notifications[i].message} </a>
                                        <span class="time">4 mins ago</span>
                                    </div>
                                </div>
                            </li>`

            );
        }
      

    }).fail((error) => {
    });
});