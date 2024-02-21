@echo off
icacls "D:\screen\sc" /deny admin:(OI)(CI)(DE,DC) /T /C 
