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
        

        public CheckingStickers(string str)
        {
            this.webBrCl.SetUrl(str);
        }
           
        // находим все товары
        public void FindAllProducts()
        {
            var products = webBrCl.driver.FindElements(By.ClassName("product"));

            IList<IWebElement> stickers;
            int i = 0;


            foreach (var prd in products)
            {
                stickers = prd.FindElements(By.ClassName("sticker"));
                    if (stickers.Count>0)
                        i++;
            }
            
            MessageBox.Show("Product number is: " + products.Count + " ; and stickers number is: " + i+" ;");
            
        }

        //~CheckingStickers()
        //{
        //    webBrCl.Close();
        //}

        



    }
}
