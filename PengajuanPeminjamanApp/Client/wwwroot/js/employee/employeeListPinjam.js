$(document).ready(function () {

    $(".tabelListPeminjaman").DataTable({
        bFilter: true,
        sDom: "fBtlpi",
        pagingType: "numbers",
        ordering: true,
        language: { search: " ", sLengthMenu: "_MENU_", searchPlaceholder: "Search...", info: "_START_ - _END_ of _TOTAL_ items" },
        initComplete: (settings, json) => {
            $(".dataTables_filter").appendTo("#tableSearch");
            $(".dataTables_filter").appendTo(".search-input");

            // Tombol Export PDF
            $("#export-pdf").on("click", function () {
                $(".tabelListPeminjaman").DataTable().buttons(3).trigger();
            });

            // Tombol Export Excel
            $("#export-excel").on("click", function () {
                $(".tabelListPeminjaman").DataTable().buttons(1).trigger();
            });

            // Tombol Print
            $("#export-print").on("click", function () {
                window.print();
            });
        },
    });

    $('.buttons-excel')[0].style.visibility = 'hidden';
    $('.buttons-excel')[0].style.position = 'absolute';
    $('.buttons-copy')[0].style.visibility = 'hidden';
    $('.buttons-copy')[0].style.position = 'absolute';
    $('.buttons-pdf')[0].style.visibility = 'hidden';
    $('.buttons-pdf')[0].style.position = 'absolute';
    $('.buttons-csv')[0].style.visibility = 'hidden';
    $('.buttons-csv')[0].style.position = 'absolute';

    $('.dt-buttons').removeClass('dt-buttons');
    $('button#addEmployee').on('click', (e) => {
    $('button#submitButton').removeAttr('hidden');
    })

    //list Fasility modal
    $.ajax({
        url: '/GetFasility',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            data.forEach(Elements => {
                valueDefaultFasiity = Elements.stock;
                $("#tbodyListFasility").append(
                    `<tr>
                        <td>${Elements.name}</td>
                        <td><input type="number" min="1" value="${Elements.stock}" id="value${Elements.name}" max="${Elements.stock}" /></td>
                        <td>
                        <div class="page-btn">
                            <button type="button" class="btn btn-primary" id="btnTambahFasility${Elements.name}" onclick="tambahFasilityTabel('${Elements.guid}')">Tambah Fasilitas</button> 
                        </div>
                        </td>
                    </tr>`);

                isAvalibe = true;
            })

        },
        error: function (error) {

        }
    });

    //list Ruangan modal
    $.ajax({
        url: '/room/get-all',
        method: 'GET',
        dataType: 'json',
        dataSrc: 'data',
        success: function (data) {

            data.data.forEach(Elements => {
                $("#tbodyListRuangan").append(
                    `<tr>
                        <td>${Elements.name}</td>
                        <td>Lantai ${Elements.floor}</td>
                        <td>
                        <div class="page-btn">
                            <button type="button" class="btn btn-primary" id="btnTambahRuangan${Elements.name}" data-bs-toggle="modal" data-bs-target="#modalUpdateRequest" onclick="tambahRuangan('${Elements.guid}')">Pilih Ruangan</button>
                        </div>
                        </td>
                    </tr>`);

            })

        },
        error: function (error) {

        }
    });

    
    // Mendapatkan nilai "data" dari atribut
    var statusElements = document.querySelectorAll("#statusPeminjaman");
    var startDatePinjam = document.querySelectorAll("#startDatePinjam");
    var endDatePinjam = document.querySelectorAll("#endDatePinjam");

    for (var i = 0; i < statusElements.length; i++) {
        var statusElement = statusElements[i];
        var status = statusElement.getAttribute("data");
        var statusBadge;

        switch (status) {
            case "Requested":
                statusBadge = '<span class="badges bg-lightyellow">Requested</span>';
                break;
            case "OnProssesed":
                statusBadge = '<span class="badges bg-lightgreen">OnProssesed</span>';
                break;
            case "Rejected":
                statusBadge = '<span class="badges bg-lightred">Rejected</span>';
                break;
            case "OnGoing":
                statusBadge = '<span class="badges bg-lightgreen">OnGoing</span>';
                break;
            case "Completed":
                statusBadge = '<span class="badges bg-lightgreen">Completed</span>';
                break;
            default:
                statusBadge = status;
                break;
        }

        statusElement.innerHTML = statusBadge;
    }

    for (var i = 0; i < startDatePinjam.length; i++) {
        var dateElement = startDatePinjam[i];
        var originalDate = dateElement.textContent;
        var formattedDate = formatDateDashboard(originalDate);
        dateElement.textContent = formattedDate;
    }

    for (var i = 0; i < endDatePinjam.length; i++) {
        var dateElement = endDatePinjam[i];
        var originalDate = dateElement.textContent;
        var formattedDate = formatDateDashboard(originalDate);
        dateElement.textContent = formattedDate;
    }

})
function ProgressBarPeminjaman(status, guid) {
    $('ul.twitter-bs-wizard-nav li a.active').removeClass('active');
    $('ul.twitter-bs-wizard-nav li #completed').html('<span id="completed">Completed</span>');
    $('ul.twitter-bs-wizard-nav li span').removeClass('badge bg-primary fs-6');
    if (status == "Completed" || status == "Rejected") {
        $('ul.twitter-bs-wizard-nav li a').addClass('active');
        $('ul.twitter-bs-wizard-nav li a div i').attr('class', 'fa fa-check');

        if (status == "Completed") {
            $('ul.twitter-bs-wizard-nav li #completed').html('<span id="completed" class="badge bg-primary fs-6">Completed</span>');
        } else {
            $('ul.twitter-bs-wizard-nav li #completed').html('<span id="completed" class="badge bg-danger fs-6">Rejected</span>');
        }

        //remove button
        $('div.action-button button').remove();

    } else if (status == "OnGoing") {
        $('ul.twitter-bs-wizard-nav li a').addClass('active');
        $('ul.twitter-bs-wizard-nav li a div i').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li #onGoing').addClass('badge bg-primary fs-6');

        $('ul.twitter-bs-wizard-nav li:nth-child(4) a.active').removeClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        //add buttons
        $('div.action-button').html('<button type="submit" id="completedButton" onclick="Completed()" class="btn btn-primary" data-bs-dismiss="modal">Completed</button>');

    } else if (status == "OnProssesed") {
        $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a').addClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(-n+2) a div i ').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        $('ul.twitter-bs-wizard-nav li #OnProssesed').addClass('badge bg-primary fs-6');

        //add button
        
    } else if (status == "Requested") {
        $('ul.twitter-bs-wizard-nav li:nth-child(1) a').addClass('active');
        $('ul.twitter-bs-wizard-nav li:nth-child(1) a div i ').attr('class', 'fa fa-check');

        $('ul.twitter-bs-wizard-nav li:nth-child(2) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(3) a div i ').attr('class', 'fe fe-clock');
        $('ul.twitter-bs-wizard-nav li:nth-child(4) a div i ').attr('class', 'fe fe-check-circle');

        $('ul.twitter-bs-wizard-nav li #requested').addClass('badge bg-primary fs-6');

        //add button
        $('div.action-button').html(`<button type="submit" id="approveButton" onclick="btnFirstupdateRequest('${guid}')" class="btn btn-primary" data-bs-dismiss="modal" data-bs-toggle="modal" data-bs-target="#modalUpdateRequest">Edit Request</button>`);

    }

}

let req;
function btnFirstupdateRequest(guid) {
    $('#tableDaftarPeminjaman').html("");
    $("#endDateTime").attr("disabled", "");
    $("#startDateTime").attr("disabled", "");
    req = guid;
    $.ajax({
        url: "/request/getRequestbyRequestGuid?guid=" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        result.forEach(Elements => {
            $("#endDateTime").val(formatDate(Elements.endDate))
            $("#startDateTime").val(formatDate(Elements.startDate))

            if (Elements.rooms != null) {
                const button = document.getElementById(`btnModalRuangan`);
                button.setAttribute("disabled", "");

                $('#tableDaftarPeminjaman').append(`<tr id="btnHapusRuangan${Elements.rooms.name}Ruangan">
                        <td>${Elements.rooms.name}</td>
                        <td>Lantai ${Elements.rooms.floor} </td>
                        <td>Ruangan</td>
                        <td id="${Elements.rooms.guid}"><a class="delete-set"><img src="/assets/img/icons/delete.svg" onclick="hapusRuangan('btnHapusRuangan${Elements.rooms.name}')" alt="svg"></a></td>
                        </tr>`)
            }

            Elements.fasilities.forEach(fasilities => {
                const button = document.getElementById(`btnTambahFasility${fasilities.name}`);
                button.setAttribute("disabled", "");
                $('#tableDaftarPeminjaman').append(`<tr id="btnTambahFasility${fasilities.name}Fasility">
                        <td>${fasilities.name}</td>
                        <td>${fasilities.totalFasility} Unit</td>
                        <td>Fasilitas</td>
                        <td id="${fasilities.fasilityGuid}" data="${fasilities.guid}"><a class="delete-set"><img src="/assets/img/icons/delete.svg" onclick="hapusFaslity('btnTambahFasility${fasilities.name}')" alt="svg"></a></td>
                        </tr>`)
            })

            
        })
    })
            
}


function hapusRuangan(idBtn) {
    document.getElementById(`${idBtn}Ruangan`).outerHTML = "";
    button = document.getElementById(`btnModalRuangan`);
    button.removeAttribute("disabled");
}

function hapusFaslity(idBtn) {
    document.getElementById(`${idBtn}Fasility`).outerHTML = "";
    button = document.getElementById(`${idBtn}`);
    button.removeAttribute("disabled");

}

function formatDate(inputDate) {
    const date = new Date(inputDate);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();

    return `${day}-${month}-${year}`;
}

function tambahFasilityTabel(guid) {
    $.ajax({
        url: '/GetFasility/' + guid,
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            let valuePinjam = document.getElementById(`value${data.name}`).value
            if ((data.totalFasility - valuePinjam) < 1) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Maaf Jumlah yang Anda Pinjam Melebihi Batas Saat Ini',

                })
            } else {
                $('#tableDaftarPeminjaman').append(`<tr>
            <td>${data.name}</td>
            <td>${valuePinjam} Unit</td>
            <td>Fasilitas</td>
            <td id="${data.guid}"><a class="delete-set"><img src="/assets/img/icons/delete.svg" onclick="hapusFaslity('btnTambahFasility${data.name}')" alt="svg"></a></td>
            </tr>`)

                const button = document.getElementById(`btnTambahFasility${data.name}`);
                button.setAttribute("disabled", "");

            }
        },
        error: function (error) {

        }
    });
}

function tambahRuangan(guid) {
    $.ajax({
        url: '/room/get/' + guid,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#tableDaftarPeminjaman').append(`<tr>
            <td>${data.data.name}</td>
            <td>Lantai ${data.data.floor}</td>
            <td>Ruangan</td>
            <td id="${data.data.guid}"><a class="delete-set"><img src="/assets/img/icons/delete.svg" onclick="hapusRuangan('btnTambahRuangan${data.data.name}')" alt="svg"></a></td>
            </tr>`)

            const button = document.getElementById(`btnModalRuangan`);
            button.setAttribute("disabled", "");
        },
        error: function (error) {

        }
    });
}

function DetailPeminjaman(guid) {
    $("#listFasilityDetail").html("");
    $("#nameRoomRequest").html("");
    $("#listFasilityDetail").append("<h4>Nama Fasilitas</h4>");
    $.ajax({
        url: "/request/getRequestbyRequestGuid?guid=" + guid,
        dataSrc: "data",
        dataType: "JSON"
    }).done((result) => {
        result.forEach(Elements => {
            let status = Elements.status;
            let stringStatus;
            switch (status) {
                case 0:
                    ProgressBarPeminjaman("Requested",guid);
                    stringStatus = "Requested";
                    break;
                case 2:
                    ProgressBarPeminjaman("OnProssesed", null);
                    stringStatus = "OnProssesed";
                    break;
                case 6:
                    ProgressBarPeminjaman("OnGoing", null);
                    stringStatus = "OnGoing";
                    break;
                case 7:
                    ProgressBarPeminjaman("Completed", null);
                    stringStatus = "Completed";
                    break;
                default:
                    ProgressBarPeminjaman("Rejected", null);
                    stringStatus = "Rejected";
                    break;
            }
            let bandageStatus;
            switch (stringStatus) {
                case "Requested":
                    bandageStatus= ` <span class="badges bg-lightyellow">Requested</span>`
                    break;
                case "OnProssesed":
                    bandageStatus= ` <span class="badges bg-lightgreen">OnProssesed</span>`
                    break;
                case "Rejected":
                    bandageStatus=` <span class="badges bg-lightred">Rejected</span>`
                    break;
                case "OnGoing":
                    bandageStatus =` <span class="badges bg-lightgreen">OnGoing</span>`
                    break;
                case "Completed":
                    bandageStatus=` <span class="badges bg-lightgreen">Completed</span>`
                    break;
                default:
                    return stringStatus;
                    break;
            }


            $.ajax({
                url: "/employee/getEmployee/" + Elements.employeeGuid,
                dataSrc: "data",
                dataType: "JSON"
            }).done((resultEmployee) => {
                $("#nameEmployee").html(resultEmployee.firstName + " " + resultEmployee.lastName);
            }).fail((error) => {
            });

            let roomName = "Tidak Meminjam Ruangan";
            if (Elements.rooms != null) {
                roomName = Elements.rooms.name
            }
            $("#startDateRequest").html(formatDate(Elements.startDate));
            $("#endDateRequest").html(formatDate(Elements.endDate));
            $("#statusRequest").html(bandageStatus);
            $("#nameRoomRequest").html(roomName);
            $("#listFasilityDetail").html("");
            $("#listFasilityDetail").append("<h4>Nama Fasilitas</h4>");
            if (Elements.fasilities.length == 0) {
                $("#listFasilityDetail").append(` <p class="list-group-item list-group-item-info m-1 text-center">
                                                Tidak Meminjam Fasilitas
                                                </p>`);
            } else {
            Elements.fasilities.forEach(fasility => {
                $("#listFasilityDetail").append(` <p class="list-group-item list-group-item-info m-1 text-center">
                                                ${fasility.name}
                                                <span class="badge d-block bg-primary">Qty : ${fasility.totalFasility}</span>
                                                </p>`);
            })

            }
        })
        //Setelah get employee
    }).fail((error) => {
    });
}

function UpdateRequest() {
    let startDates = $('#startDateTime').val();
    let endDates = $('#endDateTime').val();
    var tbl = $('#tablePeminjamanFasility tr:has(td)').map(function (i, v) {
        var $td = $('td', this);
        var unit = $td.eq(1).text();
        var unitTotal = unit.trim().split(' ')[0];
        return {
            id: ++i,
            name: $td.eq(0).text(),
            jumlah: unitTotal,
            tipe: $td.eq(2).text(),
            fasilityGuid: $td.eq(3).attr('id'),
            listFasilityGuid: $td.eq(3).attr('data'),
        }
    }).get();
    if (tbl.length > 0) {


    let isSuccess;

    let isHaveRoom = false;
    tbl.forEach(ruangan => {
        if (ruangan.tipe == "Ruangan") {
            var reqObj = {
                Guid: req,
                RoomGuid: ruangan.fasilityGuid,
                EmployeeGuid: null,
                Status: 0,
                StartDate: startDates,
                EndDate: endDates
            };
            isHaveRoom = true;
            $.ajax({
                url: '/request/update',
                method: 'POST',
                data: reqObj
            }).done((result) => {
                isSuccess = true;
            }).fail((error) => {
                isSuccess = false;
            });
        }
    })

    //jika tidak ada ruangan yang di pinjam 
    if (!isHaveRoom) {
        var reqObj = {
            Guid: req,
            RoomGuid: null,
            EmployeeGuid: null,
            Status: 0,
            StartDate: startDates,
            EndDate: endDates
        };

        $.ajax({
            url: '/request/update',
            method: 'POST',
            data: reqObj
        }).done((result) => {
            isSuccess = true;
        }).fail((error) => {
            isSuccess = false;
        })
    }

    //ambil semua data request
    let listFasil = [];
    $.ajax({
        url: '/ListFasility/GetListFasilityByRequestGuid/' + req,
        method: 'GET',
        dataType: 'json',
        async: false,
        success: function (data) {
            listFasil = data;
        }
    });

    //looping untuk mengecek fasilitas

    tbl.forEach(fasility => {
        if (fasility.tipe == "Fasilitas") {

            const matchedFasility = listFasil.find(item => item.fasilityGuid === fasility.fasilityGuid);
            if (matchedFasility) {
                if (fasility.jumlah != matchedFasility.totalFasility) {
                    matchedFasility.isStay = false;
                } else {
                    matchedFasility.isStay = true;
                }
            } else {
                fasility.isStay = false;
            }

            $.ajax({
                url: '/ListFasility/GetListFasilityByRequestGuidAndFasilityGuid',
                method: 'POST',
                data: {
                    Guid: fasility.listFasilityGuid,
                    RequestGuid: req,
                    FasilityGuid: fasility.fasilityGuid
                }
            }).done((result) => {
                if (result == null) {
                    //Jika fasilitas belum pernah di tambahkan
                    $.ajax({
                        url: '/listfasility/insert',
                        method: 'POST',
                        async: false,
                        data: {
                            FasilityGuid: fasility.fasilityGuid,
                            RequestGuid: req,
                            TotalFasility: fasility.jumlah
                        }
                    }).done((result) => {
                        isSuccess = true;
                    }).fail((error) => {
                        isSuccess = false;
                    })
                } else {
                    //Jika fasilitas sudah pernah di tambahkan

                    $.ajax({
                        url: '/listfasility/update',
                        method: 'POST',
                        async: false,
                        data: {
                            guid: fasility.listFasilityGuid,
                            RequestGuid: req,
                            FasilityGuid: fasility.fasilityGuid,
                            TotalFasility: fasility.jumlah
                        }
                    }).done((result) => {
                        isSuccess = true;
                    }).fail((error) => {
                        //isSuccess = false;
                    })
                }

            })
        }
    })
    listFasil.forEach(fasility => {
        if (!fasility.isStay) {

            $.ajax({
                url: '/ListFasility/UpdateStokFasility',
                method: 'POST',
                async: false,
                data: {
                    guid: fasility.guid,
                    RequestGuid: fasility.requestGuid,
                    fasilityGuid: fasility.fasilityGuid,
                    TotalFasility: fasility.TotalFasility
                }
            }).done((result) => {
            })

            $.ajax({
                url: "/ListFasility/Delete?guid=" + fasility.guid, // Ganti dengan URL yang sesuai
                type: "DELETE",
                async: false,
                success: function (data) {

                }
            });
        }
    });

    Swal.fire({
        icon: 'success',
        title: 'Update Success',
        showConfirmButton: false,
        allowOutsideClick: false,
        confirmButtonText: 'Save',
        timer: 1500
    }).then((result) => {
        location.reload();
    })

}else {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Peminjaman Tidak Boleh Kosong',

    })
}
}

function formatDateDashboard(originalDate) {
    var parts = originalDate.split(" ");
    var datePart = parts[0];
    var dateParts = datePart.split("/");
    var day = dateParts[0];
    var month = dateParts[1];
    var year = dateParts[2];

    return day + "/" + month + "/" + year;
}