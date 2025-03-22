document.addEventListener("DOMContentLoaded", function () {
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
          backgroundColor: "rgba(54, 162, 235, 0.6)",
          borderColor: "rgba(54, 162, 235, 1)",
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
      window.location.href = "index.html";
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
  }

  new SidebarManager();
});

function toggleUserDropdown() {
  document.getElementById("userDropdown").classList.toggle("show");
}
