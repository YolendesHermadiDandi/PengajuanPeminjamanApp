$(document).ready(function () {
    $("#endDateTime").attr("disabled", "");
    //Enablabling linked pickers
    $('#startDateTime,#endDateTime').datetimepicker({
        format: 'YYYY/MM/DD',
        useCurrent: false,
        minDate: moment().add(3, 'days'),
        defaultDate: moment().add(3, 'days')
    });

    //Setting the range of dates
    $("#startDateTime").on("dp.change", function (e) {
        $('#startDateTime').data("DateTimePicker").minDate(moment().add(3, 'days'));
        $('#endDateTime').data("DateTimePicker").minDate(e.date);
        $("#endDateTime").prop('disabled', false);
    });
});

function kalenderPeminjaman() {
    $('#ModalCalender').modal('show');
    setTimeout(function () {
        let objRoom;

        $.ajax({
            url: '/room/GetRoomDate',
            method: 'GET',
            dataType: 'json',
            async: false,
            success: function (data) {
                objRoom = data

            },
            error: function (error) {

            }
        });
        
        for (var i = 0; i < objRoom.length; i++) {
            var startDate = new Date(objRoom[i].start);
            var endDate = new Date(objRoom[i].end);

            var formattedStartDate = startDate.toISOString().split('T')[0];
            var formattedEndDate = endDate.toISOString().split('T')[0];

            objRoom[i].start = formattedStartDate;
            objRoom[i].end = formattedEndDate;
        }

        var calendarEl = document.getElementById('calendar');
        var today = new Date();
        var calendar = new FullCalendar.Calendar(calendarEl, {
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'multiMonthYear,dayGridMonth'
            },
            initialView: 'dayGridMonth',
            initialDate: today.getFullYear() + '-' + String(today.getMonth() + 1).padStart(2, '0') + '-01',
            editable: false,
            selectable: true,
            dayMaxEvents: true, // allow "more" link when too many events
            multiMonthMaxColumns: 100, // guarantee single column
            // showNonCurrentDates: true,
            // fixedWeekCount: false,
            // businessHours: true,
            // weekends: false,
            events: objRoom
        });

        calendar.render();
    }, 170);
}

let valueDefaultFasiity;
let isAvalibe = false;
document.getElementById('btnModalFasilitas').addEventListener('click', () => {
    if (isAvalibe ==  false) {
        $("#tbodyListFasility").html("");
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
    } 

})

function tambahFasilityTabel(guid) {
    $.ajax({
        url: '/GetFasility/' + guid,
        method: 'GET',
        dataType: 'json',
        success: function (data) {

            let valuePinjam = document.getElementById(`value${data.name}`).value
            if ((valueDefaultFasiity - valuePinjam) < 1) {
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

function hapusFaslity(idBtn) {
    button = document.getElementById(`${idBtn}`);
    button.removeAttribute("disabled");
}




//untuk tampil modal ruangan
document.getElementById('btnModalRuangan').addEventListener('click', () => {
        $("#tbodyListRuangan").html("");
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
                            <button type="button" class="btn btn-primary" id="btnTambahRuangan${Elements.name}" onclick="tambahRuangan('${Elements.guid}')">Tambah Ruangan</button>
                        </div>
                        </td>
                    </tr>`);

                })

            },
            error: function (error) {

            }
        });
})

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

            $('#ModalRuangan').modal('hide');
            const button = document.getElementById(`btnModalRuangan`);
            button.setAttribute("disabled", "");
        },
        error: function (error) {

        }
    });
}


function hapusRuangan(idBtn) {
    button = document.getElementById(`btnModalRuangan`);
    button.removeAttribute("disabled");
}


function ajukanRequest() {

    let req;
    var tbl = $('#tablePeminjamanFasility tr:has(td)').map(function (i, v) {
        var $td = $('td', this);
        var unit = $td.eq(1).text();
        var unitTotal = unit.trim().split(' ')[0];
        return {
            id: ++i,
            name: $td.eq(0).text(),
            jumlah: unitTotal,
            tipe: $td.eq(2).text(),
            guid: $td.eq(3).attr('id'),
        }
    }).get();

    let startDates = $('#startDateTime').val();
    let endDates = $('#endDateTime').val();

    let isReqRoom = false;
    let isReqFasility= false;
    let roomId;
    tbl.forEach(tabelRoom => {
        if (tabelRoom.tipe == "Ruangan") {
            isReqRoom = true;
            roomId = tabelRoom.guid;
        }
        if (tabelRoom.tipe == "Fasilitas") {
            isReqFasility = true;
        }
    })

    if ((!isReqFasility && !isReqRoom) || startDates == null && endDates == null) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please Fill Data Courectly',

        })
    }
    if (isReqRoom && isReqFasility) {
        let objRequest = {
            roomGuid: roomId,
            employeeGuid: null,
            status: 0,
            startDate: startDates,
            endDate: endDates,
        }

        $.ajax({
            type: "post",
            url: "/request/IsRoomIdle",
            data: {
                roomGuid: roomId,
                startDate: startDates,
                endDate: endDates
            },
        }).done((rstRequest) => {
            if (rstRequest.code == 200) {
                $.ajax({
                    type: "post",
                    url: "/request/insert",
                    data: objRequest,
                }).done((rstRequest) => {
                    tbl.forEach(tabelFasility => {
                        if (tabelFasility.tipe == "Fasilitas") {
                            let objFasility = {
                                requestGuid: rstRequest.data.guid,
                                fasilityGuid: tabelFasility.guid,
                                totalFasility: tabelFasility.jumlah,
                            }

                            $.ajax({
                                type: "post",
                                url: "/listfasility/insert",
                                data: objFasility,
                            }).done((result) => {
                            }).fail((error) => {
                            });

                            Swal.fire({
                                icon: 'success',
                                title: 'Add Request Success',
                                showConfirmButton: false,
                                timer: 1500

                            })
                            isReqFasility = false;
                            isReqRoom = false;
                        }
                    })
                }).fail((error) => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Failed to Request data',

                    })
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Maaf Raungan Yang Anda Pinjam Tidak Tersedia Di Tanggal Yang Anda Minta',

                })
            }
        })


    }

    else if (isReqRoom) {
        let objRequest = {
            roomGuid: roomId,
            employeeGuid: null,
            status: 0,
            startDate: startDates,
            endDate: endDates,
        }
        $.ajax({
            type: "post",
            url: "/request/IsRoomIdle",
            data: {
                roomGuid: roomId,
                startDate: startDates,
                endDate: endDates
            },
        }).done((rstRequest) => {
            if (rstRequest.code == 200) {
                $.ajax({
                    type: "post",
                    url: "/request/insert",
                    data: objRequest,
                }).done((rstRequest) => {
                    Swal.fire({
                        icon: 'success',
                        title: 'Add Request Success',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }).fail((error) => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Failed to Request data',

                    })
                });
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Maaf Raungan Yang Anda Pinjam Tidak Tersedia Di Tanggal Yang Anda Minta',

                })
            }
        })
    }

    else if (isReqFasility) {
        let objRequest = {
            roomGuid: null,
            employeeGuid: null,
            status: 0,
            startDate: startDates,
            endDate: endDates,
        }
        $.ajax({
            type: "post",
            url: "/request/insert",
            data: objRequest,
        }).done((rstRequest) => {
            tbl.forEach(tabelFasility => {
                if (tabelFasility.tipe == "Fasilitas") {
                    let objFasility = {
                        requestGuid: rstRequest.data.guid,
                        fasilityGuid: tabelFasility.guid,
                        totalFasility: tabelFasility.jumlah,
                    }

                    $.ajax({
                        type: "post",
                        url: "/listfasility/insert",
                        data: objFasility,
                    }).done((result) => {
                    }).fail((error) => {
                    });

                    Swal.fire({
                        icon: 'success',
                        title: 'Add Request Success',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
        }).fail((error) => {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Failed to Request data',

            })
        });
    }
}