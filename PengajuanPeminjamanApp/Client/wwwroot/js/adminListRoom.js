$(document).ready(function () {
    let dataEmployee = $("#tabelRoom").DataTable({
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
        //url: `https://localhost:7100/api/employee/${data}`,
        //url: "employee/edit/" + guid,
        //dataSrc: "data",
        //dataType: "JSON"
    }).done((result) => {

        //$('#uEmpId').val(`${result.guid}`);
        //$("#firstName").val(`${result.firstName}`);
        //$("#lastName").val(`${result.lastName}`);
        //$("#birthDate").val(`${DateFormat(result.birthDate)}`);
        //$("#genderSelect").val(`${result.gender}`);
        //$("#hiringDate").val(`${DateFormat(result.hiringDate)}`);
        //$("#email").val(`${result.email}`);
        //$("#phoneNumber").val(`${result.phoneNumber}`);
    }).fail((error) => {
    });
}


function Update() {
    //let employee = new Object();
    //employee.guid = $("#uEmpId").val();
    //employee.nik = "";
    //employee.firstName = $("#firstName").val();
    //employee.lastName = $("#lastName").val();
    //employee.birthDate = $("#birthDate").val();
    //employee.gender = parseInt($("#genderSelect").find(':selected').val());
    //employee.hiringDate = $("#hiringDate").val();
    //employee.email = $("#email").val();
    //employee.phoneNumber = $("#phoneNumber").val();
    //if (employee.birthDate == '' || employee.hiringDate == '') {
    //    return alert('birth date or hiring date cant null');
    //}
    //let birthDate = moment(employee.birthDate, "DD/MM/YYYY");
    //let hiringDate = moment(employee.hiringDate, "DD/MM/YYYY");
    //employee.birthDate = new Date(birthDate).toISOString();
    //employee.hiringDate = new Date(hiringDate).toISOString();
    //console.log(employee);
    $.ajax({
        //type: "post",
        //url: "Employee/Insert",
        //data: employee,
        //type: "put",
        //headers: {
        //    'Accept': 'application/json;charset=utf-8',
        //    'Content-Type': 'application/json;charset=utf-8'
        //},
        //url: "https://localhost:7100/api/employee/update",
        //dataType: "json",
        ////async: false,
        //data: JSON.stringify(
        //    employee
        //),
    }).done((result) => {
        //console.log(result);
        Swal.fire({
            icon: 'success',
            title: 'Update Success',
            showConfirmButton: false,
            timer: 1500
        })
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
            //$.ajax({
            //    url: "employee/delete/" + guid,
            //    dataSrc: "data",
            //    dataType: "JSON"
            //}).done((result) => {
            //    console.log(result);
            //    if (result.code == 500) {
            //        Swal.fire({
            //            icon: 'error',
            //            title: 'Oops...',
            //            text: 'Failed to delete data',

            //        });
            //    } else {
            //        Swal.fire(
            //            'Deleted!',
            //            'Your file has been deleted.',
            //            'success'
            //        )
            //    }
            //    $('#tabelEmployee').DataTable().ajax.reload();
            //}).fail((error) => {
            //    Swal.fire({
            //        icon: 'error',
            //        title: 'Oops...',
            //        text: 'Failed to delete data',

            //    })
            //});

        }
    })
}

$('#addRoom').on('click', () => {
    $('div.action-button').html('<button type="submit" id="submitButton" onclick="Insert()" class="btn btn-primary" data-bs-dismiss="modal">Submit</button>')
    document.getElementById("fasilityForm").reset();
})