//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Avesta.Web.Automation.Selenium
//{
//    public class SeleniumWorkerService
//    {
//        readonly IJavaScriptExecutor _js;
//        readonly IWebDriver _driver;
//        readonly WebDriverWait _wait;
//        readonly ChromeOptions _Options;

//        public SeleniumWorkerService()
//        {
//            _Options = new ChromeOptions();
//            _Options.AddArguments("headless");
//            _Options.AddArgument("window-size=1920x1080");
//            if (false)
//            {
//                _driver = new ChromeDriver(/*Options*/);
//            }
//            else
//            {
//                _driver = new ChromeDriver(_Options);
//            }
//            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20/*maximum time*/));
//            _js = (IJavaScriptExecutor)_driver;
//        }





//        public bool ClickOnFirstParent(By by)
//        {
//            try
//            {
//                var child = CallElement(by);
//                var parent = ExecuteJavascriptOnPage($"return arguments[0].parentNode;", child);
//                parent.Click();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }



//        public IWebElement CallElement(By by)
//        {
//            try
//            {
//                Thread.Sleep(1000);
//                //https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
//                //return _wait.Until(d => { });
//            }
//            catch (Exception e)
//            {
//                return null;
//            }
//        }




//        public bool RefreshPage()
//        {
//            try
//            {
//                _driver.Navigate().Refresh();
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }





//        static public IEnumerable<IWebElement> CallElements(By by)
//        {
//            try
//            {
//                Thread.Sleep(1000);
//                //https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
//                //return _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
//            }
//            catch (Exception e)
//            {
//                return null;
//            }
//        }


//        public string ExecuteJavascriptOnPage(string jsCode)
//        {
//            try
//            {
//                var result = (string)_js.ExecuteScript(jsCode);
//                return result;
//            }
//            catch (Exception e)
//            {
//                return string.Empty;
//            }
//        }




//        public IWebElement ExecuteJavascriptOnPage(string jsCode, params object[] args)
//        {
//            try
//            {
//                var result = (IWebElement)_js.ExecuteScript(jsCode, args);
//                return result;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }





//        public string GetPageSource()
//        {
//            try
//            {
//                return _driver.PageSource;
//            }
//            catch (Exception e)
//            {
//                return string.Empty;
//            }
//        }




//        public bool NavigateToPage(string urlPage)
//        {
//            try
//            {
//                _driver.Navigate().GoToUrl(urlPage);
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }


//        public bool ClickElem(By by)
//        {
//            return IsElementClickable(by);
//        }



//        private bool IsElementClickable(By by)
//        {
//            try
//            {
//                CallElement(by).Click();
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }




//        public bool FillInput(By by, string key)
//        {
//            try
//            {
//                //no more \n\r
//                key = key.Replace("\n", "").Replace("\r", "");

//                CallElement(by).SendKeys(key);
//                return true;
//            }
//            catch (Exception e)
//            {
//                return false;
//            }
//        }




//        public bool AwnserToAlert(bool choose)
//        {
//            try
//            {
//                var alert = _driver.SwitchTo().Alert();
//                if (choose)
//                {
//                    alert.Accept();
//                }
//                else
//                {
//                    alert.Dismiss();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }






//    }



//}
