using System.IO;
using System.Runtime.CompilerServices;

namespace WebScraper.Functions
{
    public class CsvWriter
    {
        public static void FileTemplate(string fileTemplate, string filePath)
        {
            filePath = "C:\\\\CSharp\\demoScraper\\"+filePath + ".txt";
            if (File.Exists(filePath)) return;
            using StreamWriter file = File.CreateText(filePath);
            file.WriteLine(fileTemplate);
            file.Close();
        }

        public static void YoutubeCsv(string title, string author, string views, string filePath)
        {
            filePath = "C:\\\\CSharp\\demoScraper\\"+filePath + ".txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true);
            file.Write(title + "," + author + "," + views + "\n"+"\n");
            file.Close();
        }

        public static void IndeedCsv(string title, string company, string location, string link, string filePath)
        {
            filePath = "C:\\\\CSharp\\demoScraper\\"+filePath + ".txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true);
            file.Write(title + "," + company + "," + location + "," + "\n" + link + "\n"+ "\n");
            file.Close();
        }
    }
}