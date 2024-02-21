Set fso = CreateObject("Scripting.FileSystemObject")
cspace = round(fso.GetDrive("C:").FreeSpace/1073741824,2)
if cspace < 3 then 
key = msgbox ("Свободного места на диске С:\ осталось всего " & cspace & " Gb" & vbCrLf & "Будешь удалять лишние файлы? ", 4+48 , "Внимание!")

end if