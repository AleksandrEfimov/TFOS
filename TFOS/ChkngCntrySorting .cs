using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFOS
{
    class ChkngCntrySorting
    {
        public WebBrowserClient webBrCl;
        IWebDriver driver;
        string SetUrl { get; set; }
        string valueZone1, valueZone2;

        StringBuilder log = new StringBuilder("");
        IWebElement dataTable, zonelink;
        IList<IWebElement> trows, trows_geof, zone, zone1, zoneNext;
        bool isEnteredzone = false;


        public ChkngCntrySorting(string seturl)
        {
            webBrCl = new WebBrowserClient();
            SetUrl = seturl;
            driver = webBrCl.driver;
        }

        public void CheckSortCountry()
        {
            webBrCl.logInAdmin();
            driver.Url = SetUrl;
            // обновить контекст
            NewContext(ref dataTable,ref trows);

            try
            {
                reSortingCountry(ref trows);
                MessageBox.Show("Парам-парам-пам, ВСЁ!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        // обновление контекста
        void NewContext(ref IWebElement dataTable, ref IList<IWebElement> trows)
        {
            // таблица со странами
            dataTable = driver.FindElement(By.ClassName("dataTable"));
            // массив строк
            trows = dataTable.FindElements(By.TagName("tr"));
        }
        void NewContext(ref IWebElement dataTable, ref IList<IWebElement> trows, string seturl)
        {
            driver.Url = seturl;
            // таблица со странами
            dataTable = driver.FindElement(By.ClassName("dataTable"));
            // массив строк
            trows = dataTable.FindElements(By.TagName("tr"));
        }

        // TODO: проверить передаваемые параметры в reSortingCountry - V

        // проверка сортивки стран и подзон по алфавиту
        void reSortingCountry(ref IList<IWebElement> tRowSort)
        {
            for (int i = 1; i < tRowSort.Count - 2; i++)
            {
                if (isEnteredzone)
                {
                    NewContext(ref dataTable,ref trows);
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
                            NewContext(ref dataTable,ref trows);
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
                            if (str2.CompareTo(str1) > 0)
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
                NewContext(ref dataTable,ref trows);
            }
        }

        public void CheckSortGeofence()
        {
            webBrCl.logInAdmin();
            driver.Url = SetUrl;
            NewContext(ref dataTable, ref trows);
            

            for (int j = 1; j < trows.Count - 1; j++)
            {

                zone = trows[j].FindElements(By.TagName("td"));
                zonelink = zone[2].FindElement(By.TagName("a"));

                // входим в страну,
                zonelink.Click();

                // таблица subzone
                dataTable = driver.FindElement(By.Id("table-zones"));
                trows_geof = dataTable.FindElements(By.TagName("tr"));

                // получаем и сравниваем значения 2х соседних subzone
                for (int i = 1; i < trows_geof.Count - 2; i++)
                {
                    //получаем массив ячеек

                    zone1 = trows_geof[i].FindElements(By.TagName("td"));
                    zoneNext = trows_geof[i + 1].FindElements(By.TagName("td"));

                    // 2я(3я) ячейка содержит выпадающий список

                    valueZone1 = zone1[2].FindElement(By.CssSelector("select option[selected = selected]")).GetAttribute("innerHTML");
                    valueZone2 = zoneNext[2].FindElement(By.CssSelector("select option[selected = selected]")).GetAttribute("innerHTML");

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
                //переход на предидущую 
                NewContext(ref dataTable, ref trows, SetUrl);

            }


        }




    }
}
