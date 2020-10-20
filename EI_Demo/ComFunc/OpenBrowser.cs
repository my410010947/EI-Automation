using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace EI_Demo
{
    [Binding]
    class OpenBrowser
    {
        [Before]
        public void CreateWebDriver(ScenarioContext context)
        {
            // We are using Chrome, but you can use any driver
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            IWebDriver webDriver = new ChromeDriver(@"./");
            context["WEB_DRIVER"] = webDriver;
        }

        [After]
        public void CloseWebDriver(ScenarioContext context)
        {
            var driver = context["WEB_DRIVER"] as IWebDriver;
            //driver.Quit();
        }
    }
}
