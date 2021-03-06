﻿using System;
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


        // Проход по меню админки
        private void btnCheckingAdminMenu(object sender, EventArgs e)
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
            MessageBox.Show("Assert all products have sticker is: " + SCheck.IsAllProductHaveSticker());
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
            
            AttributeComparator attrCompr = new AttributeComparator();
            attrCompr.initMainPage("http://localhost:8080/litecart/en/");

            Products.Properties pMainPage = attrCompr.Prop;
            attrCompr.initCampaignPage(pMainPage.Link);
            Products.Properties pProdPage = attrCompr.Prop;

            FormAttributeComparator acResultView = new FormAttributeComparator();
            acResultView.tbResult = attrCompr.PrintFields(pMainPage, pProdPage).ToString();

            if (acResultView.ShowDialog() != DialogResult.OK)
                return;
            

            //MessageBox.Show(attrCompr.PrintFields(pMainPage,pProdPage).ToString()) ;

            //MessageBox.Show(attrCompr.PrintFields(pProdPage).ToString()) ;

            attrCompr.webBrCl.Close();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserAction uAc = new UserAction("http://localhost:8080/litecart/en/");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(uAc.SignUp());
            sb.AppendLine(uAc.SignOut());
            sb.AppendLine(uAc.SignIn());
            MessageBox.Show(@"TestResult: \\n" + sb );
            uAc.webBrCl.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddingProductToTheRange addProd = new AddingProductToTheRange();
            addProd.webBrCl.ToCatalog();
            addProd.btnAddNewProductClick();
            addProd.CatalogGeneral();
            addProd.CatalogInformation();
            MessageBox.Show( addProd.CatalogPrices());
            addProd.webBrCl.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WorkWithCart workWithCart = new WorkWithCart();
            workWithCart.AddToCart(3);
            workWithCart.OutOfCart();
            workWithCart.webBrCl.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CheckNewWindow checkNewWindow = new CheckNewWindow();
            checkNewWindow.SwithToTab(checkNewWindow.linkArr);
            MessageBox.Show(
            checkNewWindow.sbIsError.Length > 0 ? 
            checkNewWindow.sbIsError.ToString() :
            "All fine"
            );
            checkNewWindow.webBrCl.Close();
        }

        private void ReapLogs_Click(object sender, EventArgs e)
        {
            ReaperForLogs reaper = new ReaperForLogs();
            string str = reaper.TimeToReapLogs().ToString();
            MessageBox.Show(str);
            
        }
    }
}
