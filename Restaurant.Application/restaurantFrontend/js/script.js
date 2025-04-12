
alert('You clicked login')
let loginCount = 0;
const maxCount = 3;

document.getElementById("loginBtn").addEventListener("click", function() {


    if(loginCount >= 3) {
        showAlert("Too many failed attempts. Login disabled.", "danger");
        document.getElementById("loginBtn").disabled = true;
        return;
    }
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    
    if (username.trim() === "aikay" && password.trim() === "aikay") {
        showAlert("Login successful! Redirecting...", "success");
        setTimeout(() => {
            window.location.assign("dashboard.html");
        }, 5000);
        
    } else {
        loginCount++;
        const attemptsLeft = maxCount - loginCount;
        showAlert(`Invalid Credentials. Attempts left: ${attemptsLeft}`, "danger");
        
    }
        

        if(loginCount >= 3) {
            document.getElementById("loginBtn").disabled = true;
        }
        if (username.trim() === "aikay" && password.trim() === "aikay") {
            localStorage.setItem("loggedInUser", username.trim()); 
            window.location.assign("dashboard.html");
        }
        
});
    
   
    const loginBtn = document.getElementById("loginBtn");

    function updateButtonColor()
    {
        
        const currentHour = new Date().getHours(); 
    
        if ( currentHour < 12)
        {
            loginBtn.style.backgroundColor = "Blue";   
            loginBtn.style.color = "white"; 

        }    
        else if ( currentHour < 18) 
                {
                    loginBtn.style.backgroundColor = "White"; // White
                    loginBtn.style.color = "Black"; 
                } 
                
                else
                 {
                    loginBtn.style.backgroundColor = "Red"; // Red
                    loginBtn.style.color = "White"; 
                }
        
        
       
    }
    
   updateButtonColor(); 

   function showAlert(message, type) {
    let alertBox = document.getElementById("loginAlert");
    if (!alertBox) {
        alertBox = document.createElement("div");
        alertBox.id = "loginAlert";
        alertBox.className = "alert alert-" + type;
        alertBox.setAttribute("role", "alert");
        document.getElementById("loginBtn").insertAdjacentElement("afterend", alertBox);
    }
    alertBox.className = `alert alert-${type}`;
    alertBox.textContent = message;
}

    


