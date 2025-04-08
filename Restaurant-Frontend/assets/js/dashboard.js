document.addEventListener("DOMContentLoaded", function () {
  window.addEventListener("load", function () {
    // Fade out the preloader
    const preloader = document.getElementById("preloader");
    preloader.style.opacity = "0";
    setTimeout(() => {
      preloader.style.display = "none";
    }, 5000);
  });

  const toggleSidebarButton = document.querySelector(".toggle-sidebar"); // Updated selector
  const sidebar = document.querySelector(".sidebar"); // Updated selector

  if (!toggleSidebarButton) {
    console.error("Toggle sidebar button not found");
  }
  if (!sidebar) {
    console.error("Sidebar element not found");
  }

  if (toggleSidebarButton && sidebar) {
    toggleSidebarButton.addEventListener("click", function () {
      console.log("Toggle sidebar button clicked"); // Debug log
      sidebar.classList.toggle("active"); // Updated class to match CSS
      const mainContent = document.querySelector(".main-content");
      const header = document.querySelector(".dashboard-header");
      mainContent.classList.toggle("expanded"); // Adjust main content
      header.classList.toggle("expanded"); // Adjust header
    });
  }
  // Chart.js configuration
  const ctx = document.getElementById("salesChart").getContext("2d");
  new Chart(ctx, {
    type: "bar",
    data: {
      labels: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday",
      ],
      datasets: [
        {
          label: "Daily Sales (₦)",
          data: [15000, 12000, 18000, 20000, 17000, 25000, 22000],
          backgroundColor: [
            "rgba(255, 0, 0, 0.6)",
            "rgba(0, 0, 255, 0.6)",
            "rgba(255, 255, 0, 0.6)",
            "rgba(0, 128, 0, 0.6)",
            "rgba(75, 0, 130, 0.6)",
            "rgba(255, 215, 0, 0.6)",
            "rgba(165, 42, 42, 0.6)",
          ],
          borderColor: [
            "rgba(255, 0, 0, 0.6)",
            "rgba(0, 0, 255, 0.6)",
            "rgba(255, 255, 0, 0.6)",
            "rgba(0, 128, 0, 0.6)",
            "rgba(75, 0, 130, 0.6)",
            "rgba(255, 215, 0, 0.6)",
            "rgba(165, 42, 42, 0.6)",
          ],
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {y: {beginAtZero: true}},
      plugins: {
        legend: {display: false},
        title: {display: false},
      },
    },
  });

  new SidebarManager();
});
