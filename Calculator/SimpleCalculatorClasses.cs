using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public delegate string Operation();
    public enum numSystem
    {
        bin = 2,
        oct = 8,
        dec = 10,
        hex = 16
    }
    public static class MyConverter
    {
        public static string toDec(string s, numSystem system)
        {
            string s1;
            string s2;
            return "";
        }
        public static string fromDec(string s, numSystem system)
        {
            return "";
        }
    }
    public interface ISimpleCalc
    {
        Operation operation { get; set; }
        string problem { get; set; }
        numSystem system { get; set; }
        string Sum();
        string Subtraction();
        string Multiplication();
        string Division();
        string Pow();
    }
    public class DecSimpleCalc : ISimpleCalc
    {
        public Operation operation { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
        public numSystem system { get; set; }
        public string problem { get; set; }
        private char Operationstring;
        public char operationstring
        {
            get
            {
                return Operationstring;
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
        public DecSimpleCalc(string s)
        {
            this.problem = s;
            system = numSystem.dec;
            operationstring = s.Last(a => Constants.allOperations.Contains(a));
            int n = s.LastIndexOf(operationstring);
            x = Convert.ToDecimal(s.Substring(0, n));
            y = Convert.ToDecimal(s.Substring(n+1));
        }
        public DecSimpleCalc(decimal x, decimal y)
        {
            this.x = x;
            this.y = y;
            system = numSystem.dec;
        }
        public string Sum() => (x + y).ToString("G0");
        public string Subtraction() => (x - y).ToString("G0");
        public string Multiplication() => (x * y).ToString("G0");
        public string Division()
        {
            if (y != 0)
            {
                return (x / y).ToString("0.############################");   
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
        public string Pow() => Math.Pow((double)x, (double)y).ToString("0.############################");
    }

    public class BinSimpleCalc : ISimpleCalc
    {
        public Operation operation { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public numSystem system { get; set; }
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
        public BinSimpleCalc(string s)
        {
            this.problem = s;
            system = numSystem.bin;
            operationstring = s.Last(a => Constants.allOperations.Contains(a));
            int n = s.LastIndexOf(operationstring);
            x = s.Substring(0, n);
            y = s.Substring(n + 1);
        }
        public BinSimpleCalc(string x, string y)
        {
            this.x = x;
            this.y = y;
            system = numSystem.bin;
            
        }
        public string Sum()
        {
            return "";
        }
        public string Subtraction()
        {
            return "";
        }
        public string Multiplication()
        {
            return "";
        }
        public string Division()
        {
            if (y[0] != '0')
            {
                return "";
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
        public string Pow()
        {
            return "";
        }
    }
}
