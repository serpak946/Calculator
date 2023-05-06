using Calculator.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Калькулятор : Form
    {
        TextBox textBox;
        public Калькулятор()
        {
            InitializeComponent();
        }
        private void clearButtons(object sender)
        {
            simpleCalculatorButton.ImageIndex = 0;
            mathCalculatorButton.ImageIndex = 2;
            graphicCalculatorButton.ImageIndex = 4;
            trigCalculatorButton.ImageIndex = 6;
            ((Button)sender).ImageIndex += 1;
        }

        private void openForm(object sender, Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        
        public void newHistory(SimpleCalc calc)
        {
            listBox1.Items.Add(new History(calc));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            simpleCalculatorButton_Click(simpleCalculatorButton, e);
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            MyFont.ChangeFontOnForm(this);
        }

        private void simpleCalculatorButton_Click(object sender, EventArgs e)
        {
            if (simpleCalculatorButton.ImageIndex % 2 != 0) return;
            clearButtons(sender);
            panelMain.Controls.Clear();
            openForm(sender, new SimpleCalculator());
            textBox = null;
            foreach (Control c in panelMain.Controls[0].Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    textBox = (TextBox)c;
            }
        }

        private void mathCalculatorButton_Click(object sender, EventArgs e)
        {
            if (mathCalculatorButton.ImageIndex % 2 != 0) return;
            clearButtons(sender);
            panelMain.Controls.Clear();
        }

        private void graphicCalculatorButton_Click(object sender, EventArgs e)
        {
            if (graphicCalculatorButton.ImageIndex % 2 != 0) return;
            clearButtons(sender);
            panelMain.Controls.Clear();
        }

        private void trigCalculatorButton_Click(object sender, EventArgs e)
        {
            if (trigCalculatorButton.ImageIndex % 2 != 0) return;
            clearButtons(sender);
            panelMain.Controls.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HamburgerPanel.Change(menuPanel, buttonHamburger, this);
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
            /*if (!Char.IsDigit(ch) && ch != ',' && ch != '+' && ch != '-' && ch != '*' && ch != '/' && ch != '%' && ch != (char)Keys.Enter && ch != '^' && ch != '.')
            {
                e.Handled = true;
            }*/
            if (!Constants.keysChar.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                SystemSounds.Beep.Play();
                textBox.Text += e.KeyChar.ToString();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (Constants.doubleOperations.Contains(textBox.Text[textBox.Text.Length - 1]))
                    textBox.Text += (listBox1.SelectedItem as History).answer;
                else
                    textBox.Text = (listBox1.SelectedItem as History).answer.ToString();
                listBox1.ClearSelected();
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

                // Разделение текста на две части
                int separatorIndex = itemText.IndexOf(':');
                string firstPart = itemText.Substring(0, separatorIndex);
                string secondPart = itemText.Substring(separatorIndex + 1);

                Rectangle r = e.Bounds;
                r.Offset(0, -10);
                Rectangle r1 = e.Bounds;
                r1.Offset(0, 10);

                // Отображение текста с разными цветами
                e.Graphics.DrawString(firstPart, e.Font, darkBrush, r, sf);
                e.Graphics.DrawString(secondPart, e.Font, lightBrush, r1, sf1);

                // Отрисовка рамки выбранного элемента
                e.DrawFocusRectangle();
            }
            catch (Exception)
            {

            }
        }
    }
}
