using System;
using System.Runtime.InteropServices;

namespace Autoclicker
{
    static class NativeMethods
    {
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        };

        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs,
                                         int cbSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto,
 CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn,
IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto,
 CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode,
IntPtr wParam, IntPtr lParam);
    }
}
