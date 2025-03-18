document.addEventListener("DOMContentLoaded", function () {
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
          backgroundColor: "rgba(54, 162, 235, 0.6)",
          borderColor: "rgba(54, 162, 235, 1)",
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {
        y: {beginAtZero: true},
      },
      plugins: {
        legend: {display: false},
        title: {
          display: false,
          text: "Weekly Sales Overview",
          font: {size: 20},
        },
      },
    },
  });

  // Sidebar Toggle Function
  const sidebar = document.querySelector(".sidebar");
  const mainContent = document.querySelector(".main-content");
  const toggleBtn = document.querySelector(".toggle-sidebar");

  toggleBtn.addEventListener("click", function () {
    sidebar.classList.toggle("active");
    mainContent.classList.toggle("expanded");
  });
});
