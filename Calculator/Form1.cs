using Calculator.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Калькулятор : Form
    {
        ListBox list;
        List<char> keysChar;
        List<Button> buttons;
        numSystem system = numSystem.dec;
        public Калькулятор()
        {
            InitializeComponent();
        }
        public void newHistory(DecSimpleCalc calc)
        {
            new History(calc, listBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            MyFont.ChangeFontOnForm(this);
            textBox1.Font = new Font(MyFont.LoadFont(Resources.Digital7Italic_BW658), textBox1.Font.Size);
            textBox1.ForeColor = ColorTranslator.FromHtml("#F7FFF7");
            list = listBox1;
            keysChar = Constants.decChar;
            buttons = new List<Button>() { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, buttonA, buttonB, buttonC, buttonD, buttonE, buttonF, button21, button22, button19, button17, button18 };
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (textBox1.Text != string.Empty && Constants.decimalOperations.Contains(textBox1.Text[textBox1.Text.Length - 1]))
                    textBox1.Text += MyConverter.fromDec(MyConverter.toDec(item.answer, item.system), system);
                else
                    textBox1.Text = MyConverter.fromDec(MyConverter.toDec(item.answer, item.system), system);
                listBox1.ClearSelected();
            }
            buttonEqual.Focus();
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
                    return new HexSimpleCalc(s); ;
                default: return null;
            }
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
            buttonEqual.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
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
            buttonEqual.Focus();
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
                new History("√" + s, Math.Sqrt(Convert.ToDouble(s)).ToString("0.############################"), list);
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
            textBox1.Text = "1÷" + textBox1.Text;
            buttonEqual_Click(sender, e);
            buttonEqual.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += '^';
            buttonEqual.Focus();
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
            buttonEqual.Focus();
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
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= pxl)
                        if (vPoint.Y <= pxl)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - pxl)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - pxl)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - pxl)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= pxl)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - pxl)
                        m.Result = (IntPtr)HTBOTTOM;
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
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                StringFormat sf1 = new StringFormat();
                sf1.Alignment = StringAlignment.Far;
                sf1.LineAlignment = StringAlignment.Center;

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
