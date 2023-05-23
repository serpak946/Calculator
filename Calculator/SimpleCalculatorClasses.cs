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
            bool sign = !s.StartsWith("-");
            if (!sign) s = s.Substring(1);
            if (s.IndexOf(",") == -1)
            {
                s += ",0";
            }

            string s1 = s.Substring(0, s.IndexOf(","));
            string s2 = s.Substring(s.IndexOf(",") + 1, s.Length - s.IndexOf(",") - 1);

            char[] tempArr = s1.ToCharArray();
            Array.Reverse(tempArr);
            s1 = new string(tempArr);
            decimal result = 0;
            int temp = 0;
            while (s1 != "")
            {
                int c;
                switch (s1[0])
                {
                    case 'A':
                        c = 10; break;
                    case 'B':
                        c = 11; break;
                    case 'C':
                        c = 12; break;
                    case 'D':
                        c = 13; break;
                    case 'E':
                        c = 14; break;
                    case 'F':
                        c = 15; break;
                    default:
                        c = Convert.ToInt32(s1[0].ToString()); break;
                }
                result += c * ((decimal)Math.Pow((int)system, temp));
                temp++;
                s1 = s1.Remove(0, 1);
            }
            temp = -1;
            while (s2 != "" && s2 != "0")
            {
                int c;
                switch (s2[0])
                {
                    case 'A':
                        c = 10; break;
                    case 'B':
                        c = 11; break;
                    case 'C':
                        c = 12; break;
                    case 'D':
                        c = 13; break;
                    case 'E':
                        c = 14; break;
                    case 'F':
                        c = 15; break;
                    default:
                        c = Convert.ToInt32(s2[0].ToString()); break;
                }
                result += c * ((decimal)Math.Pow((int)system, temp));
                temp--;
                s2 = s2.Remove(0, 1);
            }
            return sign ? result : result*(-1);
        }
        public static string fromDec(decimal number, numSystem system)
        {
            bool sign = number >= 0;
            if (!sign) number *= -1;
            int x = (int)Math.Truncate(number);
            decimal x1 = number - x;
            string s = "";
            do
            {
                decimal temp = 0 + (x % ((int)system));
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
                x /= (int)system;
            } while (x > 0);
            if (x1 != 0)
            {
                s += ",";
                for (int i = 0; i < x1.ToString().Length + (32 / ((int)system)); i++)
                {
                    x1 *= (int)system;
                    switch (Math.Truncate(x1))
                    {
                        case 10:
                            s += 'A'; break;
                        case 11:
                            s += 'B'; break;
                        case 12:
                            s += 'C'; break;
                        case 13:
                            s += 'D'; break;
                        case 14:
                            s += 'E'; break;
                        case 15:
                            s += 'F'; break;
                        default:
                            s += Math.Truncate(x1); break;
                    }
                    x1 -= Math.Truncate(x1);
                    if (x1 == 0)
                    {
                        break;
                    }
                }
            }
            return sign ? s : '-' + s;
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
                int n = s.LastIndexOf(s.Last(a => charLists.allOperations.Contains(a)));
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
        char operationstring { get; set; }
        string Sum();
        string Subtraction();
        string Multiplication();
        string Divide();
    }

    public abstract class SimpCalc : ISimpleCalc
    {
        public Operation operation { get; set; }
        public abstract string x { get; set; }
        public abstract string y { get; set; }
        public numSystem system { get; set; }
        public string problem { get; set; }
        private char Operationstring;
        public virtual char operationstring
        {
            get => Operationstring;
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
                }
                Operationstring = value;
            }
        }
        public abstract string Sum();
        public abstract string Subtraction();
        public abstract string Multiplication();
        public abstract string Divide();
        /// <summary>
        /// Добавляет незначащие нули и запятую до одинакового количества знаков
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns>Два вещественных числа одинаковой длины</returns>
        public (string x, string y) significantZeros(string number1, string number2)
        {
            // Проверка, содержатся ли десятичные разделители в числах
            bool hasDecimalPoint1 = number1.Contains(",");
            bool hasDecimalPoint2 = number2.Contains(",");

            // Если число не содержит десятичного разделителя, добавляем его и ноль после
            if (!hasDecimalPoint1)
            {
                number1 += ",0";
            }

            if (!hasDecimalPoint2)
            {
                number2 += ",0";
            }

            // Разделение чисел на целую и дробную части
            string[] parts1 = number1.Split(',');
            string[] parts2 = number2.Split(',');

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
            string paddedNumber1 = integerPart1 + "," + fractionalPart1;
            string paddedNumber2 = integerPart2 + "," + fractionalPart2;

            return (paddedNumber1, paddedNumber2);
        }
        public string RemoveTrailingZerosAndDot(string number)
        {
            // Удаление незначащих нулей слева
            number = number.TrimStart('0');

            // Удаление незначащих нулей справа
            if (number.Contains(','))
            {
                number = number.TrimEnd('0');
            }

            // Удаление точки, если она является последним символом
            if (number.EndsWith(","))
            {
                number = number.TrimEnd(',');
            }
            if (number.StartsWith(","))
            {
                number = '0' + number;
            }

            return number;
        }
    }

    public class DecSimpleCalc : SimpCalc
    {
        private decimal X { get; set; }
        private decimal Y { get; set; }
        private char Operationstring;
        public override char operationstring
        {
            get => Operationstring;
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
        public override string x
        {
            get => X.ToString();
            set => X = Convert.ToDecimal(value);
        }
        public override string y
        {
            get => Y.ToString();
            set => Y = Convert.ToDecimal(value);
        }
        public DecSimpleCalc(string s)
        {
            problem = s;
            system = numSystem.dec;
            X = Convert.ToDecimal(MyConverter.fromString(s).x);
            Y = Convert.ToDecimal(MyConverter.fromString(s).y);
            operationstring = MyConverter.fromString(s).operation;
        }
        public DecSimpleCalc(decimal X, decimal Y)
        {
            this.X = X;
            this.Y = Y;
            system = numSystem.dec;
        }
        public override string Sum()
        {
            return (X + Y).ToString("G0");
        }

        public override string Subtraction()
        {
            return (X - Y).ToString("G0");
        }

        public override string Multiplication()
        {
            return (X * Y).ToString("G0");
        }

        public override string Divide()
        {
            return Y != 0 ? (X / Y).ToString("0.############################") : throw new DivideByZeroException();
        }
        public string Pow()
        {
            return Math.Pow((double)X, (double)Y).ToString("0.############################");
        }
    }

    public class BinSimpleCalc : SimpCalc
    {
        public override string x { get; set; }
        public override string y { get; set; }
        public BinSimpleCalc(string s)
        {
            if (s.ToCharArray().Except(charLists.binChar).Any())
            {
                throw new ArgumentException("Недопустимые символы");
            }

            problem = s;
            system = numSystem.bin;
            (x, y, operationstring) = MyConverter.fromString(s);
            if (y == string.Empty) throw new FormatException();
        }
        public BinSimpleCalc(string x, string y)
        {
            this.x = x;
            this.y = y;
            system = numSystem.bin;
        }
        public override string Sum()
        {
            if (x[0] == '-')
            {
                return Subtraction(y, x.Substring(1, x.Length - 1));
            }

            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = ((Convert.ToInt64(x1[i]) + Convert.ToInt64(y1[i]) + temp) % 2) + result;
                temp = (Convert.ToInt64(x1[i].ToString()) + Convert.ToInt64(y1[i].ToString()) + temp) / 2;
            }
            result = temp + result;
            result = result.Insert(result.Length - n, ",");
            return RemoveTrailingZerosAndDot(result);
        }
        private string Sum(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = ((Convert.ToInt64(x1[i]) + Convert.ToInt64(y1[i]) + temp) % 2) + result;
                temp = (Convert.ToInt64(x1[i].ToString()) + Convert.ToInt64(y1[i].ToString()) + temp) / 2;
            }
            result = temp + result;
            result = result.Insert(result.Length - n, ",");
            return RemoveTrailingZerosAndDot(result);
        }
        public override string Subtraction()
        {
            if (x[0] == '-')
            {
                return "-" + Sum(x.Substring(1, x.Length - 1), y);
            }

            (string x1, string y1) = significantZeros(x, y);
            if (x1 == y1)
            {
                return "0";
            }

            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            bool sign = Convert.ToInt64(x1) >= Convert.ToInt64(y1);
            if (!sign)
            {
                (x1, y1) = (y1, x1);
            }

            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                long digitX = Convert.ToInt64(x1[i].ToString());
                long digitY = Convert.ToInt64(y1[i].ToString());

                long subtractedDigit = digitX - digitY - temp;
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
            result = result.Insert(result.Length - n, ",");
            return sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
        }
        public string Subtraction(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            if (x1 == y1)
            {
                return "0";
            }

            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            bool sign = Convert.ToInt64(x1) >= Convert.ToInt64(y1);
            if (!sign)
            {
                (x1, y1) = (y1, x1);
            }

            string result = string.Empty;
            int temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                long digitX = Convert.ToInt64(x1[i].ToString());
                long digitY = Convert.ToInt64(y1[i].ToString());

                long subtractedDigit = digitX - digitY - temp;
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
            result = result.Insert(result.Length - n, ",");
            result = sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
            return result != string.Empty ? result : "0";
        }
        public override string Multiplication()
        {
            if (x == "0" || y == "0") return "0";
            bool sign = true;
            string x1, y1;
            if (x[0] == '-')
            {
                sign = false;
                (x1, y1) = significantZeros(x.Substring(1, x.Length - 1), y);
            }
            else
            {
                (x1, y1) = significantZeros(x, y);
            }

            int xDecimalPos = x1.IndexOf(',');
            int yDecimalPos = y1.IndexOf(',');
            int n = ((x1.Length - xDecimalPos) * 2) - 2;
            x1 = x1.Replace(",", "");
            y1 = y1.Replace(",", "");
            string result = string.Empty;

            for (int i = y1.Length - 1; i >= 0; i--)
            {
                long digitY = Convert.ToInt64(y1[i].ToString());
                string partialResult = string.Empty;
                long carry = 0;

                for (int j = x1.Length - 1; j >= 0; j--)
                {
                    long digitX = Convert.ToInt64(x1[j].ToString());
                    long product = (digitX * digitY) + carry;
                    long remainder = product % 2;
                    carry = product / 2;
                    partialResult = remainder + partialResult;
                }
                if (carry > 0)
                {
                    partialResult = carry + partialResult;
                }
                int decimalPos = y1.Length - 1 - i + (x1.Length - xDecimalPos - 1);
                partialResult = partialResult.PadRight(decimalPos + partialResult.Length, '0');
                result = Sum(result, partialResult);
            }
            if (result.Length - n < 0)
            {
                result = string.Concat(Enumerable.Repeat("0", n)) + result;
                result = result.Insert(1, ",");
            }
            else
                result = result.Insert(result.Length - n, ",");
            result = RemoveTrailingZerosAndDot(result);
            return !sign ? "-" + result : result;
        }
        public override string Divide()
        {
            if (y == "0")
            {
                throw new DivideByZeroException();
            }

            string result = string.Empty;
            bool sign = x[0] != '-';
            int accuracy = 20;


            string x1;
            string y1;
            if (y.IndexOf(',') != -1)
            {
                int tempy = y.Length - y.IndexOf(",") - 1;
                int tempx = x.IndexOf(",") == -1 ? x.Length : x.IndexOf(",");
                y1 = y.Replace(",", "");
                x1 = x.Replace(",", "");
                x1 += "00000000000000000000000";
                if (tempx + tempy != x1.Length)
                {
                    x1 = x1.Insert(tempx + tempy, ",");
                }
            }
            else
            {
                y1 = y;
                x1 = x.IndexOf(',') == -1 ? x + ",00000000000000000000000" : x + "00000000000000000000000";
            }

            long tempY = Convert.ToInt64(y1);
            long tempX = Convert.ToInt64(x1[0].ToString());
            x1 = x1.Substring(1);
            for (int i = 0; i < accuracy; i++)
            {
                if (tempX < tempY)
                {
                    if (x1[0] != ',')
                    {
                        result += '0';
                        tempX = (tempX * 10) + Convert.ToInt64(x1[0].ToString());
                        x1 = x1.Substring(1);
                    }
                    else
                    {
                        result += "0,";
                        tempX = (tempX * 10) + Convert.ToInt64(x1[1].ToString());
                        x1 = x1.Substring(2);
                    }
                }
                else
                {
                    result += '1';
                    tempX = Convert.ToInt64(Subtraction(tempX.ToString(), tempY.ToString()));
                    if (x1[0] != ',')
                    {
                        tempX = (tempX * 10) + Convert.ToInt64(x1[0].ToString());
                        x1 = x1.Substring(1);
                    }
                    else
                    {
                        result += ",";
                        tempX = (tempX * 10) + Convert.ToInt64(x1[1].ToString());
                        x1 = x1.Substring(2);
                    }
                }
                if (x1.IndexOf('1') == -1 && tempX == 0)
                {
                    if (x1.Contains(","))
                    {
                        result = string.Concat(result, string.Concat(Enumerable.Repeat("0", x1.IndexOf(',') + 1)));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (x1.IndexOf(',') != -1)
                {
                    i--;
                }
            }
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : '-' + result;
        }
    }

    public class OctSimpleCalc : SimpCalc
    {
        public override string x { get; set; }
        public override string y { get; set; }
        public OctSimpleCalc(string s)
        {
            if (s.ToCharArray().Except(charLists.octChar).Any())
            {
                throw new ArgumentException("Недопустимые символы");
            }

            problem = s;
            system = numSystem.oct;
            (x, y, operationstring) = MyConverter.fromString(s);
            if (y == string.Empty) throw new FormatException();
        }
        public OctSimpleCalc(string x, string y)
        {
            this.x = x;
            this.y = y;
            system = numSystem.oct;
        }
        public override string Sum()
        {
            if (x[0] == '-')
            {
                return Subtraction(y, x.Substring(1, x.Length - 1));
            }

            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = ((Convert.ToInt64(x1[i]) + Convert.ToInt64(y1[i]) + temp) % 8) + result;
                temp = (Convert.ToInt64(x1[i].ToString()) + Convert.ToInt64(y1[i].ToString()) + temp) / 8;
            }
            result = temp + result;
            result = result.Insert(result.Length - n, ",");
            return RemoveTrailingZerosAndDot(result);
        }
        private string Sum(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = ((Convert.ToInt64(x1[i]) + Convert.ToInt64(y1[i]) + temp) % 8) + result;
                temp = (Convert.ToInt64(x1[i].ToString()) + Convert.ToInt64(y1[i].ToString()) + temp) / 8;
            }
            result = temp + result;
            result = result.Insert(result.Length - n, ",");
            return RemoveTrailingZerosAndDot(result);
        }

        public override string Subtraction()
        {
            if (x[0] == '-')
            {
                return "-" + Sum(x.Substring(1, x.Length - 1), y);
            }

            (string x1, string y1) = significantZeros(x, y);
            if (x1 == y1)
            {
                return "0";
            }

            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            bool sign = Convert.ToInt64(x1) >= Convert.ToInt64(y1);
            if (!sign)
            {
                (x1, y1) = (y1, x1);
            }

            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                long digitX = Convert.ToInt64(x1[i].ToString());
                long digitY = Convert.ToInt64(y1[i].ToString());

                long subtractedDigit = digitX - digitY - temp;
                if (subtractedDigit < 0)
                {
                    subtractedDigit += 8;
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                result = subtractedDigit + result;
            }
            result = result.Insert(result.Length - n, ",");
            return sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
        }
        public string Subtraction(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            if (x1 == y1)
            {
                return "0";
            }

            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            bool sign = Convert.ToInt64(x1) >= Convert.ToInt64(y1);
            if (!sign)
            {
                (x1, y1) = (y1, x1);
            }

            string result = string.Empty;
            int temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                long digitX = Convert.ToInt64(x1[i].ToString());
                long digitY = Convert.ToInt64(y1[i].ToString());

                long subtractedDigit = digitX - digitY - temp;
                if (subtractedDigit < 0)
                {
                    subtractedDigit += 8;
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                result = subtractedDigit + result;
            }
            result = result.Insert(result.Length - n, ",");
            result = sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
            return result != string.Empty ? result : "0";
        }
        public override string Multiplication()
        {
            if (x == "0" || y == "0") return "0";
            bool sign = x[0] != '-';
            x = x.Replace("-", "");
            (string x1, string y1) = significantZeros(x, y);
            int tempDot = y1.Length - y1.IndexOf(",") - 1 + (x1.Length - x1.IndexOf(",") - 1);
            x1 = x1.Replace(",", "");
            y1 = y1.Replace(",", "");
            string result = "0";
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < Convert.ToInt32(y1[i].ToString()); j++)
                {
                    result = Sum(result, string.Concat(x1, string.Concat(Enumerable.Repeat("0", (-1) * (i - x1.Length + 1)))));
                }
            }
            if (result.Length - tempDot < 0)
            {
                result = string.Concat(Enumerable.Repeat("0", tempDot)) + result;
                result = result.Insert(1, ",");
            }
            else
                result = result.Insert(result.Length - tempDot, ",");
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : "-" + result;
        }

        public string Multiplication(string x, string y)
        {
            if (x == "0" || y == "0") return "0";
            bool sign = x[0] != '-';
            x = x.Replace("-", "");
            (string x1, string y1) = significantZeros(x, y);
            int tempDot = y1.Length - y1.IndexOf(",") - 1 + (x1.Length - x1.IndexOf(",") - 1);
            x1 = x1.Replace(",", "");
            y1 = y1.Replace(",", "");
            string result = "0";
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < Convert.ToInt32(y1[i].ToString()); j++)
                {
                    result = Sum(result, string.Concat(x1, string.Concat(Enumerable.Repeat("0", (-1) * (i - x1.Length + 1)))));
                }
            }
            result = result.Insert(result.Length - tempDot, ",");
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : "-" + result;
        }

        public override string Divide()
        {
            if (y == "0")
            {
                throw new DivideByZeroException();
            }

            string result = string.Empty;
            bool sign = x[0] != '-';
            int accuracy = 20;


            string x1;
            string y1;
            if (y.IndexOf(',') != -1)
            {
                int tempy = y.Length - y.IndexOf(",") - 1;
                int tempx = x.IndexOf(",") == -1 ? x.Length : x.IndexOf(",");
                y1 = y.Replace(",", "");
                x1 = x.Replace(",", "");
                x1 += "00000000000000000000000";
                if (tempx + tempy != x1.Length)
                {
                    x1 = x1.Insert(tempx + tempy, ",");
                }
            }
            else
            {
                y1 = y;
                x1 = x.IndexOf(',') == -1 ? x + ",00000000000000000000000" : x + "00000000000000000000000";
            }

            long tempY = Convert.ToInt64(y1);
            long tempX = Convert.ToInt64(x1[0].ToString());
            x1 = x1.Substring(1);
            for (int i = 0; i < accuracy; i++)
            {
                if (tempX < tempY)
                {
                    if (x1[0] != ',')
                    {
                        result += '0';
                        tempX = (tempX * 10) + Convert.ToInt64(x1[0].ToString());
                        x1 = x1.Substring(1);
                    }
                    else
                    {
                        result += "0,";
                        tempX = (tempX * 10) + Convert.ToInt64(x1[1].ToString());
                        x1 = x1.Substring(2);
                    }
                }
                else
                {
                    int tempq = 1;
                    for (int j = 7; j >= 1; j--)
                    {
                        if (Convert.ToInt64(Multiplication(tempY.ToString(), j.ToString())) <= tempX)
                        {
                            result += j.ToString();
                            tempq = j;
                            break;
                        }
                    }
                    tempX = Convert.ToInt64(Subtraction(tempX.ToString(), Multiplication(tempY.ToString(), tempq.ToString())));
                    if (x1[0] != ',')
                    {
                        tempX = (tempX * 10) + Convert.ToInt64(x1[0].ToString());
                        x1 = x1.Substring(1);
                    }
                    else
                    {
                        result += ",";
                        tempX = (tempX * 10) + Convert.ToInt64(x1[1].ToString());
                        x1 = x1.Substring(2);
                    }
                }
                if (!x1.Contains("1") && !x1.Contains("2") && !x1.Contains("3") && !x1.Contains("4") && !x1.Contains("5") && !x1.Contains("6") && !x1.Contains("7") && tempX == 0)
                {
                    if (x1.Contains(","))
                    {
                        result = string.Concat(result, string.Concat(Enumerable.Repeat("0", x1.IndexOf(',') + 1)));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (x1.IndexOf(',') != -1)
                {
                    i--;
                }
            }
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : '-' + result;
        }
    }
    public class HexSimpleCalc : SimpCalc
    {
        public override string x { get; set; }
        public override string y { get; set; }
        public HexSimpleCalc(string s)
        {
            if (s.ToCharArray().Except(charLists.hexChar).Any())
            {
                throw new ArgumentException("Недопустимые символы");
            }

            problem = s;
            system = numSystem.hex;
            (x, y, operationstring) = MyConverter.fromString(s);
            if (y == string.Empty) throw new FormatException();
        }
        public HexSimpleCalc(string x, string y)
        {
            this.x = x;
            this.y = y;
            system = numSystem.hex;
        }
        private long hexToLong(char s)
        {
            switch (s)
            {
                case 'A':
                    return 10;
                case 'B':
                    return 11;
                case 'C':
                    return 12;
                case 'D':
                    return 13;
                case 'E':
                    return 14;
                case 'F':
                    return 15;
                default:
                    return Convert.ToInt64(s.ToString());
            }
        }
        private string longToHex(long s)
        {
            switch (s)
            {
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return s.ToString();
            }
        }

        public override string Sum()
        {
            if (x[0] == '-')
            {
                return Subtraction(y, x.Substring(1, x.Length - 1));
            }

            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = longToHex((hexToLong(x1[i]) + hexToLong(y1[i]) + temp) % 16) + result;
                temp = (hexToLong(x1[i]) + hexToLong(y1[i]) + temp) / 16;
            }
            result = longToHex(temp) + result;
            result = result.Insert(result.Length - n, ",");
            return RemoveTrailingZerosAndDot(result);
        }

        public string Sum(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                result = longToHex((hexToLong(x1[i]) + hexToLong(y1[i]) + temp) % 16) + result;
                temp = (hexToLong(x1[i]) + hexToLong(y1[i]) + temp) / 16;
            }
            result = longToHex(temp) + result;
            result = result.Insert(result.Length - n, ",");
            return RemoveTrailingZerosAndDot(result);
        }

        public override string Subtraction()
        {
            if (x[0] == '-')
            {
                return "-" + Sum(x.Substring(1, x.Length - 1), y);
            }

            (string x1, string y1) = significantZeros(x, y);
            if (x1 == y1)
            {
                return "0";
            }

            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            bool sign = Convert.ToInt64(x1, 16) >= Convert.ToInt64(y1, 16);
            if (!sign)
            {
                (x1, y1) = (y1, x1);
            }

            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                //long digitX = Convert.ToInt64(x1[i].ToString());
                //long digitY = Convert.ToInt64(y1[i].ToString());
                long digitX = hexToLong(x1[i]);
                long digitY = hexToLong(y1[i]);
                long subtractedDigit = digitX - digitY - temp;
                if (subtractedDigit < 0)
                {
                    subtractedDigit += 16;
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                result = longToHex(subtractedDigit) + result;
            }
            result = result.Insert(result.Length - n, ",");
            return sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
        }
        public string Subtraction(string x, string y)
        {
            (string x1, string y1) = significantZeros(x, y);
            if (x1 == y1)
            {
                return "0";
            }

            int n = x1.Length - x1.IndexOf(',') - 1;
            x1 = x1.Replace(",", ""); y1 = y1.Replace(",", "");
            bool sign = Convert.ToInt64(x1, 16) >= Convert.ToInt64(y1, 16);
            if (!sign)
            {
                (x1, y1) = (y1, x1);
            }

            string result = string.Empty;
            long temp = 0;
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                long digitX = hexToLong(x1[i]);
                long digitY = hexToLong(y1[i]);

                long subtractedDigit = digitX - digitY - temp;
                if (subtractedDigit < 0)
                {
                    subtractedDigit += 16;
                    temp = 1;
                }
                else
                {
                    temp = 0;
                }

                result = longToHex(subtractedDigit) + result;
            }
            result = result.Insert(result.Length - n, ",");
            return sign ? RemoveTrailingZerosAndDot(result) : "-" + RemoveTrailingZerosAndDot(result);
        }

        public override string Multiplication()
        {
            if (x == "0" || y == "0") return "0";
            bool sign = x[0] != '-';
            x = x.Replace("-", "");
            (string x1, string y1) = significantZeros(x, y);
            int tempDot = y1.Length - y1.IndexOf(",") - 1 + (x1.Length - x1.IndexOf(",") - 1);
            x1 = x1.Replace(",", "");
            y1 = y1.Replace(",", "");
            string result = "0";
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < hexToLong(y1[i]); j++)
                {
                    result = Sum(result, string.Concat(x1, string.Concat(Enumerable.Repeat("0", (-1) * (i - x1.Length + 1)))));
                }
            }
            if (result.Length - tempDot < 0)
            {
                result = string.Concat(Enumerable.Repeat("0", tempDot)) + result;
                result = result.Insert(1, ",");
            }
            else
                result = result.Insert(result.Length - tempDot, ",");
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : "-" + result;
        }

        public string Multiplication(string x, string y)
        {
            if (x == "0" || y == "0") return "0";
            bool sign = x[0] != '-';
            x = x.Replace("-", "");
            (string x1, string y1) = significantZeros(x, y);
            int tempDot = y1.Length - y1.IndexOf(",") - 1 + (x1.Length - x1.IndexOf(",") - 1);
            x1 = x1.Replace(",", "");
            y1 = y1.Replace(",", "");
            string result = "0";
            for (int i = x1.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < hexToLong(y1[i]); j++)
                {
                    result = Sum(result, string.Concat(x1, string.Concat(Enumerable.Repeat("0", (-1) * (i - x1.Length + 1)))));
                }
            }
            result = result.Insert(result.Length - tempDot, ",");
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : "-" + result;
        }

        public override string Divide()
        {
            if (y == "0")
            {
                throw new DivideByZeroException();
            }

            string result = string.Empty;
            bool sign = x[0] != '-';
            int accuracy = 20;


            string x1;
            string y1;
            if (y.IndexOf(',') != -1)
            {
                int tempy = y.Length - y.IndexOf(",") - 1;
                int tempx = x.IndexOf(",") == -1 ? x.Length : x.IndexOf(",");
                y1 = y.Replace(",", "");
                x1 = x.Replace(",", "");
                x1 += "00000000000000000000000";
                if (tempx + tempy != x1.Length)
                {
                    x1 = x1.Insert(tempx + tempy, ",");
                }
            }
            else
            {
                y1 = y;
                x1 = x.IndexOf(',') == -1 ? x + ",00000000000000000000000" : x + "00000000000000000000000";
            }

            string tempY = y1;
            string tempX = x1[0].ToString();
            x1 = x1.Substring(1);
            for (int i = 0; i < accuracy; i++)
            {
                if (Subtraction(tempX, tempY)[0] == '-')
                {
                    if (x1[0] != ',')
                    {
                        result += '0';
                        tempX += x1[0];
                        x1 = x1.Substring(1);
                    }
                    else
                    {
                        result += "0,";
                        tempX += x1[1];
                        x1 = x1.Substring(2);
                    }
                }
                else
                {
                    int tempq = 1;
                    for (int j = 15; j >= 1; j--)
                    {
                        if (Convert.ToInt64(Multiplication(tempY.ToString(), longToHex(j)), 16) <= Convert.ToInt64(tempX, 16))
                        {
                            result += longToHex(j);
                            tempq = j;
                            break;
                        }
                    }
                    tempX = Subtraction(tempX.ToString(), Multiplication(tempY.ToString(), longToHex(tempq)));

                    if (x1[0] != ',')
                    {
                        tempX += x1[0];
                        x1 = x1.Substring(1);
                    }
                    else
                    {
                        result += ",";
                        tempX += x[1];
                        x1 = x1.Substring(2);
                    }
                }
                if (!x1.Contains("1") && !x1.Contains("2") && !x1.Contains("3") && !x1.Contains("4") && !x1.Contains("5") && !x1.Contains("6") && !x1.Contains("7") && !x1.Contains("8") && !x1.Contains("9") && !x1.Contains("A") && !x1.Contains("B") && !x1.Contains("C") && !x1.Contains("D") && !x1.Contains("E") && !x1.Contains("F") && tempX.Replace("0", "").Replace(",", "") == string.Empty)
                {
                    if (x1.Contains(","))
                    {
                        result = string.Concat(result, string.Concat(Enumerable.Repeat("0", x1.IndexOf(',') + 1)));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (x1.IndexOf(',') != -1)
                {
                    i--;
                }
            }
            result = RemoveTrailingZerosAndDot(result);
            return sign ? result : '-' + result;
        }
    }
}