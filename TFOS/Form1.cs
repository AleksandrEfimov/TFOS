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
using System.Collections;

namespace TFOS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // логин в админ панель
        void logInAdmin(ref IWebDriver driver, ref WebDriverWait wait)
        {
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
        }

        


        // Проход по меню админки
        private void button1_Click(object sender, EventArgs e)
        {
            string log = "";
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            

            logInAdmin(ref driver, ref wait);
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

                if (i < countMenuItem-1 )
                {
                    menuItem[++i].Click();
                }

                log += "Меню: " + i + ",\n";

            } while (i < countMenuItem);

            MessageBox.Show(log);
            driver.Close();
            driver.Quit();
            driver = null;
        }





        // Проверка наличия стикеров
        private void button2_Click(object sender, EventArgs e)
        {
            // переход к товарам
            MenuChecking MCheck = new MenuChecking("http://localhost:8080/litecart/");
            MCheck.FindAllProducts();
            MCheck.webBrCl.Close();

        }


        // ЗАДАНИЕ 9-1 Проверка сортировки стран - zone  и их областей - subone
        private void button3_Click(object sender, EventArgs e)
        {

            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait( driver, TimeSpan.FromSeconds(10));

            StringBuilder log = new StringBuilder("");
            IWebElement dataTable;
            IList<IWebElement> trows, zone, zoneNext;
            bool isEnteredzone = false;

            // функция обновления контекста - описание
            void newContext()
            {
                // таблица со странами
                dataTable = driver.FindElement(By.ClassName("dataTable"));
                // массив строк
                trows = dataTable.FindElements(By.TagName("tr"));
            }
            // функция проверки сортивки стран и подзон по алфавиту - описание
            void reSorting(ref IList<IWebElement> tRowSort)
            {
                for (int i = 1; i < tRowSort.Count - 2; i++)
                {
                    if (isEnteredzone)
                    {
                        newContext();
                    }


                    //  Строки таблицы 
                    zone = trows[i].FindElements(By.TagName("td"));
                    zoneNext = trows[i + 1].FindElements(By.TagName("td"));
                    // значения ячеек - названия стран
                    string str1 = zone[4].Text;
                    string str2 = zoneNext[4].Text;
                    // проверяем, что str2 в алфавитном порядке идёт после str1
                    if (str2.CompareTo(str1) != 1)
                        MessageBox.Show("Проверяемая строка-" + i + ". Ошибка сортировки : " + str1 + " и " + str2);
                    else
                    {
                        log.AppendLine(i + "-й шаг, страны: " + str1 + " " + str2 + "; ");
                    }
                    // для отображения прохождения по всем строкам
                    zone[0].Click();

                    // проверка наличия подзон, вход, сравнение
                    if (zone[5].Text != "0")
                    {
                        // количество подзон
                        int AmountOfState = Convert.ToInt16(zone[5].Text);

                        if (AmountOfState > 0)
                        {
                            // входим в страну (не война)
                            try
                            {
                                var refCountry = zone[4].FindElement(By.TagName("a"));
                                MessageBox.Show("Переход к " + zone[4].Text);
                                // переход к подзонам
                                refCountry.Click();
                                // обновление таблицы
                                newContext();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Проблема с переходом к подзонам" + ex.ToString());
                            }

                            // сравнение подзон
                            for (int j = 1; j < trows.Count - 2; j++)
                            {
                                //Строки таблицы 
                                zone = trows[j].FindElements(By.TagName("td"));
                                zoneNext = trows[j + 1].FindElements(By.TagName("td"));
                                // значения ячеек - названия стран
                                str1 = zone[2].Text;
                                str2 = zoneNext[2].Text;
                                // проверяем, что str2 в алфавитном порядке идёт после str1
                                if (str2.CompareTo(str1) >0)
                                    MessageBox.Show("Проверяемая строка-" + j + "  " + str1 + " и " + str2);
                                else
                                {
                                    log.AppendLine(i + "." + j + " шаг, территории: " + str1 + " " + str2 + "; ");
                                }
                            }
                            // меняем флаг захода в подменю
                            isEnteredzone = true;
                            var menuItem = driver.FindElements(By.Id("app-"));
                            menuItem[2].Click();
                        }

                    }
                    // вернулись в country - треба обновить строки.
                    newContext();
                }
            }
            
            // войти в админку
            logInAdmin(ref driver, ref wait);

            // перейти на адрес
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            // обновить контекст
            newContext();

            try
            {
                reSorting(ref trows);
                MessageBox.Show("Парам-парам-пам, ВСЁ!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            driver.Close();
            driver.Quit();
        }



        // ЗАДАНИЕ 9-2
        private void button4_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            logInAdmin(ref driver, ref wait);

            IWebElement dataTable, zonelink;
            IList<IWebElement> zone, zoneNext,zone1,zoneNext1,  trows;
            string valueZone1, valueZone2;
            
            void newContext()
            {
                driver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
                dataTable = driver.FindElement(By.ClassName("dataTable"));
                trows = dataTable.FindElements(By.TagName("tr"));
            }


            newContext();

            for (int j = 1; j<trows.Count-1; j++)
            {
                
                zone = trows[j].FindElements(By.TagName("td"));
                zonelink = zone[2].FindElement(By.TagName("a"));

                // входим в страну,
                zonelink.Click();
                
                // таблица subzone
                dataTable = driver.FindElement(By.Id("table-zones"));
                trows = dataTable.FindElements(By.TagName("tr"));

                // получаем и сравниваем значения 2х соседних subzone
                for (int i = 1; i < trows.Count - 2; i++)
                {
                    //получаем массив ячеек
                    
                    zone1 = trows[i].FindElements(By.TagName("td"));
                    zoneNext1 = trows[i + 1].FindElements(By.TagName("td"));

                    // 2я(3я) ячейка содержит выпадающий список

                    valueZone1 = zone1[2].FindElement(By.CssSelector("select option[selected = selected]")).GetAttribute("innerHTML");
                    valueZone2 = zoneNext1[2].FindElement(By.CssSelector("select option[selected = selected]")).GetAttribute("innerHTML");
                    if (valueZone2.CompareTo(valueZone1) > 0)
                        MessageBox.Show("Проверяемая строка-" + j + "  " + valueZone1 + " и " + valueZone2);
                    else
                    {
                        MessageBox.Show("Ашипка-ашипка!! 1я зона:"+valueZone1+" 2я зона:"+valueZone2+ "valueZone2.CompareTo(valueZone1)"+ valueZone2.CompareTo(valueZone1));
                    }

                }
                //переход на предидущую страницу
                newContext();

            }


        driver.Close();
        driver.Quit();
        }
    }
}
