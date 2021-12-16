using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraper.Functions
{
    public class Doctor
    {
        public static void Scraper()
        {
            // Configure Chrome options to use in chrome driver
            ChromeOptions option = new ChromeOptions();
            // option.AddArgument("--headless");
            // option.AddArgument("--silent");
            // option.AddArgument("--disable-gpu");
            option.AddArgument("--log-level=3");


            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.SuppressInitialDiagnosticInformation = true;

            // Make new Chrome driver and go to Youtube
            IWebDriver driver = new ChromeDriver(service, option);
            driver.Navigate().GoToUrl("https://huisartsenpraktijkkeiberg.be/");

            //Go to appointment page
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/aside/section/div/p[11]/a")).Click();

            // Switch to current tab
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            System.Threading.Thread.Sleep(1000);

            // Example next page
            /*driver.FindElement(
                By.XPath("/html/body/div[1]/div[4]/div[1]/div/div/div[2]/div/div[1]/div[2]/div[1]/button[3]")).Click();
            System.Threading.Thread.Sleep(1000);*/


            var date = driver.FindElement(By.XPath(
                "/html/body/div[1]/div[4]/div[1]/div/div/div[2]/div/div[1]/div[1]/h2"));
            Console.WriteLine("Searching for appointment between " + date.Text);

            var appointment = false;

            while (appointment == false)
            {
                var time = driver.FindElements(By.XPath(
                    "/html/body/div[1]/div[4]/div[1]/div/div/div[2]/div/div[2]/div/table/tbody/tr/td/div[2]/div/div[3]/table/tbody/tr/td/div/div[2]/a/div[1]/div[1]"));
                var occupation = driver.FindElements(By.XPath(
                    "/html/body/div[1]/div[4]/div[1]/div/div/div[2]/div/div[2]/div/table/tbody/tr/td/div[2]/div/div[3]/table/tbody/tr/td/div/div[2]/a/div[1]/div[2]"));
                var lenght = occupation.Count;
                var days = driver.FindElements(By.XPath(
                    "/html/body/div/div[4]/div[1]/div/div/div[2]/div/div[2]/div/table/thead/tr/td/div/table/thead/tr/th"));
                int dayCounter = 0;


                for (int i = 0; i < lenght; i++)
                {
                    if (time[i].GetAttribute("data-start") == "8:00")
                    {
                        dayCounter++;
                    }

                    if (occupation[i].Text.Contains("Afspraak (geen infectie)"))
                    {
                        string available = days[dayCounter].Text + " " + time[i].GetAttribute("data-start") + " " +
                                           occupation[i].Text;
                        appointment = true;
                        Console.Write("First available appointment is :");
                        Console.WriteLine(available);
                        SendMail.Appointment(available, driver.Url);
                        break;
                    }
                }

                if (appointment == false)
                {
                    Console.WriteLine("Search was not successful.");
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 4)
                        {
                            Console.WriteLine("Retrying in " + (5 - i) + " minute.");
                            System.Threading.Thread.Sleep(60000);
                        }
                        else
                        {
                            Console.WriteLine("Retrying in " + (5 - i) + " minutes.");
                            System.Threading.Thread.Sleep(60000);
                        }
                        
                    }
                    Console.WriteLine("Retrying now.");

                    driver.Navigate().Refresh();
                    System.Threading.Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("Search was successful, an e-mail has been sent");
                }
            }
        }
    }
}