using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    public static class HamburgerPanel
    {
        private static bool open = false;
        private static int opened = 100;
        private static int closed = 45;
        public static void Change(Panel panel, Button hamburger, Form form)
        {
            if (open)
            {
                form.MinimumSize = new Size(540, 535);
                foreach (Button button in panel.Controls)
                {
                    button.Visible = !open;
                }
                hamburger.Visible = true;
                Anim(panel, form);
            }
            else
            {
                form.MinimumSize = new Size(600, 535);
                Anim(panel, form);
                foreach (Button button in panel.Controls)
                {
                    button.Visible = !open;
                }
                hamburger.Visible = true;
            }
            open = !open;
        }
        private static void Anim(Panel panel, Form form)
        {
            if (open)
            {
                for (int i = opened; i >= closed;)
                {
                    panel.Width = i;
                    form.Update();
                    i -= 4 - (int)Math.Ceiling(Math.Abs((i - 72.5) / 10));
                }
            }
            else
            {
                for (int i = closed; i <= opened;)
                {
                    panel.Width = i;
                    form.Update();
                    i += 4 - (int)Math.Ceiling(Math.Abs((i - 72.5) / 10));
                }
            }
        }
    }
}
