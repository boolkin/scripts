Консольный терминал для проверки работы Modbus TCP соединения.
Т.к. мастер устройство в модбасе это по сути клиент для ведомого устройства (потому что клиент посылает запрос, а сервер отдает ответ - то есть ответ мастеру от слейва), то решил попробовать написать такой клиент. С реальными устройствами не тестировалось, но с эмулятором Slave устройства модбаса это работает неплохо.

В данный момент не предусмотрено никакой валидации данных, поэтому при неправильном запросе (вместо HEX символов ввести другие, или нечетное их количество), то программа выпадет в ошибку
 