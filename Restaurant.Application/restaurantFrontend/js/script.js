
    alert('You clicked login')
    document.getElementById("loginBtn").addEventListener("click", function() {
        const username = document.getElementById("username").value;
        const password = document.getElementById("password").value;
        
        if (username.trim() === "aikay" && password.trim() === "aikay") {
            window.location.assign("dashboard.html");
        } else {
            alert("Invalid Credentials");
        }
    });
   



