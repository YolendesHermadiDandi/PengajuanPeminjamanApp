function GetData() {
    $(document).ready(function (e) {

        $.ajax({
            url: "/admin/geAllDataFasikit&Room",
            "dataSrc": "data",
            dataType: "JSON"
        }).done((result) => {
            let pengajuan = 0;
            let aktif = 0;
            let selesai = 0;
            let total = 0;
            let data = [];
            let category = [];
            console.log(result);

            result.monthReport.forEach(Elements => {
                data.push(Elements.data);
                category.push(Elements.category);
            })


            result.statusCount.forEach(Elements => {
                if (Elements.status == 0 || Elements.status == 1) {
                    pengajuan += Elements.count;
                } else if (Elements.status == 2 || Elements.status == 3 || Elements.status == 6) {
                    aktif += Elements.count;
                } else if (Elements.status == 4 || Elements.status == 5 || Elements.status == 7 || Elements.status == 8) {
                    selesai += Elements.count;
                }
            })
            total = total + pengajuan + aktif + selesai;
            $('#pengajuanCount').html(`<h4>${pengajuan}</h4>`);
            $('#selesaiCount').html(`<h4>${selesai}</h4>`);
            $('#aktifCount').html(`<h4>${aktif}</h4>`);
            $('#totalCount').html(`<h4>${total}</h4>`);
            try {
                var morrisData = [];
                $.each(result.vasility, function (key, val) {
                    morrisData.push({ label: "" + val.name + "", value: val.stock })
                });

                var arrColors = ['#FF788F', '#FEB019', '#3D449C', '#268FB2', '#74DE00'];
                new Morris.Donut({
                    element: 'morrisDonut1',
                    data: morrisData,
                    colors: arrColors,
                    resize: true
                });


                if ($("#s-line").length > 0) {
                    var sline = {
                        chart: {
                            height: 350,
                            type: "line",
                            zoom: { enabled: false },
                            toolbar: { show: false },
                        },
                        dataLabels: { enabled: false },
                        stroke: { curve: "smooth" },
                        series: [
                            { name: "Total Request", data: data },
                        ],
                        title: { text: "Total request perbulan", align: "left" },
                        grid: { row: { colors: ["#f1f2f3", "transparent"], opacity: 0.5 } },
                        xaxis: {
                            categories: category,
                        },
                    };
                    var chart = new ApexCharts(document.querySelector("#s-line"), sline);
                    chart.render();
                }






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

