@echo off
set /p mesto="�롥�� ����ன�� (1 - ���, 2 - ����): "
echo ��࠭� ����ன�� - %mesto%

if %mesto% equ 1 goto ccl

:cgl
netsh interface ip set address name="Wireless Network Connection 3" source=static addr=10.0.170.28 mask=255.255.255.0 gateway=10.0.170.1 gwmetric=1
goto end

:ccl
netsh interface ip set address name="Wireless Network Connection 3" source=dhcp
echo IP �����砥��� �� 㬮�砭��

:end
if %mesto% equ 2 echo IP �⠫ 10.0.170.28
echo _____
pause