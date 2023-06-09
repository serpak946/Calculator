﻿using Calculator.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator
{
    public static class MyFont
    {
        private static readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);
        public static FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);
            uint cFonts = 0;
            _ = AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);
            return privateFontCollection.Families.Last();
        }

        public static FontFamily Font = LoadFont(Resources.AdelleSansEXT_Semibold);

        public static void ChangeFontOnForm(Control form)
        {
            foreach (Control c in form.Controls)
            {
                c.Font = new Font(Font, c.Font.Size);
                if (c.Controls.Count != 0)
                {
                    ChangeFontOnForm(c);
                }
            }
        }
    }
}