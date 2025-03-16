namespace Restaurant.Domain.Handlers;

public class InputHandler
{
    public int GetValidIntInput(string prompt)
    {
        int result;
        do
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out result))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        } while (true);
        return result;
    }

    public string GetValidStringInput(string prompt)
    {
        string input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(input))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid string.");
            }
        } while (true);
        return input!;
    }

    public string[] GetValidStringArrayInput(string prompt, char separator = ',')
    {
        string[] inputArray;
        do
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine()?.Trim();
            inputArray = input?.Split(separator) ?? Array.Empty<string>();
            if (inputArray.Length > 0 && inputArray.All(item => !string.IsNullOrWhiteSpace(item)))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter valid comma-separated values.");
            }
        } while (true);
        return inputArray;
    }
}
