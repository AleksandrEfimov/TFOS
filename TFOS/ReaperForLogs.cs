using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;




namespace TFOS
{
    class ReaperForLogs 
    {
        WebBrowserClient webCl = new WebBrowserClient();
        IWebDriver driver;
        WebDriverWait wait;
        IJavaScriptExecutor js;
        StringBuilder sb = new StringBuilder();

        public ReaperForLogs()
        {
            driver = webCl.driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            js = (IJavaScriptExecutor)driver;
        }


        public StringBuilder TimeToReapLogs()
        {
            webCl.logInAdmin();
            driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1)";

            for (int i = 5; i < 10; i++)
            {
                driver.FindElement(By.CssSelector($"form[name=catalog_form] table.dataTable tr:nth-child({i}) td a")).Click();
                driver.Navigate().Back();
            }
    
            foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
            {
                sb.AppendLine(l.ToString());
            }
            return sb;
        }

        ~ReaperForLogs()
        {
            driver.Quit();
        }
    }
}
