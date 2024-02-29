using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace UdpSample
{
   class udprec
   {
      private static int localPort = Convert.ToInt16("1155"); // на какой порт принимаются UDP
      private static string table_name = ("raw_data"); // в какую таблицу добавляются данные
      private static string column_name = ("data"); // в какую колонку этой таблицы добавляются данные
      public static SqlConnection myConn = new SqlConnection("Password=12345;Persist Security Info=True;User ID=user123;Initial Catalog=testdb;Data Source=WIN7VM\\WINCC");
      // строка подключения к SQL
      SqlCommand myCommand = new SqlCommand("Command String", myConn);
      [STAThread]
      static void Main(string[] args)
      {
         System.IO.Directory.CreateDirectory("C:\\logs\\");
         try
         {
            Thread tRec = new Thread(new ThreadStart(Receiver));
            tRec.Start();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n " + ex.Message);
            System.IO.File.WriteAllText("C:\\logs\\err_" + DateTime.Now.ToString("HHmmss") + ".txt", "Возникло исключение: " + ex.ToString() + "\n " + ex.Message);
         }
      }

      public static void Receiver()
      {
         // Создаем UdpClient для чтения входящих данных
         UdpClient receivingUdpClient = new UdpClient(localPort);
         IPEndPoint RemoteIpEndPoint = null;

         try
         {
            while (true)
            {
               // Ожидание дейтаграммы
               byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

               // Преобразуем и отображаем данные. Раскоментировать для отображения в консоли
               string returnData = Encoding.UTF8.GetString(receiveBytes);
               Console.WriteLine(" -> " + returnData.ToString());

               // вставить в таблицу полученную дейтаграмму
               try
               {
                  SqlCommand cmd = new SqlCommand("INSERT INTO " + table_name + " (" + column_name + ") values (@bindata)", myConn);
                  myConn.Open();
                  var param = new SqlParameter("@bindata", SqlDbType.Binary)
                  {
                     Value = receiveBytes
                  };
                  cmd.Parameters.Add(param);
                  cmd.ExecuteNonQuery();
                  myConn.Close();
               }
               catch (Exception ex)
               {
                  Console.WriteLine("SQL исключение: " + ex.ToString() + "\n " + ex.Message);
                  System.IO.File.WriteAllText("C:\\logs\\err_sql_" + DateTime.Now.ToString("HHmmss") + ".txt", "SQL исключение: " + ex.ToString() + "\n " + ex.Message);
               }
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("UDP exception: reciever " + ex.ToString() + "\n " + ex.Message);
            System.IO.File.WriteAllText("C:\\logs\\err_udp_" + DateTime.Now.ToString("HHmmss") + ".txt", "UDP exception: reciever " + ex.ToString() + "\n " + ex.Message);
         }
      }
   }
}