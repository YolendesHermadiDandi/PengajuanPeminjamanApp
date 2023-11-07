$(document).ready(function () {

    $.ajax({
        url: '/Employee/GetProfileImage',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data == "" || data == null) {
                $("#navImgEmployee").attr("src", "/assets/img/profiles/default-profile.jpg");
                $("#dropDownImgEmployee").attr("src", "/assets/img/profiles/default-profile.jpg");
            } else {
                $("#navImgEmployee").attr("src", `/assets/img/profiles/${data}`);
                $("#dropDownImgEmployee").attr("src", `/assets/img/profiles/${data}`);
            }
        },
        error: function (error) {

        }
    });

    $.ajax({
        url: '/Employee/GetProfileName',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            var h6Element = document.getElementById("NameEmployee");

            // Ubah isi dari elemen h6
            h6Element.textContent = data;
        },
        error: function (error) {

        }
    });

})
