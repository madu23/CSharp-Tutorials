using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Enums;

namespace Restaurant.Domain.Utilities;
/// <summary>
/// Handles user input processing and validation.
/// </summary>

public static class UserInputHandler
{
    /// <summary>
    /// Converts user input into a corresponding CancelOperation enum value.
    /// </summary>
    /// <returns>Corresponding <see cref="CancelOperation"/> value based on user input.</returns>
    public static CancelOperation GetUserConfirmation(string input)
    {
        return input.Trim().ToLower() switch
        {
            "yes" => CancelOperation.Yes,
            "no" => CancelOperation.No,
            "cancel" => CancelOperation.Cancel,
            _ => CancelOperation.Invalid,
        };
    }
    /// <summary>
    /// Asks the user if they want to repeat an action (e.g., creating another staff record).
    /// </summary>
    public static async Task<bool> AskToRepeat(string action, Func<Task> actionMethod)
    {
        while (true)
        {
            Console.WriteLine($"Do you want to {action} another staff? (Yes/No)");
            CancelOperation choice = GetUserConfirmation(Console.ReadLine()?.Trim());

            if (choice == CancelOperation.Yes)
            {
                await actionMethod();
                return true;
            }
            else if (choice == CancelOperation.No)
            {
                Console.WriteLine("Returning to main menu...");
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter Yes or No.");
            }
        }
    }
}