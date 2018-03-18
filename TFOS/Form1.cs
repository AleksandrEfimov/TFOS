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
            allcountry.CheckSortCountry();
            allcountry.webBrCl.Close();
        }
        
        // ЗАДАНИЕ 9-2 Проверка сортировки геозон
        private void button4_Click(object sender, EventArgs e)
        {
            ChkngCntrySorting allcountry = new ChkngCntrySorting("http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones");
            allcountry.CheckSortGeofence();
            allcountry.webBrCl.Close();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AttributeComparison attrCompr = new AttributeComparison("http://localhost:8080/litecart/en/");

        }
    }
}
