using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Calculator
{
    public static class Constants
    {
        public static List<char> decChar { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<char> binChar { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '0', '1', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<char> octChar { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '0', '1', '2', '3', '4', '5', '6', '7', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<char> hexChar { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<Keys> keys { get; } = new List<Keys>() { Keys.Enter, Keys.Back, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.Add, Keys.Multiply, Keys.Divide, Keys.Oemcomma, Keys.OemPeriod, Keys.Subtract, (Keys)Enum.Parse(typeof(Keys), "Oem5") };
        public static List<char> allOperations { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '÷', '×' };
        public static List<char> decimalOperations { get; } = new List<char>() { '+', '-', '*', '/', '^', '÷', '×' };
    }

    interface IHistory
    {
        string problem { get; set; }
        string answer { get; set; }
        numSystem system { get; set; }
    }
    public class History : IHistory
    {
        public string problem { get; set; }
        public string answer { get; set; }
        public numSystem system { get; set; }
        public History(string problem, string answer, ListBox list)
        {
            this.problem = problem;
            this.answer = answer;
            this.system = system;
            if (!list.Items.Contains(this))
            {
                list.Items.Insert(0, this);
            }
        }
        public History(ISimpleCalc simpleCalc, ListBox list)
        {
            this.problem = simpleCalc.problem;
            this.answer = simpleCalc.operation();
            this.system = simpleCalc.system;
            if (!list.Items.Contains(this))
            {
                list.Items.Insert(0, this);
            }
        }
        public override string ToString()
        {
            return (problem.Length >= 29 ? problem.Substring(0, 26) + "..." : problem) + ':' + (answer.ToString().Length >= 29 ? answer.ToString().Substring(0, 26) + "..." : answer.ToString());
        }
        public override bool Equals(object obj)
        {
            if (obj is History other)
                return problem.Equals(other.problem);
            else
                return false;
        }
        public override int GetHashCode()
        {
            return this.problem.GetHashCode();
        }
    }
}
