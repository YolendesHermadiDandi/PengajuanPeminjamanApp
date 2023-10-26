function onScanSuccess(decodedText, decodedResult) {
    // Handle on success condition with the decoded text or result.
    console.log(`Scan result: ${decodedText}`, decodedResult);
}

var html5QrcodeScanner = new Html5QrcodeScanner(
    "reader", { fps: 10, qrbox: 250 });
html5QrcodeScanner.render(onScanSuccess);

$(document).ready(function () {
    $("button#html5-qrcode-button-camera-stop").on('click', (e) => {
        alert("oee");
    });
    $("div#reader div img").addClass("d-none");
    $("#html5-qrcode-button-camera-permission").addClass('btn btn-primary');
    $("#reader__dashboard_section_csr span button").addClass('btn btn-primary');
});
