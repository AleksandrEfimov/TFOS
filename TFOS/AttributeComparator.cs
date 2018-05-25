using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
/// <summary>
/// 
/// Зайти на главную
///     Найти блок campaigns
///         получить свойства из [0] элемента этого блока (!) в т.ч. ссылку 
///             : Инициализация объекта 1 
///         Переход по ссылке        
///             получить свойства
///                 :Инициализация объекта 2
///     Сравнить свойства описанные в задании
///     
/// 
/// </summary>


namespace TFOS
{
    using TFOS_utilities;
    class AttributeComparator: Products
    {
        internal WebBrowserClient webBrCl;
        IWebDriver driver;
        
        //string SetUrl { get; set; }
        public Products Prod;
        public Properties prop;
        
        StringBuilder strLog;
        

        public AttributeComparator()
        {
            webBrCl = new WebBrowserClient();
            driver = webBrCl.driver;
            Prod = new Products();
            strLog = new StringBuilder();
        }

        public void InitProd(string setUrl) => Prop = GetProperties(setUrl);
        

        public StringBuilder PrintFields()
        {
            

            try
            {
                FieldInfo[] fields = typeof(Properties).GetFields(BindingFlags.Public | BindingFlags.Instance);
                //FieldInfo[] fields = typeof(Products).GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (FieldInfo fieldInfo in fields)
                {
                    //strLog.AppendLine( fieldfInfo.Name.ToString() + ": " + fieldfInfo.GetValue(Prod.Prop).ToString());
                    //strLog.AppendLine(fieldInfo.Name.ToString() + ": " + fieldInfo.GetValue(Prop).ToString()??intArToStr() );
                    strLog.AppendLine(fieldInfo.Name.ToString() + ": " + fieldInfo.GetValue(Prop).ToString());
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return strLog;
        }

        public Properties GetProperties(string seturl)
        {
            driver.Url = seturl;
            var data = driver.FindElements(By.CssSelector("#box-campaigns li"));

            prop = new Properties();
            //Properties prop = new Properties()
            prop.strOfGetValue = "эта строка возвращена GetValue()";
            prop.intArray = new List<int> { 1,2,3};
            prop.Name = data[0].FindElement(By.CssSelector(".name")).GetAttribute("InnerHTML");
            prop.Link = data[0].FindElement(By.CssSelector(".link")).GetAttribute("href");

            prop.RegularPrice = (float)Convert.ToDouble(data[0].FindElement(By.CssSelector(".regular-price")).GetAttribute("InnerHTML"));
            string str = data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("color");
            prop.RegularColorRGB = ParsingRGB(str);
            //prop.RegularColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("color"));

            prop.RegularFontSize = data[0].FindElement(By.CssSelector(".regular-price")).GetAttribute("font-size");
            prop.RegularStriked = data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("strike");

            prop.ActionPrice = (float)Convert.ToDouble(data[0].FindElement(By.CssSelector(".campaign-price")).GetAttribute("InnerHTML"));
            prop.ActionColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".campaign-price")).GetCssValue("color"));
            prop.ActionFontSize = data[0].FindElement(By.CssSelector(".campaign-price")).GetAttribute("font-size");
            

            return prop;

        }



    }
}
