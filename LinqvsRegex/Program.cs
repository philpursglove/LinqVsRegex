using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqvsRegex
{
    class Program
    {
        public static int Iterations { get; set; }
        public static List<long> Ticks { get; set; }

        public static bool UseCompiledRegex { get; set; }

        static void Main()
        {
            Iterations = 100;
            Ticks = new List<long>(Iterations);
            UseCompiledRegex = false;

            string option;
            do
            {
                WriteMenu();
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        SetIterations();
                        break;
                    case "2":
                        ToggleCompiledRegex();
                        break;
                    case "3":
                        ContainsUpperCase();
                        break;
                    case "4":
                        ContainsLowerCase();
                        break;
                    case "5":
                        ContainsDigit();
                        break;
                    case "6":
                        ContainsUpperCase();
                        ContainsLowerCase();
                        ContainsDigit();
                        break;
                    case "9":
                        Environment.Exit(0);
                        break;
                    default:
                        WriteMenu();
                        break;
                }
            } while (option != "9");

        }

        private static void ToggleCompiledRegex()
        {
            UseCompiledRegex = !UseCompiledRegex;
        }

        private static void ContainsDigit()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Digit test");

            string containsDigit = "abcdef1";
            Regex digitRegex;
            if (UseCompiledRegex)
            {
                digitRegex = new Regex("[0-9]", RegexOptions.Compiled);
            }
            else
            {
                digitRegex = new Regex("[0-9]");
            }

            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                digitRegex.IsMatch(containsDigit);
                sw.Stop();
                Ticks.Add(sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine($"Average ticks for Regex ({Iterations} iterations): {Ticks.Average()}");

            Ticks.Clear();

            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                containsDigit.Any(char.IsDigit);
                sw.Stop();
                Ticks.Add(sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine($"Average ticks for Linq ({Iterations} iterations): {Ticks.Average()}");

            Ticks.Clear();

            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
        }

        private static void ContainsLowerCase()
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Lower case test");

            string containsLowerCase = "ABcDEF";
            Regex lowerCaseRegex;
            if (UseCompiledRegex)
            {
                lowerCaseRegex = new Regex("[a-z]", RegexOptions.Compiled);
            }
            else
            {
                lowerCaseRegex = new Regex("[a-z]");
            }

            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                lowerCaseRegex.IsMatch(containsLowerCase);
                sw.Stop();
                Ticks.Add(sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine($"Average ticks for Regex ({Iterations} iterations): {Ticks.Average()}");

            Ticks.Clear();

            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                containsLowerCase.Any(char.IsLower);
                sw.Stop();
                Ticks.Add(sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine($"Average ticks for Linq ({Iterations} iterations): {Ticks.Average()}");

            Ticks.Clear();

            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();

        }

        private static void ContainsUpperCase()
        {
            Stopwatch sw = new Stopwatch();
            string containsUpperCase = "abcDef";
            Regex upperCaseRegex;
            if (UseCompiledRegex)
            {
                upperCaseRegex = new Regex("[A-Z]", RegexOptions.Compiled);
            }
            else
            {
                upperCaseRegex = new Regex("[A-Z]");
            }

            Console.WriteLine("Upper case test");

            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                upperCaseRegex.IsMatch(containsUpperCase);
                sw.Stop();
                Ticks.Add(sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine($"Average ticks for Regex ({Iterations} iterations): {Ticks.Average()}");

            Ticks.Clear();

            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                containsUpperCase.Any(char.IsUpper);
                sw.Stop();
                Ticks.Add(sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine($"Average ticks for Linq ({Iterations} iterations): {Ticks.Average()}");

            Ticks.Clear();
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();

        }

        static void WriteMenu()
        {
            Console.Clear();
            Console.WriteLine("Linq vs Regex");
            Console.WriteLine("=============");
            Console.WriteLine($"1. Set number of iterations. (Current: {Iterations})");
            Console.WriteLine($"2: Toggle compiled Regexes. (Current: {UseCompiledRegex})");
            Console.WriteLine("3. Upper case test");
            Console.WriteLine("4. Lower case test");
            Console.WriteLine("5. Digit test");
            Console.WriteLine("6. Run all tests");
            Console.WriteLine("9. Exit");
        }

        static void SetIterations()
        {
            Console.WriteLine("Enter the new number of iterations to run");

            string newIterations = Console.ReadLine();

            Iterations = int.Parse(newIterations);

            Ticks = new List<long>(Iterations);
        }
    }
}
