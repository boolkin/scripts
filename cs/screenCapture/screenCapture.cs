using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

/// Provides functions to capture the entire screen, or a particular window, and save it to a file. 

public class ScreenCapture
{

    /// Creates an Image object containing a screen shot of the entire desktop 

    public Image CaptureScreen()
    {
        return CaptureWindow(User32.GetDesktopWindow());
    }

    /// Creates an Image object containing a screen shot of a specific window 

    private Image CaptureWindow(IntPtr handle)
    {
        // get te hDC of the target window 
        IntPtr hdcSrc = User32.GetWindowDC(handle);
        // get the size 
        User32.RECT windowRect = new User32.RECT();
        User32.GetWindowRect(handle, ref windowRect);
        int width = windowRect.right - windowRect.left;
        int height = windowRect.bottom - windowRect.top;
        // create a device context we can copy to 
        IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
        // create a bitmap we can copy it to, 
        // using GetDeviceCaps to get the width/height 
        IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
        // select the bitmap object 
        IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
        // bitblt over 
        GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
        // restore selection 
        GDI32.SelectObject(hdcDest, hOld);
        // clean up 
        GDI32.DeleteDC(hdcDest);
        User32.ReleaseDC(handle, hdcSrc);
        // get a .NET image object for it 
        Image img = Image.FromHbitmap(hBitmap);
        // free up the Bitmap object 
        GDI32.DeleteObject(hBitmap);
        return img;
    }


    public void CaptureScreenToFile(string filename, ImageFormat format)
    {
        Image img = CaptureScreen();
        img.Save(filename, format);
    }


    static string timenow = DateTime.Now.ToString("hhmmss");
    static string yearnow = DateTime.Now.ToString("yyyy");
    static string monthnow = DateTime.Now.ToString("MM");
    static string datenow = DateTime.Now.ToString("dd_");
    static string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\"; //сохраняет скриншот на рабочем столе


    //static bool fullscreen = true;
    static String file = path + yearnow + monthnow + datenow + timenow + ".png"; // если нужен другой формат то поменяй на нужный bmp,emf, exif, jpg, jpeg, gif, png, tiff, wmf
    static System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png; // тот же формат и тут в конце 

    public static void Main()
    {
        User32.SetProcessDPIAware();
        //System.IO.Directory.CreateDirectory(path);
        ScreenCapture sc = new ScreenCapture();

        try
        {
            sc.CaptureScreenToFile(file, format);
        }
        catch (Exception e)
        {
            Console.WriteLine("Check if file path is valid " + file);
            Console.WriteLine(e.ToString());
        }
    }

    /// Helper class containing Gdi32 API functions 

    private class GDI32
    {

        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter 
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
          int nWidth, int nHeight, IntPtr hObjectSource,
          int nXSrc, int nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
          int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    }


    /// Helper class containing User32 API functions 

    private class User32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern int SetProcessDPIAware();
    }
}