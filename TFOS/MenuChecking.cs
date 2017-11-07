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
    class MenuChecking 
    {
        // private?
        public WebBrowserClient webBrCl = new WebBrowserClient();
        

        public MenuChecking(string str)
        {
            this.webBrCl.SetUrl(str);
        }

        

        //IWebDriver driver = new ChromeDriver();
        //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); ;

        // переход к товарам
        //webBrCl.SetUrl("http://localhost:8080/litecart/");
           
        // находим все товары
        public void FindAllProducts()
        {


            var products = webBrCl.driver.FindElements(By.ClassName("product"));

            IList<IWebElement> stickers;
            int i = 1;


            foreach (var prd in products)
            {
                try
                {
                    stickers = prd.FindElements(By.ClassName("sticker"));
                    if (i < stickers.Count)
                        i = stickers.Count;

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            products[0].Click();
            if (i > 1)
                MessageBox.Show($"Amount of stickers is more than 1: {i}");

        }

        ~MenuChecking()
        {
            webBrCl.Close();
        }

        



    }
}
