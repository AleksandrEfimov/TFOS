using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

/* 1 - получаем список тоаров на Main
 * 2 - ищем link`и товаров
 * 3 - выбираем 1й
 * 4 - смотрим в списке название, если уже есть:
 * 4.1 - выбираем +1 
 * 4.2 -> 4
 * 5 - link.click()
 * 6 - на странице товара ищем Имя
 * 6.1 - если Yellow Duck - ищем DDL размера 
 * 6.2 - Выбираем размер
 * 7 - клик по кнопке добавления в корзину
 * 8 - возврат к mainpage
 * 9 - 
*/


namespace TFOS
{
    class AddToCart
    {
        public WebBrowserClient webBrCl = new WebBrowserClient(20);
        public IWebDriver driver;
        List<string> listAddedName = new List<string>();
        List <IWebElement> prodArrayOnMain = new List<IWebElement>();
        IWebElement prodName;
        IWebElement prod;

        public AddToCart()
        {
            driver = webBrCl.driver;
            driver.Url = "http://localhost:8080/litecart/en/";
        }

        public string GetProdOnMain()
        {
            try
            {
                var qntty = driver.FindElement(By.CssSelector("#cart .quantity")).Text;
                int actual_quantity = int.Parse(qntty);
                int i = 0;
                int expected_quantity = 0;

                do
                {
                    // список товаров представленых на Main
                    //prodArrayOnMain.AddRange(driver.FindElements(By.ClassName("product column shadow hover-light")));
                    //#box-most-popular > div > ul > li:nth-child(2)
                    // //*[@id="box-most-popular"]/div/ul/li[2]
                    //prodArrayOnMain.AddRange(driver.FindElements(By.XPath("*[@id=\"box - most - popular\"]/div/ul/li")));
                    prodArrayOnMain.AddRange(driver.FindElements(By.Id("box-most-popular")));

                    prodName = prodArrayOnMain[i].FindElement(By.ClassName("link"));

                    if (!listAddedName.Contains(prodName.Text))
                    {
                        listAddedName.Add(prodName.Text);
                        // вызов функции перехода на страницу и добавления там в корзину
                        prodName.Click();
                        prod = driver.FindElement(By.TagName("h1"));
                        if ("Yellow Duck".Equals(prod.Text))
                        {
                            prod = driver.FindElement(By.Name("options[Size]"));
                            prod.SendKeys("Large"); // или Large +$5
                        }
                        IWebElement cart_count = driver.FindElement(By.Id("cart"));
                        // #cart
                        Actions action = new Actions(driver)
                                            .MoveToElement(driver.FindElement(By.Name("add_cart_product")))
                                            .Click();
                        expected_quantity = actual_quantity + 1;

                        // посмтреть что такое iclock clock
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


                        //var manufacturer_id = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));
                        //ПЕРЕПИСАТь - возможен вечный цикл !!!
                        do
                        {
                            qntty = driver.FindElement(By.CssSelector("#cart .quantity")).Text;
                        } while (expected_quantity == int.Parse(qntty));
                        //возврат на стартовуюя страницу


                    }
                    else i++;

                } while (listAddedName.Count != 3);
                // && prodName.Text != "Yellow Duck"

                return "В корзину добавлено товаров: " + expected_quantity;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


    }
}
