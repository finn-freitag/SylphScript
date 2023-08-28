using SylphScript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSylph
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 1)
            {
                if (File.Exists(args[0]))
                {
                    Executor.Execute(Parser.Parse(File.ReadAllText(args[0])));
                }
            }
            else
            {
                Console.WriteLine("Invalid parameters: just a path to a *.syl file is required!");
            }
        }
    }
}
