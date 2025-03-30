document.addEventListener("DOMContentLoaded", () => {
  // Preloader
  document.body.classList.add("loading");
  setTimeout(() => {
    const preloader = document.getElementById("preloader");
    if (preloader) preloader.style.display = "none";
    document.body.classList.remove("loading");
  }, 3000);

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

  // Search Form
  const searchForm = document.querySelector(".search-form");
  if (searchForm) {
    searchForm.addEventListener("submit", (e) => {
      e.preventDefault();
      const input = searchForm.querySelector('input[type="search"]');
      const query = input ? input.value.trim() : "";
      console.log("Searching for:", query);
      query.length > 0 ? filterCards(query) : resetCards();
    });
  }
});

// Filters the dashboard cards based on the query.
const filterCards = (query) => {
  const cards = document.querySelectorAll(".custom-card");
  cards.forEach((card) => {
    card.style.display = card.textContent
      .toLowerCase()
      .includes(query.toLowerCase())
      ? ""
      : "none";
  });
};

// Resets the display property for all dashboard cards.
const resetCards = () => {
  document.querySelectorAll(".custom-card").forEach((card) => {
    card.style.display = "";
  });
};

const loginButton = document.querySelector(".btn-primary.w-100");
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
