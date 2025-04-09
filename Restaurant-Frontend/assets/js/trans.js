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

function updateChart(view) {
  document
    .querySelectorAll(".toggle-buttons span")
    .forEach((el) => el.classList.remove("active"));
  document
    .querySelector(`.toggle-buttons span[onclick="updateChart('${view}')"]`)
    .classList.add("active");
  customerMapChart.data.datasets[0].data = Array.from(
    {length: 27},
    () => Math.floor(Math.random() * 160) - 80
  );
  customerMapChart.update();
}

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
        tooltip: {
          enabled: true,
        },
        legend: {
          display: false,
        },
        centerTextPlugin: {
          text: `${value}%`,
          fontColor: "black",
        },
      },
    },
  });
}

const successfulOrderCanvas = document.getElementById("successfulOrderChart");
successfulOrderCanvas.width = 321;
successfulOrderCanvas.height = 321;

const unsuccessfulOrderCanvas = document.getElementById(
  "unsuccessfulOrderChart"
);
unsuccessfulOrderCanvas.width = 321;
unsuccessfulOrderCanvas.height = 321;

createDonutChart("successfulOrderChart", 86, "#4caf50");
createDonutChart("unsuccessfulOrderChart", 14, "#e91e63");

const ctx = document.getElementById("averageOrderChart").getContext("2d");

const dataValues = [25, 40, 55, 70, 60, 85, 90, 75, 50, 65];

new Chart(ctx, {
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
      legend: {
        display: false,
      },
    },
    scales: {
      y: {
        beginAtZero: false,
        min: 20,
        ticks: {
          stepSize: 20,
        },
        grid: {
          display: true,
        },
      },
      x: {
        grid: {
          display: false,
        },
      },
    },
    animation: {
      duration: 800,
      easing: "easeOutCubic",
    },
  },
});
