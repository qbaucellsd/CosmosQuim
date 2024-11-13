using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using System.IO;
using Cosmos.Debug.Kernel.Plugs.Asm;


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
                switch (input){
                    case "/help":
                        Console.WriteLine("\nAll available commands are:\n\n" +
                                          "/help     : See all commands\n" +
                                          "/about    : See all OS requirements\n" +
                                          "/restart  : Restarts the system\n" +
                                          "/shutdown : Shutdowns the system\n" +
                                          "/clear    : Clears the screen\n" +
                                          "/free     : Get available free space\n" +
                                          "/filetype : Get file system type\n" +
                                          "/list     : Get list of file\n" +
                                          "/listall  : Get directory listing (files and other directories)\n" +
                                          "/newfile  : Create new file\n" +
                                          "/newdir   : Create a new directory\n" +
                                          "/del      : Deleting a file or a directory\n");
                        break;
                    case "/about":
                        Console.WriteLine("\nThis system requires VMware, Cosmos and Visual Code");
                        Console.WriteLine("Project made by Quim Baucells. Student from Educem.\n");
                        break;
                    case "/restart":
                        Console.WriteLine("\nThe system will restart.");
                        Sys.Power.Reboot();
                        break;
                    case "/shutdown":
                        Console.WriteLine("\nThe system will shutdown.");
                        Sys.Power.Shutdown();
                        break;
                    case "/clear":
                        Console.Clear();
                        break;
                    case "/free":
                        var available_space = fs.GetAvailableFreeSpace(@"0:\");
                        Console.WriteLine("\nAvailable Free Space: " + available_space + "\n");
                        break;
                    case "/filetype":
                        var fs_type = fs.GetFileSystemType(@"0:\");
                        Console.WriteLine("\nFile System Type: " + fs_type + "\n");
                        break;
                    case "/list":
                        Console.WriteLine("\nList of files:\n");
                        var files_list = Directory.GetFiles(@"0:\");
                        foreach (var file in files_list){
                            Console.WriteLine(file);
                        }
                        Console.WriteLine("");
                        break;
                    case "/listall":
                        Console.WriteLine("\nList of files and directories:\n");
                        var files_list1 = Directory.GetFiles(@"0:\");
                        var directory_list = Directory.GetDirectories(@"0:\");
                        Console.ForegroundColor = ConsoleColor.Green;
                        foreach (var file1 in files_list1){
                            Console.WriteLine(file1);
                        }
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var directory in directory_list){
                            Console.WriteLine(directory);
                        }
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "/newfile":
                        try{
                            Console.Write("\nNew file name: ");
                            var input1 = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input1)){
                                var file_stream = File.Create(@"0:\" + input1 + ".txt");
                            }
                        }
                        catch (Exception e){
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    case "/newdir":
                        try{
                            Console.Write("\nNew directory name: ");
                            var input2 = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input2)){
                                Directory.CreateDirectory(@"0:\" + input2 + "\\");
                            }
                        }
                        catch (Exception e){
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    case "/del":
                        try{
                            Console.Write("\nIs it a file(f) or directory(d): ");
                            var input3 = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input3)){
                                Console.Write("\n\nFile name to delete: ");
                                var input4 = Console.ReadLine();
                                if (!string.IsNullOrEmpty(input4)){
                                    switch (input4) {
                                        case "f":
                                            File.Delete(@"0:\" + input4 + ".txt");
                                            break;
                                        case "d":
                                            Directory.Delete(@"0:\" + input4 + "\\");
                                            break;
                                    }
                                }
                            }
                        }
                        catch (Exception e){
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine("\nPlease enter a valid command. Write /help if you need help.\n");
                        break;
                }
            }
        }
    }
}
