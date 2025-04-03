document.addEventListener("DOMContentLoaded", function () {
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
