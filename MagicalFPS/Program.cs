using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MagicalFPS.Command;
using MMF;
using MMF.Utility;

namespace MagicalFPS
{
    class Program
    {
        // Win32 APIのインポート
        [DllImport("USER32.DLL")]
        private static extern IntPtr
          GetSystemMenu(IntPtr hWnd, UInt32 bRevert);

        [DllImport("USER32.DLL")]
        private static extern UInt32
          RemoveMenu(IntPtr hMenu, UInt32 nPosition, UInt32 wFlags);

        // ［閉じる］ボタンを無効化するための値
        private const UInt32 SC_CLOSE = 0x0000F060;
        private const UInt32 MF_BYCOMMAND = 0x00000000;
        static void Main(string[] args)
        {
            disableConsoleClosing();
            GameContext context=new GameContext();
            CommandListener listener=new CommandListener(context);
            MessagePump.Run(context.MainWindow,context.Render);
            listener.Dispose();
            context.Dispose();
        }

        private static void disableConsoleClosing()
        {
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;

            if (hWnd != IntPtr.Zero)
            {
                // ［閉じる］ボタンの無効化
                IntPtr hMenu = GetSystemMenu(hWnd, 0);
                RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
            }
        }
    }
}
