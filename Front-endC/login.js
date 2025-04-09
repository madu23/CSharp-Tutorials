const form = document.getElementById("loginForm");
const loginError = document.getElementById("loginError");

let loginAttempts = 3;
let isLocked = false; // Flag to check if the account is locked

form.addEventListener("submit", function (event) {
  event.preventDefault();

  // Add validation classes
  form.classList.add("was-validated");

  // Prevent submission if account is locked
  if (isLocked) {
    return;
  }

  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  // Check if form is valid
  if (!form.checkValidity()) {
    event.stopPropagation();
    return;
  }

  // Simple validation (replace with your actual validation logic)
  if (username === "rukky" && password === "superpassword") {
    // Store username before redirecting
    localStorage.setItem("username", username);
    window.location.href = "dashboard.html";
  } else {
    loginAttempts--;
    if (loginAttempts > 0) {
      loginError.textContent = `Invalid username or password! ${loginAttempts} attempts remaining`;
      loginError.classList.remove("d-none");
    } else {
      loginError.textContent =
        "Maximum login attempts reached. Please try again later.";
      loginError.classList.remove("d-none");
      loginBtn.disabled = true;
      loginBtn.classList.add("opacity-50");

      // Unlock after 20 seconds
      setTimeout(() => {
        isLocked = false;
        loginAttempts = 3;
        loginBtn.disabled = false;
        loginBtn.classList.remove("opacity-50");
        loginError.classList.add("d-none");
      }, 20000);
    }
    form.classList.remove("was-validated");
  }
});

// Only hide error when user starts typing if there are attempts left
form.addEventListener("input", function () {
  if (loginAttempts > 0) {
    loginError.classList.add("d-none");
  }
});

// Add this at the beginning of the file
window.addEventListener("load", function () {
  // Fade out the preloader
  const preloader = document.getElementById("preloader");
  preloader.style.opacity = "0";
  setTimeout(() => {
    preloader.style.display = "none";
  }, 500);
});

// Simple button color change based on time of day
const loginBtn = document.querySelector(".btn-primary.w-100");

function updateButtonColor() {
  const hour = new Date().getHours();

  // Morning: before 12pm
  if (hour < 12) {
    loginBtn.style.backgroundColor = "#e8f4f8";
    loginBtn.style.color = "#2c3e50";
  }
  // Afternoon: 12pm to 6pm
  else if (hour < 18) {
    loginBtn.style.backgroundColor = "#3498db";
    loginBtn.style.color = "#ffffff";
  }
  // Evening: after 6pm
  else {
    loginBtn.style.backgroundColor = "#e74c3c";
    loginBtn.style.color = "#ffffff";
  }
}

// Initial color update
updateButtonColor();

// Update color every hour
setInterval(updateButtonColor, 3600000);
