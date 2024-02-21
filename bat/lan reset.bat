@echo off

netsh interface ip set address name="Ethernet" source=dhcp
echo IP назначается по умолчанию


@rem незабудьте сохранить батник в кодировке OEM-866