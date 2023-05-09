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
        public static decimal toDec(string s, numSystem system)
        {
            return Convert.ToInt32(s, ((int)system));
        }
        public static string fromDec(decimal number, numSystem system)
        {
            int x = (int)Math.Truncate(number);
            string s = "";
            do
            {
                int temp = 0 + x % ((int)system);
                switch (temp)
                {
                    case 10:
                        s = 'A' + s; break;
                    case 11:
                        s = 'B' + s; break;
                    case 12:
                        s = 'C' + s; break;
                    case 13:
                        s = 'D' + s; break;
                    case 14:
                        s = 'E' + s; break;
                    case 15:
                        s = 'F' + s; break;
                    default:
                        s = temp + s; break;
                }
                x = x / ((int)system);
            } while (x > 0);
            return s;
        }
        /// <summary>
        /// Метод, который переводит текст в два числа и операцию, если одно число, то в качестве операции возвращает '!'
        /// </summary>
        /// <param name="s">Текстовая строка примера</param>
        /// <returns>два числа и операция в типе char</returns>
        public static (string x, string y, char operation) fromString(string s)
        {
            try
            {
                int n = s.LastIndexOf(s.Last(a => Constants.allOperations.Contains(a)));
                string x = s.Substring(0, n);
                string y = s.Substring(n + 1);
                return (x, y, s[n]);
            }
            catch (InvalidOperationException)
            {
                return (s, s, '!');
            }
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
            x = Convert.ToDecimal(MyConverter.fromString(s).x);
            y = Convert.ToDecimal(MyConverter.fromString(s).y);
            operationstring = MyConverter.fromString(s).operation;
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
            operationstring = MyConverter.fromString(s).operation;
            x = MyConverter.fromString(s).x.ToString();
            y = MyConverter.fromString(s).y.ToString();
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
