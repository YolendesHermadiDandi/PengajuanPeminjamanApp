﻿function getProfile() {
    $(document).ready((e) => {
        $.ajax({
            url: "/admin/profileData",
            dataSrc: "data",
            dataType: "JSON"
        }).done((result) => {
            if (result.img == "") {
                $("#result-img").attr("src", "/assets/img/profiles/default-profile.jpg");
                $("#original-img").attr("src", "/assets/img/profiles/default-profile.jpg");
            } else {
                $("#result-img").attr("src", `/assets/img/profiles/${result.img}`);
                $("#original-img").attr("src", `/assets/img/profiles/${result.img}`);
            }

            $('#nik').val(`${result.employee.nik}`);
            $('#guid').val(`${result.employee.guid}`);
            $('#firstName').val(`${result.employee.firstName}`);
            $('#lastName').val(`${result.employee.lastName}`);
            $('#email').val(`${result.employee.email}`);
            $('#phoneNumber').val(`${result.employee.phoneNumber}`);
            $('#birthDate').val(`${result.employee.birthDate}`);
            $('#gender').val(`${result.employee.gender == 0 ? "Perempuan" : "Laki-laki"}`);
            $('#hiringDate').val(`${result.employee.hiringDate}`);
            $('#Password').val(`${result.employee.password}`);
            $('div.profile-contentname').html(`<h2>${result.employee.firstName + " " + result.employee.lastName}</h2>
                                            <h4>Ini adalah halaman profil utama anda.</h4>`)
        }).fail((err) => { });
    })

}

getProfile();

function showButton() {
    $('#imageSave').removeAttr('hidden');
    $('#imageCancel').removeAttr('hidden');
}

function Cancel() {
    let img = $("#original-img").attr('src')
    $("#result-img").attr("src", img);
    getProfile();
    $("#password").val('');
    $("#passwords").val('');
    //$('#imageSave').attr('hidden', true);
    //$('#imageCancel').attr('hidden', true);
}

function base64ImageToBlob(str) {
    // extract content type and base64 payload from original string
    var pos = str.indexOf(';base64,');
    var type = str.substring(5, pos);
    var b64 = str.substr(pos + 8);

    // decode base64
    var imageContent = atob(b64);

    // create an ArrayBuffer and a view (as unsigned 8-bit)
    var buffer = new ArrayBuffer(imageContent.length);
    var view = new Uint8Array(buffer);

    // fill the view, using the decoded base64
    for (var n = 0; n < imageContent.length; n++) {
        view[n] = imageContent.charCodeAt(n);
    }

    // convert ArrayBuffer to Blob
    var blob = new Blob([buffer], { type: type });

    return blob;
}
function Save() {
    //$('#imageSave').attr('hidden', true);
    //$('#imageCancel').attr('hidden', true);
    try {
        var img = $('#result-img').attr('src')
        var conver = base64ImageToBlob(img);
        var file_data = conver;
        if (file_data != null) {
            var form_data = new FormData();
            form_data.append('file', file_data);
            $.ajax({
                type: "post",
                url: "/admin/imgUpload",
                data: form_data,
                processData: false,
                contentType: false,
            }).done((result) => {
                Swal.fire({
                    icon: 'success',
                    title: 'Success Update Profile',
                    showConfirmButton: false,
                    timer: 1500
                })
                getProfile();
                GetData();
            }).fail((err) => { });
        }
    } catch (e) {

    }

}
function UploadImg() {
    var $uploadCrop;

    function readFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {

                // Bind the image to the croppie container
                $('#image-container').removeAttr('hidden');
                $('#crop-btn').removeAttr('hidden');
                $uploadCrop.croppie('bind', {
                    url: e.target.result
                });
            };

            reader.readAsDataURL(input.files[0]);
        }
    }

    // Initialize Croppie
    $uploadCrop = $('#image-container').croppie({
        enableExif: true,
        viewport: {
            width: 150,
            height: 150,
            type: 'square'
        },
        boundary: {
            width: 200,
            height: 200
        }
    });

    // Handle file input change
    $('#upload').on('change', function () {
        readFile(this);
    });

    // Handle crop button click
    $('#crop-btn').on('click', function () {
        $uploadCrop.croppie('result', {
            type: 'canvas',
            size: 'original'
        }).then(function (resp) {
            // Update the 'src' attribute of the <img> element
            //$('#resultCrop').val('c:\passwords.txt');
            $('#result-img').attr('src', resp);
        });
    });
}
function Update() {
    try {
        let data = new Object();
        data.guid = $('#guid').val();
        data.password = $("#password").val();
        data.confirmPassword = $("#passwords").val();
        $.ajax({
            type: "post",
            url: "/admin/profileUpdate",
            data: data,
        }).done((result) => {
            if (result == -1) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Password tidak sesuai dengan konfirm password',

                })
            } else if (result == -2) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Password/Confirm password tidak boleh kosong',
                })
            }
            else {
                Swal.fire({
                    icon: 'success',
                    title: 'Update Success',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
            $('#tabelFasility').DataTable().ajax.reload();
        }).fail((error) => {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Failed to update data',

            })
            $('#tabelFasility').DataTable().ajax.reload();
        });
    } catch (e) {

    }
}