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
            driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);   //call another implicit wait
            driver.FindElement(By.Id("nav-search-submit-button")).Click(); //use the search id to click
         
            IWebElement drop = driver.FindElement(By.Name("s"));  //element name of amazon price drop filter
            SelectElement s=new SelectElement(drop);
            s.SelectByText("Price: Low to High");
            int count = 0;
            int length = 0;
            List<String> products = new List<String>();
            StreamWriter sw = new StreamWriter("C:\\selamazon.txt");  //text file declared here to list results
            sw.WriteLine("Here are the top ten results:");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //implicit wait called here to allow the page to load all elements

            IList<IWebElement> choices = driver.FindElements(By.TagName("h2")); 
            //choices should only consist of the h2 tag which on amazon labels the results exclusively
          
            foreach (IWebElement choice in choices)
            {
            
                Console.WriteLine(choice.Text);
                products.Add(choice.Text);
                Console.WriteLine("-----------------------");
           
            }

            Console.WriteLine("Products:");
            length = products.Count();

            foreach (string product in products)
            {
                Console.WriteLine(product);
                while((count != 10) || (count != length))
                {
                    sw.WriteLine(product);
                    count++;
                }
            }
            sw.Close();   //close the streamwriter
            driver.Quit(); //quit the driver
        }
    }
}