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
        double x { get; set; }
        double y { get; set; }
        string problem { get; set; }
        double Sum();
        double Subtraction();
        double Multiplication();
        double Division();
        double Sqrt();
        double Square();
        double Pow();
        string BIN();
        string OCT();
        string DEC();
        string HEX();
    }
    public class SimpleCalc : ISimpleCalc
    {
        public delegate double Operation();
        public Operation operation;
        public double x { get; set; }
        public double y { get; set; }
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
                    case '*':
                        operation = Multiplication; break;
                    case '/':
                        operation = Division; break;
                }
                Operationstring = value;
            }
        }
        public SimpleCalc(string s)
        {
            this.problem = s;
            operationstring = s.Last(a => Constants.allOperations.Contains(a));
            int n = s.LastIndexOf(operationstring);
            x = Convert.ToDouble(s.Substring(0, n));
            y = Convert.ToDouble(s.Substring(n+1));
        }
        public SimpleCalc(double x)
        {
            this.x = x;
        }
        public SimpleCalc(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public double Sum() => x+y;
        public double Subtraction() => x-y;
        public double Multiplication() => x*y;
        public double Division()
        {
            if (y != 0)
            {
                return x/y;
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
        public double Sqrt() => Math.Sqrt(x);
        public double Square() => x*x;
        public double Pow() => Math.Pow(x, y);
        public string BIN()
        {
            return "";
        }
        public string OCT()
        {
            return "";
        }
        public string DEC()
        {
            return "";
        }
        public string HEX()
        {
            return "";
        }
    }
}
