function validateForm(event) {
    var email = document.getElementById("Email").value;
    var password = document.getElementById("Password").value;

    if (email === "" || password === "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Gagal login. Email atau kata sandi Tidak Boleh Kosong.',
        });
        event.preventDefault();
    }
}

var button = document.getElementById("button-addon2");
var input = document.getElementById("Email");

// Fungsi untuk memulai atau melanjutkan hitungan mundur
function startCountdown() {
    var endTime = localStorage.getItem("countdownEndTime");
    
    if (!endTime) {
        endTime = new Date().getTime() + 5 * 60 * 1000; // 5 menit dalam milidetik
        localStorage.setItem("countdownEndTime", endTime);
    }

    button.setAttribute("disabled", "true");

    // Mengatur interval untuk memperbarui teks tombol
    var x = setInterval(function () {
        var now = new Date().getTime();
        var distance = endTime - now;

        if (distance <= 0) {
            clearInterval(x);
            button.innerHTML = "Kirim";
            localStorage.removeItem("countdownEndTime");
            button.removeAttribute("disabled");
        } else {
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);
            button.innerHTML = minutes + "m " + seconds + "s";
        }
    }, 1000); // Memperbarui setiap 1 detik
}

// Menambahkan event click pada tombol
button.addEventListener("click", function () {
    startCountdown();
});

