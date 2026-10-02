using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // 1. Диалог выбора порта
        Console.Write("Введите порт для прослушивания (по умолчанию 3003): ");
        string input = Console.ReadLine();
        
        int port;
        if (!int.TryParse(input, out port))
        {
            port = 3003; // Значение по умолчанию
        }

        // 2. Инициализация UDP-клиента
        UdpClient udpServer = new UdpClient(port);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);

        // Исправлено: замена $ на String.Format
        Console.WriteLine(String.Format("\n[СЕРВЕР] Слушаю UDP порт {0}...", port));
        Console.WriteLine("Нажмите Ctrl+C для выхода.\n");

        // 3. Бесконечный цикл прослушивания
        while (true)
        {
            try
            {
                // Блокирующий вызов: ждем пакет
                byte[] receivedBytes = udpServer.Receive(ref remoteEP);

                // Переводим байты в строку
                string stringData = Encoding.UTF8.GetString(receivedBytes);

                // Переводим байты в HEX формат
                string hexData = BitConverter.ToString(receivedBytes);

                // Исправлено: замена $ на String.Format
                Console.WriteLine(String.Format("{0}: {1} байт : \"{2}\" ({3})", DateTime.Now.ToString("HH:mm:ss.fff"), receivedBytes.Length, stringData, hexData));
            }
            catch (Exception ex)
            {
                // Исправлено: замена $ на string.Concat или конкатенацию (+)
                Console.WriteLine("Ошибка при получении данных: " + ex.Message);
            }
        }
    }
}
