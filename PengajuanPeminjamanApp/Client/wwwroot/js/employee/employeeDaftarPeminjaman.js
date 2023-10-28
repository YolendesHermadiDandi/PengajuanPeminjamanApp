$(document).ready(function () {

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

