using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace TFOS
{
    class CheckingAdminMenu
    {
        
        public WebBrowserClient webBrCl;
        IWebDriver driver;


        string SetUrl { get;set; }

        public CheckingAdminMenu(string seturl)
        {
            webBrCl = new WebBrowserClient(30);
            SetUrl = seturl;
            driver = webBrCl.driver;
        }

        public void CheckMenu()
        {
            webBrCl.logInAdmin();
            var menuItem = driver.FindElements(By.CssSelector("#app-"));
            int countMenuItem = menuItem.Count;
            int i = -1;
            do
            {
                //refresh
                menuItem = driver.FindElements(By.CssSelector("#app-"));

                // проверим есть ли открытые подменю
                // TODO: инкапсулировать в функцию 
                var menuSubItem = driver.FindElements(By.CssSelector("ul.docs li"));
                int countMenuSubItem = menuSubItem.Count;

                if (countMenuSubItem > 0)
                    {
                        for (int j = 0; j < countMenuSubItem; j++)
                            {
                                // refresh
                                menuSubItem = driver.FindElements(By.CssSelector("ul.docs li"));
                                menuSubItem[j].Click();
                            }
                        // готовим переход к следующему пункту меню
                        i++;
                        //refresh
                        if (i < countMenuItem)
                            {
                                menuItem = driver.FindElements(By.CssSelector("#app-"));
                                menuItem[i].Click();
                                continue;
                            }
                    }
                if (i < countMenuItem - 1)
                    {
                        menuItem[++i].Click();
                    }

            } while (i < countMenuItem);
        }
        ~CheckingAdminMenu()
        {
        }
    }
}   
        
            

        
        