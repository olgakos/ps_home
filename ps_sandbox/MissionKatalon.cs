using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace PsSandbox
{
    [TestFixture]
    public class MissionKatalon
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://62.152.34.179:8080/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void Mission1Bad()
        {
            //OpenHomePage
            driver.Navigate().GoToUrl(baseURL);
            //Login
            driver.FindElement(By.Name("login")).Click();
            driver.FindElement(By.Name("login")).Clear();
            driver.FindElement(By.Name("login")).SendKeys("Stranger");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("66PojexoIEB0");
            driver.FindElement(By.Name("valid_auth")).Click(); // клик на "войти"
            //ОЖИДАНИЕ!
            Thread.Sleep(5000);

            //GoToReesterMissions
            driver.Navigate().GoToUrl(baseURL + "/Missions/");
            //ОЖИДАНИЕ!
            Thread.Sleep(5000);

            //InitNewItemCreation (кнопка "Новый")
            driver.FindElement(By.Id("ButtonAddNewElem")).Click();

            //ждем кнопку со стрелкой < т.е. разворачиваем картчоку на весь экран (чтобы скрыть с монитора лишние поля с похожими именами)
            driver.FindElement(By.Id("button_hidden_datatables")).Click();

            //ОЖИДАНИЕ!
            Thread.Sleep(5000);

            //начинаем заполняить обязательные поля:
            driver.FindElement(By.Id("select2-StaffID_NEW_NEW-container")).Click(); //нашли поле "сотрудник"
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys("TestMan1");//печатаем (полностью) имя из выпадающего списка
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys(Keys.Enter);//подтвержение
            driver.FindElement(By.Id("select2-Firm_NEW_NEW-container")).Click(); //нашли поле "место работы"
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys("Компания 2");//печатаем (полностью) имя из выпадающего списка
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys(Keys.Enter);// подтвержение
            driver.FindElement(By.Id("select2-Filial_NEW_NEW-container")).Click(); //нашли поле "Филиал"
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys("Москва");//печатаем (полностью) имя из выпадающего списка
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys(Keys.Enter);// подтвержение
            driver.FindElement(By.Id("Purpose_NEW")).Click(); //нашли поле "цель поездки"
            driver.FindElement(By.Id("Purpose_NEW")).SendKeys("Цель"); // печатем короткий прозвольынй текст
            driver.FindElement(By.Id("MissionPlace_NEW")).Click(); //нашли поле "место комантировки"
            driver.FindElement(By.Id("MissionPlace_NEW")).SendKeys("Тверь"); // печатем короткий прозвольынй текст
            driver.FindElement(By.Id("StartMisDate_NEW")).Click(); //нашли поле "дата начала" ВИДЖЕТ
            //driver.FindElement(By.LinkText("28")).Click();
            driver.FindElement(By.LinkText("Сегодня")).Click(); //кнопка сегодня или иное, 
            driver.FindElement(By.Id("FinishMisDate_NEW")).Click(); //нашли поле "дата окончания" ВИДЖЕТ
            driver.FindElement(By.LinkText("28")).Click(); //дата, наприемер 28 число
            driver.FindElement(By.Id("select2-MissionPurpose_NEW_NEW-container")).Click(); //нашли поле "Назначение командировки"
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys("Командировка");//печатаем (полностью) имя из выпадающего списка
            driver.FindElement(By.XPath("(//input[@type='search'])[2]")).SendKeys(Keys.Enter);
            driver.FindElement(By.Id("WantResult_NEW")).Click(); //нашли поле "Ожидаемый результат"
            driver.FindElement(By.Id("WantResult_NEW")).SendKeys("Ожидаемый"); // печатем короткий прозвольынй текст
            driver.FindElement(By.Id("FinalResult_NEW")).Click(); //нашли поле "Полученный результат"
            driver.FindElement(By.Id("FinalResult_NEW")).SendKeys("Полученный");// печатем короткий прозвольынй текст

            //нашли-нажали кнопку "Сохранить карточку"
            driver.FindElement(By.Id("panel_edit_button_action_save")).Click();

            //нашли-нажали кнопку "Разлогиниться"
            driver.FindElement(By.XPath("//button[@onclick='logout();']")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
