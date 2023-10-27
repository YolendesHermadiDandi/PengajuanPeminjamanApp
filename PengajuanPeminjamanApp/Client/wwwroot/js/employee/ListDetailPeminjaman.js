$(document).ready(function () {
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