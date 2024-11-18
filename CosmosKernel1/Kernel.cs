using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using System.IO;
using Cosmos.Debug.Kernel.Plugs.Asm;
using System.Data;
using System.Linq.Expressions;


namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        //Canvas canvas;
        Sys.FileSystem.CosmosVFS fs = new Cosmos.System.FileSystem.CosmosVFS();

        protected override void BeforeRun(){

            Sys.KeyboardManager.SetKeyLayout(new Sys.ScanMaps.ESStandardLayout());
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("           |  | \\     /\\     / |  | Operative\r\n" +
                              "Welcome to |__|   \\_/    \\_/   |__| System\n");
         //   canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
         //   canvas.Clear(Color.Blue);
        }

        protected override void Run(){
            Console.Write("cmd: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)){
                switch (input){
                    case "/help":
                        Console.WriteLine("\nAll available commands are:\n\n" +
                                          "/help     : Get all commands\n" +
                                          "/about    : Get all OS requirements\n" +
                                          "/restart  : Restarts the system\n" +
                                          "/shutdown : Shutdowns the system\n" +
                                          "/clear    : Clears the screen\n" +
                                          "/free     : Get available free space\n" +
                                          "/filetype : Get file system type\n" +
                                          "/list     : Get list of file\n" +
                                          "/listall  : Get directory listing (files and other directories)\n" +
                                          "/newfile  : Create new file\n" +
                                          "/newdir   : Create a new directory\n" +
                                          "/del      : Deleting a file or a directory\n" +
                                          "/write    : Write to file\n" +
                                          "/read     : Read all text from a specific file\n");
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
                        Console.WriteLine("");
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
                        Console.WriteLine("");
                        break;
                    case "/del":
                        try{
                            Console.Write("\nIs it a file(f) or directory(d): ");
                            var input3 = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input3)){
                                Console.Write("\nFile name to delete: ");
                                var input4 = Console.ReadLine();
                                if (!string.IsNullOrEmpty(input4)){
                                    switch (input3) {
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
                        Console.WriteLine("");
                        break;
                    case "/write":
                        try{
                            Console.Write("\nFile name to write: ");
                            var input3 = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input3)){
                                Console.Write("\nText: ");
                                var input4 = Console.ReadLine();
                                if (!string.IsNullOrEmpty(input4)){
                                    File.WriteAllText(@"0:\" + input3 + ".txt", input4);
                                }
                            }
                        }
                        catch (Exception e){
                            Console.WriteLine(e.ToString());
                        }
                        Console.WriteLine("");
                        break;
                    case "/read":
                        try{
                            Console.Write("\nFile name to read: ");
                            var input3 = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input3)){
                                Console.WriteLine("");
                                Console.WriteLine(File.ReadAllText(@"0:\" + input3 + ".txt"));
                            }
                        }
                        catch (Exception e){
                            Console.WriteLine(e.ToString());
                        }
                        Console.WriteLine("");
                        break;
                    default:
                        try{
                            var result = EvaluarExpresion(input);
                            Console.WriteLine("cmd: " + result);
                        }
                        catch (Exception e){
                            Console.WriteLine("\nPlease enter a valid command or operation. Write /help if you need any help.\n");
                        }
                        break;
                }
            }
        }

        private double EvaluarExpresion(string expresion){
            double resultado = 0;
            string[] partes = expresion.Split(new char[] { '+', '-', '*', '/' });
            double operando1 = Convert.ToDouble(partes[0].Trim());
            double operando2 = Convert.ToDouble(partes[1].Trim());
            char operador = expresion[partes[0].Length];

            switch (operador){
                case '+':
                    resultado = operando1 + operando2;
                    break;
                case '-':
                    resultado = operando1 - operando2;
                    break;
                case '*':
                    resultado = operando1 * operando2;
                    break;
                case '/':
                    if (operando2 != 0)
                        resultado = operando1 / operando2;
                    else
                        throw new Exception("You cannot divide by 0.\n");
                    break;
                default:
                    throw new Exception("Operator not supported.\n");
            }
            return resultado;
        }
    }
}