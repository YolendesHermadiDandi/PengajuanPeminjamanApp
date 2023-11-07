$(document).ready(function () {

    let dataFasility = $("#tabelFasility").DataTable({
        ajax: {
            //url: "https://localhost:7100/api/employee",
            url: "/fasility/get-all",
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
                data: "stock",
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
    $('button#addEmployee').on('click', (e) => {
        $('button#submitButton').removeAttr('hidden');
    })
})

function getUpdateFasility(guid) {
    $('div.action-button').html('<button type="submit" id="updateButton" onclick="Update()" class="btn btn-primary" data-bs-dismiss="modal">Update</button>');
    /*   let guid = */
    $.ajax({
        url: "/fasility/edit/" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        $('#uFasilityId').val(`${result.guid}`);
        $("#name").val(`${result.name}`);
        $("#stock").val(`${result.stock}`);
    }).fail((error) => {
    });
}


function Update() {
    let fasility = new Object();
    fasility.guid = $('#uFasilityId').val();
    fasility.name = $("#name").val();
    fasility.stock = $("#stock").val();
    $.ajax({
        type: "post",
        url: "/fasility/update",
        data: fasility,
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Update Success',
            showConfirmButton: false,
            timer: 1500
        })
        $('#tabelFasility').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data',

        })
        $('#tabelFasility').DataTable().ajax.reload();
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
                url: "/fasility/delete/" + guid,
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
                $('#tabelFasility').DataTable().ajax.reload();
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

$('#addFasility').on('click', () => {
    $('div.action-button').html('<button type="submit" id="submitButton" onclick="Insert()" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>')
    document.getElementById("fasilityForm").reset();
});

function Insert() {
    let fasility = new Object();
    fasility.name = $("#name").val();
    fasility.stock = $("#stock").val();
    $.ajax({
        type: "post",
        url: "/fasility/insert",
        data: fasility,
    }).done((result) => {
        Swal.fire({
            icon: 'success',
            title: 'Insert Success',
            showConfirmButton: false,
            timer: 1500
        });
        $('#tabelFasility').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
        $('#tabelFasility').DataTable().ajax.reload();
    });


}
