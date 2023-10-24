$(document).ready(function () {

    //saat mengeklik button fasilitas
    document.getElementById('btnModalFasilitas').addEventListener('click', () => {
        $.ajax({
            url: 'URL_API_ANDA',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                modalContent.innerHTML = JSON.stringify(data, null, 2);
            },
            error: function (error) {
                console.error('Gagal mengambil data dari API', error);
                modalContent.innerHTML = 'Gagal mengambil data dari API.';
            }
    });
})