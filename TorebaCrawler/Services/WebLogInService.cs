using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace TorebaCrawlerCore
{
    public static class WebLogInService
    {

        /// <summary>
        /// Provide a RemoteWebDriver that is logged into a Toreba account
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static RemoteWebDriver LogInToreba(RemoteWebDriver driver, string UserName, string Password)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

                driver.Navigate().GoToUrl("https://www.toreba.net/en/account/login");
                driver.FindElementByName("user_id").SendKeys(UserName);
                driver.FindElementByName("password").SendKeys(Password);
                driver.FindElementByClassName("btn_notice").Click();

                watch.Stop();
                Debug.WriteLine($"Finished Log-In in { watch.ElapsedMilliseconds } ms.");                

                return driver;

        }


        public static RemoteWebDriver OpenToreba(RemoteWebDriver driver)
        {

            driver.Navigate().GoToUrl("https://www.toreba.net/en/play");

            return driver;

        }
    }
}
