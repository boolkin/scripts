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
using System.IO;
using System.Net;
using System.Text;

public class ScreenCapture
{

   
    static string st = "";
	public static void Main()	{

		do{   
			st = Console.ReadLine(); 
			WebClient client = new WebClient ();
			byte[] reply = Encoding.Default.GetBytes(client.DownloadString ("http://10.0.13.11/"));

			Console.WriteLine (Encoding.UTF8.GetString(reply));
			// System.Threading.Thread.Sleep(20000);
		} while (st =="1");

        
	}
    
}