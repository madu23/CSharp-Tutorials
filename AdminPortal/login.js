let loginAttempts = 0;
let isLocked = false; // Flag to check if the account is locked

// Check if there's a lockout state in localStorage
if (localStorage.getItem('isLocked') === 'true') {
    const lockoutExpiry = parseInt(localStorage.getItem('lockoutExpiry'), 10);
    const currentTime = Date.now();

    if (currentTime < lockoutExpiry) {
        isLocked = true;
        setTimeout(() => {
            resetLockout();
        }, lockoutExpiry - currentTime); // Unlock after the remaining time
    } else {
        resetLockout(); // If the lockout has expired, reset it immediately
    }
}

function validateLogin(event) {
    event.preventDefault();

    const username = document.getElementById('username').value.trim();
    const password = document.getElementById('password').value.trim();
    const loginButton = document.getElementById('button');
    const errorMessage = document.getElementById('errorMessage');

    // Prevent submission if the account is locked
    if (isLocked) {
        return;
    }

    // Check if credentials are correct
    if (username === 'superAdmin' && password === 'P@ssw0rd#') {
        localStorage.setItem("username", username); // Store the username
        window.location.href = 'dashboard.html'; // Redirect to the dashboard
    } else {
        loginAttempts++;

        // Check remaining attempts
        const remainingAttempts = 3 - loginAttempts;
        if (remainingAttempts > 0) {
            errorMessage.textContent = `Incorrect username or password! ${remainingAttempts} attempt${remainingAttempts > 1 ? 's' : ''} remaining.`;
            errorMessage.classList.remove("d-none");
        } else {
            errorMessage.textContent = "Maximum login attempts reached. Please try again later.";
            errorMessage.classList.remove("d-none");
            loginButton.disabled = true; // Disable the button
            loginButton.innerText = "Locked Out"; // Change button text
            isLocked = true;

            // Store lockout state and expiry time in localStorage
            const lockoutExpiry = Date.now() + 20000; // 20 seconds from now
            localStorage.setItem('isLocked', 'true');
            localStorage.setItem('lockoutExpiry', lockoutExpiry.toString());

            // Reset lock state after 20 seconds
            // setTimeout(() => {
            //     resetLockout();
            // }, 20000);
        }
    }
}

function resetLockout() {
    isLocked = false;
    loginAttempts = 0;
    localStorage.removeItem('isLocked');
    localStorage.removeItem('lockoutExpiry');

    const loginButton = document.getElementById('button');
    const errorMessage = document.getElementById('errorMessage');

    if (loginButton) {
        loginButton.disabled = false;
        loginButton.innerText = "Login";
    }

    if (errorMessage) {
        errorMessage.classList.add("d-none");
    }
}

function changeButtonColor() {
    const now = new Date();
    const hours = now.getHours();
    const button = document.getElementById("button");

    if (button) {
        if (hours < 12) {
            button.style.backgroundColor = "white";
            button.style.color = "black";
        } else if (hours >= 12 && hours < 18) {
            button.style.backgroundColor = "blue";
            button.style.color = "white";
        } else {
            button.style.backgroundColor = "red";
            button.style.color = "white";
        }
    }
}

document.addEventListener('DOMContentLoaded', function () {
    // Change button color based on the time of day
    changeButtonColor();
    setInterval(changeButtonColor, 60000);

    // Fade out preloader
    const preloader = document.getElementById("preloader");
    if (preloader) {
        preloader.style.opacity = "0";
        setTimeout(() => {
            preloader.style.display = "none";
        }, 500);
    }
});