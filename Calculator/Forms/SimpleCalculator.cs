using Calculator.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Calculator.Forms
{
    public partial class SimpleCalculator : Form
    {
        ListBox list;
        List<char> keysChar;
        List<Button> buttons;
        numSystem system = numSystem.dec;
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
        public SimpCalc chooseClass(string s)
        {
            switch (system)
            {
                case numSystem.bin:
                    return new BinSimpleCalc(s);
                case numSystem.oct:
                    return new OctSimpleCalc(s);
                case numSystem.dec:
                    return new DecSimpleCalc(s);
                case numSystem.hex:
                    return null;
                default: return null;
            }
        }

        private void SimpleCalculator_Load(object sender, EventArgs e)
        {
            MyFont.ChangeFontOnForm(this);
            textBox1.Font = new Font(MyFont.LoadFont(Resources.Digital7Italic_BW658), textBox1.Font.Size);
            textBox1.ForeColor = ColorTranslator.FromHtml("#F7FFF7");
            list = ParentForm.Controls["panelHistory"].Controls["listBox1"] as ListBox;
            keysChar = Constants.decChar;
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, buttonA, buttonB, buttonC, buttonD, buttonE, buttonF, button21, button22, button19, button17, button18 };
            new BinSimpleCalc("1+1").toDec("103,01", numSystem.bin);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (textBox1.Text[textBox1.Text.Length - 1] == '.') textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1) + ',';
                if (numOfOp(textBox1.Text) == 2)
                {
                    char t = textBox1.Text[textBox1.TextLength - 1];
                    SimpCalc calc = chooseClass(textBox1.Text.Substring(0, textBox1.Text.Length - 1));
                    textBox1.Text = calc.operation().ToString() + t;
                    new History(calc, list);
                }
            }
        }

        public void buttonEqual_Click(object sender, EventArgs e)
        {
            try
            {
                if (numOfOp(textBox1.Text) != 0)
                {
                    SimpCalc calc = chooseClass(textBox1.Text);
                    textBox1.Text = calc.operation().ToString();
                    History hist = new History(calc, list);
                }
                else
                {
                    SimpCalc temp = chooseClass((list.Items[0] as IHistory).problem);
                    textBox1.Text += (temp.operationstring.ToString() + temp.y.ToString());
                    buttonEqual_Click(sender, e);
                }
            }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Не пример"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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

        private void SimpleCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!keysChar.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                switch (ch)
                {
                    case (char)Keys.Back:
                        buttonDelete_Click(sender, e); break;
                    case '*':
                        textBox1.Text += '×'; break;
                    case '/':
                        textBox1.Text += '÷'; break;
                    default:
                        textBox1.Text += e.KeyChar.ToString(); break;
                }
            }
        }

        private void buttonNum_Click(object sender, EventArgs e)
        {
            textBox1.Text += (sender as Button).Text;
        }

        private void SimpleCalculator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEqual_Click(sender, e);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int n;
            string s;
            try
            {
                n = textBox1.Text.LastIndexOf(textBox1.Text.Last(a => Constants.allOperations.Contains(a)));
                s = textBox1.Text.Substring(n + 1, textBox1.TextLength - n - 1);
                string s1 = textBox1.Text.Substring(0, n);
                textBox1.Text = textBox1.Text.Substring(0, n + 1);
                char op = textBox1.Text[n];
                if (op == '×' || op == '÷')
                    textBox1.Text += (Convert.ToDecimal(s) / 100).ToString("0.############################");
                else
                    textBox1.Text += (Convert.ToDecimal(s) * Convert.ToDecimal(s1) / 100).ToString("0.############################");
            }
            catch (InvalidOperationException)
            {
                s = textBox1.Text.Substring(0, textBox1.TextLength);
                textBox1.Text = (Convert.ToDecimal(s) / 100).ToString("0.############################");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int n;
            string s;
            try
            {
                n = textBox1.Text.LastIndexOf(textBox1.Text.Last(a => Constants.allOperations.Contains(a)));
                s = textBox1.Text.Substring(n + 1, textBox1.TextLength - n - 1);
                textBox1.Text = textBox1.Text.Substring(0, n + 1);
                textBox1.Text += Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################");
                new History("√"+s, Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################"), list);
            }
            catch (InvalidOperationException)
            {
                s = textBox1.Text.Substring(0, textBox1.TextLength);
                textBox1.Text = Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################");
                new History("√" + s, Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################"), list);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int n;
            string s;
            try
            {
                n = textBox1.Text.LastIndexOf(textBox1.Text.Last(a => Constants.allOperations.Contains(a)));
                s = textBox1.Text.Substring(n + 1, textBox1.TextLength - n - 1);
                textBox1.Text = textBox1.Text.Substring(0, n + 1);
                textBox1.Text += (chooseClass("1÷" + s)).operation();
                new History(chooseClass("1÷" + s), list);
            }
            catch (InvalidOperationException)
            {
                s = textBox1.Text.Substring(0, textBox1.TextLength);
                textBox1.Text = (chooseClass("1÷" + s)).operation();
                new History(chooseClass("1÷" + s), list);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += '^';
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int n;
            string s;
            try
            {
                n = textBox1.Text.LastIndexOf(textBox1.Text.Last(a => Constants.allOperations.Contains(a)));
                s = textBox1.Text.Substring(n + 1, textBox1.TextLength - n - 1);
                textBox1.Text = textBox1.Text.Substring(0, n + 1);
                textBox1.Text += (chooseClass(s + "^2")).operation();
                new History(chooseClass(s + "^2"), list);
            }
            catch (InvalidOperationException)
            {
                s = textBox1.Text.Substring(0, textBox1.TextLength);
                textBox1.Text = (chooseClass(s + "^2")).operation();
                new History(chooseClass(s + "^2"), list);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                keysChar = Constants.binChar;
                buttons.ForEach(button => button.Enabled = false);
                button10.Enabled = true;
                button1.Enabled = true;
                if (textBox1.Text != string.Empty)
                {
                    if (MyConverter.fromString(textBox1.Text).operation != '!')
                    {
                        textBox1.Text = MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.bin) + MyConverter.fromString(textBox1.Text).operation + MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system), numSystem.bin);
                    }
                    else
                    {
                        textBox1.Text = MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.bin);
                    }
                }
                system = numSystem.bin;          
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                keysChar = Constants.octChar;
                buttons.ForEach(button => button.Enabled = false);
                button10.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                if (textBox1.Text != string.Empty)
                {
                    if (MyConverter.fromString(textBox1.Text).operation != '!')
                    {
                        textBox1.Text = MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.oct) + MyConverter.fromString(textBox1.Text).operation + MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system), numSystem.oct);
                    }
                    else
                    {
                        textBox1.Text = MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.oct);
                    }
                }
                system = numSystem.oct;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                keysChar = Constants.decChar;
                buttons.ForEach(button => button.Enabled = false);
                button10.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button21.Enabled = true;
                button22.Enabled = true;
                button19.Enabled = true;
                button17.Enabled = true;
                button18.Enabled = true;
                if (textBox1.Text != string.Empty)
                {
                    if (MyConverter.fromString(textBox1.Text).operation != '!')
                    {
                        textBox1.Text = MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system).ToString() + MyConverter.fromString(textBox1.Text).operation + MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system);
                    }
                    else
                    {
                        textBox1.Text = MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system).ToString();
                    }
                }
                system = numSystem.dec;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                keysChar = Constants.hexChar;
                buttons.ForEach(button => button.Enabled = true);
                button21.Enabled = false;
                button22.Enabled = false;
                button19.Enabled = false;
                button17.Enabled = false;
                button18.Enabled = false;
                if (textBox1.Text != string.Empty)
                {
                    if (MyConverter.fromString(textBox1.Text).operation != '!')
                    {
                        textBox1.Text = MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.hex) + MyConverter.fromString(textBox1.Text).operation + MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system), numSystem.hex);
                    }
                    else
                    {
                        textBox1.Text = MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.hex);
                    }
                }
                system = numSystem.hex;
            }
        }
    }
}
