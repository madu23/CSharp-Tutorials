// Customer Map Chart initialization
const ctxCustomer = document
  .getElementById("customerMapChart")
  .getContext("2d");
let customerMapChart = new Chart(ctxCustomer, {
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
          gradient.addColorStop(0, "#e91e63");
          gradient.addColorStop(1, "#ffccbc");
          return gradient;
        },
        borderWidth: 0,
      },
    ],
  },
  options: {
    responsive: true,
    plugins: {
      legend: {display: false}, // Hide the legend
    },
    scales: {
      y: {
        beginAtZero: false,
        grid: {display: true}, // Disable vertical grid lines
      },
      x: {
        grid: {display: false}, // Disable horizontal grid lines
      },
    },
    animation: {
      duration: 800,
      easing: "easeOutQuad",
    },
  },
});

function updateChart(view) {
  // Smooth toggle animation by updating active classes
  document
    .querySelectorAll(".toggle-buttons span")
    .forEach((el) => el.classList.remove("active"));
  document
    .querySelector(`.toggle-buttons span[onclick="updateChart('${view}')"]`)
    .classList.add("active");

  // Update chart data based on view (dummy data used here)
  customerMapChart.data.datasets[0].data = Array.from(
    {length: 27},
    () => Math.floor(Math.random() * 160) - 80
  );
  customerMapChart.update();
}

// Donut Charts for Transaction Summary
function createDonutChart(elementId, value, color) {
  new Chart(document.getElementById(elementId), {
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
      responsive: true,
      animation: {
        duration: 800,
        easing: "easeOutBounce",
      },
    },
  });
}

createDonutChart("successfulOrderChart", 86, "#4caf50");
createDonutChart("unsuccessfulOrderChart", 14, "#e91e63");

// Average Order Chart initialization
new Chart(document.getElementById("averageOrderChart"), {
  type: "bar",
  data: {
    labels: Array.from({length: 10}, (_, i) => i + 1),
    datasets: [
      {
        data: Array.from({length: 10}, () => Math.floor(Math.random() * 100)),
        backgroundColor: "#e91e63",
      },
    ],
  },
  options: {
    responsive: true,
    scales: {y: {beginAtZero: true}},
    animation: {
      duration: 800,
      easing: "easeOutCubic",
    },
  },
});
