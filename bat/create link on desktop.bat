@echo off
echo [InternetShortcut] >> "%AllUsersProfile%\desktop\Taskmgr.url"
echo URL="C:\Windows\System32\taskmgr.exe" >> "%AllUsersProfile%\desktop\Taskmgr.url"
echo IconFile=C:\Windows\System32\taskmgr.exe >> "%AllUsersProfile%\desktop\Taskmgr.url"
echo IconIndex=0 >> "%AllUsersProfile%\desktop\Taskmgr.url"