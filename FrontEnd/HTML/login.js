let loginAttempts = 0;
const maxAttempts = 3;

document.addEventListener("DOMContentLoaded", function () {
  const loginForm = document.getElementById("login-box");

  if (loginForm) {
    loginForm.addEventListener("submit", function (event) {
      event.preventDefault();

      const username = document.getElementById("login-username").value.trim();
      const password = document.getElementById("login-password").value.trim();
      const alertBox = document.getElementById("login-alert");
      const loginButton = loginForm.querySelector("button[type='submit']");

      const validUsername = "Elizabeth";
      const validPassword = "superPassword";

      if (username === validUsername && password === validPassword) {
        alertBox.className = "alert alert-success mt-2 small";
        alertBox.innerText = "Login successful! Redirecting...";
        localStorage.setItem("loggedUser", username);
        setTimeout(() => {
          window.location.href = "dashboard.html";
        }, 1000);
      } else {
        loginAttempts++;
        const attemptsLeft = maxAttempts - loginAttempts;

        if (loginAttempts < maxAttempts) {
          alertBox.className = "alert alert-warning mt-2 small";
          alertBox.innerText = `Incorrect login. You have ${attemptsLeft} trial${attemptsLeft === 1 ? "" : "s"} left!`;
        } else {
          alertBox.className = "alert alert-danger mt-2 small";
          alertBox.innerText = "Too many failed attempts. Please try again later.";
          loginButton.disabled = true;
          loginButton.classList.remove("btn-primary");
          loginButton.classList.add("btn-primary-disabled");
        }
        alertBox.classList.remove("d-none");
      }
    });
  }
});

document.addEventListener("DOMContentLoaded", function(){
  let hours = new Date().getHours();
  let buttonElement = document.querySelector(".btn-primary");
  if (buttonElement) {
    if (hours >= 6 && hours < 12) {
      buttonElement.style.backgroundColor = "white";
      buttonElement.style.color = "#49392c";
      buttonElement.style.border = "1px solid grey";
    } else if (hours >= 12 && hours < 18) {
      buttonElement.style.backgroundColor = "blue";
      buttonElement.style.color = "white";
      buttonElement.style.border = "none";
    } else {
      buttonElement.style.backgroundColor = "red";
      buttonElement.style.color = "white";
      buttonElement.style.border = "none";
    }
  }
});
