const form = document.getElementById("loginForm");
const loginError = document.getElementById("loginError");

form.addEventListener("submit", function (event) {
  event.preventDefault();

  // Add validation classes
  form.classList.add("was-validated");

  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  // Check if form is valid
  if (!form.checkValidity()) {
    event.stopPropagation();
    return;
  }

  // Simple validation (replace with your actual validation logic)
  if (username === "rukky" && password === "superpassword") {
    window.location.href = "dashboard.html";
  } else {
    loginError.classList.remove("d-none");
    form.classList.remove("was-validated");
  }
});

// Hide error message when user starts typing
form.addEventListener("input", function () {
  loginError.classList.add("d-none");
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
