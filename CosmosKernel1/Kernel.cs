using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("           |  | \\     /\\     / |  | Operative\r\n" +
                              "Welcome to |__|   \\_/    \\_/   |__| System\n");
        }

        protected override async void Run()
        {
            Console.WriteLine("1. Help\n" +
                              "2. About\n" +
                              "3. Restart\n" +
                              "4. Shutdown\n");
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();
            int numero = int.Parse(input);
            switch (numero)
            {
                case 1:
                    Console.WriteLine("You don't get help in this system :)");
                    break;
                case 2:
                    Console.WriteLine("This system requires VMware, Comos and Visual Code");
                    break;
                case 3:
                    Console.WriteLine("The system will restart.");
                    Cosmos.System.Power.Reboot();
                    break;
                case 4:
                    Console.WriteLine("The system will shutdown.");
                    Cosmos.System.Power.Shutdown();
                    break;
            }
        }
    }
}
