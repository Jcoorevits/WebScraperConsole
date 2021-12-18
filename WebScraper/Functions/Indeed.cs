using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraper.Functions
{
    public class Indeed
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
            driver.Navigate().GoToUrl("https://be.indeed.com/");

            // Accept cookies
            System.Threading.Thread.Sleep(500);
            driver.FindElement(By.CssSelector(
                    "#onetrust-accept-btn-handler"))
                .Click();

            // User input search term and submit
            Console.Write("What job title would you like to search: ");
            var job = Console.ReadLine();
            Console.Write("What location would you like to search?: ");
            var where = Console.ReadLine();
            var findElementJob = driver.FindElement(By.CssSelector("#text-input-what"));
            findElementJob.Click();
            findElementJob.SendKeys(job);
            System.Threading.Thread.Sleep(500);
            var findElementLocation = driver.FindElement(By.CssSelector("#text-input-where"));
            findElementLocation.Click();
            findElementLocation.SendKeys(where);
            findElementLocation.Submit();
            
            // Make CSV template if not exists
            CsvWriter.FileTemplate("Title,Company,Location,Link", job+where);

            // Filter days
            driver.FindElement(By.CssSelector("#filter-dateposted")).Click();
            driver.FindElement(By.CssSelector("#filter-dateposted-menu > li:nth-child(2)")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#popover-x > button")).Click();

            var title = driver.FindElements(By.XPath("/html/body/table[2]/tbody/tr/td/table/tbody/tr/td[1]/div[5]/div/a/div[1]/div/div[1]/div/table[1]/tbody/tr/td/div[1]/h2/span"));
            var company = driver.FindElements(By.XPath("//span[contains(@class, 'companyName')]"));
            var location = driver.FindElements(By.XPath("//div[contains(@class, 'companyLocation')]"));
            
            var link = driver.FindElements(By.XPath("/html/body/table[2]/tbody/tr/td/table/tbody/tr/td[1]/div[5]/div/a"));

            int count = title.Count();
            for (int i = 0; i < count; i++)
            {
                
                Console.WriteLine(title[i].Text);
                Console.WriteLine(company[i].Text);
                Console.WriteLine(location[i].Text);
                CsvWriter.IndeedCsv(title[i].Text, company[i].Text, location[i].Text, link[i].GetAttribute("href") ,job+where);
                Console.WriteLine(link[i].GetAttribute("href"));
            }
            driver.Quit();
        }
    }
}