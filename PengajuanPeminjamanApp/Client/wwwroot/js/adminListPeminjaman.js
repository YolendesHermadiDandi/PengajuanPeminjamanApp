$(document).ready(function () {
    let dataEmployee = $("#tabelPeminjaman").DataTable({
        ajax: {
            url: "/request/get-all",
            "dataSrc": function (data) {
                if (data == null) {
                    return [];
                } else {
                    return data;
                };
            },
            dataType: "JSON"
        },
        columns: [
            {
                defaultContent: "",
                data: "",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "defaultContent": "",
                data: "nama",
            },
            {
                "defaultContent": "",
                data: "status",
                render: function (data, type, row) {
                    switch (row.status) {
                        case "Requested":
                            return ` <span class="badges bg-lightyellow">Requested</span>`
                            break;
                        case "OnProssesed":
                            return ` <span class="badges bg-lightgreen">OnProssesed</span>`
                            break;
                        case "Rejected":
                            return ` <span class="badges bg-lightred">Rejected</span>`
                            break;
                        case "OnGoing":
                            return ` <span class="badges bg-lightgreen">OnGoing</span>`
                            break;
                        case "Completed":
                            return ` <span class="badges bg-lightgreen">Completed</span>`
                            break;
                        default:
                            return row.status;
                            break;
                    }
                }

            },
            {
                "defaultContent": "",
                data: "requestGuid",
                render: function (data, type, row) {
                    return `<a class="me-3" data-bs-toggle="modal" onclick="ProgressBarPeminjaman('${row.requestGuid}')" data-bs-target="#detailPeminjamanModal">
                                    <img src="/assets/img/icons/eye.svg" alt="img">
                                </a>`;
                }
            }
        ],
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
                    //columns: [1, 2, 3, 5, 7, 8]
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
function ProgressBarPeminjaman(guid) {
    $.ajax({
        url: "/request/get-details/" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        console.log(result);
        $('#uRequestId').val(`${result.request.data[0].guid}`);
        $('#uEmpEmail').val(`${result.email}`);
        $("ul#requestDetail li#namaEmployee").html(`<h4>Nama employee</h4> <h6>${result.nama}</h6>`)
        $("ul#requestDetail li#startDate").html(`<h4>Tanggal peminjaman</h4> <h6>${DateFormat(result.request.data[0].startDate)}</h6 >`)
        $("ul#requestDetail li#endDate").html(`<h4>Tanggal berakhir peminjaman</h4> <h6>${DateFormat(result.request.data[0].endDate)}</h6>`)
        $("ul#requestDetail li#statusPeminjaman").html(`<h4>Status Peminjaman</h4> <h6>${result.requestStatus}</h6>`)
        $("ul#requestDetail li#namaRuangan").html(`<h4>Nama Ruangan</h4>`)
        if (result.request.data[0].rooms != null) {
            $("ul#requestDetail li#namaRuangan").html(`<h4>Nama Ruangan</h4> <h6>${result.request.data[0].rooms.name}</h6>`)
        } else {
            $("ul#requestDetail li#namaRuangan").html(`<h4>Nama Ruangan</h4> <h6>Data Kosong</h6>`)
        };
        $("ul#requestDetail li#listFasilitas").html('<h4>Nama Fasilitas</h4>');

        if (result.request.data[0].fasilities != null && result.request.data[0].fasilities.length > 0) {
            result.request.data[0].fasilities.forEach(elemets => {
                $("ul#requestDetail li#listFasilitas").append(` <p class="list-group-item list-group-item-info m-1 text-center">
                                                            ${elemets.name}
                                                            <span class="badge d-block bg-primary">Qty : ${elemets.totalFasility}</span>
                                                            </p>`);
            });
        } else {
            $("ul#requestDetail li#listFasilitas").append(` <p class="list-group-item list-group-item-info m-1 text-center">
                                        Kosong                                                           
                                        </p>`);
        }

        status = result.requestStatus
        //Status bar update
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
            $('div.action-button').html(`<button type="submit" id="completedButton" onclick="Update('${status}')" class="btn btn-primary" data-bs-dismiss="modal">Completed</button>`);

        } else if (status == "OnProssesed") {
            $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a').addClass('active');
            $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a div i ').attr('class', 'fa fa-check');

            $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
            $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

            $('ul.twitter-bs-wizard-nav li #OnProssesed').addClass('badge bg-primary fs-6');

            //add button
            $('div.action-button').html(`<button type="submit" id="onGoingButton" onclick="Update('${status}')" class="btn btn-primary" data-bs-dismiss="modal">OnGoing</button>`);
        } else if (status == "Requested") {
            $('ul.twitter-bs-wizard-nav li:nth-child(1) a').addClass('active');
            $('ul.twitter-bs-wizard-nav li:nth-child(1) a div i ').attr('class', 'fa fa-check');

            $('ul.twitter-bs-wizard-nav li:nth-child(2) a div i ').attr('class', 'fe fe-clock');
            $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
            $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

            $('ul.twitter-bs-wizard-nav li #requested').addClass('badge bg-primary fs-6');

            //add button
            $('div.action-button').html(`<button type="submit" id="approveButton" onclick="Update('${status}')" class="btn btn-primary" data-bs-dismiss="modal">Approved</button>
                                     <button type="submit" id="rejectedButton" onclick="Rejected()" class="btn btn-danger" data-bs-dismiss="modal">Rejected</button>`);

        }
    }).fail((error) => {
    });
}

function Rejected() {
    let request = new Object();
    request.guid = $('#uRequestId').val();
    request.status = "Rejected";

    let sendEmail = new Object();
    sendEmail.fromEmail = 'Admin@no-replay.com';
    sendEmail.recipientEmail = $('#uEmpEmail').val();
    sendEmail.message = "Pengajuan Peminjaman Anda " + request.status;
    sendEmail.requestGuid = request.guid;

    let statusUpdate = new Object();
    statusUpdate.Guid = request.guid;
    statusUpdate.Status = request.status;
    //sendEmail.message = htmlcode;
    $.ajax({
        type: "post",
        url: "/request/statusUpdate",
        data: statusUpdate
    }).done((result) => {
        $.ajax({
            type: "post",
            url: "/request/sendEmail",
            data: sendEmail
        }).done((result) => { }).fail((error) => { });
        Swal.fire({
            icon: 'success',
            title: 'Success',
            showConfirmButton: false,
            timer: 1500
        });
        $('#tabelPeminjaman').DataTable().ajax.reload();
    }).fail((err) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
        $('#tabelPeminjaman').DataTable().ajax.reload();
    })

}

function Update(status) {
    let request = new Object();
    request.guid = $('#uRequestId').val();
    switch (status) {
        case "Requested":
            request.status = "OnProssesed";
            break;
        case "OnProssesed":
            request.status = "OnGoing";
            break;
        case "OnGoing":
            request.status = "Completed";
            break;
    }

    let sendEmail = new Object();
    sendEmail.fromEmail = 'Admin@no-replay.com';
    sendEmail.recipientEmail = $('#uEmpEmail').val();
    sendEmail.message = "Pengajuan Peminjaman Anda " + request.status;
    sendEmail.requestGuid = request.guid;

    let statusUpdate = new Object();
    statusUpdate.Guid = request.guid;
    statusUpdate.Status = request.status;
    //sendEmail.message = htmlcode;
    $.ajax({
        type: "post",
        url: "/request/statusUpdate",
        data: statusUpdate
    }).done((result) => {
        /*if (request.status == "OnGoing") {*/
        $.ajax({
            type: "post",
            url: "/request/sendEmail",
            data: sendEmail
        }).done((result) => {

        }).fail((error) => {

        });
        //}
        Swal.fire({
            icon: 'success',
            title: 'Success',
            showConfirmButton: false,
            timer: 1500
        });
        $('#tabelPeminjaman').DataTable().ajax.reload();
    }).fail((err) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
        $('#tabelPeminjaman').DataTable().ajax.reload();
    })

}
function DateFormat(date) {
    const today = new Date(date);
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    return formattedToday = dd + '-' + mm + '-' + yyyy
}