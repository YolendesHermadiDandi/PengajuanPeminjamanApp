
function ProgressBarPeminjaman(status) {
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
        $('div.action-button').html(`<button type="submit" id="completedButton" onclick="Update('OnGoing')" class="btn btn-primary" data-bs-dismiss="modal">Completed</button>`);

    } else if (status == "OnProssesed") {
        $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a').addClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a div i ').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        $('ul.twitter-bs-wizard-nav li #OnProssesed').addClass('badge bg-primary fs-6');

        //add button
        $('div.action-button').html('<button type="submit" id="onGoingButton" onclick="OnGoing()" class="btn btn-primary" data-bs-dismiss="modal">OnGoing</button>');
    } else if (status == "Requested") {
        $('ul.twitter-bs-wizard-nav li:nth-child(1) a').addClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(1) a div i ').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li:nth-child(2) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        $('ul.twitter-bs-wizard-nav li #requested').addClass('badge bg-primary fs-6');

        //add button
        $('div.action-button').html(`<button type="submit" id="approveButton" onclick="Approve()" class="btn btn-primary" data-bs-dismiss="modal">Approved</button>
                                     <button type="submit" id="rejectedButton" onclick="Rejected()" class="btn btn-danger" data-bs-dismiss="modal">Rejected</button>`);

    }


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
        /*if (request.status) {*/
        $.ajax({
            type: "post",
            url: "/request/sendEmail",
            data: sendEmail
        }).done((result) => {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                showConfirmButton: false,
                timer: 1500
            });
            $('#tabelPeminjaman').DataTable().ajax.reload();

        }).fail((error) => {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Failed to update data',

            })
            $('#tabelPeminjaman').DataTable().ajax.reload();
        });
    }).fail((err) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data',

        })
        $('#tabelPeminjaman').DataTable().ajax.reload();
    });
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