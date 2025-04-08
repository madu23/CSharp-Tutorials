document.addEventListener("DOMContentLoaded", () => {
  window.addEventListener("load", function () {
    // Fade out the preloader
    const preloader = document.getElementById("preloader");
    preloader.style.opacity = "0";
    setTimeout(() => {
      preloader.style.display = "none";
    }, 5000);
  });
  // Login Form
  const loginForm = document.getElementById("loginForm");
  if (loginForm) {
    loginForm.addEventListener("submit", (event) => {
      event.preventDefault();
      const username = document.getElementById("username")?.value;
      const password = document.getElementById("password")?.value;
      const errorMessage = document.getElementById("errorMessage");

      if (errorMessage) {
        errorMessage.style.margin = "10px";
        errorMessage.style.fontSize = "20px";
      }

      if (username === "admin" && password === "12345") {
        if (errorMessage) {
          errorMessage.style.color = "green";
          errorMessage.textContent =
            "Login Successful! Redirecting to dashboard...";
        }
        setTimeout(() => {
          window.location.href = "dashboard.html";
        }, 2000);
      } else {
        if (errorMessage) {
          errorMessage.style.color = "red";
          errorMessage.textContent =
            username !== "admin"
              ? "Username cannot be found"
              : "Please input correct password";
        }
      }
    });
  }

  const loginButton = document.querySelector(".btn-primary.w-100");
  if (loginButton) {
    function updateLoginButtonColor() {
      const currentHour = new Date().getHours();
      if (currentHour < 12) {
        loginButton.style.backgroundColor = "white";
        loginButton.style.color = "black";
      } else if (currentHour < 18) {
        loginButton.style.backgroundColor = "blue";
        loginButton.style.color = "white";
      } else {
        loginButton.style.backgroundColor = "red";
        loginButton.style.color = "white";
      }
    }

    updateLoginButtonColor();
  }
});
