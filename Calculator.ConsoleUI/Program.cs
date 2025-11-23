using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic;

namespace Calculator.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Run Calculator");
            Console.WriteLine("2. Run Test Suite");
            Console.Write("Select option: ");
            string option = Console.ReadLine();

            if (option == "2")
            {
                CalculatorTests.RunAllTests();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            else
            {
                RunCalculatorLoop();
            }
        }

        static void RunCalculatorLoop()
        {
            while (true)
            {
                IExpressionCalculator calc = new clsExpressionCalculator();

                Console.Write("\nenter math expression (or 'exit'): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") break;

                if (calc.TryEvaluate(input, out double result))
                {
                    Console.WriteLine($"{input} = {result}");
                }
                else
                {
                    Console.WriteLine("there is problem with the expression format, ERROR");
                }
            }
        }
    }
}