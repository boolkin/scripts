// 2>nul||@goto :batch
/*
:batch
@echo off
setlocal

:: find csc.exe
set "csc="
for /r "%SystemRoot%\Microsoft.NET\Framework\" %%# in ("*csc.exe") do  set "csc=%%#"

if not exist "%csc%" (
   echo no .net framework installed
   exit /b 10
)

if not exist "%~n0.exe" (
   call %csc% /nologo /r:"Microsoft.VisualBasic.dll" /out:"%~n0.exe" "%~dpsfnx0" || (
      exit /b %errorlevel% 
   )
)
%~n0.exe %*
endlocal & exit /b %errorlevel%

*/


using System;
using System.Windows.Forms;
using System.Drawing;

namespace RectanglesEx
{
    class Program : Form
    {
        public Program()
        {
            InitUI();
        }

        private void InitUI()
        {
            Text = "Rectangles";
            Paint += new PaintEventHandler(OnPaint);

            ClientSize = new Size(550, 450);
            CenterToScreen();
        }

        void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.Sienna, 10, 15, 90, 60);
            g.FillRectangle(Brushes.Green, 130, 15, 90, 60);
            g.FillRectangle(Brushes.Maroon, 250, 15, 90, 60);
            g.FillRectangle(Brushes.Chocolate, 10, 105, 90, 60);
            g.FillRectangle(Brushes.Gray, 130, 105, 90, 60);
            g.FillRectangle(Brushes.Coral, 250, 105, 90, 60);
            g.FillRectangle(Brushes.Brown, 10, 195, 90, 60);
            g.FillRectangle(Brushes.Teal, 130, 195, 90, 60);
            g.FillRectangle(Brushes.Goldenrod, 250, 195, 90, 60);
        }

        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.Run(new Program());
        }
    }
}