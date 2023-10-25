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
            },
            {
                "defaultContent": "",
                data: "guid",
                render: function (data, type, row) {
                    return ` <input type="hidden" id="roomId" name="roomyId" value="${row.guid}">
                            <a class="me-3" data-bs-toggle="modal" onclick="getUpdateRuangan('${row.guid}')" data-bs-target="#adddUpdateRoom">
                                    <img src="/assets/img/icons/edit.svg" alt="img">
                             </a>
                             <a class="me-3 confirm-text" onclick=Delete('${row.guid}')>
                                    <img src="/assets/img/icons/delete.svg" alt="img">
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
    $('button#addRoom').on('click', (e) => {
        $('button#submitButton').removeAttr('hidden');
    })
})

function getUpdateRuangan(guid) {
    $('div.action-button').html('<button type="submit" id="updateButton" onclick="Update()" class="btn btn-primary" data-bs-dismiss="modal">Update</button>');
    /*   let guid = */
    $.ajax({
        url: "/room/edit/" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        $('#uRoomId').val(`${result.guid}`);
        $("#name").val(`${result.name}`);
        $("#floor").val(`${result.floor}`);
    }).fail((error) => {
    });
}


function Update() {
    let room = new Object();
    room.guid = $('#uRoomId').val();
    room.name = $("#name").val();
    room.floor = $("#floor").val();
    $.ajax({
        type: "post",
        url: "/room/update",
        data: room,
    }).done((result) => {
        //console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'Update Success',
            showConfirmButton: false,
            timer: 1500
        })
        $('#tabelRoom').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data',

        })
        $('#tabelRoom').DataTable().ajax.reload();
    });
}
//Delete
function Delete(guid) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/room/delete/" + guid,
                dataSrc: "data",
                dataType: "JSON"
            }).done((result) => {
                if (result.code == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Failed to delete data',

                    });
                } else {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                }
                $('#tabelRoom').DataTable().ajax.reload();
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to delete data',

                })
            });

        }
    })
}

function Insert() {
    let room = new Object();
    room.name = $("#name").val();
    room.floor = $("#floor").val();;
    $.ajax({
        type: "post",
        url: "/room/insert",
        data: room,
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Insert Success',
            showConfirmButton: false,
            timer: 1500
        });
        $('#tabelRoom').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
        $('#tabelRoom').DataTable().ajax.reload();
    });


}

$('#addRoom').on('click', () => {
    $('div.action-button').html('<button type="submit" id="submitButton" onclick="Insert()" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>')
    document.getElementById("roomForm").reset();
})