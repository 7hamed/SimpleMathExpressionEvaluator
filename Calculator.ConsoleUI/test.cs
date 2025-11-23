using System;
using Calculator.Logic;

namespace Calculator.ConsoleUI
{
    public static class CalculatorTests
    {
        public static void RunAllTests()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("      RUNNING CALCULATOR TESTS");
            Console.WriteLine("=======================================");

            int passed = 0;
            int failed = 0;

            // --- 1. Basic Arithmetic ---
            RunTest("10 + 20", 30, ref passed, ref failed);
            RunTest("30 - 10", 20, ref passed, ref failed);
            RunTest("5 * 6", 30, ref passed, ref failed);
            RunTest("100 / 2", 50, ref passed, ref failed);
            RunTest("10 / 4", 2.5, ref passed, ref failed);

            // --- 2. Decimals ---
            RunTest("1.5 + 2.5", 4.0, ref passed, ref failed);
            RunTest("5.5 * 2", 11.0, ref passed, ref failed);
            RunTest("10.5 - 0.5", 10.0, ref passed, ref failed);

            // --- 3. Negative Numbers ---
            RunTest("-5 + 10", 5, ref passed, ref failed);   // Negative at start
            RunTest("10 + -5", 5, ref passed, ref failed);   // Negative on right side
            RunTest("-10 * -10", 100, ref passed, ref failed); // Both negative
            RunTest("-10 - -5", -5, ref passed, ref failed);

            // --- 4. Advanced Operations (Mod, Pow) ---
            RunTest("10 % 3", 1, ref passed, ref failed);
            RunTest("10 mod 3", 1, ref passed, ref failed); // Case insensitive check
            RunTest("10 MOD 3", 1, ref passed, ref failed); // Upper case check
            RunTest("2 ^ 3", 8, ref passed, ref failed);
            RunTest("2 pow 3", 8, ref passed, ref failed);
            RunTest("4 POW 0.5", 2, ref passed, ref failed); // Square root via power

            // --- 5. Trigonometry (Note: Math.Sin/Cos uses Radians) ---
            RunTest("sin 0", 0, ref passed, ref failed);
            RunTest("cos 0", 1, ref passed, ref failed);
            RunTest("tan 0", 0, ref passed, ref failed);
            // cos(pi) should be -1
            RunTest($"cos {Math.PI}", -1, ref passed, ref failed);

            // --- 6. Formatting & Spacing ---
            RunTest("  50  +  50  ", 100, ref passed, ref failed); // Spaces
            RunTest("100+100", 200, ref passed, ref failed);       // No spaces

            // --- 7. Edge Cases / Invalid Inputs (Should fail gracefully) ---
            RunInvalidTest("5 +", ref passed, ref failed);         // Missing right operand
            RunInvalidTest("sin", ref passed, ref failed);         // Missing operand
            RunInvalidTest("+ 5", ref passed, ref failed);         // Missing left operand
            RunInvalidTest("5 + * 5", ref passed, ref failed);     // Double operator
            RunInvalidTest("hello", ref passed, ref failed);       // Gibberish

            // --- UPDATED: Division by Zero ---
            // Since your logic now throws an exception for zero, this should be an InvalidTest
            //RunInvalidTest("5 / 0", ref passed, ref failed);
            RunTest("5 / 0", double.PositiveInfinity, ref passed, ref failed);       // No spaces

            Console.WriteLine("\n=======================================");
            Console.WriteLine($"TESTS COMPLETED: {passed} Passed, {failed} Failed.");
            Console.WriteLine("=======================================");
        }

        private static void RunTest(string input, double expected, ref int passed, ref int failed)
        {
            IExpressionCalculator calc = new clsExpressionCalculator();

            // Use epsilon for double comparison to avoid precision issues
            double epsilon = 0.0001;

            if (calc.TryEvaluate(input, out double result))
            {
                if (result == expected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[PASS] '{input}' = {result}");
                    passed++;
                }
                else if (Math.Abs(result - expected) < epsilon)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[PASS] '{input}' = {result}");
                    passed++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[FAIL] '{input}' -> Expected: {expected}, Got: {result}");
                    failed++;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] '{input}' -> Failed to evaluate (Parser Error).");
                failed++;
            }
            Console.ResetColor();
        }

        private static void RunInvalidTest(string input, ref int passed, ref int failed)
        {
            IExpressionCalculator calc = new clsExpressionCalculator();

            // We expect TryEvaluate to return FALSE here
            if (!calc.TryEvaluate(input, out double result))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] '{input}' correctly identified as Invalid.");
                passed++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] '{input}' should be Invalid but returned: {result}");
                failed++;
            }
            Console.ResetColor();
        }
    }
}