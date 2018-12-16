using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace Homework7_8
{
    [TestFixture]
    class AdvSelenium_part2 : SetUpFixture
    {
        IJavaScriptExecutor jsexec;

       [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", SaveFolder);
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            options.AddArgument("--start-fullscreen");
            driver = new ChromeDriver(options);
            jsexec = (IJavaScriptExecutor)driver;
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }

        // ● Go to https://unsplash.com/search/photos/test
        //●	Scroll to the last picture on the page
        //●	Click on Download button using JS.
        //●	Verify that file was downloaded.     
        [Test]
        public void GetPicture()
        {
            driver.Navigate().GoToUrl("https://unsplash.com/search/photos/test");

            //Scrolling till the end of page (actually till footer with text, but it's at the end of page)
            By pageEnd = By.ClassName("_1cQDt");
            while (driver.FindElements(pageEnd).Count == 0) 
            {
                jsexec.ExecuteScript("window.scrollBy(0, 250)", "");
            }
            jsexec.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(pageEnd));

            //●	Scroll to the last picture on the page
            //●	Click on Download button using JS.
            //●	Verify that file was downloaded.
            Thread.Sleep(2000);
            //vono rahue number of columns, then which column has biggest number of elements, then i created xpath that uses those numbers
            //to get to the needed element. If there are 2 columns with same number, it should select the right one
            int number_of_divs = driver.FindElements(By.XPath("//div[@class='_3aQBo _2T3hc _1Q2WQ']")).Count();
            int picture_index = 0;
            int column = 0;
            int pict_in_column;
            for (int i=1; i<=number_of_divs; i++)
            {
                Thread.Sleep(2000);
                pict_in_column = driver.FindElements(By.XPath($"(//div[@class='_3aQBo _2T3hc _1Q2WQ'])[{i}]/div[@class='_1pn7R']")).Count();
                if (pict_in_column >= picture_index)
                {
                    picture_index = pict_in_column;
                    column = i;
                }
            }

            By lastPicture = By.XPath($"((//div[@class='_3aQBo _2T3hc _1Q2WQ'])[{column}]/div[@class='_1pn7R'])[{picture_index}]");
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(lastPicture)).Perform();
            Thread.Sleep(1000);

            //question - is there a way to just add part of the xpath to previously used xpath? because picture is already hovered on, 
            //and i just want to click on appeared download button, and xpath becomes too big
            jsexec.ExecuteScript($"arguments[0].click()", driver.FindElement(By.XPath($"((//div[@class='_3aQBo _2T3hc _1Q2WQ'])[{column}]/div[@class='_1pn7R'])[{picture_index}]//a[@title='Download photo']")));
            Thread.Sleep(6000);
        }
    }
}
