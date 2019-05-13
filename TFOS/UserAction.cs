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
        Random _rnd = new Random();
        string emailAddress;
        string passwordValue = "1";
        public UserAction(string seturl)
        {
            driver = webBrCl.driver;
            driver.Url = seturl;
            emailAddress = "email" + _rnd.Next(DateTime.Now.Year).ToString() + "@ru.ru";
        }

        public string SignUp()
        {
            driver.Url = "http://localhost:8080/litecart/en/create_account";
            
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

                
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                
                var zone_code = wait.Until(ExpectedConditions.ElementIsVisible
                    (By.CssSelector("td:nth-child(2) > select")));



                //zone_code = SignUpForm.FindElement(By.CssSelector("td:nth-child(2) > select"));
                var email = driver.FindElement(By.Name("email"));
                email.SendKeys(emailAddress);
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
                password.SendKeys(passwordValue);
                var confirmed_password = driver.FindElement(By.Name("confirmed_password"));
                confirmed_password.SendKeys("1");
                var btnCreateAcc = driver.FindElement(By.Name("create_account"));
                btnCreateAcc.Click(); //Submit();

                var boxAcc = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-account")));
                
                return $"Create account email: {emailAddress}, password: {passwordValue} -Success!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        public string SignOut()
        {
            driver.FindElement(By.CssSelector("#box-account li:nth-child(4) a")).Click();
            return (driver.FindElements(By.CssSelector("#box-account li:nth-child(4) a")).Count() == 0) ?
                "SignOut -Success" : "SignOut -Unsuccess"; 
        }

        public string SignIn()
        {
            driver.FindElement(By.Name("email")).SendKeys(emailAddress);
            driver.FindElement(By.Name("password")).SendKeys(passwordValue);
            driver.FindElement(By.Name("login")).Click();
            return (driver.FindElements(By.CssSelector("#box-account li:nth-child(4) a")).Count() > 0) ?
                "Second SignIn - Success" : "Second SignIn - Unsuccess";
        }



    }
}
