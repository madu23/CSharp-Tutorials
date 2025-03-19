document.addEventListener("DOMContentLoaded", function () {
  /*** 1️⃣ Sidebar Active State ***/
  const menuItems = document.querySelectorAll(".menu");

  menuItems.forEach((menu) => {
    menu.addEventListener("click", function () {
      // Remove active class from all
      menuItems.forEach((item) => item.classList.remove("active"));
      // Add active class to clicked menu
      this.classList.add("active");
    });
  });

  /*** 2️⃣ Toggle Functionality for Graph ***/
  const toggleButtons = document.querySelectorAll(".toggle-btn");

  toggleButtons.forEach((button) => {
    button.addEventListener("click", function () {
      // Remove active class from all buttons
      toggleButtons.forEach((btn) => btn.classList.remove("active"));

      // Set clicked button as active
      this.classList.add("active");

      // You can update graph data here based on selection
      console.log(`Selected: ${this.textContent}`);
    });
  });
});
