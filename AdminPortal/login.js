function validateLogin(event) {
    event.preventDefault(); 
    
    let username = document.getElementById('username').value.trim();
    let password = document.getElementById('password').value.trim();
    
    if (username === 'superAdmin' && password === 'P@ssw0rd#') {
        window.location.href = 'dashboard.html'; 
    } else {
        alert('Incorrect username or password!');
    }
}
