$(document).ready(function () {

    let dataFasility = $("#tabelEmployee").DataTable({
        ajax: {
            url: "/admin/getAllEmployee",
            dataSrc: function (data) {
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
                data: "",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            {
                data: "birthDate",
                //render: function (data, type, row) {
                //    return DateFormat(row.birthDate);
                //}
            },
            {
                data: "gender",
                render: function (data, type, row) {
                    return row.gender == "0" ? "Perempuan" : "Laki-laki";
                }
            },
            {
                data: "hiringDate",
                //render: function (data, type, row) {
                //    return DateFormat(row.hiringDate);
                //}
            },
            { data: "email" },
            { data: "phoneNumber" },
            {
                data: "guid",
                //
                render: function (data, type, row) {
                    return ` <input type="hidden" id="empId" name="empId" value="${row.guid}">
                        <a class="me-3" onclick=getUpdateEmployee('${row.guid}') data-bs-target="#adddUpdateEmployee" data-bs-toggle="modal">
                            <img src="/assets/img/icons/edit.svg" alt="img">
                        </a>
                        <a class="me-3 confirm-text"  onclick=Delete('${row.guid}')>
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
    })
    $('.dt-buttons').removeClass('dt-buttons');
    $('button#addEmployee').on('click', (e) => {
        $('button#submitButton').removeAttr('hidden');
    })
})

function getUpdateEmployee(guid) {
    $('div.action-button').html('<button type="submit" id="updateButton" onclick="Update()" class="btn btn-primary" data-bs-dismiss="modal">Update</button>');
    /*   let guid = */
    $.ajax({
        url: "/admin/employee/edit/" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {

        $('#uEmpId').val(`${result.guid}`);
        $("#firstName").val(`${result.firstName}`);
        $("#lastName").val(`${result.lastName}`);
        $("#birthDate").val(`${result.birthDate}`);
        $("#genderSelect").val(`${result.gender}`);
        $("#hiringDate").val(`${result.hiringDate}`);
        $("#email").val(`${result.email}`);
        $("#phoneNumber").val(`${result.phoneNumber}`);
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
        //console.log(result);
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
                console.log(result);
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

$('#addEmployee').on('click', () => {
    $('div.action-button').html('<button type="submit" id="submitButton" onclick="Insert()" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>')
    document.getElementById("emploteeForm").reset();
})

function Insert() {
    let employee = new Object();
    employee.firstName = $("#firstName").val();
    employee.lastName = $("#lastName").val();
    employee.birthDate = $("#birthDate").val();
    employee.gender = parseInt($("#genderSelect").find(':selected').val());
    employee.hiringDate = $("#hiringDate").val();
    employee.email = $("#email").val();
    employee.phoneNumber = $("#phoneNumber").val();
    if (employee.birthDate == '' || employee.hiringDate == '') {
        return alert('birth date or hiring date cant null');
    }
    $.ajax({
        type: "post",
        url: "/admin/insertEmployee",
        data: employee,
    }).done((result) => {
        console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'Insert Success',
            showConfirmButton: false,
            timer: 1500
        });
        $('#tabelEmployee').DataTable().ajax.reload();
    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
        $('#tabelEmployee').DataTable().ajax.reload();
    });



}
