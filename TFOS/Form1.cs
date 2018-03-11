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
        //void logInAdmin(ref IWebDriver driver, ref WebDriverWait wait)
        //{
        //    // заходим на страничку
        //    driver.Url = "http://localhost:8080/litecart/admin/";
        //    // логин
        //    IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
        //    driver.FindElement(By.Name("username")).SendKeys("admin");
        //    // пароль
        //    //// в поисках ошибки применён xPath - не помог, но работает.
        //    driver.FindElement(By.XPath("//*[@id=\"box-login\"]/form/div[1]/table/tbody/tr[2]/td[2]/span/input")).SendKeys("admin");
        //    //// данному логину необходим был Клик, в то время как Гугл требовал сабмит
        //    driver.FindElement(By.Name("login")).Click();
        //}

        


        // Проход по меню админки
        private void button1_Click(object sender, EventArgs e)
        {
            CheckingAdminMenu ChAdmMenu = new CheckingAdminMenu("http://localhost:8080/litecart/");
            ChAdmMenu.CheckMenu();
            ChAdmMenu.webBrCl.Close();
        }
        
        // Проверка наличия стикеров
        private void button2_Click(object sender, EventArgs e)
        {
            // переход к товарам
            CheckingStickers SCheck = new CheckingStickers("http://localhost:8080/litecart/");
            SCheck.FindAllProducts();
            SCheck.webBrCl.Close();

        }


        // ЗАДАНИЕ 9-1 Проверка сортировки стран - zone  и их областей - subone
        private void button3_Click(object sender, EventArgs e)
        {
            ChkngCntrySorting allcountry = new ChkngCntrySorting("http://localhost:8080/litecart/admin/?app=countries&doc=countries");
            allcountry.CheckSort();
            allcountry.webBrCl.Close();
        }
        
        // ЗАДАНИЕ 9-2
        private void button4_Click(object sender, EventArgs e)
        {


            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            /// !!! logInAdmin(ref driver, ref wait);

            IWebElement dataTable, zonelink;
            IList<IWebElement> zone, zone1, zoneNext1,  trows; // zoneNext
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

                    if (valueZone2.CompareTo(valueZone1) < 0)
                        MessageBox.Show("Обнаружена ошибка в сортировке. Подробнее:/n" 
                                                        + valueZone1 + "и" + valueZone2);



                    //if (valueZone2.CompareTo(valueZone1) > 0)
                    //    MessageBox.Show("Проверяемая строка-" + j + "  " + valueZone1 + " и " + valueZone2);
                    //else
                    //{
                    //    MessageBox.Show("Ашипка-ашипка!! 1я зона:"+valueZone1+" 2я зона:"+valueZone2+ "valueZone2.CompareTo(valueZone1)"+ valueZone2.CompareTo(valueZone1));
                    //}

                } 
                //переход на предидущую страницу
                newContext();

            }


        driver.Close();
        driver.Quit();
        }
    }
}
