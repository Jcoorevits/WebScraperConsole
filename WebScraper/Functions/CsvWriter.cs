using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace WebScraper.Functions
{
    public class CsvWriter
    {
        public static void FileTemplate(string fileTemplate, string filePath)
        {
            // Specify the directory you want to manipulate.
            string path = @"c:\webScraperInfo";
            
            // Determine whether the directory exists.
            if (Directory.Exists(path))
            {
                return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            
            
            filePath = "C:\\\\webScraperInfo\\" + filePath + ".txt";
            if (File.Exists(filePath)) return;
            using StreamWriter file = File.CreateText(filePath);
            file.WriteLine(fileTemplate);
            file.Close();
        }

        public static void YoutubeCsv(string title, string author, string views, string filePath)
        {
            filePath = "C:\\\\webScraperInfo\\" + filePath + ".txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true);
            file.Write(title + "," + author + "," + views + "\n" + "\n");
            Console.WriteLine("Your info has been stored in: c:/webScraperInfo");
            file.Close();
        }

        public static void IndeedCsv(string title, string company, string location, string link, string filePath)
        {
            filePath = "C:\\\\webScraperInfo\\" + filePath + ".txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true);
            file.Write(title + "," + company + "," + location + "," + "\n" + link + "\n" + "\n");
            Console.WriteLine("Your info has been stored in: c:/webScraperInfo");
            file.Close();
        }
    }
}