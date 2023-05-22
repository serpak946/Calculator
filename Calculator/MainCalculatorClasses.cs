using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Calculator
{
    public static class Constants
    {
        public static List<char> decChar { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', (char)Keys.Back, '.', ',', '×', '÷', };
        public static List<char> binChar { get; } = new List<char>() { '+', '-', '*', '/', '0', '1', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<char> octChar { get; } = new List<char>() { '+', '-', '*', '/', '0', '1', '2', '3', '4', '5', '6', '7', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<char> hexChar { get; } = new List<char>() { '+', '-', '*', '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', (char)Keys.Back, '.', ',', '×', '÷' };
        public static List<char> allOperations { get; } = new List<char>() { '+', '-', '*', '/', '%', '^', '÷', '×' };
        public static List<char> decimalOperations { get; } = new List<char>() { '+', '-', '*', '/', '^', '÷', '×' };
    }

    internal interface IHistory
    {
        string problem { get; set; }
        string answer { get; set; }
        numSystem system { get; set; }
        string ToString();
        bool Equals(object obj);
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
            system = numSystem.dec;
            if (!list.Items.Contains(this))
            {
                list.Items.Insert(0, this);
            }
        }
        public History(ISimpleCalc simpleCalc, ListBox list)
        {
            problem = simpleCalc.problem;
            answer = simpleCalc.operation();
            system = simpleCalc.system;
            if (!list.Items.Contains(this))
            {
                list.Items.Insert(0, this);
            }
        }
        public override string ToString()
        {
            return (problem.Length >= 29 ? problem.Substring(0, 26) + "..." : problem) + ':' + (answer.ToString().Length >= 29 ? answer.ToString().Substring(0, 26) + "..." : answer.ToString()) + ':' + ((int)system);
        }
        public override bool Equals(object obj)
        {
            return obj is History other && problem.Equals(other.problem) && system.Equals(other.system);
        }
        public override int GetHashCode()
        {
            return Tuple.Create(problem, system).GetHashCode();
        }
    }
}