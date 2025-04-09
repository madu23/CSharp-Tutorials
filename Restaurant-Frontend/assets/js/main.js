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
  let loginAttempts = 0;
  if (loginForm) {
    loginForm.addEventListener("submit", (event) => {
      event.preventDefault();
      const username = document.getElementById("username")?.value;
      const password = document.getElementById("password")?.value;
      const errorMessage = document.getElementById("errorMessage");
      const notification = document.getElementById("notification");
      const loginButton = document.querySelector(".btn-primary.w-100");

      if (username === "Devine" && password === "superPassword") {
        if (notification) {
          notification.classList.remove("alert-danger");
          notification.classList.add("alert-success");
          notification.textContent =
            "Login Successful! Redirecting to dashboard...";
          notification.classList.remove("d-none");
        }
        // Store the username in localStorage
        localStorage.setItem("loggedInUser", username);
        setTimeout(() => {
          window.location.href = "dashboard.html";
        }, 2000);
      } else {
        loginAttempts++;
        if (notification) {
          notification.classList.remove("alert-success");
          notification.classList.add("alert-danger");
          if (loginAttempts >= 3) {
            notification.textContent =
              "Maximum retry exceeded. Login disabled. Reload the page to try again.";
          } else {
            const remainingAttempts = 3 - loginAttempts;
            notification.textContent = `Invalid username or password. Please try again. You have ${remainingAttempts} more attempt${
              remainingAttempts > 1 ? "s" : ""
            }.`;
          }
          notification.classList.remove("d-none");
        }

        if (loginAttempts >= 3 && loginButton) {
          loginButton.disabled = true;
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

// Display the username on the dashboard and implement logout logic
document.addEventListener("DOMContentLoaded", () => {
  const usernameDisplay = document.getElementById("usernameDisplay");
  const loggedInUser = localStorage.getItem("loggedInUser");
  const logoutButton = document.getElementById("logoutButton");

  if (usernameDisplay && loggedInUser) {
    usernameDisplay.textContent = `Hello, ${loggedInUser}`;
  }

  if (logoutButton) {
    logoutButton.addEventListener("click", (event) => {
      event.preventDefault();
      localStorage.removeItem("loggedInUser");
      window.location.href = "index.html";
    });
  }
});
