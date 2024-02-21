Const DeleteReadOnly = TRUE

Set objFSO = CreateObject("Scripting.FileSystemObject")
objFSO.DeleteFile("C:\ProgramData\Rockwell Automation\FactoryTalk Activation\*"), DeleteReadOnly