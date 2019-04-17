using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TFOS
{
    public partial class WebBrowserClient
    {
                
        // логин в админ панель
        public void logInAdmin()
        {
            try
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

                var login = wait.Until(ExpectedConditions.ElementExists(By.Id("widget-stats")));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Check the site. Admin." + ex);
            }
        }
    }
}
