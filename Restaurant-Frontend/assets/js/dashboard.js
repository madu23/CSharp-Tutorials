document.addEventListener("DOMContentLoaded", function () {
  // Add loading class to body
  document.body.classList.add("loading");

  // Delay page load by 3 seconds
  setTimeout(() => {
    const preloader = document.getElementById("preloader");
    preloader.style.display = "none";
    document.body.classList.remove("loading");
  }, 3000);

  // Chart.js configuration
  const ctx = document.getElementById("salesChart").getContext("2d");

  new Chart(ctx, {
    type: "bar",
    data: {
      labels: [
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday",
      ],
      datasets: [
        {
          label: "Daily Sales (₦)",
          data: [15000, 12000, 18000, 20000, 17000, 25000, 22000],
          backgroundColor: [
            "rgba(255, 0, 0, 0.6)", // red
            "rgba(0, 0, 255, 0.6)", // blue
            "rgba(255, 255, 0, 0.6)", // yellow
            "rgba(0, 128, 0, 0.6)", // green
            "rgba(75, 0, 130, 0.6)", // indigo
            "rgba(255, 215, 0, 0.6)", // gold
            "rgba(165, 42, 42, 0.6)", // brown
          ],
          borderColor: [
            "rgba(255, 0, 0, 0.6)", // red
            "rgba(0, 0, 255, 08)", // blue
            "rgba(255, 255, 0, 0.6)", // yellow
            "rgba(0, 128, 0, 0.6)", // green
            "rgba(75, 0, 130, 0.6)", // indigo
            "rgba(255, 215, 0, 0.6)", // gold
            "rgba(165, 42, 42, 0.6)", // brown
          ],
          borderWidth: 1,
        },
      ],
    },
    options: {
      responsive: true,
      scales: {y: {beginAtZero: true}},
      plugins: {
        legend: {display: false},
        title: {
          display: false,
          text: "Weekly Sales Overview",
          font: {size: 20},
        },
      },
    },
  });

  class SidebarManager {
    constructor() {
      this.sidebar = document.querySelector(".sidebar");
      this.mainContent = document.querySelector(".main-content");
      this.toggleBtn = document.querySelector(".toggle-sidebar");
      this.navLinks = document.querySelectorAll(".nav-link, .dropdown-item");
      this.dropdownToggles = document.querySelectorAll(
        '[data-bs-toggle="collapse"]'
      );
      this.userInfo = document.getElementById("userInfo");
      this.userDropdown = document.getElementById("userDropdown");
      this.logoutLink = document.querySelector(
        ".dropdown-item[href='#logout']"
      );

      this.init();
      this.openSidebarOnLoad(); // Ensure sidebar is opened on load
    }

    init() {
      this.toggleBtn.addEventListener("click", () => this.toggleSidebar());
      this.navLinks.forEach((link) =>
        link.addEventListener("click", (event) =>
          this.handleNavLinkClick(event, link)
        )
      );
      this.dropdownToggles.forEach((toggle) =>
        toggle.addEventListener("click", (event) =>
          this.handleDropdownToggle(event, toggle)
        )
      );
      this.logoutLink.addEventListener("click", () => this.handleLogout());
      document.addEventListener("click", (event) =>
        this.handleDocumentClick(event)
      );
      this.userInfo.addEventListener("click", (event) =>
        this.toggleUserDropdown(event)
      ); // Add event listener for user info
      this.userInfo
        .querySelector("i")
        .addEventListener("click", (event) => this.toggleUserDropdown(event)); // Add event listener for user icon
    }

    toggleSidebar() {
      const isActive = this.sidebar.classList.toggle("active");
      if (window.innerWidth <= 1000) {
        this.mainContent.classList.toggle("blurred", isActive);
        this.toggleBtn.classList.toggle("sidebar-toggle", isActive);
      } else {
        this.mainContent.classList.toggle("expanded", isActive);
        document
          .querySelector(".dashboard-header")
          .classList.toggle("expanded", isActive);
      }
    }

    handleNavLinkClick(event, link) {
      this.navLinks.forEach((nav) => nav.classList.remove("active"));
      link.classList.add("active");
    }

    handleLogout() {
      window.location.href = "index.html"; // Redirect to index page
    }

    handleDocumentClick(event) {
      if (
        !this.sidebar.contains(event.target) &&
        !this.userInfo.contains(event.target) &&
        !this.toggleBtn.contains(event.target)
      ) {
        if (
          window.innerWidth <= 1000 &&
          this.sidebar.classList.contains("active")
        ) {
          this.sidebar.classList.remove("active");
          this.mainContent.classList.remove("blurred");
          this.toggleBtn.classList.remove("sidebar-toggle");
        }

        document
          .querySelectorAll(".collapse.show")
          .forEach((openDropdown) => openDropdown.classList.remove("show"));
        this.userDropdown.classList.remove("show");
      }
    }

    openSidebarOnLoad() {
      this.sidebar.classList.add("active");
      if (window.innerWidth <= 1000) {
        this.mainContent.classList.add("blurred");
        this.toggleBtn.classList.add("sidebar-toggle");
      } else {
        this.mainContent.classList.add("expanded");
        document.querySelector(".dashboard-header").classList.add("expanded");
      }
    }

    toggleUserDropdown(event) {
      event.stopPropagation(); // Prevent the event from bubbling up to the document
      this.userDropdown.classList.toggle("show");
    }
  }

  new SidebarManager();
});

function toggleUserDropdown() {
  document.getElementById("userDropdown").classList.toggle("show");
}
