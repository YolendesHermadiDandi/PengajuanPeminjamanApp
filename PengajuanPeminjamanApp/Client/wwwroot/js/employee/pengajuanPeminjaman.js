$(document).ready(function () {

    //saat mengeklik button fasilitas
});

document.getElementById('btnModalFasilitas').addEventListener('click', () => {
    $("#tbodyListFasility").html("");
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
                        <td><input type="number" min="1" value="${Elements.stock}"  max="${Elements.stock}" /></td>
                        <td>
                        <div class="page-btn">
                            <button type="button" class="btn btn-primary">Tambah Fasilitas</button>
                        </div>
                        </td>
                    </tr>`);
            })

        },
        error: function (error) {

        }
    });
})

var table = document.getElementsByTagName("table")[2];
var tbody = table.getElementsByTagName("tbody")[2];
tbody.onclick = function (e) {
    e = e || window.event;
    var data = [];
    var target = e.srcElement || e.target;
    while (target && target.nodeName !== "TR") {
        target = target.parentNode;
    }
    if (target) {
        var cells = target.getElementsByTagName("td");
        for (var i = 0; i < cells.length; i++) {
            data.push(cells[i].innerHTML);
        }
    }
    var trnode = document.createElement("tr");

    for (var i = 0; i < data.length; i++) {
        var tdnode = document.createElement("td");
        var textnode = document.createTextNode(data[i]);
        tdnode.appendChild(textnode);
        trnode.appendChild(tdnode);
    }
    alert(trnode)
    document.getElementById("tbodyListFasility").appendChild(trnode);
};