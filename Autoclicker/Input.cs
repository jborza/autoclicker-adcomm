using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Autoclicker.NativeMethods;

namespace Autoclicker
{
    static class Input
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto,
CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //mouse event constants
        const int MOUSEEVENTF_LEFTDOWN = 2;
        const int MOUSEEVENTF_LEFTUP = 4;

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        //input type constant
        const int INPUT_MOUSE = 0;
        public const int WH_MOUSE = 7;
        public const int WH_MOUSE_LL = 14;

        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x0208
        }
       
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        public static void DoOneClick(Point location)
        {
            Cursor.Position = location;
            DoOneClick();
        }

        public static void DoOneClick()
        {
            //set up the INPUT struct and fill it for the mouse down
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = 0;
            i.mi.dy = 0;
            i.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0;
            //send the input 
            NativeMethods.SendInput(1, ref i, Marshal.SizeOf(i));
            Thread.Sleep(30);
            //set the INPUT for mouse up and send it
            i = MouseUp(i);
        }

        private static INPUT MouseUp(INPUT i)
        {
            i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
            NativeMethods.SendInput(1, ref i, Marshal.SizeOf(i));
            return i;
        }
    }

}
