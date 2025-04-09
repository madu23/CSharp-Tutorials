// Add this after your existing window.addEventListener code
window.addEventListener("load", function () {
  // Get username from localStorage
  const username = localStorage.getItem("username");

  // Update the welcome message
  const welcomeSpan = document.getElementById("welcomeMessage");
  if (welcomeSpan) {
    welcomeSpan.textContent = `Hello, ${username}`;
  }
});

// Add this after your existing window.addEventListener code
document.getElementById("logoutBtn").addEventListener("click", function () {
  // Clear the stored username
  localStorage.removeItem("username");
  // Redirect to login page
  window.location.href = "login.html";
});

// Purpose: To create a chart for the sales data
var ctx = document.getElementById("salesChart").getContext("2d");
new Chart(ctx, {
  type: "bar",
  data: {
    labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"],
    datasets: [
      {
        label: "Sales",
        data: [12, 19, 10, 14, 17],
        backgroundColor: ["blue", "orange", "red", "teal", "black"],
      },
    ],
  },
});
// Sidebar Toggle Function

document.getElementById("sidebarToggle").addEventListener("click", function () {
  const sidebar = document.getElementById("sidebar");
  sidebar.classList.toggle("collapsed");
});

// Add this at the beginning of the file
window.addEventListener("load", function () {
  // Fade out the preloader
  const preloader = document.getElementById("preloader");
  preloader.style.opacity = "0";
  setTimeout(() => {
    preloader.style.display = "none";
  }, 5000);
});
