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
                render: function (data, type, row) {
                    return DateFormat(row.birthDate);
                }
            },
            {
                data: "gender",
                render: function (data, type, row) {
                    return row.gender == "0" ? "Perempuan" : "Laki-laki";
                }
            },
            {
                data: "hiringDate",
                render: function (data, type, row) {
                    return DateFormat(row.hiringDate);
                }
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
    $('div#listUpdatePassword').remove();
    $('div#listUpdateConfirmPassword').remove();
    /*   let guid = */
    $.ajax({
        url: "/admin/employee/edit/" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {

        $('#uEmpId').val(`${result.guid}`);
        $('#uNik').val(`${result.nik}`);
        $("#firstName").val(`${result.firstName}`);
        if (result.lastName == null) {
            $('#lastName').val("");
        } else {
        $("#lastName").val(`${result.lastName}`);        
        }
        $("#birthDate").val(`${DateFormat(result.birthDate)}`);
        $("#genderSelect").val(`${result.gender}`);
        $("#hiringDate").val(`${DateFormat(result.hiringDate)}`);
        $("#email").val(`${result.email}`);
        $("#phoneNumber").val(`${result.phoneNumber}`);
    }).fail((error) => {
    });
}


function Update() {
    let employee = new Object();
    employee.guid = $("#uEmpId").val();
    employee.nik = $("#uNik").val();
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

    let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
    let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY");
    employee.birthDate = new Date(birthDate).toISOString();
    employee.hiringDate = new Date(hiringDate).toISOString();

    $.ajax({
        type: "post",
        url: "/admin/employee/update",
        data: employee,
    }).done((result) => {
        console.log(result);
        if (result.code == 200) {

            Swal.fire({
                icon: 'success',
                title: 'Update Success',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Failed to update data',

            });
        }
        $('#tabelEmployee').DataTable().ajax.reload();
    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to update data',

        })
        $('#tabelEmployee').DataTable().ajax.reload();
    });


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
    employee.password = $("#password").val();
    employee.confirmPassword = $("#passwords").val();

    let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
    let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY");
    employee.birthDate = new Date(birthDate).toISOString();
    employee.hiringDate = new Date(hiringDate).toISOString();
    if (employee.password == employee.confirmPassword) {

        $.ajax({
            type: "post",
            url: "/admin/insertEmployee",
            data: employee,
        }).done((result) => {
            console.log(result);
            if (result.code == 400) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to insert data',

                })
            } else {
                Swal.fire({
                    icon: 'success',
                    title: 'Insert Success',
                    showConfirmButton: false,
                    timer: 1500
                });

            }
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

    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Failed to insert data',

        })
    }


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

