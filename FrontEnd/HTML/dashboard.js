document.addEventListener("DOMContentLoaded", function () {
  // Get username from localStorage (login page) and update it in the greeting
  const userName = localStorage.getItem("loggedUser") || "Admin";

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

      document
        .querySelectorAll(".nav-link")
        .forEach((nav) => nav.classList.remove("active"));

      // Hide the charts
      document.getElementById("daily-sales").classList.add("d-none");
      document.getElementById("weekly-sales").classList.add("d-none");
      document.getElementById("monthly-sales").classList.add("d-none");

      // Make the chart selected active and show the corresponding chart
      this.classList.add("active");
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
      // make all the button default
      sidebarButtons.forEach((btn) => {
        btn.classList.remove("btn-primary");
        btn.classList.add("btn-light");
      });

      // Make the clicked button primary
      button.classList.remove("btn-light");
      button.classList.add("btn-primary");
    });
  });
});

document.addEventListener("DOMContentLoaded", function () {
  fetch("./dashbord-data.json")
    .then((response) => response.json())
    .then((data) => {
      // 1. Table
      const tableBody = this.querySelector("#employee-table-body");
      tableBody.innerHTML = ""; // this is to clear old content
      for (let employee of data.employees) {
        tableBody.innerHTML += `
        <tr>
        <th scope="row">${employee.id}</th>
        <td>${employee.firstName}</td>
        <td>${employee.lastName}</td>
        <td>${employee.designation}</td>
        </tr>
        `;
      }

      // 2. Card
      document.getElementById("orders-value").innerText =
        data.totalOrders.value;
      document.getElementById(
        "orders-change"
      ).innerHTML = `<i class="fas fa-arrow-up"></i> ${data.totalOrders.change}`;

      document.getElementById("delivered-value").innerText =
        data.totalDelivered.value;
      document.getElementById(
        "delivered-change"
      ).innerHTML = `<i class="fas fa-arrow-up"></i> ${data.totalDelivered.change}`;

      document.getElementById("revenue-value").innerText =
        data.totalRevenue.value;
      document.getElementById(
        "revenue-change"
      ).innerHTML = `<i class="fas fa-arrow-down"></i> ${data.totalRevenue.change}`;

      document.getElementById("canceled-value").innerText =
        data.totalCanceled.value;
      document.getElementById(
        "canceled-change"
      ).innerHTML = `<i class="fas fa-arrow-up"></i> ${data.totalCanceled.change}`;

      //3. Charts
      
    });
});
