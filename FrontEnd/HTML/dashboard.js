document.addEventListener("DOMContentLoaded", function () {
  // Get username from localStorage
  const userName = localStorage.getItem("loggedUser") || "Admin";

  // Update the greeting
  const nameElements = document.getElementsByClassName("user-name");
  if (nameElements) {
    Array.from(nameElements).forEach((el) => {
      el.innerText = userName;
    });
  }
});

document.addEventListener("DOMContentLoaded", function () {
  document.querySelectorAll(".nav-link").forEach((link) => {
    link.addEventListener("click", function (event) {
      event.preventDefault();

      // Remove 'active' from all links
      document
        .querySelectorAll(".nav-link")
        .forEach((nav) => nav.classList.remove("active"));

      // Hide all charts
      document.getElementById("daily-sales").classList.add("d-none");
      document.getElementById("weekly-sales").classList.add("d-none");
      document.getElementById("monthly-sales").classList.add("d-none");

      // Add 'active' to the clicked link
      this.classList.add("active");

      // Show the corresponding chart
      const selectedTab = this.getAttribute("data-tab");
      document
        .getElementById(`${selectedTab}-sales`)
        .classList.remove("d-none");
    });
  });

  const sidebarButtons = document.querySelectorAll(
    ".btn.d-flex.align-items-center"
  );

  sidebarButtons.forEach((button) => {
    button.addEventListener("click", () => {
      // Remove btn-primary and add btn-light for all buttons
      sidebarButtons.forEach((btn) => {
        btn.classList.remove("btn-primary");
        btn.classList.add("btn-light");
      });

      // Add btn-primary and remove btn-light to the clicked button
      button.classList.remove("btn-light");
      button.classList.add("btn-primary");
    });
  });
});
