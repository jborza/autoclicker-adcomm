using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Autoclicker
{
    public partial class Form1 : Form
    {
        static int hHook = 0;
        NativeMethods.HookProc MouseHookProcedure;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetupHook()
        {
            MouseHookProcedure = new NativeMethods.HookProc(MouseHookProc);
            hHook = HookHelpers.SetHook(MouseHookProcedure);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupHook();
        }

        public int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            Input.MouseHookStruct data = (Input.MouseHookStruct)
                Marshal.PtrToStructure(lParam, typeof(Input.MouseHookStruct));
            if ((Input.MouseMessages)wParam == Input.MouseMessages.WM_MBUTTONUP)
                StartStopClicker();
            return NativeMethods.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        private void StartStopClicker()
        {
            timer.Enabled = !timer.Enabled;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Input.DoOneClick();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Input.UnhookWindowsHookEx(hHook);
        }
    }
}
