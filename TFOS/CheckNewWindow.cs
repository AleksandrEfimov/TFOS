using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;


namespace TFOS
{
    class CheckNewWindow
    {
        public WebBrowserClient webBrCl = new WebBrowserClient(20);
        public IWebDriver driver;
        List<string> prodInCart = new List<string>();
        IWebElement Prod;
        IWebElement prodsOnMainPage;
        int quantityInCart = 0;
        WebDriverWait wait;
        IJavaScriptExecutor js;
        public StringBuilder sbIsError;
        Dictionary<string, string> dictErr = new Dictionary<string,string>();
        public List<IWebElement> linkArr => driver.FindElements(By.CssSelector("form a[href^='http']")).ToList();

        int GetActualQuantity() => int.Parse(driver.FindElement(By.CssSelector("#cart .quantity")).Text);
        void btnAddToCart() => driver.FindElement(By.Name("add_cart_product")).Submit();

        public CheckNewWindow()
        {
            driver = webBrCl.driver;
            webBrCl.logInAdmin();
            webBrCl.SetUrl("http://localhost:8080/litecart/admin/?app=countries&doc=countries");
            
        }

        public void SwithToTab(List<IWebElement> linkArr)
        {
            int i = 0;
            string[] baseHandles = { driver.CurrentWindowHandle };
            foreach (var link in linkArr)
            {
                link.Click();
                var href = link.GetAttribute("href");
                var newTab = driver.WindowHandles.Except(baseHandles);
                driver.SwitchTo().Window(newTab.First());
                if (href != driver.Url)
                {
                    //dictErr.Add(driver.Url, link.GetAttribute("href"));
                    sbIsError.Append(i++ +") " + href + " - " + driver.Url);
                }
            }
        }
    }
}
