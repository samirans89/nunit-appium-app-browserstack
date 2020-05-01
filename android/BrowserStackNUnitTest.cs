using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using BrowserStack;

namespace android
{
	public class BrowserStackNUnitTest
	{
		protected RemoteWebDriver driver;
		protected string profile;
		protected string device;
		private Local browserStackLocal;

		public BrowserStackNUnitTest(string profile, string device)
		{
				this.profile = profile;
				this.device = device;
		}

		[SetUp]
		public void Init()
		{
			NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
			NameValueCollection devices = ConfigurationManager.GetSection("environments/" + device) as NameValueCollection;

			DesiredCapabilities capabilities = new DesiredCapabilities();

			foreach (string key in caps.AllKeys)
			{
				capabilities.SetCapability(key, caps[key]);
			}

			foreach (string key in devices.AllKeys)
			{
				capabilities.SetCapability(key, devices[key]);
			}

			String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
			if (username == null)
			{
				username = ConfigurationManager.AppSettings.Get("user");
			}

			String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
			if (accesskey == null)
			{
				accesskey = ConfigurationManager.AppSettings.Get("key");
			}

			capabilities.SetCapability("browserstack.user", username);
			capabilities.SetCapability("browserstack.key", accesskey);

			Object local_cap = capabilities.GetCapability("browserstack.local");
			if (local_cap != null && local_cap.ToString().Equals("true"))
			{
				//browserStackLocal = new Local();
				List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
						new KeyValuePair<string, string>("key", accesskey)
				};
                // BrowserStackLocal.exe file won't work on Mac.
                //Language Bindings attempts to start the 'exe' file.
                //For Mac, please start BrowserStackLocal Mac binary separately on command line.

                //browserStackLocal.start(bsLocalArgs);
			}


			String hubUrl = "http://" + ConfigurationManager.AppSettings.Get("user") + ":" + ConfigurationManager.AppSettings.Get("key") + "@" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/";
			driver = new RemoteWebDriver(new Uri(hubUrl), capabilities);
		}

		[TearDown]
		public void Cleanup()
		{
			driver.Quit();
			//if (browserStackLocal != null)
			//{
				browserStackLocal.stop();
			//}
		}

	}
}
