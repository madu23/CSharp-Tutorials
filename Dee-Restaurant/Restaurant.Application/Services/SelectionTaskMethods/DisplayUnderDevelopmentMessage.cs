using System;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.SelectionTaskMethods
{
    public class DisplayUnderDevelopmentMessage
    {
        public async Task ShowMessage()
        {
            string message = "Restaurant Menu is still under development !!!";
            foreach (var word in message.Split(' '))
            {
                Console.Write(word + " ");
                await Task.Delay(200);
            }
            Console.WriteLine();
        }
    }
}