using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFOS
{
    class UserAction
    {
        WebBrowserClient webBrCl = new WebBrowserClient();
        IWebDriver driver;
        public UserAction(string seturl)
        {
            driver = webBrCl.driver;
            driver.Url = seturl;
        }

        public string SignUp()
        {
            driver.Url= "http://localhost:8080/litecart/en/create_account";
            try
            {
                var SignUpForm = driver.FindElement(By.Name("customer_form"));
                var tr = SignUpForm.FindElements(By.TagName("tr"));
                var firstname = SignUpForm.FindElement(By.Name("firstname"));
                //lastname, address1, postcode, city
                // ddl 
                string i = tr.Count.ToString();
                return i;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }







    }
}
