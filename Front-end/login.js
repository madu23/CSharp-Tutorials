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
