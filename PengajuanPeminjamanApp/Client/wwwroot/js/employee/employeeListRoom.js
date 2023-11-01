$(document).ready(function () {
    let dataRoom = $("#tabelRoom").DataTable({
        ajax: {
            url: "/room/get-all",
            "dataSrc": function (data) {
                if (data.data == null) {
                    return [];
                } else {
                    return data.data;
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
                data: "name",
            },
            {
                "defaultContent": "",
                data: "floor",
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
                    columns: [1, 2, 3, 5, 7, 8]
                },
                className: 'btn btn-outline-danger',
                titleAttr: 'pdf',
                text: '<i class="fa-solid fa-file-pdf"></i>',
                //orientation: 'landscape',
                //pageSize: 'LEGAL'
            }
        ],
    });
    $('.dt-buttons').removeClass('dt-buttons');
    $('button#addRoom').on('click', (e) => {
        $('button#submitButton').removeAttr('hidden');
    })
})