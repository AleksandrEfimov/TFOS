using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace TFOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Проход по меню админки
        private void button1_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait;
            string log = "";
            // set timeout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


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


            // как критерий входа в админ-панель - виджет статистики интернет-магазина
            try
            {
                var login = wait.Until(ExpectedConditions.ElementExists(By.Id("widget-stats")));
                //if (login != null)
                  //  MessageBox.Show("Login completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check the site");
            }

            
            var menuItem = driver.FindElements(By.CssSelector("#app-"));
            int countMenuItem = menuItem.Count;


           
            int i = -1;
            do
            {
                
                //refresh
                menuItem = driver.FindElements(By.CssSelector("#app-"));

                    // проверим есть ли открытые подменю
                    var menuSubItem = driver.FindElements(By.CssSelector("ul.docs li"));
                    int countMenuSubItem = menuSubItem.Count;

                    if (countMenuSubItem > 0)
                    {
                        for (int j = 0; j < countMenuSubItem; j++)
                        {
                            // refresh
                            menuSubItem = driver.FindElements(By.CssSelector("ul.docs li"));
                            menuSubItem[j].Click();
                            log += "Меню: " + i + " подменю: " + j + ",\n";
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

                menuItem[++i].Click();
               
                log += "Меню: " + i + ",\n";

            } while (i < countMenuItem);

            MessageBox.Show(log);
            
            driver.Quit();
            driver = null;
        }





        // Проверка наличия стикеров
        private void button2_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait;
            string log = "";
            // set timeout
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Url = "http://localhost:8080/litecart/";
           
            var products = driver.FindElements(By.ClassName("product"));
            //MessageBox.Show("Amount of products: "+ products.Count);

            IWebElement sticker;
            int i = 0;
            foreach (var prd in products)
            {
                
                try
                {
                    sticker = prd.FindElement(By.ClassName("sticker"));
                    i++;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                
                
            }
            products[0].Click();
            MessageBox.Show("Amount of stickers: " + i);


            driver.Close();
            driver.Quit();
            driver = null;
        }
    }
}
