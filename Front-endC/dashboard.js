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
/*var ctx = document.getElementById("salesChart").getContext("2d");
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
});*/

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

// Load dashboard-data.json using XHR
var xhr = new XMLHttpRequest();
xhr.open("GET", "data-dashoard.json", true);
xhr.onreadystatechange = function () {
  if (xhr.readyState === 4 && xhr.status === 200) {
    var data = JSON.parse(xhr.responseText);

    // Update card values
    document.getElementById("menuCount").textContent = data.menuCount;
    document.getElementById("totalCustomer").textContent = data.totalCustomer;
    document.getElementById("orderCount").textContent = data.orderCount;
    document.getElementById("totalRevenue").textContent =
      "₦" + data.totalRevenue.toLocaleString();

    // Update employee table
    var tbody = document.getElementById("employeeTableBody");
    tbody.innerHTML = ""; // clear old
    data.employees.forEach(function (emp) {
      var row = `<tr>
        <td>${emp.staffNo}</td>
        <td>${emp.firstName}</td>
        <td>${emp.lastName}</td>
        <td>${emp.designation}</td>
        <td style="color: ${emp.status === "Active" ? "green" : "red"};">
      ${emp.status}
    </td>
      </tr>`;
      tbody.innerHTML += row;
    });

    // Create sales chart
    var ctx = document.getElementById("salesChart").getContext("2d");
    new Chart(ctx, {
      type: "bar",
      data: {
        labels: data.dailySales.map((item) => item.time),
        datasets: [
          {
            label: "Hourly Sales",
            data: data.dailySales.map((item) => item.sale),
            backgroundColor: ["blue", "orange", "red", "teal", "black"],
          },
        ],
      },
      options: {
        responsive: true,
        scales: {
          y: { beginAtZero: true },
        },
      },
    });
  }
};
xhr.send();
