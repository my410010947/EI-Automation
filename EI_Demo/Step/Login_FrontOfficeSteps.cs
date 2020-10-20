using System;
using Xunit;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System.Threading;
using System.IO;
using System.Text;

namespace EI_Demo.Step
{
    [Binding]
    public class Login_FrontOfficeSteps
    {
        private readonly ScenarioContext context;
        public Login_FrontOfficeSteps(ScenarioContext context)
        {
            string newValue = "abc";
            ContinueRunTest(newValue, 5000);
            Write(Environment.CurrentDirectory + "\\App_Data\\Signal.txt", newValue);
            this.context = context;
        }

        [Given(@"Open Front Office Page (.*)")]
        public void GivenOpenFrontOfficePage(string p0)
        {
            IWebDriver driver = this.context.Get<IWebDriver>("WEB_DRIVER");
            driver.Navigate().GoToUrl(p0);
        }
        
        [When(@"And Input User (.*) (.*) and click Login")]
        public void WhenAndInputUserAndClickLogin(string userID, string userPW)
        {
            IWebDriver driver = this.context.Get<IWebDriver>("WEB_DRIVER");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            driver.FindElement(By.Id("i0116")).Click();
            driver.FindElement(By.Id("i0116")).Clear();
            driver.FindElement(By.Id("i0116")).SendKeys(userID);

            driver.FindElement(By.Id("idSIButton9")).Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            driver.FindElement(By.Id("passwordInput")).Click();
            driver.FindElement(By.Id("passwordInput")).Clear();
            driver.FindElement(By.Id("passwordInput")).SendKeys(userPW);

            driver.FindElement(By.Id("kmsiInput")).Click();
            driver.FindElement(By.Id("submitButton")).Click();
        }
        
        [Then(@"Login Front Office succesful")]
        public void ThenLoginFrontOfficeSuccesful()
        {
            IWebDriver driver = this.context.Get<IWebDriver>("WEB_DRIVER");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            Assert.Equal("Service Request", driver.FindElement(By.XPath("/html/body/ioh-root/ioh-form/main/h1")).Text);
        }

        private void ContinueRunTest(string newValue, int milliseSecond)
        {
            try
            {
                string oldResult = Read(Environment.CurrentDirectory + "\\App_Data\\Signal.txt");
                if (newValue.Trim() == oldResult.Trim())
                {
                    Thread.Sleep(milliseSecond);
                    string tempResult = "bcd";
                    ContinueRunTest(tempResult, milliseSecond);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private string Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line;
            StringBuilder sb = new StringBuilder();
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line);
            }
            sr.Close();
            return sb.ToString();
        }

        private void Write(string path, string content)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            byte[] data = System.Text.Encoding.Default.GetBytes(content);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }
    }
}
