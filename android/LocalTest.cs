using System;
using System.Threading;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace android
{
    [TestFixture("local", "galaxy-s9")]
    public class LocalTest : BrowserStackNUnitTest
    {
        public LocalTest(string profile, string device) : base(profile,device){ }

        [Test]
        public void testLocal()
        {
            IWebElement searchElement = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(By.Id("com.example.android.basicnetworking:id/test_action")));
            searchElement.Click();
            IWebElement testElement = new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(By.ClassName("android.widget.TextView")));

            ReadOnlyCollection<IWebElement> allTextViewElements = driver.FindElements(By.ClassName("android.widget.TextView"));

            Thread.Sleep(5000);

            foreach (IWebElement textElement in allTextViewElements)
            {
                if (textElement.Text.Contains("The active connection is"))
                {
                    Assert.True(textElement.Text.Contains("The active connection is wifi"),"Incorrect Text");        
                }
            }
        }
    }
}
