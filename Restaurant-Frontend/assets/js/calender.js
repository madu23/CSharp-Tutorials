document.addEventListener("DOMContentLoaded", function () {
  const calendar = document.getElementById("calendar");
  const monthNames = [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
  ];

  const date = new Date();
  const month = date.getMonth();
  const year = date.getFullYear();

  function generateCalendar(month, year) {
    calendar.innerHTML = "";
    const firstDay = new Date(year, month).getDay();
    const daysInMonth = 32 - new Date(year, month, 32).getDate();

    const monthAndYear = document.createElement("div");
    monthAndYear.className = "month-year";
    monthAndYear.innerHTML = monthNames[month] + " " + year;
    calendar.appendChild(monthAndYear);

    const daysRow = document.createElement("div");
    daysRow.className = "days-row";
    const days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
    days.forEach((day) => {
      const dayElement = document.createElement("div");
      dayElement.className = "day";
      dayElement.innerHTML = day;
      daysRow.appendChild(dayElement);
    });
    calendar.appendChild(daysRow);

    const datesRow = document.createElement("div");
    datesRow.className = "dates-row";
    for (let i = 0; i < firstDay; i++) {
      const emptyCell = document.createElement("div");
      emptyCell.className = "date empty";
      datesRow.appendChild(emptyCell);
    }

    for (let i = 1; i <= daysInMonth; i++) {
      const dateCell = document.createElement("div");
      dateCell.className = "date";
      dateCell.innerHTML = i;
      datesRow.appendChild(dateCell);
    }
    calendar.appendChild(datesRow);
  }

  generateCalendar(month, year);
});
