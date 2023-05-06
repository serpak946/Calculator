using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    interface ISimpleCalc
    {
        decimal x { get; set; }
        decimal y { get; set; }
        string problem { get; set; }
        decimal Sum();
        decimal Subtraction();
        decimal Multiplication();
        decimal Division();
        decimal Pow();
        string BIN();
        string OCT();
        string DEC();
        string HEX();
    }
    public class SimpleCalc : ISimpleCalc
    {
        public delegate decimal Operation();
        public Operation operation;
        public decimal x { get; set; }
        public decimal y { get; set; }
        public string problem { get; set; }
        private char Operationstring;
        public char operationstring
        {
            get
            {
                return this.Operationstring;
            }
            set
            {
                switch (value)
                {
                    case '+':
                        operation = Sum; break;
                    case '-':
                        operation = Subtraction; break;
                    case '×':
                        operation = Multiplication; break;
                    case '÷':
                        operation = Division; break;
                    case '^':
                        operation = Pow; break;
                }
                Operationstring = value;
            }
        }
        public SimpleCalc(string s)
        {
            this.problem = s;
            operationstring = s.Last(a => Constants.allOperations.Contains(a));
            int n = s.LastIndexOf(operationstring);
            x = Convert.ToDecimal(s.Substring(0, n));
            y = Convert.ToDecimal(s.Substring(n+1));
        }
        public SimpleCalc(decimal x)
        {
            this.x = x;
        }
        public SimpleCalc(decimal x, decimal y)
        {
            this.x = x;
            this.y = y;
        }
        public decimal Sum() => Convert.ToDecimal((x + y).ToString("G0"));
        public decimal Subtraction() => Convert.ToDecimal((x - y).ToString("G0"));
        public decimal Multiplication() => Convert.ToDecimal((x * y).ToString("G0"));
        public decimal Division()
        {
            if (y != 0)
            {
                return Convert.ToDecimal((x / y).ToString("0.############################"));   
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
        public decimal Pow() => (decimal)Math.Pow((double)x, (double)y);
        public string BIN()
        {
            return "bin";
        }
        public string OCT()
        {
            return "oct";
        }
        public string DEC()
        {
            return "dec";
        }
        public string HEX()
        {
            return "hex";
        }
    }
}
