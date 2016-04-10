using System.IO;
using Text.ABNF.Core.DIGIT;
using Txt;

namespace Sample1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autofac;
    using Text;
    using Text.ABNF;

    internal class Program
    {
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TerminalLexerFactory>().As<ITerminalLexerFactory>().SingleInstance();
            builder.RegisterType<ValueRangeLexerFactory>().As<IValueRangeLexerFactory>().SingleInstance();
            builder.RegisterType<ConcatenationLexerFactory>().As<IConcatenationLexerFactory>().SingleInstance();
            builder.RegisterType<RepetitionLexerFactory>().As<IRepetitionLexerFactory>().SingleInstance();
            builder.RegisterType<AlternativeLexerFactory>().As<IAlternativeLexerFactory>().SingleInstance();
            builder.RegisterType<OptionLexerFactory>().As<IOptionLexerFactory>().SingleInstance();
            builder.RegisterType<SignLexerFactory>().As<ILexerFactory<Sign>>().SingleInstance();
            builder.RegisterType<DigitLexerFactory>().As<ILexerFactory<Digit>>().SingleInstance();
            builder.RegisterType<IntegerLexerFactory>().As<ILexerFactory<Integer>>().SingleInstance();

            // With all dependencies wired up, register a delegate that returns a new IntegerLexer by calling IntegerLexerFactory.Create()
            builder.Register(
                ctx =>
                {
                    var integerLexerFactory = ctx.Resolve<ILexerFactory<Integer>>();
                    return integerLexerFactory.Create();
                })
                .As<ILexer<Integer>>()
                .SingleInstance();
            return builder.Build();
        }

        private static void Main(string[] args)
        {
            ILexer<Integer> integerLexer;
            using (var container = BuildContainer())
            {
                integerLexer = container.Resolve<ILexer<Integer>>();
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
            } while (readResult.Success || inputs.Count < 2);
            Console.WriteLine("===============================================");
            var bigIntegers = inputs.Select(x => x.ToBigInteger()).ToList();
            var sum = bigIntegers.Aggregate((x, y) => x + y);
            Console.Write(string.Join(" + ", bigIntegers));
            Console.Write(" = ");
            Console.Write(sum);
            Console.ReadLine();
        }
    }
}
