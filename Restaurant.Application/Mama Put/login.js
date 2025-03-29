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
