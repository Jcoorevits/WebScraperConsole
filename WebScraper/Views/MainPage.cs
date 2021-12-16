using System;

namespace WebScraper.Views
{
    class MainPage
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome, my name is Selenium. Select a number of your liking.");
            Console.WriteLine("1: Youtube web scraper.");
            Console.WriteLine("2: Indeed job search web scraping.");
            Console.WriteLine("3: Doctor appointment scanner");
            Console.Write("Enter choice: ");
        }
    }
}