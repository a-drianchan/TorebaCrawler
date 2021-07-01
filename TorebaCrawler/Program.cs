using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.Profiler;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TorebaCrawlerCore.Services;

namespace TorebaCrawlerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Toreba Crawler Core");

            // Setup Headless Chrome Options
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            //options.AddArgument("user-data-dir=./data/ChromeCookies");
            options.AddArgument("--headless");
            options.AddArgument("--log-level=3");

            RemoteWebDriver driver = WebLogInService.LogInToreba(new ChromeDriver(options), "username", "password");

            DataAccess MachineDBAccess = new DataAccess();
            bool status = true;

            var timer = Stopwatch.StartNew();

            while (true)
            {
                // Restart driver if unable to load replay page
                if(!status)
                {
                    driver.Close();
                    driver = WebLogInService.LogInToreba(new ChromeDriver(options), "username", "password");
                    status = true;
                }

                timer.Restart();

                status = MachineExtractionService.ScrapWithHtmlAgilityPack(driver, MachineDBAccess);

                timer.Stop();
                Console.WriteLine($"Finished Updating Machine List at { DateTime.Now }, finished Update in {timer.ElapsedMilliseconds} ms.");

                timer.Restart();

                status = ReplayScraperService.ScrapForWinningReplay(driver, MachineDBAccess);

                timer.Stop();
                Console.WriteLine($"Finished Updating Replay List at { DateTime.Now }, finished Update in {timer.ElapsedMilliseconds} ms.");

                Console.WriteLine("Taking one minute break.");
                Thread.Sleep(60000);
                Console.WriteLine("Resume scraping");

            }

        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Exiting...");

        }
    }
}
