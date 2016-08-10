namespace RGSS
{
    using System;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Drawing;
    using System.Diagnostics;
    using System.IO;
    using System.Security;
    using System.Security.Cryptography;
    using HWND = System.IntPtr;

    public class Defaults
    {
        public const string pWndClassName = "RGSS Player";

        public const string pDefaultLibrary = "RGSS301.dll";
        public const string pDefaultTitle = "Forjerum";
        public const string pDefaultScripts = "Data\\Scripts.rvdata2";
        public const string pDefaultRTP = "\\Game.ini";

        public const int nScreenWidth = 544;
        public const int nScreenHeight = 416;

        public const int nEvalErrorCode = 6;

        public static readonly byte[] salt = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; // Must be at least eight bytes.  MAKE THIS SALTIER!
        public const int iterations = 1042; // Recommendation is >= 1000.

    public static void DecryptFile(string sourceFilename, string destinationFilename, string password)
    {

        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Parse\\parsecached.wingofdev"))
        {
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Parse\\parsecached.wingofdev");
        }

        AesManaged aes = new AesManaged();
        aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
        aes.KeySize = aes.LegalKeySizes[0].MaxSize;
        // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
        aes.Key = key.GetBytes(aes.KeySize / 8);
        aes.IV = key.GetBytes(aes.BlockSize / 8);
        aes.Mode = CipherMode.CBC;
        ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

        using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
        {
            using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
            {
                try
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
                catch (CryptographicException exception)
                {
                    if (exception.Message == "Padding is invalid and cannot be removed.")
                        throw new ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                    else
                        throw;
                }
            }
        }
    }
    }

    public class Player
    {
        public static void Main(String[] Args)
        {
            if (Args.Length > 0)
            {
                if (Args[0] == "console")
                {
                    ShowConsoleWindow();
                    RGSSPlayer Window = new RGSSPlayer(Defaults.pWndClassName);
                }
                else
                {
                    RGSSPlayer Window = new RGSSPlayer(Defaults.pWndClassName);
                }
            }
            else
            {
                RGSSPlayer Window = new RGSSPlayer(Defaults.pWndClassName);
            }
        }
        public static void ShowConsoleWindow()
        {
            var handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
            }
            else
            {
                ShowWindow(handle, SW_SHOW);
            }
        }

        public static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, SW_HIDE);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
    }

    public static class ExtendedWindowStyles
    {

        public static readonly UInt32

        WS_EX_ACCEPTFILES = 0x00000010,

        WS_EX_APPWINDOW = 0x00040000,

        WS_EX_CLIENTEDGE = 0x00000200,

        WS_EX_COMPOSITED = 0x02000000,

        WS_EX_CONTEXTHELP = 0x00000400,

        WS_EX_CONTROLPARENT = 0x00010000,

        WS_EX_DLGMODALFRAME = 0x00000001,

        WS_EX_LAYERED = 0x00080000,

        WS_EX_LAYOUTRTL = 0x00400000,

        WS_EX_LEFT = 0x00000000,

        WS_EX_LEFTSCROLLBAR = 0x00004000,

        WS_EX_LTRREADING = 0x00000000,

        WS_EX_MDICHILD = 0x00000040,

        WS_EX_NOACTIVATE = 0x08000000,

        WS_EX_NOINHERITLAYOUT = 0x00100000,

        WS_EX_NOPARENTNOTIFY = 0x00000004,

        WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,

        WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,

        WS_EX_RIGHT = 0x00001000,

        WS_EX_RIGHTSCROLLBAR = 0x00000000,

        WS_EX_RTLREADING = 0x00002000,

        WS_EX_STATICEDGE = 0x00020000,

        WS_EX_TOOLWINDOW = 0x00000080,

        WS_EX_TOPMOST = 0x00000008,

        WS_EX_TRANSPARENT = 0x00000020,

        WS_EX_WINDOWEDGE = 0x00000100;

    }

    public static class ClassStyles
    {
        public static readonly UInt32
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_CLASSDC = 0x0040,
        CS_DBLCLKS = 0x0008,
        CS_DROPSHADOW = 0x00020000,
        CS_GLOBALCLASS = 0x4000,
        CS_HREDRAW = 0x0002,
        CS_NOCLOSE = 0x0200,
        CS_OWNDC = 0x0020,
        CS_PARENTDC = 0x0080,
        CS_SAVEBITS = 0x0800,
        CS_VREDRAW = 0x0001;
    }
    public static class WindowStyles
    {

        public static readonly UInt32

        WS_BORDER = 0x00800000,

        WS_CAPTION = 0x00C00000,

        WS_CHILD = 0x40000000,

        WS_CHILDWINDOW = 0x40000000,

        WS_CLIPCHILDREN = 0x02000000,

        WS_CLIPSIBLINGS = 0x04000000,

        WS_DISABLED = 0x08000000,

        WS_DLGFRAME = 0x00400000,

        WS_GROUP = 0x00020000,

        WS_HSCROLL = 0x00100000,

        WS_ICONIC = 0x20000000,

        WS_MAXIMIZE = 0x01000000,

        WS_MAXIMIZEBOX = 0x00010000,

        WS_MINIMIZE = 0x20000000,

        WS_MINIMIZEBOX = 0x00020000,

        WS_OVERLAPPED = 0x00000000,

        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        WS_POPUP = 0x80000000,

        WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

        WS_SIZEBOX = 0x00040000,

        WS_SYSMENU = 0x00080000,

        WS_TABSTOP = 0x00010000,

        WS_THICKFRAME = 0x00040000,

        WS_TILED = 0x00000000,

        WS_TILEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

        WS_VISIBLE = 0x10000000,

        WS_VSCROLL = 0x00200000;

    }

    public class RGSSPlayer : IDisposable
    {
        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.StructLayout(
            System.Runtime.InteropServices.LayoutKind.Sequential,
           CharSet = System.Runtime.InteropServices.CharSet.Unicode
        )]
        public struct WNDCLASS
        {
            public uint style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpszClassName;
        }

        public struct HWNED
        {
            public uint style;
            public IntPtr lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpszMenuName;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
            public string lpszClassName;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern System.UInt16 RegisterClassW(
            [System.Runtime.InteropServices.In] ref WNDCLASS lpWndClass
        );

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr CreateWindowExW(
           UInt32 dwExStyle,
           [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
       string lpClassName,
           [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
       string lpWindowName,
           UInt32 dwStyle,
           Int32 x,
           Int32 y,
           Int32 nWidth,
           Int32 nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam
        );

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern System.IntPtr DefWindowProcW(
            IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam
        );

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern bool DestroyWindow(
            IntPtr hWnd
        );

        private const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        private bool m_disposed;
        private IntPtr m_hwnd;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                }

                // Dispose unmanaged resources
                if (m_hwnd != IntPtr.Zero)
                {
                    DestroyWindow(m_hwnd);
                    m_hwnd = IntPtr.Zero;
                }

            }
        }
        [DllImport("user32.dll")]
        public static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);
        [DllImport("gdi32.dll")]
        static extern IntPtr GetStockObject(int fnObject);
        public RGSSPlayer(string class_name)
        {
            if (class_name == null) throw new System.Exception("class_name is null");
            if (class_name == String.Empty) throw new System.Exception("class_name is empty");

            m_wnd_proc_delegate = CustomWndProc;

            // Create WNDCLASS
            WNDCLASS wind_class = new WNDCLASS();
            wind_class.lpszClassName = class_name;
            wind_class.lpfnWndProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(m_wnd_proc_delegate);

            Bitmap myIco = new Bitmap("icon.ico");
            wind_class.hIcon = myIco.GetHicon() ;

            int Black_Brush = 4;
            wind_class.hbrBackground = GetStockObject(Black_Brush);

            uint dwStyle = ClassStyles.CS_DBLCLKS | ClassStyles.CS_OWNDC | ClassStyles.CS_HREDRAW | ClassStyles.CS_VREDRAW;
            wind_class.style = dwStyle;
            wind_class.hInstance = Process.GetCurrentProcess().Handle;

            UInt16 class_atom = RegisterClassW(ref wind_class);

            int last_error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();

            if (class_atom == 0 && last_error != ERROR_CLASS_ALREADY_EXISTS)
            {
                MessageBox.Show("Houve uma falha ao inicializar o Game.exe - Código 1");
                Application.Exit();
            }

            int width = Defaults.nScreenWidth + System.Windows.Forms.SystemInformation.FixedFrameBorderSize.Width * 2;
            int height = Defaults.nScreenHeight + System.Windows.Forms.SystemInformation.FixedFrameBorderSize.Height * 2 + System.Windows.Forms.SystemInformation.CaptionHeight;

            uint wStyle = WindowStyles.WS_POPUP | WindowStyles.WS_CAPTION | WindowStyles.WS_SYSMENU | WindowStyles.WS_MINIMIZEBOX | WindowStyles.WS_VISIBLE;

            // Create window
            m_hwnd = CreateWindowExW(
                ExtendedWindowStyles.WS_EX_WINDOWEDGE,
                class_name,
                Defaults.pDefaultTitle,
                wStyle,
                (System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - width) / 2,
                ((System.Windows.Forms.SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - Defaults.nScreenHeight) / 2 - System.Windows.Forms.SystemInformation.CaptionHeight),
                Defaults.nScreenWidth,
                Defaults.nScreenHeight,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );

            int SW_SHOW = 5;

            ShowWindow(m_hwnd, SW_SHOW);

            Port RPort, WPort;

            RPort = new Port();
            WPort = new Port();


            RGSS.Initialize(m_hwnd);
            RGSS.SetupFonts();

            //Thread thr = new Thread(WingOfDomain);
            //thr.Start();

            	//	1、设置RTP
           StringBuilder szRtpName = new StringBuilder(1024) ;

           if (!RGSS.SetupRTP(System.Environment.CurrentDirectory + Defaults.pDefaultRTP, szRtpName, 1024))
           {
               MessageBox.Show("Houve uma falha ao inicializar o Game.exe - Código 2");
               Application.Exit();
           }

           RGSS.Eval(String.Format("WriterLock = {0}\nWriteBuffer = {1}\n", RPort.Lock.ToString(), RPort.Buffer.ToString()));
           RGSS.Eval(String.Format("ReaderLock = {0}\nReaderBuffer = {1}\n", WPort.Lock.ToString(), WPort.Buffer.ToString()));

           //byte[] charb = new byte[8];
           //Defaults.DecryptFile(AppDomain.CurrentDomain.BaseDirectory + "Parse\\parse.wingofdev", AppDomain.CurrentDomain.BaseDirectory + "Parse\\parsecached.wingofdev", "0011011W");
           
           RGSS.GameMain(m_hwnd, Defaults.pDefaultScripts, "\0\0\0\0");
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);
        private static void WingOfDomain()
        {
            while (true)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Parse\\parsecached.wingofdev"))
                {
                    try
                    {
                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Parse\\parsecached.wingofdev");
                    }
                    catch
                    {
                        //??
                    }
                }
                if (IsKeyDown(Keys.F12)) { Environment.Exit(1); }
            }
        }
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }
        private static KeyStates GetKeyState(Keys key)
        {
            KeyStates state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }
        public static bool IsKeyDown(Keys key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }

        public static bool IsKeyToggled(Keys key)
        {
            return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
        }
        private static IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return DefWindowProcW(hWnd, msg, wParam, lParam);
        }

        private WndProc m_wnd_proc_delegate;
    }

    public class RGSS
    {
        [DllImport("RGSS301", CharSet = CharSet.Unicode, EntryPoint = "RGSSGameMain")]
        public static extern void GameMain(IntPtr a, string b, string c);
        [DllImport("RGSS301", EntryPoint = "RGSSInitialize3")]
        public static extern void Initialize(HWND a);
        [DllImport("RGSS301", CharSet = CharSet.Unicode, EntryPoint = "RGSSSetupRTP")]
        public static extern bool SetupRTP(string ini, StringBuilder error, int errlen);
        [DllImport("RGSS301", CharSet = CharSet.Unicode, EntryPoint = "RGSSSetupFonts")]
        public static extern void SetupFonts();
        [DllImport("RGSS301", CharSet = CharSet.Ansi, EntryPoint = "RGSSEval")]
        public static extern int Eval(string text);

        [DllImport("msvcrt", EntryPoint = "malloc", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr alloc(int length);
        [DllImport("msvcrt", EntryPoint = "free", CallingConvention = CallingConvention.Cdecl)]
        public static extern void free(IntPtr ptr);

        [DllImport("Kernel32", EntryPoint = "InitializeCriticalSection")]
        public static extern IntPtr LockInit(IntPtr lockvar);
        [DllImport("Kernel32", EntryPoint = "DeleteCriticalSection")]
        public static extern IntPtr LockFree(IntPtr lockvar);
        [DllImport("Kernel32", EntryPoint = "EnterCriticalSection")]
        public static extern IntPtr LockLock(IntPtr lockvar);
        [DllImport("Kernel32", EntryPoint = "LeaveCriticalSection")]
        public static extern IntPtr LockUnlock(IntPtr lockvar);
    };
    public class Port
    {
        public IntPtr Buffer, Lock;

        public Port(int size = 1024)
        {
            Buffer = RGSS.alloc(size);
            Lock = RGSS.alloc(128);
            RGSS.LockInit(Lock);
        }

        ~Port()
        {
            RGSS.LockUnlock(Lock);
            RGSS.free(Lock);
            RGSS.free(Buffer);
        }
    }
}
