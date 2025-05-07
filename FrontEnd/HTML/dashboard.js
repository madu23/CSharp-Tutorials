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

document.addEventListener("DOMContentLoaded", function () {
  const stats = {
    totalOrders: {
      value: "1,121",
      change: "+8.75%",
    },
    totalDelivered: {
      value: "908",
      change: "+9.6%",
    },
    totalRevenue: {
      value: "2,020,500",
      change: "-1.75%",
    },
    totalCanceled: {
      value: "2",
      change: "+0.3%",
    },
  };

  // Update values
  document.getElementById("orders-value").innerText = stats.totalOrders.value;
  document.getElementById(
    "orders-change"
  ).innerHTML = `<i class="fas fa-arrow-up"></i> ${stats.totalOrders.change}`;

  document.getElementById("delivered-value").innerText =
    stats.totalDelivered.value;
  document.getElementById(
    "delivered-change"
  ).innerHTML = `<i class="fas fa-arrow-up"></i> ${stats.totalDelivered.change}`;

  document.getElementById("revenue-value").innerText = stats.totalRevenue.value;
  document.getElementById(
    "revenue-change"
  ).innerHTML = `<i class="fas fa-arrow-down"></i> ${stats.totalRevenue.change}`;

  document.getElementById("canceled-value").innerText =
    stats.totalCanceled.value;
  document.getElementById(
    "canceled-change"
  ).innerHTML = `<i class="fas fa-arrow-up"></i> ${stats.totalCanceled.change}`;
});

document.addEventListener("DOMContentLoaded", function () {
  const employees = [
    {
      id: 1,
      firstName: "Oluwafunmilayo",
      lastName: "Lemboye",
      designation: "Admin",
    },
    { id: 2, firstName: "Mark", lastName: "Otto", designation: "HR" },
    { id: 3, firstName: "Jacob", lastName: "Thornton", designation: "Manager" },
    { id: 4, firstName: "John", lastName: "Doe", designation: "Head Chef" },
    {
      id: 5,
      firstName: "Oluwafunmilayo",
      lastName: "Lemboye",
      designation: "Secetary",
    },
    {
      id: 6,
      firstName: "Mark",
      lastName: "Otto",
      designation: "Ass. Head Chef",
    },
    {
      id: 7,
      firstName: "Jacob",
      lastName: "Thornton",
      designation: "Ass. Manager",
    },
    { id: 8, firstName: "John", lastName: "Doe", designation: "Waiter" },
  ];

  const tableBody = document.getElementById("employee-table-body");
  // Clear the old content
  tableBody.innerHTML = "";
  employees.forEach((emp) => {
    const row = document.createElement("tr");
    row.innerHTML = `
      <th scope="row">${emp.id}</th>
      <td>${emp.firstName}</td>
      <td>${emp.lastName}</td>
      <td>${emp.designation}</td>
    `;
    tableBody.appendChild(row);
  });
});
