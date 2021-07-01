using HtmlAgilityPack;
using OpenQA.Selenium.DevTools.DOM;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace TorebaCrawlerCore.Services
{
    public class ReplayScraperService
    {
        /// <summary>
        /// Go through the Winning Replay page and scrap all links.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool ScrapForWinningReplay(RemoteWebDriver driver, IDataAccess dataAccess)
        {
            List<Replay> ReplayList = new List<Replay>();

            driver.Navigate().GoToUrl("https://www.toreba.net/replay");

            // Download the dynamic website using Selenium, but process with HtmlAgilityPack for speed
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(driver.PageSource);

            HtmlNodeCollection _replayNodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'area')]//li");

            // Extract link from the 20 replays
            for( int count = 0; count < 20; count++)
            {
                try
                {
                    var link = _replayNodes[count].SelectSingleNode(".//dd[@class='factors']").SelectSingleNode(".//a[@class='js_serviceName']").GetAttributeValue("href", String.Empty);
                    int _replayID = Int32.Parse(link.Split('/')[5]);

                    if (dataAccess.GetReplayByReplayID(_replayID).Count == 0)
                    {
                        Console.WriteLine($" { _replayID } not found. Adding to database.");
                        Debug.WriteLine($" { _replayID } not found. Adding to database.");
                        ReplayList.Add(PopulateReplayDetail(driver, link));
                    }
                    else
                    {
                        Debug.WriteLine($"Replay { _replayID } found. Skipping database insert.");
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    Console.WriteLine("Unable to fetch data from Toreba.");

                    return false;
                }
            }

            foreach (Replay replay in ReplayList)
            {
                dataAccess.InsertReplay(replay);
            }

            return true;
        }

        /// <summary>
        /// Given the Replay link, generate a replay object
        /// </summary>
        /// <param name="driver"> Browser</param>
        /// <param name="link"> Replay Link</param>
        /// <returns></returns>
        public static Replay PopulateReplayDetail(RemoteWebDriver driver, String link)
        {
            Replay Replay = new Replay();

            driver.Navigate().GoToUrl(link);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(driver.PageSource);

            // TODO ADD IN CODE TO DEAL WITH EMPTY NODE / PRIZE NAME
            String _replayName = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'area')]").SelectSingleNode("//div[contains(@class,'video_title js_serviceName')]").InnerText;
            String _prizeName = "N/A";

            if (_replayName.Split(new char[] { '[', ']' }).Length == 3 )
            {
                _prizeName = _replayName.Split(new char[] { '[', ']' })[1];
            }

            Replay.PrizeName = _prizeName;

            String VideoLink = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'area')]").
                SelectSingleNode("//div[contains(@class,'entry_replay')]//video//source").
                GetAttributeValue("src",String.Empty);

            String[] _detailList;

            if (VideoLink != String.Empty)
            {
                String _description = VideoLink.Split("/")[7];
                _detailList = _description.Split(new char[] { '_', '.' });

                Replay.MachineID = Int32.Parse(_detailList[0]);
                Replay.ReplayID = Int32.Parse(_detailList[3]);
                Replay.ReplayLink = VideoLink;

                string _timeString = _detailList[1] + _detailList[2];
                Replay.Time = DateTime.ParseExact(_timeString, "yyyyMMddhhmmss", CultureInfo.CreateSpecificCulture("en-US"));
            }

            return Replay;
        }

    }
}
