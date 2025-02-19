//using System;
//using System.Globalization;
//using System.Collections.Generic;
//using System.Runtime;
//using System.Collections.ObjectModel;

//namespace MyFirstProgram
//{
//    class Program
//    {
//        static string[] intercontinentalMenu = { "Chichken & Salad", "Chinese Rice", "Pasta", "Sushi" };
//        static string[] localMenu = { "Fried Rice", "Beans", "Eba & Egwusi", "Yam & Egg" };
//        static void Main(string[] args)
//        {
//            var parentDog = new Dog();
//            parentDog.Name = "My Dog";
//            parentDog.Color = "Black";
//            parentDog.Size = "Big";

//            var child1 = new Dog()
//            {
//                Name = "Puppy A",
//                Color = "Black"
//            };
//            var child2 = new Dog()
//            {
//                Name = "Puppy B",
//                Color = "Black"
//            };
//            var child3 = new Dog()
//            {
//                Name = "Puppy C",
//                Color = "Black"
//            };
//            parentDog.AddChild(child1);
//            parentDog.AddChild(child2);
//            parentDog.AddChild(child3);

//            Console.WriteLine("List of Puppies");
//            foreach (var puppy in parentDog.Children)
//            {
//                Console.WriteLine(puppy.Name);
//            }
//            Console.WriteLine();
//            Console.WriteLine("========================");
//            parentDog.RemoveChild("Puppy B");
//            foreach (var puppy in parentDog.Children)
//            {
//                Console.WriteLine(puppy.Name);
//            }
//        }

//    }

//    //public class Restaurant
//    //{
//    //    public Restaurant()
//    //    {

//    //    }
//    //    public Restaurant(string name="", string address = "You did not pass any value")
//    //    {
//    //        Name = name;
//    //        Address = address;
//    //    }
//    //    public Restaurant(string name, string address="Whatever address", string openingHours="12am")
//    //    {
//    //        Name = name;
//    //        Address = address;
//    //        OpeningHours = openingHours;
//    //    }
//    //    public Restaurant(string name, string[] localMenus, List<string> intercontinentalMenu)
//    //    {
//    //        Name = name;
//    //        LocalMenus = localMenus;
//    //        IntercontinentalMenu = intercontinentalMenu;
//    //    }
//    //    public string Name { get; set; }
//    //    public string Address { get; set; }
//    //    public string OpeningHours { get; set; }
//    //    public DateTime Established { get; set; } // date should be in "dd-mm-yyyy"
//    //    public List<MenuItem> LocalMenus { get; set; }
//    //    public List<MenuItem> IntercontinentalMenu { get; set; }



//    //    public void GetRestaurantDetails()
//    //    {
//    //        Console.WriteLine("Enter the name of the restaurant");
//    //        Name = Console.ReadLine();
//    //        Console.WriteLine("Enter the address of the restaurant");
//    //        Address = Console.ReadLine();
//    //        Console.WriteLine("Enter the opening hours of the restaurant");
//    //        OpeningHours = Console.ReadLine();
//    //        Console.WriteLine("Enter inception date");
//    //        var dateOfInception = Console.ReadLine();
//    //        Established = Convert.ToDateTime(dateOfInception);
//    //        return;
//    //    }
//    //    public string DisplayRestaurantDetails(string name, int amount, string address, bool isHotFood)
//    //    {
//    //        return $"Name: {Name}, Address: {Address}, OpeningHours: {OpeningHours}";
//    //    }

//    //    public void DisplayRestaurantDetails(string name, int amount, bool isHot, string address)
//    //    {
//    //        Console.WriteLine(OpeningHours);
//    //    }
//    //    public int CalculateBusinessDuration(DateTime inceptionDate)
//    //    {
//    //        var duration = DateTime.Now.Subtract(inceptionDate).Days;
//    //        return duration;
//    //    }
//    //    public int CalculateBusinessDuration()
//    //    {
//    //        var duration = DateTime.Now.Subtract(Established).Days;
//    //        return duration;
//    //    }
//    //    public string DisplayRestaurantDetails(string name, string address)
//    //    {
//    //        Console.WriteLine(OpeningHours);
//    //        return "";
//    //    }
//    //}

//    public class Money
//    {
//        public decimal Amount { get; set; }
//        public string Currency { get; set; }
//        public string Symbol { get; set; }
//        public string Name { get; set; }
//        protected string Country { get; set; }
//        public void Save(int amount)
//        {
//            Amount = amount;
//        }
//    }
//    public class MenuItem
//    {

//    }

//    public abstract class Animal
//    {
//        public string Name { get; set; }
//        public string Color { get; set; }
//        public string Size { get; set; }
//        public decimal Weight { get; set; }
//        public decimal Height { get; set; }
//        public abstract void MakeSound(string sound);
//    }

//    public class Dog : Animal
//    {
//        public void Bark()
//        {
//            Console.WriteLine("Bark Bark");
//        }
//        public override void MakeSound(string sound)
//        {
//            Console.WriteLine(sound);
//        }
//        public List<Dog> Children { get; set; } = new List<Dog>();
//        public Dictionary<string, Dog> Room { get; set; }

//        public void AddChild(Dog dog)
//        {
//            Children = Children ?? new List<Dog>();
//            Children.Add(dog);
//        }
//        public void RemoveChild(string name)
//        {
//            var childToRemove = Children.Where(_ => _.Name == name).SingleOrDefault();
//            if (childToRemove != null)
//            {
//                Children.Remove(childToRemove);
//            }
//        }
//    }

//    public class Cat : Animal
//    {
//        public void Meow(string sound)
//        {
//            Console.WriteLine("Meow Meow");
//        }
//        public override void MakeSound(string sound)
//        {
//            Console.WriteLine("Meow Meow");
//        }
//    }

//    public class MyCustomClass<T>
//    {
//        public T Value { get; set; }
//    }

//    public class MyCustomClass2<T> where T : class
//    {
//        public T Value { get; set; }
//    }

//}

///*
// Project Description.
//- Build a restaurant application that allows customers to walk-in and place orders. A customer must be registered before they can place an order. An order can contain one or more menu. When orders are placed, the kitchen should receive the order and fulfill the order based on First come first serve. At the end of day, business should able to generate reports of how many order they received, fulfilled and the total amount of revenue generated. All orders fulfilled must capture the staff that fulfilled that order.
// */



///*
//Assignment.
//Expand the Restaurant application to allow admin to setup intercontinental Menu and local Menu.
//This menu item must be setup using the Restaurant class constructor.
////Requirements:-
//Admin should be able to enter a comma separated list of local menu from the prompt-
//Admin should be able to enter a comma separated list of intercontinental menu from the prompt-
//Display the Restaurant details including a list of the local and intercontinental menus
//*/



///*
//Assignment.
//Expand the Restaurant application to allow Adding and Removal of Item from any of the Menus (Local, Intl).
////Requirements:-
//Admin should be able to optionally add Restaurant Menu at setup
//Admin should have a functionality that allow them to add or remove a menu from the different categories (Intl or Local)
//To remove a local Menu, An admin will enter Remove in the prompt and the application should present the two menu categories with a number
//The Admin can then process to use the number to specify which Menu he want to modify
//After selecting the menu to modify, the application should display all the menu under that menu with index numbering
//The admin can then proceed to use the item number to remove the item from the list


//Example: Add new Menu Item
//- Admin enter Add
//- Application display
//    *** 1 Local
//    *** 2 Intercontinental
//- Admin enter 2
//- Admin enter the name of the menu
//- Menu is automatically added to the Intercontinental Menu list
//- Application displays the updated menus (both local and intl)
//    *** List menu in intercontinental
//        1 Chinese Rice
//        2 Pizza

//Example: Remove an existing Menu Item
//- Admin enter Remove
//- Application display
//    *** 1 Local
//    *** 2 Intercontinental
//- Admin enter 1
//- Applciation display all the menu item in local Menu
//    1 Rice
//    2 Beans
//    3 Eba
//- Admin enter the number of menu item he/she wants to remove
//- Menu item is removed from the Menu list
//- Application displays the updated menus (both local and intl)

//Note: Menu Item should now be a class (MenuItem) with two properties (Name, IsAvailable)
//*/

//// Convert the current Restaurant menu application using OOP
//// Requirements: When an admin user runs the console app, the application should collect the following inputs from the admin ("Name", "Address", "OpeningHours").
//// After collecting these input from the user, use the information provided by the user to create/instantiate a new restaurant and output the information to the console
//// Kindly provide the pseudo code for this task
//// Hint -> Create an object or a class called Restaurant with the following properties
//// - Name
//// - Address
//// - OpeningHours
////Additional Requirements
//// Allow the Admin to specify which of the properties that should be outputted to the console
//// Example: Display WHich of these Information do you want display (Name, Address, OpeningHours)
//// if enters : Name
//// then only the name of the restaurant should be displayed on the Console
//// if enters: Name, OpeningHours
//// then only the name and OpeningHours of the restaurant should be displayed on the Console
//// Please use Method overloading


///* OOP - Object Oriented Programming

//- Must have Properties
//- Optionally can have functions
//- Name

//Features or Concept
//- Inheritance - Passing the trait and characteristics of a parent object to a child object
//- Polymorphism - the ability to take on many forms
//- Encapsulation - decide the visibility of a component of an object or the object itself
//- Abstraction
//*/
///* if
//else
//elseif
//switch */

//// Properties & functions of Array
//// - Index
//// - Length
//// - Count()
//// - Resize()
//// - Reverse()
//// - Sort()

//// Assignment
//// Create a Student class with the following fields and methods
//// - name
//// - age
//// - grade
//// - course

//// methods
//// - greet() : should accept a string parameter called name and it should write the name to the Console
//// - study() : should write "I am studying "course" to the console"


//// find out what is the size of the following datat types:
//// int
//// float
//// decimal
//// double
//// bool
//// char
//// enum
//// string

//// Write a simple program with the following variables
//// - x
//// - y
//// - z
//// - total

//// the value of variable z should be Hello World
//// the value of variable x should be 10
//// the value of variable y should be 20
//// the value of total should be the sum of x and y

//// Write out the values of x, y, z, total to the Console

//// Console.WriteLine(total)


//// Write a simple program with the following variables
//// - studentAges


//// The length of studentAges should be 20 (clasroom)
//// the value of variable studentAges should 1, 2, 3, 5, ....
//// the sequence of the value should be the addition of the current "index" + the "previous index"

//// Control Flow Statement
///* if
//else
//elseif
//switch

//# Loops
//for
//foreach
//do
//do while
//while

//// Operators
//+ - * /
//> < >= <= ! != && || ^ = bool
//(5 > 8) = true or false

//XOR
//XAND */




//// expand your restaurant app to have two menus called (intercontinental & local)
//// the intercontinental menu should have the following menu item
//// - Chichken & Salad, Chinese Rice, Pasta, Sushi

//// the local menu should have the following menu item
//// - Chichken & Salad, Chinese Rice, Pasta, Sushi
//// if a Customer Name starts with A, please show them the Intercontinental Menu and display whatever they selected to the console. The display to the console should
//// include the customer name, date and time, and also the Customer Gender



//// Write a program that prints out any input passed to it. If I pass multiple values separated by comma (,) it should print out each of them in a separate line
//// example 1 - If I enter Rice Output should be Rice
//// example 2 - If I enter Rice, Beans, Chicken & Chips - Output should be
///* Rice
//   Beans
//   Chicken & Chips
//   */
