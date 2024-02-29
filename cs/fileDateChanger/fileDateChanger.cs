using System;
using System.IO;

public class fileDateChanger
{
    public static void Main()
    {
        Console.WriteLine("Перетащите в консоль файл дату которого надо изменить");
        string filePath = Console.ReadLine();
        filePath = filePath.Trim('"');

        Console.WriteLine("Введите желаемую дату в формате yyyy-mm-dd");
        string strDate = Console.ReadLine();
        
        DateTime newDate = DateTime.Parse(strDate);
        File.SetCreationTime(filePath, newDate);
        File.SetLastWriteTime(filePath, newDate);
    }
}