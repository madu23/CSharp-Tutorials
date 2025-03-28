document.addEventListener("DOMContentLoaded", function () {
  /*** 1️⃣ Sidebar Active State ***/
  const menuItems = document.querySelectorAll("nav button");

  menuItems.forEach((menu) => {
      menu.addEventListener("click", function () {
          // Remove active class from all buttons
          menuItems.forEach((item) => item.classList.remove("active"));
          // Add active class to the clicked button
          this.classList.add("active");
      });
  });

  /*** 2️⃣ Navbar Toggle (for Mobile) ***/
  const navToggle = document.getElementById("navToggle");
  const navbar = document.querySelector(".navbar-collapse");

  if (navToggle) {
      navToggle.addEventListener("click", function () {
          navbar.classList.toggle("show");
      });
  }

  /*** 3️⃣ Sidebar Toggle (for Small Screens) ***/
  const sidebarToggle = document.getElementById("sidebarToggle");
  const sidebar = document.querySelector("nav");

  if (sidebarToggle) {
      sidebarToggle.addEventListener("click", function () {
          sidebar.classList.toggle("d-none");
      });
  }
});

/*** 4️⃣ Graph Switching Function ***/
function changeGraph(pageId) {
  document.getElementById("graphFrame").src =
      "https://lookerstudio.google.com/embed/reporting/21ae1fd8-bf1f-421b-a0ed-a9b94d3b3918/page/" + pageId;

  // Remove "active" class from all graph buttons
  let buttons = document.querySelectorAll(".btn-group .btn");
  buttons.forEach((btn) => btn.classList.remove("active"));

  // Add "active" class to the clicked button
  event.target.classList.add("active");
}
