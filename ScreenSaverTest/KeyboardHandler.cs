using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenSaverTest
{
    public static class KeyboardHandler
    {
        // Structure contain information about low-level keyboard input event 
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;

            public bool HasAltModifier() => (flags & 0x20) == 0x20;
        }

        // System level functions to be used for hook and unhook keyboard input  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);

        // Declaring Global objects     
        private static IntPtr ptrHook;
        private static LowLevelKeyboardProc objKeyboardProcess;

        private static bool AltKeyPressed() => (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
        private static bool CtrlKeyPressed() => (Control.ModifierKeys & Keys.Control) == Keys.Control;
        private static bool CtrlAltKeysPressed() => Control.ModifierKeys == (Keys.Control | Keys.Alt);

        private static IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                if (
                    objKeyInfo.key == Keys.RWin ||
                    objKeyInfo.key == Keys.LWin ||
                    (AltKeyPressed() && (objKeyInfo.key == Keys.Tab || objKeyInfo.key == Keys.F4 || objKeyInfo.key == Keys.Escape)) ||
                    (CtrlKeyPressed() && objKeyInfo.key == Keys.Escape) ||
                    (CtrlAltKeysPressed() && objKeyInfo.key == Keys.Delete)
                   )
                {
                    return (IntPtr)1;
                }
            }

            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        public static void CreateHookKeyboard()
        {
            if (ptrHook == null)
            {
                RemoveHookOnProgramClosing();
            }
            else
            {
                RemoveHookKeyboard();
            }

            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }

        private static void RemoveHookOnProgramClosing()
        {
            AppDomain.CurrentDomain.ProcessExit += delegate { RemoveHookKeyboard(); };
        }

        private static void RemoveHookKeyboard()
        {
            UnhookWindowsHookEx(ptrHook);
        }
    }
}