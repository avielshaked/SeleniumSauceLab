using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumSauceLab
{
    [TestFixture("chrome", "45", "Windows 7", "", "")]
    [TestFixture("chrome", "57", "Windows 10", "", "")]
    [Parallelizable]
    public class Lab1 : TestLab
    {

        public Lab1(string browser,string  version,string  os,string  deviceName, string deviceOrientation):base(browser,version,os,deviceName,deviceOrientation)
        {

        }
       
        [Test]
        public void AppleTest()
        {
            driver.Navigate().GoToUrl("https://www.apple.com/");
            StringAssert.Contains("Apple", driver.Title);
            IWebElement query = driver.FindElement(By.ClassName("ac-gn-link-mac"));
            query.Click();
            Assert.True(driver.Url.Contains("Mac"),"not the right url");           
        }

        [Test]
        public void googleTest2()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            StringAssert.Contains("Google", driver.Title);
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Sauce Labs");
            query.Submit();
        }

        [Test]
        public void DownloadPDf()
        {
            driver.Url = "http://all-free-download.com/free-vector/download/wild-nature-background-fox-leaf-icons-repeating-design_6829632.html";
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='detail_content']/div[2]/a")).Click();
        }
    }
}
