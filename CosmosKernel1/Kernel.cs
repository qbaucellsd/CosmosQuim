using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;


namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        //Canvas canvas;
        Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();

        protected override void BeforeRun()
        {

            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("           |  | \\     /\\     / |  | Operative\r\n" +
                              "Welcome to |__|   \\_/    \\_/   |__| System\n");
         //   canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
         //   canvas.Clear(Color.Blue);
        }

        protected override void Run()
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
                    Console.Clear();
                    Console.WriteLine("You don't get help in this system :)\n");
                    Console.WriteLine("Press enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("This system requires VMware, Cosmos and Visual Code\n");
                    Console.WriteLine("Press enter to return to menu...");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("The system will restart.");
                    Console.WriteLine("Press enter to confirm...");
                    Console.ReadLine();
                    Sys.Power.Reboot();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("The system will shutdown.");
                    Sys.Power.Shutdown();
                    Console.WriteLine("Press enter to confirm...");
                    Console.ReadLine();
                    break;
            
            }
        }
    }
}
