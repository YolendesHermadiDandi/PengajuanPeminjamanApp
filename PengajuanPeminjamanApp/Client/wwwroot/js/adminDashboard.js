$(document).ready(function (e) {

    $.ajax({
        url: "/admin/geAllDataFasikit&Room",
        "dataSrc": "data",
        dataType: "JSON"
    }).done((resilt) => {
        var morrisData = [];

        //new Morris.Donut({
        //    element: 'morrisDonut1',
        //    data: [
        //        { label: "Men", value: 12 },
        //        { label: "Women", value: 30 },
        //        { label: "Kids", value: 20 }
        //    ],
        //    colors: ['#3D449C', '#268FB2', '#74DE00'],
        //    resize: true
        //});

        $.each(resilt.data, function (key, val) {
            morrisData.push({ label: "" + val.name + "", value: val.stock })
        });

        var arrColors = ['#FF788F','#FEB019','#3D449C', '#268FB2', '#74DE00'];
        new Morris.Donut({
            element: 'morrisDonut1',
            data: morrisData,
            colors: arrColors, 
            resize: true
        });
    }).fail((err) => { });


});
