using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using System.Collections;
using OpenQA.Selenium.Support.UI;


namespace TFOS
{
    public class WebBrowserClient
    {
        public IWebDriver driver;

        public WebBrowserClient()
        {
            this.driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(this.driver, TimeSpan.FromSeconds(10));
            this.driver.Url = "ccc";

        }


        public void SetUrl(string str)
        {
            driver.Url = $"{str}";
        }

        // логин в админ панель
        public void logInAdmin(ref IWebDriver driver, ref WebDriverWait wait)
        {
            // заходим на страничку
            driver.Url = "http://localhost:8080/litecart/admin/";
            // логин
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            // пароль
            //// в поисках ошибки применён xPath - не помог, но работает.
            driver.FindElement(By.XPath("//*[@id=\"box-login\"]/form/div[1]/table/tbody/tr[2]/td[2]/span/input")).SendKeys("admin");
            //// данному логину необходим был Клик, в то время как Гугл требовал сабмит
            driver.FindElement(By.Name("login")).Click();
        }


        public void Close()
        {
            this.driver.Close();
            this.driver.Quit();
            this.driver = null;
        }

    }
}
