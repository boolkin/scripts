Dim objHTTP
strToSend = "q1=191510&191510=2&q2=191511&191511=1&q3=191512&191512=2&q4=191513&191513=2&q5=191514&191514=1&q6=191515&191515=5&q7=191516&191516=4&q8=191517&191517=2&q9=191518&191518=3&q10=191519&191519=1&q11=191520&191520=1&Width=&iter=5&bil=2&test=776"

Set objHTTP = CreateObject("Microsoft.XMLHTTP")

Call objHTTP.Open("POST", "https://tests24.ru/?iter=4&bil=2&test=776", false)

objHTTP.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
objHTTP.setRequestHeader "User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)"
objHTTP.setRequestHeader "Accept-Encoding", "gzip, deflate"
objHTTP.Send strToSend

Set FSO = CreateObject("Scripting.FileSystemObject")
Set f = FSO.CreateTextFile("c:/test3.html", True, True)
f.WriteLine(objHTTP.ResponseText)
f.Close
MsgBox(objHTTP.ResponseText)