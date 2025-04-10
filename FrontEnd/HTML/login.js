let loginAttempts = 0;
const maxAttempts = 3;
document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("loginForm");

  if (loginForm) {
    loginForm.addEventListener("submit", function (event) {
      event.preventDefault();

      // Get user input
      const username = document.getElementById("username").value.trim();
      const password = document.getElementById("password").value.trim();
      const alertBox = document.getElementById("loginAlert");
      const loginButton = loginForm.querySelector("button[type='submit']");

      // Dummy credentials (replace with real authentication)
      const validUsername = "admin";
      const validPassword = "superPassword";

      if (username === validUsername && password === validPassword) {
        alertBox.className = "text-success mt-2 small";
        alertBox.innerText = "Login successful! Redirecting...";

        localStorage.setItem("loggedUser", username);
        window.location.href = "dashboard.html"; // Redirect to dashboard
      } else {
        loginAttempts++;

        const attemptsLeft = maxAttempts - loginAttempts;

        if (loginAttempts < maxAttempts) {
          alertBox.className = "text-warning mt-2 small";
          alertBox.innerText = `Incorrect login again. You have ${attemptsLeft} trial${
            attemptsLeft === 1 ? "" : "s"
          } left!`;
        } else {
          alertBox.className = "text-danger mt-2 small";
          alertBox.innerText =
            "Too many failed attempts. Please try again later.";

          loginButton.disabled = true;
          loginButton.classList.add("btn-secondary");
          loginButton.classList.remove("btn-dark");
        }

        alertBox.classList.remove("d-none");
      }
    });
  }
});

document.addEventListener("DOMContentLoaded", function () {
  let hours = new Date().getHours(); // Get current hour

  let buttonElement = document.querySelector(".btn-dark");

  if (buttonElement) {
    // To ensure the button exists before applying styles
    if (hours >= 6 && hours < 12) {
      // Morning (From 6 AM - 11:59 AM)
      buttonElement.style.backgroundColor = "white";
      buttonElement.style.color = "#49392c";
      buttonElement.style.border = "grey";
    } else if (hours >= 12 && hours < 18) {
      // Afternoon (From 12 PM - 5:59 PM)
      buttonElement.style.backgroundColor = "blue";
      buttonElement.style.border = "none";
    } else {
      // Evening & Night (From 6 PM - 5:59 AM)
      buttonElement.style.backgroundColor = "red";
      buttonElement.style.border = "none";
    }
  }
});
