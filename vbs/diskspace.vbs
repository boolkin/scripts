Set fso = CreateObject("Scripting.FileSystemObject")
cspace = round(fso.GetDrive("C:").FreeSpace/1073741824,2)
if cspace < 3 then 
key = msgbox ("���������� ����� �� ����� �:\ �������� ����� " & cspace & " Gb" & vbCrLf & "������ ������� ������ �����? ", 4+48 , "��������!")

end if