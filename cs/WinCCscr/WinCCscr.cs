using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace SerialPortExample
{
  class SerialPortProgram
  {
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool ShowWindow(IntPtr hWnd, int showWindowCommand);
    private static int localPort = 3000;
    [STAThread]
    static void Main(string[] args)
    {
      // Создаем поток для прослушивания
      Thread tRec = new Thread(new ThreadStart(Receiver));
      tRec.Start();
    }
    public static void ActivateWindow()
    {
      //активация окна по названию проги https://www.cyberforum.ru/csharp-beginners/thread1308321.html
      foreach (Process p in Process.GetProcessesByName("UVscreenCamera"))
      {
        ShowWindow(p.MainWindowHandle, 1);
        SetForegroundWindow(p.MainWindowHandle);
      }
    }
    public static void CloseWindow()
    {
      int cycle = 1;
      int proc = 0;
      while (cycle > 0)
      {
        foreach (Process p in Process.GetProcessesByName("explorer"))
        {
          if (p.MainWindowTitle.ToLower().Contains("screen"))
          {
            Console.WriteLine("cycle --> " + cycle + " procc: " + proc);
            p.Kill();
            cycle = -100;
          }
          proc += 1;
        }
        cycle += 1;
        if (cycle > 2000) cycle = -100;
      }
    }
    public static void Receiver()
    {
      // Создаем UdpClient для чтения входящих данных
      UdpClient receivingUdpClient = new UdpClient(localPort);
      IPEndPoint RemoteIpEndPoint = null;

      try
      {
        Console.WriteLine(
           "\n-----------*******Общий чат*******-----------");

        while (true)
        {
          // Ожидание дейтаграммы
          byte[] receiveBytes = receivingUdpClient.Receive(
             ref RemoteIpEndPoint);

          // Преобразуем и отображаем данные
          string returnData = Encoding.UTF8.GetString(receiveBytes);
          Console.WriteLine(" --> " + returnData);
          if (returnData == "start")
          {
            ActivateWindow();
            System.Windows.Forms.SendKeys.SendWait("{F11}");
          }
          else if (returnData == "stop")
          {
            ActivateWindow();
            System.Windows.Forms.SendKeys.SendWait("{F10}");
            CloseWindow();
          }
          else if (returnData == "exit")
          {
            ActivateWindow();
            System.Windows.Forms.SendKeys.SendWait("{F10}");
            CloseWindow();
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
      }
    }
  }
}