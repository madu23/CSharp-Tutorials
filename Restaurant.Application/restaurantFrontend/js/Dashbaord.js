const sidebar = document.querySelector(".sidebar");
const mainContent = document.querySelector(".main-content");
const toggleBtn = document.querySelector(".toggle-sidebar");

toggleBtn.addEventListener("click", function () {
sidebar.classList.toggle("active");
mainContent.classList.toggle("expanded");
})

const ctx = document.getElementById('salesChart').getContext('2d');

const salesChart = new Chart(ctx, {
    type: 'bar', // Change to 'line', 'pie', etc. if needed
    data: {
        labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'],
        datasets: [{
            label: 'Daily Sales',
            data: [120, 190, 300, 250, 220], // Replace with dynamic data if needed
            backgroundColor: ['blue', 'orange', 'red', 'teal', 'navy'],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

document.addEventListener("DOMContentLoaded", function() {
    const username = localStorage.getItem("loggedInUser") || "Admin"; 
    const userInfoSpan = document.querySelector(".user-info span"); 
    if (userInfoSpan) {
        userInfoSpan.textContent = `Hello, ${username}`;
    }
});
