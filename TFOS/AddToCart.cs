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
        List <IWebElement> prodArrayOnMain;
        IWebElement prodName;
        IWebElement prod;

        public AddToCart()
        {
            driver = webBrCl.driver;
            driver.Url = "http://localhost:8080/litecart/en/";
        }

        public void GetProdOnMain()
        {
            int i = 0;
            do
            { 
                // список товаров представленых на Main
                prodArrayOnMain.AddRange( driver.FindElements(By.ClassName("product column shadow hover-light")));
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
                    IWebElement cart_count = driver.FindElement(By.XPath("[@id=\"cart\"]/a[2]/span[1]"));
                    Actions action = new Actions(driver)
                                        .MoveToElement(driver.FindElement(By.Name("add_cart_product")))
                                        .Click();
                    // написать ожидание изменения числа товаров в корзине
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    var manufacturer_id = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));



                    // работаем с новой страницей


                }
                else i++;

            } while (listAddedName.Count != 3 );
            // && prodName.Text != "Yellow Duck"
        }


    }
}
