function validateLogin(event) {
    event.preventDefault(); 
    
    var username = document.getElementById('username').value.trim();
    var password = document.getElementById('password').value.trim();
    
    if (username === 'superAdmin' && password === 'P@ssw0rd#') {
        window.location.href = 'dashboard.html'; 
    } else {
        alert('Incorrect username or password!');
    }
}
