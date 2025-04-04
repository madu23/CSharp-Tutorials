
    alert('You clicked login')
    document.getElementById("loginBtn").addEventListener("click", function() {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;
        
        if (username.trim() === "aikay" && password.trim() === "aikay") {
            window.location.assign("dashboard.html");
        } else {
            alert("Invalid Credentials");
        }


        const loginBtn = document.getElementById("loginBtn");
        
        function updateButtonColor()
        {
            
            const currentHour = new Date().getHours(); // Get the current hour (0-23)
        
            if (currentHour >= 8 && currentHour < 12)
            {
                loginBtn.style.backgroundColor = "Blue"; // Blue
                loginBtn.style.color = "white"; // White text
            }
            
            else if (currentHour >= 12 && currentHour < 18) 
            {
                loginBtn.style.backgroundColor = "White"; // White
                loginBtn.style.color = "Black"; // Black text for contrast
            } 
            
            else
             {
                loginBtn.style.backgroundColor = "Red"; // Red
                loginBtn.style.color = "White"; // White text
            }
        }
        
        // Run the function on page load
        document.addEventListener("DOMContentLoaded", updateButtonColor);
        
        // Optional: Update the button color every minute in case the user keeps the page open
        setInterval(updateButtonColor, 60000);
        
    });
   
    


