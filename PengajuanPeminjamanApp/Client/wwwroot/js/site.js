function QRScan() {
    alert();
    //function docReady(fn) {
    //    // see if DOM is already available
    //    if (document.readyState === "complete"
    //        || document.readyState === "interactive") {
    //        // call on next available tick
    //        setTimeout(fn, 1);
    //    } else {
    //        document.addEventListener("DOMContentLoaded", fn);
    //    }
    //}

    //docReady(function () {
    //    var resultContainer = document.getElementById('qr-reader-results');
    //    var lastResult, countResults = 0;
    //    function onScanSuccess(decodedText, decodedResult) {
    //        if (decodedText !== lastResult) {
    //            ++countResults;
    //            lastResult = decodedText;
    //            // Handle on success condition with the decoded message.
    //            console.log(`Scan result ${decodedText}`, decodedResult);
    //            try {
    //                $.ajax({
    //                    url: "/qrScan/getRequest/" + decodedText,
    //                    dataSrc: "data",
    //                    dataType: "JSON"
    //                }).done((result) => {
    //                    if (result.code == 200) {
    //                        Swal.fire({
    //                            icon: 'success',
    //                            title: 'Success',
    //                            showConfirmButton: false,
    //                            timer: 1500
    //                        })
    //                        //$("#QRModal").modal('hide');
    //                    } else {
    //                        Swal.fire({
    //                            icon: 'error',
    //                            title: 'You Not Invited',
    //                            showConfirmButton: false,
    //                            timer: 1500
    //                        })
    //                        //$("#QRModal").modal('hide');
    //                    }
    //                }).fail((err) => {
    //                    Swal.fire({
    //                        icon: 'error',
    //                        title: 'You Not Invited',
    //                        showConfirmButton: false,
    //                        timer: 1500
    //                    })
    //                });
    //            } catch (e) {
    //                alert(e);
    //            }
    //            setTimeout(function () {
    //                window.location.reload(1);
    //            }, 2000);
    //        }
    //    }

    //    var html5QrcodeScanner = new Html5QrcodeScanner(
    //        "qr-reader", { fps: 10, qrbox: 250 });
    //    html5QrcodeScanner.render(onScanSuccess);
    //});

};
