$(document).ready(function () {

    $.ajax({
        url: '/request/GetCountStatusRequestByEmployeeGuid',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            let pengajuan = 0;
            let aktif = 0;
            let selesai = 0;
            let total = 0;

            data.forEach(Elements => {
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


        },
        error: function (error) {

        }
    });

})
