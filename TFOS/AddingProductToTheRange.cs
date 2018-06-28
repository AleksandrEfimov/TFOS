using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TFOS
{
    class AddingProductToTheRange
    {
        public WebBrowserClient webBrCl = new WebBrowserClient(20);
        public IWebDriver driver;
        string _name = "DuckPie-" + DateTime.Now;
        public AddingProductToTheRange()
        {
            driver = webBrCl.driver;
            webBrCl.logInAdmin();
        }

        public void btnAddNewProductClick()
        {
            var button = driver.FindElements(By.CssSelector(".button"));
            button[1].Click();

        }
        

        public string CatalogGeneral()
        {


            try
            {
                var tabGeneral = driver.FindElement(By.LinkText("General"));
                tabGeneral.Click();
                var status = driver.FindElements(By.Name("status"));
                if (status[0].Selected == false)
                    status[0].Click();
                var nameEn = driver.FindElement(By.Name("name[en]"));
                
                nameEn.SendKeys(_name);
                var code = driver.FindElement(By.Name("code"));
                code.SendKeys("123");
                var categories = driver.FindElements(By.Name("categories[]"));
                if (categories[1].Selected != true)
                    categories[1].Click();
                var prod_group = driver.FindElements(By.Name("product_groups[]"));
                prod_group[2].Click();
                var quantity = driver.FindElement(By.Name("quantity"));
                quantity.Clear();
                quantity.SendKeys("3.14");
                var file_input = driver.FindElement(By.Name("new_images[]"));
                var txtFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, ".\\img\\duck.jpg");
                file_input.SendKeys(txtFilePath);
                //file_input.;
                //file_input.Click();
                var sendFile = driver.FindElement(By.Id("add-new-image"));
                sendFile.Click();
                var dateValidFrom = driver.FindElement(By.Name("date_valid_from"));
                dateValidFrom.SendKeys(Keys.Home+"25.12.2018");
                var dateValidTo = driver.FindElement(By.Name("date_valid_to"));
                dateValidTo.SendKeys(Keys.Home + "31.12.2018");


                //var nameEn = driver.FindElement(By.Name("name[en]"));
                return "tab-general filled";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }




        }

        public string CatalogInformation()
        {
            try
            {
                var tabInformation = driver.FindElement(By.LinkText("Information"));
                tabInformation.Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                var manufacturer_id = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));
                manufacturer_id.SendKeys("ACME Corp.");
                //var supplier_id = driver.FindElement(By.Name("supplier_id"));
                var keywords = driver.FindElement(By.Name("keywords"));
                keywords.SendKeys("good things");
                var short_descriptionEN = driver.FindElement(By.Name("short_description[en]"));
                short_descriptionEN.SendKeys("Sensation! It is lost picture of great drawer!");
                var trumbowyg_editor = driver.FindElements(By.ClassName("trumbowyg-editor"));
                trumbowyg_editor[0].Clear();
                trumbowyg_editor[0].SendKeys("Bla bla!"+ Keys.Control + "a");
                trumbowyg_editor[0].SendKeys(Keys.Control + "b");

                trumbowyg_editor[0].SendKeys(Keys.Right + Keys.Enter);
                trumbowyg_editor[0].SendKeys(Keys.Control + "b");
                trumbowyg_editor[0].SendKeys( "O bla blushki!" + 
                    Keys.Shift + Keys.Home + 
                    Keys.Control + "i");
                //var defCont = driver.SwitchTo().DefaultContent();
                //driver.FindElements(By.ClassName("trumbowyg-bold-button "))[0].Click();
                var head_titleEN = driver.FindElement(By.Name("head_title[en]"));
                head_titleEN.SendKeys("duckPie - lucky pie");

                var meta_descriptionEN = driver.FindElement(By.Name("meta_description[en]"));
                meta_descriptionEN.SendKeys("If you understand me...");


                return "tab-information filled";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string CatalogPrices()
        {
            try
            {
                driver.FindElement(By.LinkText("Prices")).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                var purchase_price = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("purchase_price")));
                purchase_price.Clear();
                purchase_price.SendKeys("2.71");
                var purchase_price_currency_code = driver.FindElement(By.Name("purchase_price_currency_code"));
                purchase_price_currency_code.SendKeys("Euros");
                //var campaignsNew_1Start_date = driver.FindElement(By.Name("campaigns[new_1][start_date]"));
                //campaignsNew_1Start_date.SendKeys("2018-06-27");
                //var campaignsNew_1End_date = driver.FindElement(By.Name("campaigns[new_1][end_date]"));
                //campaignsNew_1End_date.SendKeys("2018-08-26");
                //var persent = driver.FindElement(By.Name("c"));
                //persent.SendKeys("50");
                var btnSave = driver.FindElement(By.Name("save"));
                btnSave.Click();

                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5); 
                
                var productIsAdded = wait.Until(ExpectedConditions.ElementExists(By.LinkText(_name))); 
                return "Product named"+_name+" success added in ranger!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }



    }
}
