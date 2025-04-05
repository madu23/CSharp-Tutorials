function updateButtonColor() {
    const btn = document.getElementById('loginBtn');
    const hour = new Date().getHours();

    if (hour < 12) {
        btn.className = 'btn btn-light w-100';
    } else if (hour < 18) {
        btn.className = 'btn btn-primary w-100';
    } else {
        btn.className = 'btn btn-danger w-100';
    }
}

function validateLogin(event) {
    event.preventDefault();

    var username = document.getElementById('username').value.trim();
    var password = document.getElementById('password').value.trim();

    if (username === 'user' && password === 'password') {
        window.location.href = 'dashboard.html';
    } else {
        alert('Invalid username or password!');
    }
}
window.onload = updateButtonColor;
