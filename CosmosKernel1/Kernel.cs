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
            Console.Write("cmd: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)){
                switch (input)
                {
                    case "/help":
                        Console.Clear();
                        Console.WriteLine("All available commands are:\n" +
                                          "/help     : See all commands\n" +
                                          "/about    : See all OS requirements\n" +
                                          "/restart  : Restarts the system\n" +
                                          "/shutdown : Shutdowns the system\n" +
                                          "/clear    : Clears the screen\n");
                        Console.WriteLine("Press enter to return to menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "/about":
                        Console.Clear();
                        Console.WriteLine("This system requires VMware, Cosmos and Visual Code\n");
                        Console.WriteLine("Press enter to return to menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "/restart":
                        Console.Clear();
                        Console.WriteLine("The system will restart.");
                        Console.WriteLine("Press enter to confirm...");
                        Console.ReadLine();
                        Sys.Power.Reboot();
                        break;
                    case "/shutdown":
                        Console.Clear();
                        Console.WriteLine("The system will shutdown.");
                        Console.WriteLine("Press enter to confirm...");
                        Console.ReadLine();
                        Sys.Power.Shutdown();
                        break;
                    case "/clear":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command. Write /help if you need help.");
                        Console.WriteLine(input);
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
