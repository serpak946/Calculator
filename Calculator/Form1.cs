﻿using Calculator.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Калькулятор : Form
    {
        private ListBox list;
        private List<char> keysChar;
        private List<Button> buttons;
        private numSystem system = numSystem.dec;

        public Калькулятор()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            MyFont.ChangeFontOnForm(this);
            textBox1.Font = new Font(MyFont.LoadFont(Resources.Digital7Italic_BW658), textBox1.Font.Size);
            textBox1.ForeColor = ColorTranslator.FromHtml("#F7FFF7");
            list = listBox1;
            keysChar = charLists.decChar;
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, buttonA, buttonB, buttonC, buttonD, buttonE, buttonF, button21, button22, button19, button17, button18 };
            textBox1.DeselectAll();
            buttonEqual.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            if (buttonMaximize.Text == "🗖")
            {
                WindowState = FormWindowState.Maximized;
                buttonMaximize.Text = "🗗";
            }
            else
            {
                WindowState = FormWindowState.Normal;
                buttonMaximize.Text = "🗖";
            }
        }

        private void buttonMinimie_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void Калькулятор_KeyPress(object sender, KeyPressEventArgs e)
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
                    case (char)Keys.Enter:
                        buttonEqual_Click(sender, e); break;
                    case '%':
                        button22_Click(sender, e); break;
                    default:
                        textBox1.Text += e.KeyChar.ToString(); break;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            History item = (History)listBox1.SelectedItem;
            if (listBox1.SelectedIndex != -1)
            {
                if (textBox1.Text != string.Empty && charLists.decimalOperations.Contains(textBox1.Text[textBox1.Text.Length - 1]))
                {
                    textBox1.Text += MyConverter.fromDec(MyConverter.toDec(item.answer, item.system), system);
                }
                else
                {
                    textBox1.Text = MyConverter.fromDec(MyConverter.toDec(item.answer, item.system), system);
                }

                listBox1.ClearSelected();
            }
            buttonEqual.Focus();
        }

        public int numOfOp(string s)
        {
            int n = s.Count(a => charLists.allOperations.Contains(a));
            return s[0] == '-' ? n - 1 : n;
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
                    return new HexSimpleCalc(s);
                default: return null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (textBox1.Text[textBox1.Text.Length - 1] == '.')
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1) + ',';
                }

                if (numOfOp(textBox1.Text) == 2)
                {
                    try
                    {
                        char t = textBox1.Text[textBox1.TextLength - 1];
                        SimpCalc calc = chooseClass(textBox1.Text.Substring(0, textBox1.Text.Length - 1));
                        textBox1.Text = calc.operation().ToString() + t;
                        new History(calc, list);
                    }
                    catch (FormatException)
                    {
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 2, 1);
                    }
                }
                if (textBox1.Text.StartsWith("+") || textBox1.Text.StartsWith("^") || textBox1.Text.StartsWith("÷") || textBox1.Text.StartsWith("×"))
                {
                    textBox1.Text = textBox1.Text.Substring(1);
                }
                if (textBox1.Text.StartsWith("-+") || textBox1.Text.StartsWith("-^") || textBox1.Text.StartsWith("-÷") || textBox1.Text.StartsWith("-×"))
                {
                    textBox1.Text = textBox1.Text.Substring(2);
                }
                if (textBox1.Text.EndsWith(","))
                {
                    if (textBox1.Text == ",") 
                        textBox1.Text = "0,";
                    else
                        if (charLists.allOperations.Contains(textBox1.Text[textBox1.Text.Length - 2]))
                            textBox1.Text = textBox1.Text.Insert(textBox1.Text.Length - 1, "0");
                }
            }
            textBox1.DeselectAll();
            buttonEqual.Focus();
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
                    textBox1.Text += temp.operationstring.ToString() + temp.y.ToString();
                    buttonEqual_Click(sender, e);
                }
            }
            catch (ArgumentOutOfRangeException) { MessageBox.Show("Не пример"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            buttonEqual.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }

            buttonEqual.Focus();
        }

        private void buttonNum_Click(object sender, EventArgs e)
        {
            textBox1.Text += (sender as Button).Text;
            buttonEqual.Focus();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int n;
            string s;
            if (textBox1.Text == string.Empty || textBox1.Text == "-") return;
            try
            {
                n = textBox1.Text.LastIndexOf(textBox1.Text.Last(a => charLists.allOperations.Contains(a)));
                s = textBox1.Text.Substring(n + 1, textBox1.TextLength - n - 1);
                string s1 = textBox1.Text.Substring(0, n);
                textBox1.Text = textBox1.Text.Substring(0, n + 1);
                char op = textBox1.Text[n];
                if (op == '×' || op == '÷')
                {
                    textBox1.Text += (Convert.ToDecimal(s) / 100).ToString("0.############################");
                }
                else
                {
                    string res = (Convert.ToDecimal(s) * Convert.ToDecimal(s1) / 100).ToString("0.############################");
                    textBox1.Text += res[0] == '-' ? res.Substring(1) : res;
                }
            }
            catch (InvalidOperationException)
            {
                s = textBox1.Text.Substring(0, textBox1.TextLength);
                textBox1.Text = (Convert.ToDecimal(s) / 100).ToString("0.############################");
            }
            catch (FormatException) { MessageBox.Show("Неправильный ввод"); }
            buttonEqual.Focus();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int n;
            string s;
            if (textBox1.Text == string.Empty || textBox1.Text == "-") return; 
            try
            {
                n = textBox1.Text.LastIndexOf(textBox1.Text.Last(a => charLists.allOperations.Contains(a)));
                if (n == 0)
                {
                    MessageBox.Show("Корень от отрицательного числа"); return;
                }
                else
                {
                    s = textBox1.Text.Substring(n + 1, textBox1.TextLength - n - 1);
                    textBox1.Text = textBox1.Text.Substring(0, n + 1);
                    textBox1.Text += Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################");
                    new History("√" + s, Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################"), list);
                }
            }
            catch (InvalidOperationException)
            {
                s = textBox1.Text.Substring(0, textBox1.TextLength);
                textBox1.Text = Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################");
                new History("√" + s, Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################"), list);
            }
            buttonEqual.Focus();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                int n = -1;
                for (int i = 0; i < textBox1.Text.Length; i++)
                {
                    if (charLists.decimalOperations.Contains(textBox1.Text[i])) n = i;
                }
                if (n != -1)
                {
                    string temp = textBox1.Text.Substring(0, n+1);
                    textBox1.Text = "1÷" + textBox1.Text.Substring(n+1);
                    buttonEqual_Click(sender, e);
                    textBox1.Text = temp + textBox1.Text;
                    return;
                }

                if (textBox1.Text[0] != '-')
                    textBox1.Text = "1÷" + textBox1.Text;
                else
                    textBox1.Text = "-1÷" + textBox1.Text.Substring(1);
                buttonEqual_Click(sender, e);
                buttonEqual.Focus();
            }
            else textBox1.Text = "1÷";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += '^';
            buttonEqual.Focus();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox1.Text != "-")
            {
                textBox1.Text += '^';
                textBox1.Text += '2';
                buttonEqual_Click(sender, e);
            }
            buttonEqual.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                keysChar = charLists.binChar;
                buttons.ForEach(button => button.Enabled = false);
                button10.Enabled = true;
                button1.Enabled = true;
                if (textBox1.Text != string.Empty)
                {
                    try
                    {
                        textBox1.Text = MyConverter.fromString(textBox1.Text).operation != '!'
                            ? MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.bin) + MyConverter.fromString(textBox1.Text).operation + MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system), numSystem.bin)
                            : MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.bin);
                    }
                    catch (OverflowException) 
                    { 
                        MessageBox.Show("Число было слишком маленьким или слишком большим");
                        textBox1.Text = string.Empty;
                    }
                }
                system = numSystem.bin;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                keysChar = charLists.octChar;
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
                    try
                    {
                        textBox1.Text = MyConverter.fromString(textBox1.Text).operation != '!'
                            ? MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.oct) + MyConverter.fromString(textBox1.Text).operation + MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system), numSystem.oct)
                            : MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.oct);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Число было слишком маленьким или слишком большим");
                        textBox1.Text = string.Empty;
                    }
                }
                system = numSystem.oct;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                keysChar = charLists.decChar;
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
                    try {
                        textBox1.Text = MyConverter.fromString(textBox1.Text).operation != '!'
                            ? MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system).ToString() + MyConverter.fromString(textBox1.Text).operation + MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system)
                            : MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system).ToString();
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Число было слишком маленьким или слишком большим");
                        textBox1.Text = string.Empty;
                    }
                }
                system = numSystem.dec;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                keysChar = charLists.hexChar;
                buttons.ForEach(button => button.Enabled = true);
                button21.Enabled = false;
                button22.Enabled = false;
                button19.Enabled = false;
                button17.Enabled = false;
                button18.Enabled = false;
                if (textBox1.Text != string.Empty)
                {
                    try 
                    {
                        textBox1.Text = MyConverter.fromString(textBox1.Text).operation != '!'
                            ? MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.hex) + MyConverter.fromString(textBox1.Text).operation + MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).y.ToString(), system), numSystem.hex)
                            : MyConverter.fromDec(MyConverter.toDec(MyConverter.fromString(textBox1.Text).x.ToString(), system), numSystem.hex);
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Число было слишком маленьким или слишком большим");
                        textBox1.Text = string.Empty;
                    }
                }
                system = numSystem.hex;
            }
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox1.Copy();
            textBox1.DeselectAll();
            buttonEqual.Focus();
        }


        private void panelControl_MouseDown(object sender, MouseEventArgs e)
        {
            panelControl.Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 0x10;
            const int HTBOTTOMRIGHT = 17;
            const int pxl = 5;
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        ((int)m.LParam >> 16) & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= pxl)
                    {
                        if (vPoint.Y <= pxl)
                        {
                            m.Result = (IntPtr)HTTOPLEFT;
                        }
                        else
                        {
                            m.Result = vPoint.Y >= ClientSize.Height - pxl ? (IntPtr)HTBOTTOMLEFT : (IntPtr)HTLEFT;
                        }
                    }
                    else if (vPoint.X >= ClientSize.Width - pxl)
                    {
                        if (vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else
                        {
                            m.Result = vPoint.Y >= ClientSize.Height - pxl ? (IntPtr)HTBOTTOMRIGHT : (IntPtr)HTRIGHT;
                        }
                    }
                    else if (vPoint.Y <= pxl)
                    {
                        m.Result = (IntPtr)HTTOP;
                    }
                    else if (vPoint.Y >= ClientSize.Height - pxl)
                    {
                        m.Result = (IntPtr)HTBOTTOM;
                    }

                    break;
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                // Отрисовка заднего фона элемента
                e.DrawBackground();

                // Получение текста элемента списка
                string itemText = ((ListBox)sender).Items[e.Index].ToString();

                // Создание объекта StringFormat
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                StringFormat sf1 = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                };

                // Создание объекта Brush для темного цвета
                Brush darkBrush = new SolidBrush(ColorTranslator.FromHtml("#919591"));

                // Создание объекта Brush для светлого цвета
                Brush lightBrush = new SolidBrush(ColorTranslator.FromHtml("#F7FFF7"));

                // Разделение текста на три части
                int separatorIndex = itemText.IndexOf(':');
                int secondSeparatorIndex = itemText.LastIndexOf(':');
                string firstPart = itemText.Substring(0, separatorIndex);
                string secondPart = itemText.Substring(separatorIndex + 1, secondSeparatorIndex - separatorIndex - 1);
                string thirdPart = itemText.Substring(secondSeparatorIndex + 1);

                Rectangle r = e.Bounds;
                r.Offset(0, -10);
                Rectangle r1 = e.Bounds;
                r1.Offset(0, 10);
                Rectangle r2 = e.Bounds;
                r2.Offset(0, -15);

                FontFamily f = e.Font.FontFamily;
                Font f1 = new Font(f, 10.0F);

                // Отображение текста с разными цветами
                e.Graphics.DrawString(firstPart, e.Font, darkBrush, r, sf);
                e.Graphics.DrawString(secondPart, e.Font, lightBrush, r1, sf1);
                e.Graphics.DrawString(thirdPart, f1, darkBrush, r2, sf1);

                // Отрисовка рамки выбранного элемента
                e.DrawFocusRectangle();
            }
            catch (Exception)
            {

            }
        }
    }
}