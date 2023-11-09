$(document).ready(function () {

    // Mendapatkan nilai "data" dari atribut
    var statusElements = document.querySelectorAll("#statusPeminjaman");
    var startDatePinjam = document.querySelectorAll("#startDatePinjam");
    var endDatePinjam = document.querySelectorAll("#endDatePinjam");
    console.log(statusElements)

    for (var i = 0; i < statusElements.length; i++) {
        var statusElement = statusElements[i];
        var status = statusElement.getAttribute("data");
        var statusBadge;

        switch (status) {
            case "Requested":
                statusBadge = '<span class="badges bg-lightyellow">Requested</span>';
                break;
            case "OnProssesed":
                statusBadge = '<span class="badges bg-lightgreen">OnProssesed</span>';
                break;
            case "Rejected":
                statusBadge = '<span class="badges bg-lightred">Rejected</span>';
                break;
            case "OnGoing":
                statusBadge = '<span class="badges bg-lightgreen">OnGoing</span>';
                break;
            case "Completed":
                statusBadge = '<span class="badges bg-lightgreen">Completed</span>';
                break;
            default:
                statusBadge = status;
                break;
        }

        statusElement.innerHTML = statusBadge;
    }

    for (var i = 0; i < startDatePinjam.length; i++) {
        var dateElement = startDatePinjam[i];
        var originalDate = dateElement.textContent;
        var formattedDate = formatDateDashboard(originalDate);
        dateElement.textContent = formattedDate;
    }

    for (var i = 0; i < endDatePinjam.length; i++) {
        var dateElement = endDatePinjam[i];
        var originalDate = dateElement.textContent;
        var formattedDate = formatDateDashboard(originalDate);
        dateElement.textContent = formattedDate;
    }

    $(".tabelListPeminjaman").DataTable({
        bFilter: true,
        sDom: "fBtlpi",
        pagingType: "numbers",
        ordering: true,
        language: { search: " ", sLengthMenu: "_MENU_", searchPlaceholder: "Search...", info: "_START_ - _END_ of _TOTAL_ items" },
        initComplete: (settings, json) => {
            $(".dataTables_filter").appendTo("#tableSearch");
            $(".dataTables_filter").appendTo(".search-input");

            // Tombol Export PDF
            $("#export-pdf").on("click", function () {
                $(".tabelListPeminjaman").DataTable().buttons(3).trigger();
            });

            // Tombol Export Excel
            $("#export-excel").on("click", function () {
                $(".tabelListPeminjaman").DataTable().buttons(1).trigger();
            });

            // Tombol Print
            $("#export-print").on("click", function () {
                window.print();
            });
        },
    });

    $('.buttons-excel')[0].style.visibility = 'hidden';
    $('.buttons-excel')[0].style.position = 'absolute';
    $('.buttons-copy')[0].style.visibility = 'hidden';
    $('.buttons-copy')[0].style.position = 'absolute';
    $('.buttons-pdf')[0].style.visibility = 'hidden';
    $('.buttons-pdf')[0].style.position = 'absolute';
    $('.buttons-csv')[0].style.visibility = 'hidden';
    $('.buttons-csv')[0].style.position = 'absolute';

    $('.dt-buttons').removeClass('dt-buttons');
    $('button#addEmployee').on('click', (e) => {
        $('button#submitButton').removeAttr('hidden');
    })
})
