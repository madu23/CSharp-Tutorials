document.addEventListener("DOMContentLoaded", () => {
  // ===== Preloader fade out =====
  window.addEventListener("load", () => {
    const preloader = document.getElementById("preloader");
    if (preloader) {
      preloader.style.opacity = "0";
      setTimeout(() => {
        preloader.style.display = "none";
      }, 1500);
    }
  });

  // ===== Login Form Logic =====
  const loginForm = document.getElementById("loginForm");
  let loginAttempts = 0;
  if (loginForm) {
    loginForm.addEventListener("submit", (event) => {
      event.preventDefault();
      const username = document.getElementById("username")?.value;
      const password = document.getElementById("password")?.value;
      const notification = document.getElementById("notification");
      const loginButton = document.querySelector(".btn-primary.w-100");

      if (username === "Devine" && password === "superPassword") {
        if (notification) {
          notification.classList.remove("alert-danger");
          notification.classList.add("alert-success");
          notification.textContent =
            "Login Successful! Redirecting to dashboard...";
          notification.classList.remove("d-none");
        }
        localStorage.setItem("loggedInUser", username);
        setTimeout(() => {
          window.location.href = "dashboard.html";
        }, 2000);
      } else {
        loginAttempts++;
        if (notification) {
          notification.classList.remove("alert-success");
          notification.classList.add("alert-danger");
          if (loginAttempts >= 3) {
            notification.textContent =
              "Maximum retry exceeded. Login disabled. Reload the page to try again.";
          } else {
            const remainingAttempts = 3 - loginAttempts;
            notification.textContent = `Invalid username or password. Please try again. You have ${remainingAttempts} more attempt${
              remainingAttempts > 1 ? "s" : ""
            }.`;
          }
          notification.classList.remove("d-none");
        }
        if (loginAttempts >= 3 && loginButton) {
          loginButton.disabled = true;
        }
      }
    });
  }

  // ===== Login Button Color Based on Time =====
  const loginButton = document.querySelector(".btn-primary.w-100");
  if (loginButton) {
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
  }

  // ===== Display Username and Logout Logic on Dashboard =====
  const usernameDisplay = document.getElementById("usernameDisplay");
  const loggedInUser = localStorage.getItem("loggedInUser");
  const logoutButton = document.getElementById("logoutButton");

  if (usernameDisplay && loggedInUser) {
    usernameDisplay.textContent = `Hello, ${loggedInUser}`;
  }

  if (logoutButton) {
    logoutButton.addEventListener("click", (event) => {
      event.preventDefault();
      localStorage.removeItem("loggedInUser");
      window.location.href = "index.html";
    });
  }

  // ===== Dashboard Data Load and binding with AJAX =====

  fetch("assets/js/dashboard-data.json")
    .then((res) => {
      if (!res.ok) throw new Error("Network response was not ok");
      return res.json();
    })
    .then((data) => {
      // Cards
      const totalMenusElem = document.getElementById("totalMenusValue");
      const totalRevenueElem = document.getElementById("totalRevenueValue");
      const totalOrdersElem = document.getElementById("totalOrdersValue");
      const totalCustomersElem = document.getElementById("totalCustomersValue");
      if (
        totalMenusElem &&
        totalRevenueElem &&
        totalOrdersElem &&
        totalCustomersElem
      ) {
        totalMenusElem.textContent = data.menuCount;
        totalRevenueElem.textContent = "₦" + data.totalRevenue.toLocaleString();
        totalOrdersElem.textContent = data.orderCount;
        totalCustomersElem.textContent = data.totalCustomer;
      }

      // Employees
      const employeeTableBody = document.getElementById("employeeTableBody");
      if (employeeTableBody && Array.isArray(data.employees)) {
        employeeTableBody.innerHTML = data.employees
          .map(
            (emp) => `
            <tr>
              <td>${emp.staffNo.toString().padStart(2, "0")}</td>
              <td>${emp.firstName}</td>
              <td>${emp.lastName}</td>
              <td>${emp.designation}</td>
              <td class="${
                emp.status === "Active" ? "text-success" : "text-danger"
              }">${emp.status}</td>
            </tr>
          `
          )
          .join("");
      }

      // Sales Chart
      let salesChart;
      function createSalesChart(labels, values, label) {
        const ctx = document.getElementById("salesChart").getContext("2d");
        if (salesChart) salesChart.destroy();
        salesChart = new Chart(ctx, {
          type: "bar",
          data: {
            labels: labels,
            datasets: [
              {
                label: label,
                data: values,
                backgroundColor: function (context) {
                  const chart = context.chart;
                  const {ctx, chartArea} = chart;
                  if (!chartArea) return null;
                  const gradient = ctx.createLinearGradient(
                    0,
                    chartArea.top,
                    0,
                    chartArea.bottom
                  );
                  gradient.addColorStop(0, "#f3d423");
                  gradient.addColorStop(1, "#f19720");
                  return gradient;
                },
                borderColor: "rgba(241, 151, 32, 1)",
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
              title: {display: false},
            },
          },
        });
      }

      if (Array.isArray(data.dailySales)) {
        const labels = data.dailySales.map((d) => d.time);
        const values = data.dailySales.map((d) => d.sale);
        createSalesChart(labels, values, "Hourly Sales");
      }

      window.handleToggle = function (el, type) {
        document
          .querySelectorAll(".toggle-buttons span")
          .forEach((btn) => btn.classList.remove("active"));

        // Active class for all clicked buttons
        el.classList.add("active");
        let labels = [];
        let values = [];
        let labelText = "";
        if (type === "daily") {
          labels = data.dailySales.map((d) => d.time);
          values = data.dailySales.map((d) => d.sale);
          labelText = "Hourly Sales";
        } else if (type === "weekly") {
          labels = data.weeklySales.map((d) => d.day);
          values = data.weeklySales.map((d) => d.sale);
          labelText = "Daily Sales (Past 7 Days)";
        } else if (type === "monthly") {
          labels = data.monthlySales.map((d) => d.date);
          values = data.monthlySales.map((d) => d.sale);
          labelText = "Daily Sales (This Month)";
        }
        createSalesChart(labels, values, labelText);
      };
    })
    .catch((err) => {
      console.error("Failed to load dashboard data:", err);
    });

  // ===== Sidebar Toggle =====
  const toggleSidebarButton = document.querySelector(".toggle-sidebar");
  const sidebar = document.querySelector(".sidebar");

  if (!toggleSidebarButton) {
    console.error("Toggle sidebar button not found");
  }
  if (!sidebar) {
    console.error("Sidebar element not found");
  }

  if (toggleSidebarButton && sidebar) {
    toggleSidebarButton.addEventListener("click", () => {
      sidebar.classList.toggle("active");
      const mainContent = document.querySelector(".main-content");
      const header = document.querySelector(".dashboard-header");
      mainContent?.classList.toggle("expanded");
      header?.classList.toggle("expanded");
    });
  }

  // ===== Chart.js Center Text Plugin =====
  const centerTextPlugin = {
    id: "centerTextPlugin",
    beforeDraw(chart, args, options) {
      const {ctx, width, height} = chart;
      ctx.save();
      const fontSize = (height / 200).toFixed(2);
      ctx.font = `${fontSize}em Poppins`;
      ctx.textBaseline = "middle";
      ctx.fillStyle = options.fontColor || "black";
      const text = options.text || "";
      const textX = Math.round((width - ctx.measureText(text).width) / 2);
      const textY = height / 2;
      ctx.fillText(text, textX, textY);
      ctx.restore();
    },
  };
  Chart.register(centerTextPlugin);

  // ===== Customer Map Bar Chart =====
  const ctxCustomer = document
    .getElementById("customerMapChart")
    ?.getContext("2d");

  let customerMapChart;

  if (ctxCustomer) {
    customerMapChart = new Chart(ctxCustomer, {
      type: "bar",
      data: {
        labels: Array.from({length: 27}, (_, i) => ("0" + (i + 1)).slice(-2)),
        datasets: [
          {
            data: Array.from(
              {length: 27},
              () => Math.floor(Math.random() * 160) - 80
            ),
            backgroundColor: function (context) {
              const chart = context.chart;
              const {ctx, chartArea} = chart;
              if (!chartArea) return null;
              const gradient = ctx.createLinearGradient(
                0,
                chartArea.top,
                0,
                chartArea.bottom
              );
              gradient.addColorStop(0, "#f3d423");
              gradient.addColorStop(1, "#f19720");
              return gradient;
            },
            borderWidth: 0,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: {display: false},
        },
        scales: {
          y: {
            beginAtZero: false,
            grid: {display: true},
          },
          x: {
            grid: {display: false},
          },
        },
        animation: {
          duration: 800,
          easing: "easeOutQuad",
        },
      },
    });
  }

  // ===== Update Customer Map Chart Function =====
  window.updateChart = function (view) {
    document
      .querySelectorAll(".toggle-buttons span")
      .forEach((el) => el.classList.remove("active"));
    const activeBtn = document.querySelector(
      `.toggle-buttons span[onclick="updateChart('${view}')"]`
    );
    if (activeBtn) activeBtn.classList.add("active");

    if (customerMapChart) {
      customerMapChart.data.datasets[0].data = Array.from(
        {length: 27},
        () => Math.floor(Math.random() * 160) - 80
      );
      customerMapChart.update();
    }
  };

  // ===== Donut Chart Factory =====
  function createDonutChart(elementId, value, color) {
    const ctx = document.getElementById(elementId);
    if (!ctx) return;
    new Chart(ctx, {
      type: "doughnut",
      data: {
        datasets: [
          {
            data: [value, 100 - value],
            backgroundColor: [color, "#e0e0e0"],
          },
        ],
      },
      options: {
        cutout: "70%",
        radius: "60%",
        responsive: true,
        animation: {
          duration: 800,
          easing: "easeOutBounce",
        },
        hover: {
          mode: "nearest",
          animationDuration: 400,
        },
        interaction: {
          mode: "nearest",
          intersect: true,
        },
        elements: {
          arc: {
            hoverOffset: 10,
          },
        },
        plugins: {
          tooltip: {enabled: true},
          legend: {display: false},
          centerTextPlugin: {
            text: `${value}%`,
            fontColor: "black",
          },
        },
      },
    });
  }

  // ===== Create Donut Charts =====
  const successfulOrderCanvas = document.getElementById("successfulOrderChart");
  if (successfulOrderCanvas) {
    successfulOrderCanvas.width = 321;
    successfulOrderCanvas.height = 321;
    createDonutChart("successfulOrderChart", 86, "#4caf50");
  }

  const unsuccessfulOrderCanvas = document.getElementById(
    "unsuccessfulOrderChart"
  );
  if (unsuccessfulOrderCanvas) {
    unsuccessfulOrderCanvas.width = 321;
    unsuccessfulOrderCanvas.height = 321;
    createDonutChart("unsuccessfulOrderChart", 14, "#e91e63");
  }

  // ===== Average Order Bar Chart =====
  const averageOrderCtx = document
    .getElementById("averageOrderChart")
    ?.getContext("2d");
  if (averageOrderCtx) {
    const dataValues = [25, 40, 55, 70, 60, 85, 90, 75, 50, 65];
    new Chart(averageOrderCtx, {
      type: "bar",
      data: {
        labels: Array.from({length: dataValues.length}, (_, i) => i + 1),
        datasets: [
          {
            data: dataValues,
            backgroundColor: function (context) {
              const chart = context.chart;
              const {ctx, chartArea} = chart;
              if (!chartArea) return null;
              const gradient = ctx.createLinearGradient(
                0,
                chartArea.top,
                0,
                chartArea.bottom
              );
              gradient.addColorStop(0, "#f19720");
              gradient.addColorStop(1, "#f3d423");
              return gradient;
            },
            borderWidth: 0,
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: {display: false},
        },
        scales: {
          y: {
            beginAtZero: false,
            min: 20,
            ticks: {
              stepSize: 20,
            },
            grid: {display: true},
          },
          x: {grid: {display: false}},
        },
        animation: {
          duration: 800,
          easing: "easeOutCubic",
        },
      },
    });
  }
});
