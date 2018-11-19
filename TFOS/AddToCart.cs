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
    class Cart
    {
        public WebBrowserClient webBrCl = new WebBrowserClient(20);
        public IWebDriver driver;
        List<string> prodInCart = new List<string>();
        List <IWebElement> prodItemsArr = new List<IWebElement>();
        IWebElement prodsOnMainPage;
        IWebElement prod;
        string mainPage = "http://localhost:8080/litecart/en/";
        int expected_quantity = 0;

        public Cart()
        {
            driver = webBrCl.driver;
            driver.Url = mainPage;
        }
        /// <summary>
        /// Выбирает уникальные для корзины товары.
        /// </summary>
        /// <returns></returns>
        public string SelectUniqItems()
        {
            GetProdLinksOnMainPage();
                                          
            int prodLinksNumber = 0;

                do
                {
                    bool added = false;
                    GetProdLinksOnMainPage();
                    while (!added)
                    {
                        //отбрасываем товары одноимённые добавленным ранее 
                        // Text содержит:
                        // Blue Duck
                        // ACME Corp.
                        // $20
                        if (!listNameAddedProds.Contains(prodItemsArr[prodLinksNumber].Text))
                        {
                            listNameAddedProds.Add(prodItemsArr[prodLinksNumber].Text);
                            // вызов функции перехода на страницу и добавления там в корзину
                            prodItemsArr[prodLinksNumber].Click();
                        }
                        else prodLinksNumber++;
                    }
                } while (listNameAddedProds.Count < 3);

                IWebElement cart_count = driver.FindElement(By.Id("cart"));
                
                return "В корзину добавлено товаров: " + expected_quantity;
            
        }
        void AddToCart()
        {
            
            ArrayList isSize = driver.FindElements(By.Name("options[Size]"));
            if(isSize.Length > 0)

            

            //Actions action = new Actions(driver)
            //                   .MoveToElement(driver.FindElement(By.Name("add_cart_product")))
            //                  .Click();
            var item  = driver.FindElement(By.Name("add_cart_product"));
            item.Submit();

            expected_quantity = GetActualQuantity() + 1; //кривая актуаль


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            //ожидаем добавления в корзину (пока товар долетит)
            int j = 10;
            do
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                j--;
            } while (expected_quantity != GetActualQuantity());

            // ??? 
            return  
            added = true;
        }

        int QantityInCart()
        {
            int qantity = Convert.ToInt16(driver.FindElement(By.CssSelector("#cart .quantity")).Text);
            return qantity;
        }



        /// <summary>
        /// инициализирует массив prodItemsArr товарами со страницы mainPage
        /// </summary>
        void GetProdLinksOnMainPage()
        {
            prodItemsArr.Clear();
            driver.Url = mainPage;
            prodsOnMainPage = driver.FindElement(By.Id("box-most-popular"));
            prodItemsArr.AddRange(prodsOnMainPage.FindElements(By.ClassName("link")));
        }
        
        void ClearCart()
        {
            driver.FindElement(By.CssSelector("#cart"))?.Click();

            //# order_confirmation-wrapper > table > tbody > tr:nth-child(3) > td.item
            driver.Url = mainPage;
            
        }
        
        int GetActualQuantity() => int.Parse(driver.FindElement(By.CssSelector("#cart .quantity")).Text); 

    }
}
