using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using Txt;
using Txt.ABNF;
using Registration = Txt.Registration;

namespace Sample1
{
    internal class Program
    {
        private static Container BuildContainer()
        {
            var container = new Container();
            foreach (var registration in AbnfRegistrations.GetRegistrations(container.GetInstance))
            {
                Register(registration, container);
            }
            foreach (var registration in Registrations.GetRegistrations(typeof(Program).GetTypeInfo().Assembly, container.GetInstance))
            {
                Register(registration, container);
            }
            container.Verify();
            return container;
        }

        private static void Main(string[] args)
        {
            ILexer<Integer> integerLexer;
            using (var container = BuildContainer())
            {
                integerLexer = container.GetInstance<ILexer<Integer>>();
            }
            Console.WriteLine("This sample program reads numbers and calculates their sum.");
            Console.WriteLine(
                "==============================================================================================");
            Console.WriteLine(File.ReadAllText("Grammar.abnf"));
            Console.WriteLine(
                "==============================================================================================");
            Console.WriteLine("Enter your numbers, one number per line.");
            Console.WriteLine();
            Console.WriteLine("Input ends when your input violates the grammar rules above.");
            Console.WriteLine();
            ReadResult<Integer> readResult;
            var inputs = new List<Integer>();
            do
            {
                Console.Write("Enter a number: ");
                var input = Console.ReadLine();
                using (ITextSource textSource = new StringTextSource(input))
                using (ITextScanner textScanner = new TextScanner(textSource))
                {
                    readResult = integerLexer.Read(textScanner);
                    if (readResult.Success)
                    {
                        inputs.Add(readResult.Element);
                        Console.Write("Input accepted: ");
                        var color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(readResult.Text);
                        Console.ForegroundColor = color;
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        if (!readResult.EndOfInput)
                        {
                            Console.Write("Input rejected: ");
                            var color = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(readResult.Text);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(readResult.ErrorText);
                            Console.ForegroundColor = color;
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                }
            } while (readResult.Success || (inputs.Count < 2));
            Console.WriteLine("===============================================");
            var bigIntegers = inputs.Select(x => x.ToBigInteger()).ToList();
            var sum = bigIntegers.Aggregate((x, y) => x + y);
            Console.Write(string.Join(" + ", bigIntegers));
            Console.Write(" = ");
            Console.Write(sum);
            Console.ReadLine();
        }

        private static void Register(Registration registration, Container container)
        {
            if (registration.Implementation != null)
            {
                container.RegisterSingleton(registration.Service, registration.Implementation);
            }
            else if (registration.Factory != null)
            {
                container.RegisterSingleton(registration.Service, registration.Factory);
            }
            else if (registration.Instance != null)
            {
                container.RegisterSingleton(registration.Service, registration.Instance);
            }
        }
    }
}
