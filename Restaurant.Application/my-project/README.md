# My Project

This project is a TypeScript application that features a main menu and a submenu system. 

## Project Structure

```
my-project
├── src
│   ├── MainMenu.ts        # Contains the MainMenu class
│   └── submenus
│       └── SubMenu.ts     # Contains the SubMenu class
├── package.json           # npm configuration file
├── tsconfig.json          # TypeScript configuration file
└── README.md              # Project documentation
```

## Classes

### MainMenu

- **Properties:**
  - `title`: The title of the main menu.
  - `options`: The options available in the main menu.

- **Methods:**
  - `displayMenu()`: Displays the main menu to the user.
  - `handleSelection()`: Processes user input for the main menu.

### SubMenu

- **Properties:**
  - `title`: The title of the submenu.
  - `items`: The items available in the submenu.

- **Methods:**
  - `displaySubMenu()`: Displays the submenu to the user.
  - `handleItemSelection()`: Processes user input for submenu items.

## Installation

To install the project dependencies, run:

```
npm install
```

## Usage

To compile the TypeScript files, use:

```
tsc
```

After compilation, you can run the application using:

```
node dist/MainMenu.js
```

## Contributing

Feel free to submit issues or pull requests for improvements or bug fixes.