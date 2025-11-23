using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic
{
    internal class clsMathExpression
    {
        internal double LeftSideOperand { get; set; }
        internal bool isLeftSideInitialized { get; set; }
        internal double RightSideOperand { get; set; }
        internal bool isRightSideInitialized { get; set; }
        internal enMathOperation Operation { get; set; }

        public clsMathExpression()
        {
            LeftSideOperand = 0;
            isLeftSideInitialized = false;

            RightSideOperand = 0;
            isRightSideInitialized = false;

            Operation = enMathOperation.None;
        }

        public bool isExpressionCorrect() // as format
        {
            if (Operation == enMathOperation.None)
                return false;

            else if (!isLeftSideInitialized || !isRightSideInitialized)
                return false;

            else if (Operation == enMathOperation.Modulus || Operation == enMathOperation.Power)
            {
                if (!isLeftSideInitialized)
                    return false;
            }

            return true;
        }
    }
}
