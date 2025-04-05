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

function changeButtonColor() {
    const now = new Date();
    const hours = now.getHours();
    const button = document.getElementById("button");

    if (button) {
        if (hours >= 0 && hours < 8) {
            button.style.backgroundColor = "white";
        } else if (hours >= 8 && hours < 16) {
            button.style.backgroundColor = "blue";
        } else {
            button.style.backgroundColor = "red";
        }
    }
}

document.addEventListener('DOMContentLoaded', function() {
    changeButtonColor();
    setInterval(changeButtonColor, 60000);
});