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

// Sidebar
document.addEventListener("DOMContentLoaded", function () {
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

let chartInstance = null;
let chartData = null;

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

      // 3. Charts
      chartData = data; // to store it globally and use it below
      showChart(data, "daily");
    });
});

// CHART
document.addEventListener("DOMContentLoaded", function () {
  document.querySelectorAll(".nav-link").forEach((link) => {
    link.addEventListener("click", function (event) {
      event.preventDefault();
      document
        .querySelectorAll(".nav-link")
        .forEach((nav) => nav.classList.remove("active"));
      this.classList.add("active");
      const selectedTab = this.getAttribute("data-tab");
      showChart(chartData, selectedTab);
    });
  });

  function getChartStyle(viewType) {
    const chartStyleMap = {
      daily: "bar",
      weekly: "pie",
      monthly: "bar",
      yearly: "line",
    };
    return chartStyleMap[viewType] || "bar"; // The default will be bar
  }

  window.showChart = function (data, type) {
    if (!chartData) return;

    const salesChartContainer = document.querySelector("#chart-container");
    salesChartContainer.innerHTML = "";
    const canvas = document.createElement("canvas");
    canvas.id = "salesChart";
    salesChartContainer.appendChild(canvas);

    const ctx = canvas.getContext("2d");

    let labels = [];
    let sales = [];

    if (type === "daily") {
      labels = data.dailySales.map((row) => row.time);
      sales = data.dailySales.map((row) => row.sale);
    } else if (type === "weekly") {
      labels = data.weeklySales.map((row) => row.day);
      sales = data.weeklySales.map((row) => row.sale);
    } else if (type === "monthly") {
      labels = data.monthlySales.map((row) => row.week);
      sales = data.monthlySales.map((row) => row.sale);
    } else if (type === "yearly") {
      labels = data.yearlySales.map((row) => row.month);
      sales = data.yearlySales.map((row) => row.sale);
    }

    if (chartInstance) {
      chartInstance.destroy();
    }

    chartInstance = new Chart(ctx, {
      type: getChartStyle(type),
      data: {
        labels,
        datasets: [
          {
            label: `${type.charAt(0).toUpperCase() + type.slice(1)} Sales`,
            data: sales,
            backgroundColor: [
              "#B5C0D0",
              "#C3B1E1",
              "#D6BFA7",
              "#E3D5CA",
              "#B0C4B1",
              "#FFE6E6",
              "#F7E9D7",
              "#D2E0FB",
              "#C9E4CA",
              "#F5EEE6",
              "#D3CEDF",
              "#EADBC8",
            ],
            borderWidth: 1,
          },
        ],
      },
      options: {
        responsive: true,
        scales: {
          y: {
            beginAtZero: true,
            title: { display: true, text: "Sales Count" },
          },
          x: {
            title: {
              display: true,
              text:
                type === "daily"
                  ? "Hour of Day"
                  : type === "weekly"
                  ? "Day of Week"
                  : type === "monthly"
                  ? "Week"
                  : "Month",
            },
          },
        },
        // maintainAspectRatio: false
      },
    });
  };
});
