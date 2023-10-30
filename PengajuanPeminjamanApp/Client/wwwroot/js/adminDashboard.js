function GetData() {
$(document).ready(function (e) {

    $.ajax({
        url: "/admin/geAllDataFasikit&Room",
        "dataSrc": "data",
        dataType: "JSON"
    }).done((resilt) => {
        try {
            var morrisData = [];
            $.each(resilt.data, function (key, val) {
                morrisData.push({ label: "" + val.name + "", value: val.stock })
            });

            var arrColors = ['#FF788F', '#FEB019', '#3D449C', '#268FB2', '#74DE00'];
            new Morris.Donut({
                element: 'morrisDonut1',
                data: morrisData,
                colors: arrColors,
                resize: true
            });
        } catch (e) {

        }
    }).fail((err) => { });

    $.ajax({
        url: "/admin/getUserData",
        "dataSrc": "data",
        dataType: "JSON"
    }).done((result) => {
        $.ajax({
            url: "/admin/profileData",
            dataSrc: "data",
            dataType: "JSON"
        }).done((result) => {

            if (result.img == "") {
                $("#navUserImage").attr("src", "/assets/img/profiles/default-profile.jpg");
                $("#navImgageProfile").attr("src", "/assets/img/profiles/default-profile.jpg");
            } else {
                $("#navUserImage").attr("src", `/assets/img/profiles/${result.img}`);
                $("#navImgageProfile").attr("src", `/assets/img/profiles/${result.img}`);
            }
        });
        $("#uUserId").val(result.data.userGuid);
        $("div.profilesets").html(`<h6>${result.data.name}</h6>
                                   <h5>Admin</h5>`)

    }).fail((err) => { });


});
}
GetData();

