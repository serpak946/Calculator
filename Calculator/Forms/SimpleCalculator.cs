using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using Calculator.Properties;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Calculator.Forms
{
    public partial class SimpleCalculator : Form
    {

        public SimpleCalculator()
        {
            InitializeComponent();
        }

        public int numOfOp(string s)
        {
            int n = s.Count(a => Constants.allOperations.Contains(a));
            if (s[0] == '-')
            {
                return n - 1;
            }
            else return n;
            
        }

        private void SimpleCalculator_Load(object sender, EventArgs e)
        {
            MyFont.ChangeFontOnForm(this);
            textBox1.Font = new Font(MyFont.LoadFont(Resources.Digital7Italic_BW658), textBox1.Font.Size);
            textBox1.ForeColor = ColorTranslator.FromHtml("#F7FFF7");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (numOfOp(textBox1.Text) == 2)
                {
                    SimpleCalc calc = new SimpleCalc(textBox1.Text.Substring(0, textBox1.Text.Length - 2));
                    textBox1.Text = calc.operation().ToString();
                    (ParentForm.Controls["panelHistory"].Controls["listBox1"] as ListBox).Items.Insert(0, new History(calc));
                }
            }
            //textBox1.Text[textBox1.Text.Length-1].ToString()
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            SimpleCalc calc = new SimpleCalc(textBox1.Text);
            textBox1.Text = calc.operation().ToString();
            (ParentForm.Controls["panelHistory"].Controls["listBox1"] as ListBox).Items.Insert(0, new History(calc));
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
        }
    }
}
