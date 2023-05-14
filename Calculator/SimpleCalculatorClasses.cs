using System;
using System.Linq;

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
            return Convert.ToInt32(s.Split(',')[0], ((int)system));
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
        string Divide();
        string Pow();
    }

    public abstract class SimpCalc : ISimpleCalc
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
                        operation = Divide; break;
                    case '^':
                        operation = Pow; break;
                }
                Operationstring = value;
            }
        }
        public abstract string Sum();

        public abstract string Subtraction();
        public abstract string Multiplication();
        public abstract string Divide();
        public abstract string Pow();
    }

    public class DecSimpleCalc : SimpCalc
    {
        public new decimal x, y;
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
        public override string Sum() => (x + y).ToString("G0");
        public override string Subtraction() => (x - y).ToString("G0");
        public override string Multiplication() => (x * y).ToString("G0");
        public override string Divide()
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
        public override string Pow() => Math.Pow((double)x, (double)y).ToString("0.############################");
    }

    public class BinSimpleCalc : SimpCalc
    {
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
        public override string Sum()
        {
            if (x[0] == '-') return Subtraction(y, x.Substring(1, x.Length-1));
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf('.') - 1;
            x1 = x1.Replace(".", ""); y1 = y1.Replace(".", "");
            string result = string.Empty;
            int temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = ((Convert.ToInt32(x1[i]) + Convert.ToInt32(y1[i]) + temp) % 2) + result;
                temp = ((Convert.ToInt32(x1[i].ToString()) + Convert.ToInt32(y1[i].ToString()) + temp) / 2);
            }
            result = temp + result;
            result = result.Insert(result.Length - n, ".");
            return RemoveTrailingZerosAndDot(result);
        }
        private string Sum(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf('.') - 1;
            x1 = x1.Replace(".", ""); y1 = y1.Replace(".", "");
            string result = string.Empty;
            int temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = ((Convert.ToInt32(x1[i]) + Convert.ToInt32(y1[i]) + temp) % 2) + result;
                temp = ((Convert.ToInt32(x1[i].ToString()) + Convert.ToInt32(y1[i].ToString()) + temp) / 2);
            }
            result = temp + result;
            result = result.Insert(result.Length - n, ".");
            return RemoveTrailingZerosAndDot(result);
        }
        public override string Subtraction()
        {
            if (x[0] == '-') return "-"+Sum(x.Substring(1, x.Length-1), y);
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf('.') - 1;
            x1 = x1.Replace(".", ""); y1 = y1.Replace(".", "");
            bool sign = (Convert.ToInt32(x1) >= Convert.ToInt32(y1));
            if (!sign) (x1, y1) = (y1, x1);
            string result = string.Empty;
            int temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                int digitX = Convert.ToInt32(x1[i].ToString());
                int digitY = Convert.ToInt32(y1[i].ToString());

                int subtractedDigit = digitX - digitY - temp;
                if (subtractedDigit < 0)
                {
                    subtractedDigit += 2;
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                result = subtractedDigit + result;
            }
            result = result.Insert(result.Length - n, ".");
            return sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
        }
        public string Subtraction(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf('.') - 1;
            x1 = x1.Replace(".", ""); y1 = y1.Replace(".", "");
            bool sign = (Convert.ToInt32(x1) >= Convert.ToInt32(y1));
            if (!sign) (x1, y1) = (y1, x1);
            string result = string.Empty;
            int temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                int digitX = Convert.ToInt32(x1[i].ToString());
                int digitY = Convert.ToInt32(y1[i].ToString());

                int subtractedDigit = digitX - digitY - temp;
                if (subtractedDigit < 0)
                {
                    subtractedDigit += 2;
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                result = subtractedDigit + result;
            }
            result = result.Insert(result.Length - n, ".");
            return sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
        }
        public override string Multiplication()
        {
            bool sign = true;
            string x1, y1;
            if (x[0] == '-')
            {
                sign = false;
                (x1, y1) = significantZeros(x.Substring(1, x.Length-1), y);
            }
            else
                (x1, y1) = significantZeros(x, y);
            int xDecimalPos = x1.IndexOf('.');
            int yDecimalPos = y1.IndexOf('.');
            int n = (x1.Length - xDecimalPos) * 2 - 1;
            x1 = x1.Replace(".", "");
            y1 = y1.Replace(".", "");
            string result = string.Empty;

            for (int i = y1.Length - 1; i >= 0; i--)
            {
                int digitY = Convert.ToInt32(y1[i].ToString());
                string partialResult = string.Empty;
                int carry = 0;

                for (int j = x1.Length - 1; j >= 0; j--)
                {
                    int digitX = Convert.ToInt32(x1[j].ToString());
                    int product = (digitX * digitY) + carry;
                    int remainder = product % 2;
                    carry = product / 2;
                    partialResult = remainder + partialResult;
                }

                if (carry > 0)
                {
                    partialResult = carry + partialResult;
                }

                int decimalPos = (y1.Length - 1 - i) + (x1.Length - xDecimalPos - 1);
                partialResult = partialResult.PadRight(decimalPos + partialResult.Length, '0');
                result = Sum(result, partialResult);
            }

            result = result.Insert(result.Length - n, ".");
            result = RemoveTrailingZerosAndDot(result);
            return !sign?"-"+result:result;
        }

        public override string Divide()
        {
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf('.') - 1;
            x1 = x1.Replace(".", ""); y1 = y1.Replace(".", "");
            string result = string.Empty;
            int decimalPointIndex = 0;
            bool decimalPointInserted = false;

            // Check if divisor is zero
            if (y1 == "0")
            {
                throw new DivideByZeroException("Divisor cannot be zero.");
            }

            // Initialize dividend and remainder
            string dividend = string.Empty;
            string remainder = string.Empty;

            // Divide the integer part of the dividend by the divisor
            for (int i = 0; i < x1.Length; i++)
            {
                dividend += x1[i].ToString();
                int quotient = 0;
                while (IsGreaterThanOrEqual(dividend, y1))
                {
                    dividend = Subtraction(dividend, y1);
                    quotient++;
                }
                result += quotient.ToString();
            }

            // Insert decimal point
            result = result.Insert(result.Length - n, ".");
            decimalPointIndex = result.IndexOf('.');

            // Divide the fractional part of the dividend by the divisor
            for (int i = 0; i < n; i++)
            {
                dividend += "0";
                int quotient = 0;
                while (IsGreaterThanOrEqual(dividend, y1))
                {
                    dividend = Subtraction(dividend, y1);
                    quotient++;
                }
                result += quotient.ToString();

                // Check if the fractional part is repeating
                if (dividend == "0")
                {
                    break;
                }
                else if (!decimalPointInserted)
                {
                    remainder = dividend;
                    dividend = string.Empty;
                    result += ".";
                    decimalPointInserted = true;
                }
                else if (dividend == remainder)
                {
                    result = result.Insert(decimalPointIndex + i + 2, "(");
                    result += ")";
                    break;
                }
            }

            // Remove trailing zeros and decimal point if necessary
            result = RemoveTrailingZerosAndDot(result);

            return result;
        }
        private bool IsGreaterThanOrEqual(string x, string y)
        {
            // Pad x and y with leading zeros to make them the same length
            int maxLength = Math.Max(x.Length, y.Length);
            x = x.PadLeft(maxLength, '0');
            y = y.PadLeft(maxLength, '0');

            // Compare x and y
            for (int i = 0; i < maxLength; i++)
            {
                if (x[i] > y[i])
                {
                    return true;
                }
                else if (x[i] < y[i])
                {
                    return false;
                }
            }

            // x and y are equal
            return true;
        }
        public override string Pow()
        {
            return "";
        }
        private (string x, string y) significantZeros(string binaryNumber1, string binaryNumber2)
        {
            // Проверка, содержатся ли десятичные разделители в числах
            bool hasDecimalPoint1 = binaryNumber1.Contains(".");
            bool hasDecimalPoint2 = binaryNumber2.Contains(".");

            // Если число не содержит десятичного разделителя, добавляем его и ноль после
            if (!hasDecimalPoint1)
                binaryNumber1 += ".0";

            if (!hasDecimalPoint2)
                binaryNumber2 += ".0";

            // Разделение чисел на целую и дробную части
            string[] parts1 = binaryNumber1.Split('.');
            string[] parts2 = binaryNumber2.Split('.');

            // Получение целой и дробной частей для каждого числа
            string integerPart1 = parts1[0];
            string fractionalPart1 = parts1[1];
            string integerPart2 = parts2[0];
            string fractionalPart2 = parts2[1];

            // Вычисление максимальной длины целой и дробной частей
            int maxIntegerPartLength = Math.Max(integerPart1.Length, integerPart2.Length);
            int maxFractionalPartLength = Math.Max(fractionalPart1.Length, fractionalPart2.Length);

            // Добавление незначащих нулей для целой части первого числа
            integerPart1 = integerPart1.PadLeft(maxIntegerPartLength, '0');

            // Добавление незначащих нулей для дробной части первого числа
            fractionalPart1 = fractionalPart1.PadRight(maxFractionalPartLength, '0');

            // Добавление незначащих нулей для целой части второго числа
            integerPart2 = integerPart2.PadLeft(maxIntegerPartLength, '0');

            // Добавление незначащих нулей для дробной части второго числа
            fractionalPart2 = fractionalPart2.PadRight(maxFractionalPartLength, '0');

            // Склеивание чисел с добавленными нулями
            string paddedNumber1 = integerPart1 + "." + fractionalPart1;
            string paddedNumber2 = integerPart2 + "." + fractionalPart2;

            return (paddedNumber1, paddedNumber2);
        }
        private string RemoveTrailingZerosAndDot(string number)
        {
            // Удаление незначащих нулей слева
            number = number.TrimStart('0');

            // Удаление незначащих нулей справа
            number = number.TrimEnd('0');

            // Удаление точки, если она является последним символом
            if (number.EndsWith("."))
            {
                number = number.TrimEnd('.');
            }

            return number;
        }

    }
}
