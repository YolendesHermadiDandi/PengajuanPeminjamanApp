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
var input = document.getElementById("email");

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

var code = "";
button.addEventListener("click", function () {
    if (localStorage.getItem("countdownEndTime")) {
        startCountdown();
    } else {
        startCountdown();
        $.ajax({
            url: '/Auth/ResetPasswordEmail/' + input.value,
            method: 'GET',
            dataType: 'json',
            async: false,
            success: function (data) {
                startCountdown();
                Swal.fire({
                    icon: 'success',
                    title: 'Silahkan periksa email anda untuk mendapatkan OTP, OTP Berlaku selama 5 menit.',
                    showConfirmButton: false,
                    timer: 2500
                })
                code = data[0].otp;
            },
            error: function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Maaf email yang anda masukan salah, silahkan ketik ulang email anda',
                })

            }
        });
    }
    
});

if (localStorage.getItem("countdownEndTime")) {
    startCountdown();
}
function isPasswordValid(password) {
    // Ekspresi reguler untuk memeriksa minimal 8 karakter, 1 angka, dan 1 simbol
    var passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    return passwordRegex.test(password);
}

function validateFormPassword(event) {
    var rePassword = document.getElementById("RePassword").value;
    var password = document.getElementById("Password").value;
    var OTP = document.getElementById("OTP").value;

    if (OTP != code) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'OTP yang anda masukan salah.',
        });
        event.preventDefault();
    } else if (isPasswordValid(password)) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Password harus memiliki minimal 8 karakter, 1 angka dan 1 simbol.',
        });
        event.preventDefault();
    } else if (rePassword != password) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Maaf password yang anda masukan tidak sama, periksa kembali password anda.',
        });
        event.preventDefault();
    }
}