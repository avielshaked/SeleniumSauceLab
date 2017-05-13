using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using NUnit.Framework.Interfaces;
using System.IO;

namespace SeleniumSauceLab
{   
    public class TestLab
    {

        internal static string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        internal static string directory = Path.Combine(userProfile, "Downloads");

        protected IWebDriver driver;
        private String browser;
        private String version;
        private String os;
        private String deviceName;
        private String deviceOrientation;


        public TestLab(String browser, String version, String os, String deviceName, String deviceOrientation)
        {
            this.browser = browser;
            this.version = version;
            this.os = os;
            this.deviceName = deviceName;
            this.deviceOrientation = deviceOrientation;
        }

        [SetUp]
        public void Init()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", directory);

            DesiredCapabilities caps = options.ToCapabilities() as DesiredCapabilities;
            caps.SetCapability(CapabilityType.BrowserName, browser);
            caps.SetCapability(CapabilityType.Version, version);
            caps.SetCapability(CapabilityType.Platform, os);
            caps.SetCapability("deviceName", deviceName);
            caps.SetCapability("deviceOrientation", deviceOrientation);
            caps.SetCapability("username", "aviel-shaked");
            caps.SetCapability("accessKey", "2da32c14-a5e8-4a5f-8c81-f90804097bdc");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);
            

            driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
            
            
            
        }

        //[SetUp]
        //public void setup()
        //{
        //    driver = new ChromeDriver();
        //}

        [TearDown]
        public void CleanUp()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Passed);
            try
            {
                // Logs the result to Sauce Labs
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // Terminates the remote webdriver session
                driver.Quit();
            }
        }

    }
}
