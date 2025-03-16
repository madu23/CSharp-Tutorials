# Dee-Restaurant Project

## Overview
Dee-Restaurant is a restaurant management application that allows users to manage staff and restaurant-related tasks efficiently. The application provides functionalities for creating, editing, viewing, and deleting staff members, as well as managing restaurant operations.

## Project Structure
The project is organized into several key directories:

- **Restaurant.Application**: Contains the core application logic and services.
  - **Services**: Includes the main service classes and methods for handling various tasks.
    - **SelectionTask.cs**: The main entry point for managing selections related to staff and restaurant management.
    - **SelectionTaskMethods**: Contains individual files for each method related to selection tasks.
      - `HandleCreateStaff.cs`: Method for creating new staff members.
      - `HandleDeleteStaff.cs`: Method for deleting staff members.
      - `HandleEditStaff.cs`: Method for editing existing staff details.
      - `HandleRestaurantManagement.cs`: Method for managing restaurant-related tasks.
      - `HandleSelection.cs`: Method for managing main menu selections.
      - `HandleStaffManagement.cs`: Method for managing staff-related functionalities.
      - `HandleViewStaff.cs`: Method for viewing staff details.
      - `DisplayUnderDevelopmentMessage.cs`: Method for displaying development messages.

- **Restaurant.Domain**: Contains domain entities, database context, and handlers.
  - **Db**: Database context files.
  - **Entities**: Domain entities such as `Staff`.
  - **Handlers**: Classes for handling input, menu options, and staff operations.

- **Restaurant.Infrastructure**: Contains infrastructure-related files.

- **Restaurant.UI**: Contains user interface-related files.

- **Restaurant.Tests**: Contains test files for unit and integration testing.

## Setup Instructions
1. Clone the repository to your local machine.
2. Open the project in your preferred IDE.
3. Restore the necessary packages and dependencies.
4. Build the project to ensure all components are correctly configured.

## Usage Guidelines
- Run the application to access the main menu.
- Follow the prompts to manage staff and restaurant operations.
- Use the provided options to create, edit, view, or delete staff members.
- Note that some features may still be under development.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.