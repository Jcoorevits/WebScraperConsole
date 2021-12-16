using System;
using WebScraper.Functions;
using WebScraper.Views;

namespace WebScraper
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                MainPage.Welcome();
                String choice = Console.ReadLine();
                if (choice == "1")
                {
                    Youtube.Scraper();
                }

                else if (choice == "2")
                {
                    Indeed.Scraper();
                }

                else if (choice == "3")
                {
                    Doctor.Scraper();
                }
                else
                {
                    Console.WriteLine("I'm afraid your input doesn't match the numbers. Please try again");
                }
            }
        }
    }
}