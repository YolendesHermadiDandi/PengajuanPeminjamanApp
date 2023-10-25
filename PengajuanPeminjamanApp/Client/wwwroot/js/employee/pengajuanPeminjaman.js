$(document).ready(function () {

    //saat mengeklik button fasilitas
});

document.getElementById('btnModalFasilitas').addEventListener('click', () => {
    $.ajax({
        url: '/GetFasility',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data)
            data.forEach(Elements => {
                $("#tbodyListFasility").append(
                    `<tr>
                        <td>${Elements.name}</td>
                        <td>${Elements.stock}</td>
                        <td>
                        <div class="page-btn">
                            <a class="badge bg-primary"><img src="/assets/img/icons/plus.svg" alt="img" style="color: azure;" class="me-1"></a>
                        </div>
                        </td>
                    </tr>`);
            })

        },
        error: function (error) {

        }
    });
})