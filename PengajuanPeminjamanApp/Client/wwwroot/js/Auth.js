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