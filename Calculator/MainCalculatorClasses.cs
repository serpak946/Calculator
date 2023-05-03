using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public static class Constants
    {
        public static List<char> allOperations { get; } = new List<char>() { '+', '-', '*', '/', '%', '^' };
        public static List<char> doubleOperations { get; } = new List<char>() { '+', '-', '*', '/', '^' };
        public static List<char> oneOperations { get; } = new List<char>() { '%' };
    }

    interface IHistory
    {
        string problem { get; set; }
        double answer { get; set; }
    }
    public class History : IHistory
    {
        public string problem { get; set; }
        public double answer { get; set; }
        public History(string problem, double answer)
        {
            this.problem = problem;
            this.answer = answer;
        }
        public History(SimpleCalc simpleCalc)
        {
            this.problem = simpleCalc.problem;
            this.answer = simpleCalc.operation();
        }
        public override string ToString()
        {
            if (problem.Length >= 20)
            {
                problem = problem.Substring(0, 17);
                problem += "...";
            }
            return problem + ':' + answer.ToString();
        }
    }
}
