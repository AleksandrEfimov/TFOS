using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Collections;

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
    class WorkWithCart
    {
        public WebBrowserClient webBrCl = new WebBrowserClient(20);
        public IWebDriver driver;
        List<string> prodInCart = new List<string>();
        IWebElement Prod;
        IWebElement prodsOnMainPage;
        int quantityInCart = 0;
        WebDriverWait wait;
        IJavaScriptExecutor js;

        int GetActualQuantity() => int.Parse(driver.FindElement(By.CssSelector("#cart .quantity")).Text);
        void btnAddToCart() => driver.FindElement(By.Name("add_cart_product")).Submit();
                
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public WorkWithCart()
        {
            driver = webBrCl.driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            js = (IJavaScriptExecutor)driver;
        }


        private void ClickProdOnMainPage(int number = default)
        {
            webBrCl.ToMain();
            driver.FindElements(By.CssSelector("#box-most-popular li"))[number].Click();
        }

        /// <summary>
        /// На странице товара выбираем размер, если есть. Кликаем добавить.
        /// </summary>
        public bool AddToCart(int expextedQantity)
        {

            for (int i = 0; i < expextedQantity; i++)
            {
                ClickProdOnMainPage();

                var isSize = driver.FindElements(By.Name("options[Size]"));
                if (isSize.Count > 0)
                {
                    // isSize[0].Click();
                    SelectElement select = new SelectElement(isSize[0]);
                    select.SelectByText("Small");
                }
                btnAddToCart();
                //Actions action = new Actions(driver)
                //                   .MoveToElement(driver.FindElement(By.Name("add_cart_product")))
                //                  .Click();
                //ExpectedConditions ex = new ExpectedConditions(driver);
                var expectedQ = i + 1;
                //ожидаем добавления в корзину (пока товар долетит)
                wait.Until(wd => js.ExecuteScript(
                        "return document.querySelector('span.quantity').innerText").ToString()
                        .Equals(expectedQ.ToString()));
                
            }
            if (expextedQantity == GetActualQuantity()) 
               {
                    return true;
               }
            else return false;
        }



        ~WorkWithCart()
        {
            webBrCl.Close();
        }
        
    }


    
}
