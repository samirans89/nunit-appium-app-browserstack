using System;
using System.Threading;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace android
{
  [TestFixture("parallel", "pixel")]
  [TestFixture("parallel", "galaxy-s6")]
  [TestFixture("parallel", "galaxy-s7")]
  [Parallelizable(ParallelScope.Fixtures)]
  public class ParallelTest : BrowserStackNUnitTest
  {
    public ParallelTest(string profile, string device) : base(profile,device){ }

    [Test]
    public void searchWikipedia()
    {
      Thread.Sleep(5000);
      IWebElement searchElement2 = driver.FindElement(By.XPath("(//android.widget.ImageView[@content-desc='Search Wikipedia'])[1]"));
      searchElement2.Click();
      Thread.Sleep(5000);
      IWebElement insertTextElement2 = driver.FindElement(By.Id("org.wikipedia.alpha:id/search_src_text"));
      insertTextElement2.SendKeys("Browserstack");
      Thread.Sleep(5000);
      ReadOnlyCollection<IWebElement> allProductsName = (ReadOnlyCollection<IWebElement>)driver.FindElements(By.ClassName("android.widget.TextView"));
      Assert.True(allProductsName.Count > 0);
    }
  }
}
