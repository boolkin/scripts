sendkey
```cs
using System;
public class Sendkeys
{
	public static void Main()
    	{
		System.Windows.Forms.SendKeys.SendWait("{F11}");
	}    
}
```


key-logger
```cs
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace mykeyloggerprog
{
    class Program
    {
        //импорт библиотеки для считывания кода нажатых клавиш
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [STAThread]
        static void Main(String[] args)
        {

            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 255; i++)
                {
                    int state = GetAsyncKeyState(i); // метод импортированной ранее библиотеки возвращает код клавиши
                                                     // условия для проверки некоторых конкретных кнопок
                    if (state != 0)
                    {
                        if (((Keys)i) == Keys.F10)
                        {
                          	Thread.Sleep(1000);
                          	System.Windows.Forms.SendKeys.SendWait("{F11}");
				            Console.WriteLine("foo");
                        }	
                    }
                }
            }
        }
    }
}
```


Hello world
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hello
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
        }
    }
}
```