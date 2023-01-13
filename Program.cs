using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;


namespace SeleniumAmazon
{
    class Program
    {
        public static void Main(string[] args)
        {
            string item;
           
            Console.WriteLine("Hello, what item will you be searching for?");
            item= Console.ReadLine();

            Console.WriteLine();
            callDriver(item);
            

        }
        public static void callDriver(string search)
        {
            
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.amazon.com");
            driver.FindElement(By.Name("field-keywords")).SendKeys(search);
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
            driver.FindElement(By.Id("nav-search-submit-button")).Click();
         
            IWebElement drop = driver.FindElement(By.Name("s"));
            SelectElement s=new SelectElement(drop);
            s.SelectByText("Price: Low to High");
            int count = 0;
            int length = 0;
            List<String> products = new List<String>();
            StreamWriter sw = new StreamWriter("C:\\seleniumamazon.txt");
            sw.WriteLine("Here are the top ten results:");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IList<IWebElement> choices = driver.FindElements(By.TagName("h2"));
          
            foreach (IWebElement choice in choices)
            {
            
                Console.WriteLine(choice.Text);
                products.Add(choice.Text);
                Console.WriteLine("-----------------------");
           
            }
            Console.WriteLine();
            Console.WriteLine("Products:");
            length = products.Count();

            foreach (string product in products)
            {
                Console.WriteLine(product);
                while(count != 10 || count != length)
                {
                    sw.WriteLine(product);
                    count++;
                }
            }
        }
    }
}