document.addEventListener("DOMContentLoaded", function () {
    // Get username from localStorage
    const userName = localStorage.getItem("loggedUser") || "Admin";
  
    // Update the greeting
    const greetingElement = document.getElementById("greeting");
    if (greetingElement) {
      greetingElement.innerText = `Hello ${userName}`;
    }
  });

document.addEventListener("DOMContentLoaded", function () {
  /*** Sidebar Active State ***/
  const menuItems = document.querySelectorAll("nav button");

  menuItems.forEach((menu) => {
      menu.addEventListener("click", function () {
          // Remove active class from all buttons
          menuItems.forEach((item) => item.classList.remove("active"));
          // Add active class to the clicked button
          this.classList.add("active");
      });
  });

  /*** Navbar Toggle (for Mobile) ***/
  const navToggle = document.getElementById("navToggle");
  const navbar = document.querySelector(".navbar-collapse");

  if (navToggle) {
      navToggle.addEventListener("click", function () {
          navbar.classList.toggle("show");
      });
  }

  /*** Sidebar Toggle (for Small Screens) ***/
  const sidebarToggle = document.getElementById("sidebarToggle");
  const sidebar = document.querySelector("nav");

  if (sidebarToggle) {
      sidebarToggle.addEventListener("click", function () {
          sidebar.classList.toggle("d-none");
      });
  }
});

/*** Graph Switching Function ***/
const ctx = document.getElementById("salesChart").getContext("2d");

let salesChart = new Chart(ctx, {
  type: 'bar',
  data: {
    labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
    datasets: [{
      label: 'Sales',
      data: [12, 19, 3, 5, 2, 3, 7],
      backgroundColor: '#212529',
      borderRadius: 5
    }]
  },
  options: {
    responsive: true,
    plugins: {
      legend: {
        display: true
      },
    }
  }
});
function changeGraph(type) {
    let newData;
  
    if (type === 'daily') {
      newData = [12, 19, 3, 5, 2, 3, 7];
    } else if (type === 'weekly') {
      newData = [70, 55, 40, 90, 66, 45, 75];
    } else {
      newData = [300, 280, 260, 400, 500, 320, 380];
    }
  
    salesChart.data.datasets[0].data = newData;
    salesChart.update();
  
    // Remove "active" class from all graph buttons
    let buttons = document.querySelectorAll(".btn-group .btn");
    buttons.forEach((btn) => btn.classList.remove("active"));
  
    // Add "active" class to the clicked button
    event.target.classList.add("active");
  }
