document.addEventListener("DOMContentLoaded", function () {
    // Display the logged-in user's name
    const usernameDisplay = document.getElementById("usernaemDisplay");
    const loggedInUser = localStorage.getItem("username");
    if (loggedInUser && usernameDisplay) {
        usernameDisplay.textContent = `Welcome, ${loggedInUser}`;
    }

    // Handle logout button click
    const logoutButton = document.getElementById("logoutButton");
    if (logoutButton) {
        logoutButton.addEventListener("click", function () {
            // Clear the logged-in user and redirect to login page
            localStorage.removeItem("username");
            window.location.href = "index.html"; // Redirect to login page
        });
    }

    // Create the sales chart
    var ctx = document.getElementById('salesChart').getContext('2d');
    var salesChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Label 1', 'Label 2', 'Label 3', 'Label 4', 'Label 5', 'Label 6'],
            datasets: [{
                data: [4, 15, 19, 3, 5, 9],
                backgroundColor: ['black', 'orange', 'red', 'green', 'blue', 'brown']
            }]
        },
        options: {
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    display: false
                }
            }
        }
    });
});