using ConsoleSylph.Functions;
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
                    FunctionsRegistry.Functions.Add(new beep());
                    FunctionsRegistry.Functions.Add(new print());
                    FunctionsRegistry.Functions.Add(new readKey());
                    FunctionsRegistry.Functions.Add(new readLine());
                    FunctionsRegistry.Functions.Add(new setTitle());
                    FunctionsRegistry.Functions.Add(new sylph());
                    FunctionsRegistry.Functions.Add(new Types.ColorFunctions.Color());

                    TypeRegistry.Types.Add(new Types.Color());

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
