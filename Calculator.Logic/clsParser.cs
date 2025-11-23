using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic
{
    internal class clsParser
    {
        static string OperationSymbols = "+*/%^";

        public static clsMathExpression Parse(string input)
        {
            clsMathExpression expression = new clsMathExpression();
            input = input.Trim();

            string token = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];

                if (currentChar == '.')
                {
                    if (token.Length == 0)
                        token += currentChar;

                    else if (i == input.Length - 1)
                    {
                        _SetRightOperand(expression,ref token);
                        break;
                    }
                    else if (token.Length > 0 && char.IsDigit(input[i - 1]))
                    {
                        token += currentChar;
                    }
                    
                }
                else if (char.IsDigit(currentChar))
                {
                    if (expression.isLeftSideInitialized && expression.Operation == enMathOperation.None)
                    {
                        _setOperation(expression,ref token);
                    }

                    token += currentChar;

                    if (i == input.Length - 1)
                    {
                        _SetRightOperand(expression,ref token);
                        break;
                    }
                }
                else if (OperationSymbols.Contains(currentChar))
                {
                    if (!expression.isLeftSideInitialized)
                    {
                        _SetLeftOperand(expression,ref token);
                    }

                    if (expression.Operation == enMathOperation.None)
                    {
                        expression.Operation = _ParseMathOperation(currentChar.ToString());
                        //token = string.Empty;
                    }
                    else
                        throw new Exception("Expression Error Format. two operation at same time");
                }
                else if (currentChar == '-')
                {
                    if (i == 0)
                        token += currentChar;
                    else
                    {
                        if (!expression.isLeftSideInitialized)
                        {
                            _SetLeftOperand(expression,ref token);
                        }

                        if (expression.Operation == enMathOperation.None && token.Length == 0)
                        {
                            expression.Operation = enMathOperation.Subtraction;
                            //token = string.Empty;
                        }
                        else if (expression.Operation == enMathOperation.None)
                        {
                            _setOperation(expression,ref token);
                            token += currentChar.ToString();
                        }
                        else
                            token += currentChar;

                    }

                }
                else if (char.IsLetter(currentChar))
                {
                    if (i == 0)
                        expression.isLeftSideInitialized = true;

                    else if (!expression.isLeftSideInitialized)
                    {
                        _SetLeftOperand(expression,ref token);
                    }

                    token += currentChar;

                }
                else if (currentChar == ' ')
                {
                    if (!expression.isLeftSideInitialized)
                    {
                        _SetLeftOperand(expression,ref token);
                    }
                    else if (expression.Operation == enMathOperation.None)
                    {
                        _setOperation(expression,ref token);
                    }
                }

            }

            if (!expression.isExpressionCorrect())
                throw new Exception("Expression Error Format.");
            
            return expression;
        }

        private static void _SetLeftOperand(clsMathExpression expression,ref string token)
        {
            expression.LeftSideOperand = double.Parse(token);
            expression.isLeftSideInitialized = true;
            token = string.Empty;
        }

        private static void _SetRightOperand(clsMathExpression expression,ref string token)
        {
            expression.RightSideOperand = double.Parse(token);
            expression.isRightSideInitialized = true;
            token = string.Empty;
        }

        private static void _setOperation(clsMathExpression expression,ref string token) // for [mod, pow, sin, cos, tan]
        {
            expression.Operation = _ParseMathOperation(token);
            token = string.Empty;
        }

        private static enMathOperation _ParseMathOperation(string input)
        {
            if (!OperationSymbols.Contains(input))
                input = input.ToLower();

            switch (input)
            {
                case "+":
                    return enMathOperation.Addition;

                case "*":
                    return enMathOperation.Multiplication;

                case "/":
                    return enMathOperation.Division;

                case "mod":
                case "%":
                    return enMathOperation.Modulus;

                case "pow":
                case "^":
                    return enMathOperation.Power;

                case "sin":
                    return enMathOperation.Sin;

                case "cos":
                    return enMathOperation.Cos;

                case "tan":
                    return enMathOperation.Tan;

                default:
                    return enMathOperation.None;
            }
        }
    }
}
