using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFOS
{
    class UserAction
    {
        public WebBrowserClient webBrCl = new WebBrowserClient();
        IWebDriver driver;
        Random rnd = new Random();
        public UserAction(string seturl)
        {
            driver = webBrCl.driver;
            driver.Url = seturl;
        }

        public string SignUp()
        {
            driver.Url = "http://localhost:8080/litecart/en/create_account";
            var _rnd = rnd.Next(DateTime.Now.Year).ToString();
            try
            {

                var SignUpForm = driver.FindElement(By.Name("customer_form"));
                var firstname = SignUpForm.FindElement(By.Name("firstname"));
                
                firstname.SendKeys("firstName"+_rnd);
                var lastname = SignUpForm.FindElement(By.Name("lastname"));
                lastname.SendKeys("lastName" + _rnd);
                var address1 = SignUpForm.FindElement(By.Name("address1"));
                address1.SendKeys("address1");
                var postcode = SignUpForm.FindElement(By.Name("postcode"));
                postcode.SendKeys("10001");
                var city = SignUpForm.FindElement(By.Name("city"));
                city.SendKeys("city");
                var ddlCountry = SignUpForm.FindElement(By.Name("country_code"));
                
                var selectCountry = new SelectElement(ddlCountry);
                selectCountry.SelectByText("United States");

                //var zone_code = SignUpForm.FindElement(By.Name("zone_code"));
                
//# create-account > div > form > table > tbody > tr:nth-child(5) > td:nth-child(2) > select
                //*[@id="create-account"]/div/form/table/tbody/tr[5]/td[2]/select
                
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                // zone_code = SignUpForm.FindElement(wait.Until(By.CssSelector("td:nth-child(2) > select")));
                var zone_code = wait.Until(ExpectedConditions.ElementIsVisible
                    (By.CssSelector("td:nth-child(2) > select")));



                //zone_code = SignUpForm.FindElement(By.CssSelector("td:nth-child(2) > select"));
                var email = driver.FindElement(By.Name("email"));
                email.SendKeys("email" + _rnd + "@ru.ru");
                var selectZone = new SelectElement(zone_code);
                //selectZone.SelectByText("Alaska");
                var zoneCodeVisible = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("select > option:nth-child(2)")));
                selectZone.SelectByValue("AK");
                
                var phone = driver.FindElement(By.Name("phone"));
                phone.Clear();
                phone.SendKeys("0123456789");
                var newsletterSubsc = driver.FindElement(By.Name("newsletter"));
                if(newsletterSubsc.Selected == true)
                    newsletterSubsc.Click();
                var password = driver.FindElement(By.Name("password"));
                password.SendKeys("1");
                var confirmed_password = driver.FindElement(By.Name("confirmed_password"));
                confirmed_password.SendKeys("1");
                var btnCreateAcc = driver.FindElement(By.Name("create_account"));
                btnCreateAcc.Click(); //Submit();
                var boxAcc = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-account")));
                
                var logout = boxAcc.FindElement(By.LinkText("Logout"));
                logout.Click();

                return "Create account successfuul!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        
    }
}
