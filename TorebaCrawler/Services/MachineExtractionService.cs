using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.Network;
using OpenQA.Selenium.Remote;
using ScrapySharp.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TorebaCrawlerCore.Enums;

namespace TorebaCrawlerCore
{
    public static class MachineExtractionService
    {

        /// <summary>
        /// Extract a list of all machines 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool ScrapWithHtmlAgilityPack(RemoteWebDriver driver, IDataAccess dataAccess)
        {

            driver.Navigate().GoToUrl("https://www.toreba.net/en/play");

            List <Machine> MachineList = new List<Machine>();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(driver.PageSource);
            
            HtmlNodeCollection _machineNodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'layout_search clearfix m_itemMenu_wrapper')]//li");

            if (_machineNodes != null)
            {
                foreach (HtmlNode node in _machineNodes)
                {
                    IEnumerable<string> _propertyString = node.GetClasses();

                    if (_propertyString.First().Contains("hardware_"))
                    {
                        Machine temp = new Machine();

                        // Add Name
                        temp.MachineName = node.SelectSingleNode(".//h3[@class='title js_serviceName']").InnerText;

                        // Add TP
                        string _costString = node.SelectSingleNode(".//span[@class='m_TP']").InnerText;
                        temp.Cost = Int32.Parse(_costString, NumberStyles.AllowThousands);

                        // Add Machine ID
                        temp.MachineID = Int32.Parse(_propertyString.First().Split("_")[1]);

                        // Machine Type
                        if (_propertyString.Contains("bridge")) { temp.MachineType = MachineType.Bridge; }
                        else if (_propertyString.Contains("drop")) { temp.MachineType = MachineType.Drop; }
                        else if (_propertyString.Contains("panel")) { temp.MachineType = MachineType.Panel; }
                        else if (_propertyString.Contains("takoyaki")) { temp.MachineType = MachineType.Takoyaki; }
                        else if (_propertyString.Contains("ring")) { temp.MachineType = MachineType.Ring; }
                        else { temp.MachineType = MachineType.None; };

                        // Cost Type
                        if (_propertyString.Contains("only_tp")) { temp.CostType = CostType.TpOnly; }
                        else if (_propertyString.Contains("no_tutorial")) { temp.CostType = CostType.NoTutorial; }
                        else { temp.CostType = CostType.All; };

                        // Status
                        if (_propertyString.Contains("SoldOut")) { temp.StatusType = StatusType.SoldOut; }
                        else if (_propertyString.Contains("playing")) { temp.StatusType = StatusType.InUse; }
                        else { temp.StatusType = StatusType.Open; };

                        // Deal with special / additional tags that will be used 
                        if (temp.MachineName.Contains("Add-On")) { temp.SpecialTagType = SpecialTagType.Sale; }
                        else if (_propertyString.Contains("super_chance")) { temp.SpecialTagType = SpecialTagType.SuperChance; }
                        else if (_propertyString.Contains("one_get")) { temp.SpecialTagType = SpecialTagType.PrizeGrab; }
                        else if (_propertyString.Contains("limited")) { temp.SpecialTagType = SpecialTagType.Limited; }
                        else if (_propertyString.Contains("new")) { temp.SpecialTagType = SpecialTagType.New; }
                        else if (_propertyString.Contains("recommended")) { temp.SpecialTagType = SpecialTagType.Recommended; }
                        else { temp.SpecialTagType = SpecialTagType.None; };

                        MachineList.Add(temp);
                    }
                }

                watch.Stop();
                Debug.WriteLine($"Web scrap finished in { watch.ElapsedMilliseconds } ms");
            }
            else
            {
                return false;
            }
       
            // Add machine to the datatbase

            foreach (Machine machine in MachineList)
            {
                dataAccess.InsertMachine(machine);
            }

            return true;
        }

    }
}
