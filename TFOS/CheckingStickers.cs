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
    class CheckingStickers 
    {
        // private?
        public WebBrowserClient webBrCl = new WebBrowserClient();
        IWebDriver wd;
        

        public CheckingStickers(string str)
        {
            webBrCl.SetUrl(str);
            wd = webBrCl.driver;
        }
           
        // находим все товары
        public bool IsAllProductHaveSticker()
        {
            // var products = webBrCl.driver.FindElements(By.ClassName("product"));
            var products = wd.FindElements(By.CssSelector(".product.column.shadow.hover-light"));
            
            foreach (var prd in products)
            {
                if (prd.FindElements(By.CssSelector(".sticker")).Count == 0)
                    return false;
            }
            return true;
        }

        //~CheckingStickers()
        //{
        //    webBrCl.Close();
        //}

        



    }
}
