document
  .getElementById("loginForm")
  .addEventListener("submit", function (event) {
    event.preventDefault();

    let username = document.getElementById("username").value;
    let password = document.getElementById("password").value;
    let errorMessage = document.getElementById("errorMessage");

    errorMessage.style.marginTop = "10px";
    errorMessage.style.fontSize = "14px";

    if (username === "admin" && password === "12345") {
      errorMessage.style.color = "green";
      errorMessage.textContent =
        "Login Successful! Redirecting to dashboard...";
      setTimeout(() => {
        window.location.href = "dashboard.html";
      }, 2000); // Redirect after 2 seconds
    } else {
      errorMessage.style.color = "red";
      if (username !== "admin") {
        errorMessage.textContent = "Username cannot be found";
      } else if (password !== "12345") {
        errorMessage.textContent = "Please input correct password";
      }
    }
  });
