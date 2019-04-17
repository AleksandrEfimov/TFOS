using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;

namespace TFOS
{
    public partial class WebBrowserClient
    {
        public IWebDriver driver;
        
        int delay = 15;
        WebDriverWait wait;

        public WebBrowserClient()
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(delay));
           // driver.Url = "http://ya.ru";

        }

        

        public WebBrowserClient(int _delay)
        {
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(_delay));
            //driver.Url = "http://yandex.ru";
        }

        
        public void SetUrl(string str)
        {
            driver.Url = $"{str}";
        }


        
        
        
        

        public void ToCatalog()
        {
            var catLink = this.driver.FindElement(By.LinkText("Catalog"));
            catLink.Click();
        }

        public void Close()
        {
            driver.Close();
            driver.Quit();
            driver = null;
        }
        public void ToMain()
        {
            driver.Url = "http://localhost:8080/litecart/en/";
        }

    }
}
