using System.Diagnostics;

namespace Autoclicker
{
    class HookHelpers
    {
        public static int SetHook(NativeMethods.HookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(Input.WH_MOUSE_LL, proc,
                    Windows.GetModuleHandle(curModule.ModuleName), 0);
            }
        }
    }
}
