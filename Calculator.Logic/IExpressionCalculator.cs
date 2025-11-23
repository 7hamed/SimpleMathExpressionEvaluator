using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator.Logic
{
    public interface IExpressionCalculator
    {
        /// <summary>
        /// Evaluates a mathematical expression, e.g. "2+3*5".
        /// </summary>
        /// <param name="expression">The expression string.</param>
        /// <returns>The numeric result.</returns>
        double Evaluate(string expression);

        bool TryEvaluate(string expression, out double result);
    }
}
