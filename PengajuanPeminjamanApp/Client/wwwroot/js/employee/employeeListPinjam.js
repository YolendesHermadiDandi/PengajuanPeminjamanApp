$(document).ready(function () {
    let dataEmployee = $("#tabelEmployee").DataTable({
        dom: 'Bftp',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible'
                },
                className: 'btn btn-outline-success',
                titleAttr: 'excel',
                text: '<i class="fa-solid fa-file-excel"></i>',
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: ':visible',
                    columns: [1, 2, 3, 5, 7, 8]
                },
                className: 'btn btn-outline-danger',
                titleAttr: 'pdf',
                text: '<i class="fa-solid fa-file-pdf"></i>',
                //orientation: 'landscape',
                //pageSize: 'LEGAL'
            },
            {
                extend: 'colvis',
                className: 'btn btn-outline-info text-black',
            }
        ],
    });
    $('.dt-buttons').removeClass('dt-buttons');
    $('button#addEmployee').on('click', (e) => {
        $('button#submitButton').removeAttr('hidden');
    })
})
function ProgressBarPeminjaman(status) {
    console.log(status);
    $('ul.twitter-bs-wizard-nav li a.active').removeClass('active');
    $('ul.twitter-bs-wizard-nav li #completed').html('<span id="completed">Completed</span>');
    $('ul.twitter-bs-wizard-nav li span').removeClass('badge bg-primary fs-6');
    if (status == "Completed" || status == "Rejected") {
        $('ul.twitter-bs-wizard-nav li a').addClass('active');
        $('ul.twitter-bs-wizard-nav li a div i').attr('class', 'fa fa-check');

        if (status == "Completed") {
            $('ul.twitter-bs-wizard-nav li #completed').html('<span id="completed" class="badge bg-primary fs-6">Completed</span>');
        } else {
            $('ul.twitter-bs-wizard-nav li #completed').html('<span id="completed" class="badge bg-danger fs-6">Rejected</span>');
        }

        //remove button
        $('div.action-button button').remove();

    } else if (status == "OnGoing") {
        $('ul.twitter-bs-wizard-nav li a').addClass('active');
        $('ul.twitter-bs-wizard-nav li a div i').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li #onGoing').addClass('badge bg-primary fs-6');

        $('ul.twitter-bs-wizard-nav li:nth-child(4) a.active').removeClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        //add buttons
        $('div.action-button').html('<button type="submit" id="completedButton" onclick="Completed()" class="btn btn-primary" data-bs-dismiss="modal">Completed</button>');

    } else if (status == "OnProssesed") {
        $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a').addClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a div i ').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        $('ul.twitter-bs-wizard-nav li #OnProssesed').addClass('badge bg-primary fs-6');

        //add button
        
    } else if (status == "Requested") {
        $('ul.twitter-bs-wizard-nav li:nth-child(1) a').addClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(1) a div i ').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li:nth-child(2) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        $('ul.twitter-bs-wizard-nav li #requested').addClass('badge bg-primary fs-6');

        //add button
        $('div.action-button').html(`<button type="submit" id="approveButton" onclick="Approve()" class="btn btn-primary" data-bs-dismiss="modal">Edit Request</button>`);

    }

}

function DetailPeminjaman(guid) {
    $("#listFasilityDetail").html("");
    $("#nameRoomRequest").html("");
    $("#listFasilityDetail").append("<h4>Nama Fasilitas</h4>");
    $.ajax({
        url: "/request/getRequestbyRequestGuid?guid=" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        console.log(result)
        result.forEach(Elements => {
            let status = Elements.status;
            let stringStatus;
            switch (status) {
                case 0:
                    ProgressBarPeminjaman("Requested");
                    stringStatus = "Requested";
                    break;
                case 2:
                    ProgressBarPeminjaman("OnProssesed");
                    stringStatus = "OnProssesed";
                    break;
                case 6:
                    ProgressBarPeminjaman("OnGoing");
                    stringStatus = "OnGoing";
                    break;
                case 7:
                    ProgressBarPeminjaman("Completed");
                    stringStatus = "Completed";
                    break;
                default:
                    ProgressBarPeminjaman("Completed");
                    stringStatus = "Completed";
                    break;
            }

            console.log(Elements.employeeGuid)
            $.ajax({
                url: "employee/getEmployee/" + Elements.employeeGuid,
                dataSrc: "data",
                dataType: "JSON"
            }).done((resultEmployee) => {
                $("#nameEmployee").html(resultEmployee.firstName + " " + resultEmployee.lastName);
            }).fail((error) => {
            });
            $("#startDateRequest").html(Elements.startDate);
            $("#endDateRequest").html(Elements.endDate);
            $("#statusRequest").html(stringStatus);
            $("#nameRoomRequest").html(Elements.rooms.name);
            $("#listFasilityDetail").html("");
            $("#listFasilityDetail").append("<h4>Nama Fasilitas</h4>");
            Elements.fasilities.forEach(fasility => {
                $("#listFasilityDetail").append(` <p class="list-group-item list-group-item-info m-1 text-center">
                                                ${fasility.name}
                                                <span class="badge d-block bg-primary">Qty : ${fasility.totalFasility}</span>
                                                </p>`);
            })
        })
        //Setelah get employee
    }).fail((error) => {
    });
}