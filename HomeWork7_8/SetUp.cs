using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Homework7_8
{
    [SetUpFixture]
    public class SetUpFixture
    {
        public IWebDriver driver;
        public string SaveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AlexPScreenshots");

        [OneTimeSetUp]
        public void CreateFolder()
        {
            if (!Directory.Exists(SaveFolder))
            {
                Directory.CreateDirectory(SaveFolder);
            }
        }

        [OneTimeTearDown]
        public void Clean()
        {
            driver.Quit();
        }
    }
}