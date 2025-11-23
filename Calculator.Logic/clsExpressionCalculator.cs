using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 1. (+) when be at first of numbers
 * 2. when be to operation in same expression (do flag)
*/

namespace Calculator.Logic
{
    public class clsExpressionCalculator : IExpressionCalculator
    {
        public double Evaluate(string expression)
        {
            return _EvaluateMathExpression(clsParser.Parse(expression));
        }

        public bool TryEvaluate(string expression, out double result)
        {
            try
            {
                result = _EvaluateMathExpression(clsParser.Parse(expression));
                return true;
            }
            catch (Exception e)
            {
                result = 0;
                return false;
            }
        }

        private double _EvaluateMathExpression(clsMathExpression expression)
        {
            switch (expression.Operation)
            {
                case enMathOperation.Addition:
                    return expression.LeftSideOperand + expression.RightSideOperand;

                case enMathOperation.Subtraction:
                    return expression.LeftSideOperand - expression.RightSideOperand;

                case enMathOperation.Multiplication:
                    return expression.LeftSideOperand * expression.RightSideOperand;

                case enMathOperation.Division:
                    return expression.LeftSideOperand / expression.RightSideOperand;

                case enMathOperation.Modulus:
                    return expression.LeftSideOperand % expression.RightSideOperand;

                case enMathOperation.Power:
                    return Math.Pow(expression.LeftSideOperand, expression.RightSideOperand);

                case enMathOperation.Sin:
                    return Math.Sin(expression.RightSideOperand);

                case enMathOperation.Cos:
                    return Math.Cos(expression.RightSideOperand);

                case enMathOperation.Tan:
                    return Math.Tan(expression.RightSideOperand);

                case enMathOperation.None:
                    throw new Exception("the format of Expression is wrong");

                default:
                    throw new Exception("ERROR");
            }
        }
    }
}
