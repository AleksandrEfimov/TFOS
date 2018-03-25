using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class AttributeComparison: Products
    {
        WebBrowserClient webBrCl;
        IWebDriver driver;
        Products [] Prod;
        string SetUrl { get; set; }

        public AttributeComparison()
        {
            webBrCl = new WebBrowserClient();
            driver = webBrCl.driver;
        }

        public void InitProd(int i, Properties prop )
        {
            Prod[i] = new Products();
        }

        public Properties GetProperties(string seturl)
        {
            driver.Url = seturl;
            var data = driver.FindElements(By.CssSelector("#box-campaigns li"));

            Properties prop = new Properties()
            {
                Name = data[0].FindElement(By.CssSelector(".name")).GetAttribute("InnerHTML"),
                Link = data[0].FindElement(By.CssSelector(".link")).GetAttribute("href"),

                RegularPrice = (float)Convert.ToDouble(data[0].FindElement(By.CssSelector(".regular-price")).GetAttribute("InnerHTML")),
                RegularColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("color")),
                RegularFontSize = data[0].FindElement(By.CssSelector(".regular-price")).GetAttribute("font-size"),
                RegularStriked = data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("strike"),

                ActionPrice = (float)Convert.ToDouble(data[0].FindElement(By.CssSelector(".compaign-price")).GetAttribute("InnerHTML")),
                ActionColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".compaign-price")).GetCssValue("color")),
                ActionFontSize = data[0].FindElement(By.CssSelector(".compaign-price")).GetAttribute("font-size"),
            };

            return prop;

        }



    }
}
