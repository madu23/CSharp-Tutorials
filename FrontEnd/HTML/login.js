document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("loginForm");

  if (loginForm) {
    loginForm.addEventListener("submit", function (event) {
      event.preventDefault();

      // Get user input
      const username = document.getElementById("username").value.trim();
      const password = document.getElementById("password").value.trim();

      // Dummy credentials (replace with real authentication)
      const validUsername = "admin";
      const validPassword = "superPassword";

      if (username === validUsername && password === validPassword) {
        alert("Login successful!");
        window.location.href = "dashboard.html"; // Redirect to dashboard
      } else {
        alert("Invalid username or password. Try again.");
      }
    });
  }
});
