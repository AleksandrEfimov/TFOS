using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TFOS_utilities;
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
    class AttributeComparator : Products
    {
        internal WebBrowserClient webBrCl;
        IWebDriver driver;
        public Products Prod;
        private Properties prop;
        StringBuilder strLog;


        public AttributeComparator()
        {
            webBrCl = new WebBrowserClient();
            driver = webBrCl.driver;
            Prod = new Products();
            strLog = new StringBuilder();
        }
       
        public void initMainPage(string setUrl) => Prop = GetPropertiesMainPage(setUrl);
        public void initCampaignPage(string setUrl) => Prop = GetPropertiesProdPage(setUrl);
       
        public StringBuilder PrintFields(Properties propMain, Properties propProd)
        {
            strLog.Clear();
            strLog.AppendLine("Task\t\tMainPage\t\tProdPage\t\t\tResult");
            string composite = "{0}\t\t{1}\t\t{2}\t\t{3}\r\n";
            try
            {
                PropertyInfo[] propArr = typeof(Properties).GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                foreach (PropertyInfo propertyInfo in propArr)
                {
                    strLog.AppendFormat(composite, 
                        propertyInfo.Name.ToString(),
                        propertyInfo.GetValue(propMain).ToString(), 
                        propertyInfo.GetValue(propProd).ToString(),
                        propertyInfo.GetValue(propMain).Equals(propertyInfo.GetValue(propProd)));
                }
                string str = "CampPriceSizeIsBigger\t\t" +
                    propMain.CampaignFontSize + ">"+ propMain.RegularFontSize+
                    "\t\t"+
                    propProd.CampaignFontSize + ">" + propProd.RegularFontSize + 
                    "\t\t"+
                    ((propMain.CampaignFontSize > propMain.RegularFontSize) == 
                                                          (propProd.CampaignFontSize > propProd.RegularFontSize));
                strLog.AppendLine(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
               
            }
            return strLog;
        }



        public Properties GetPropertiesMainPage(string seturl)
        {
            driver.Url = seturl;
            
            var data = driver.FindElements(By.CssSelector("#box-campaigns li"));

            prop = new Properties();
            
            prop.Name = data[0].FindElement(By.CssSelector(".name")).Text;
            prop.Link = data[0].FindElement(By.CssSelector(".link")).GetAttribute("href");
            // Regular
            prop.RegularPrice = data[0].FindElement(By.CssSelector(".regular-price")).Text;
            prop.RegularColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("color"));
            prop.RegularFontSize = ParsingFontSize(data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("font-size"));
            prop.RegularStriked = data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("text-decoration-line");
            // Campaigns
            prop.CampaignPrice = data[0].FindElement(By.CssSelector(".campaign-price")).Text;
            prop.CampaignColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".campaign-price")).GetCssValue("color"));
            prop.CampaignFontSize = ParsingFontSize( data[0].FindElement(By.CssSelector(".campaign-price")).GetCssValue("font-size"));
            prop.CampaignFontStrong = data[0].FindElement(By.CssSelector(".campaign-price")).TagName;

            return prop;

        }
        // Properties from product page
        public Properties GetPropertiesProdPage(string seturl)
        {
            driver.Url = seturl;
            var data = driver.FindElements(By.CssSelector("#box-product"));

            prop = new Properties();
            
            //prop.Name = data[0].FindElement(By.CssSelector("h1 .title")).Text;
            prop.Name = data[0].FindElement(By.CssSelector("#box-product > div:nth-child(1) > h1")).Text;
            //prop.Link = data[0].FindElement(By.CssSelector(".link")).GetAttribute("baseURI");
            prop.Link = data[0].GetAttribute("baseURI");
            // Regular
            prop.RegularPrice = data[0].FindElement(By.CssSelector(".regular-price")).Text;
            prop.RegularColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("color"));
            prop.RegularFontSize = ParsingFontSize(data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("font-size"));
            prop.RegularStriked = data[0].FindElement(By.CssSelector(".regular-price")).GetCssValue("text-decoration-line");
            // Campaigns
            prop.CampaignPrice = data[0].FindElement(By.CssSelector(".campaign-price")).Text;
            prop.CampaignColorRGB = ParsingRGB(data[0].FindElement(By.CssSelector(".campaign-price")).GetCssValue("color"));
            prop.CampaignFontSize = ParsingFontSize(data[0].FindElement(By.CssSelector(".campaign-price")).GetCssValue("font-size"));
            prop.CampaignFontStrong = data[0].FindElement(By.CssSelector(".campaign-price")).TagName;

            return prop;

        }



    }
}
