using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
Console.OutputEncoding = Encoding.UTF8;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            string unicodeChar = "⣤";
            Console.WriteLine("Aquí tienes el carácter Unicode: " + unicodeChar);
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
    }
}
