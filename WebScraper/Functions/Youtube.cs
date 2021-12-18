using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebScraper.Functions
{
    class Youtube
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
            driver.Navigate().GoToUrl("https://www.youtube.com/");


            // Accept cookies
            driver.FindElement(By.XPath(
                    "/html/body/ytd-app/ytd-consent-bump-v2-lightbox/tp-yt-paper-dialog/div[4]/div[2]/div[5]/div[2]/ytd-button-renderer[2]/a/tp-yt-paper-button"))
                .Click();

            // User input search term and submit
            Console.Write("What would you like to search: ");
            var input = Console.ReadLine();
            var findElement = driver.FindElement(By.XPath(
                "/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input"));
            findElement.Click();
            findElement.SendKeys(input);
            findElement.Submit();
            
            // Make CSV template if not exists
            CsvWriter.FileTemplate("Title,Author,Views", input);
            
            // filter search term on new video's
            var filters = driver.FindElement(By.XPath(
                "/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/a/tp-yt-paper-button/yt-formatted-string"));

            filters.Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath(
                    "/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[1]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string"))
                .Click();
            System.Threading.Thread.Sleep(1000);

            filters.Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.XPath(
                    "/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string"))
                .Click();
            System.Threading.Thread.Sleep(1000);

            // Look for title, author, view
            var songTitles = driver.FindElements(By.CssSelector("#video-title > yt-formatted-string"));
            var songAuthor = driver.FindElements(By.CssSelector("#channel-info"));
            var songViews = driver.FindElements(By.CssSelector("#metadata-line > span:nth-child(1)"));

            // Get first 5 objects
            for (int i = 0; i < 5; i++)
            {
                CsvWriter.YoutubeCsv(songTitles[i].Text, songAuthor[i].Text, songViews[i].Text, input );
                Console.WriteLine(songTitles[i].Text);
                Console.WriteLine(songAuthor[i].Text);
                Console.WriteLine(songViews[i].Text);
            }
            driver.Quit();
        }
    }
}