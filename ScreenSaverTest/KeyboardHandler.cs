﻿using System;
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

        private static IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                if (objKeyInfo.key == Keys.RWin ||
                    objKeyInfo.key == Keys.LWin ||
                    (objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags)) ||
                    (objKeyInfo.key == Keys.Escape && HasAltModifier(objKeyInfo.flags)) ||
                    (objKeyInfo.key == Keys.Escape && (Control.ModifierKeys & Keys.Control) == Keys.Control) ||
                    ((Control.ModifierKeys & Keys.Control) == Keys.Control && HasAltModifier(objKeyInfo.flags) && objKeyInfo.key == Keys.Delete)
                    )
                {
                    return (IntPtr)1; // 1 define as handled
                }
            }

            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        private static bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        public static void CreateHookKeyboard()
        {
            if (ptrHook != null)
            {
                RemoveHookKeyboard();
            }

            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);

            RemoveHookOnProgramClosing();
        }

        private static void RemoveHookOnProgramClosing()
        {
            AppDomain.CurrentDomain.ProcessExit += delegate { RemoveHookKeyboard(); };
        }

        public static void RemoveHookKeyboard()
        {
            UnhookWindowsHookEx(ptrHook);
        }
    }
}
