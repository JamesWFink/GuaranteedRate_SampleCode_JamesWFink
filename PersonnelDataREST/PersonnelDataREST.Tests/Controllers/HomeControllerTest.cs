using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonnelDataREST;
using PersonnelDataREST.Controllers;
using PersonnelData.DAL;
using PersonnelData.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace PersonnelDataREST.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		private static InternetExplorerDriver _webDriver;

		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
		{
			_webDriver = new InternetExplorerDriver(new InternetExplorerOptions() { IntroduceInstabilityByIgnoringProtectedModeSettings = true });
		}

		[ClassCleanup()]
		public static void MyClassCleanup()
		{
			_webDriver.Quit();
		}

		private TestContext testContextInstance;
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}

		[TestMethod]
		public void WebApp_Index()
		{
			HomeController controller = new HomeController();
			ViewResult result = controller.Index() as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void WebApp_Home_AddRecord()
		{
			HomeController controller = new HomeController();
			JsonResult result = controller.AddRecord("Last", "First", "Female", "blue", "04/13/1971");
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void WebApp_Home_GetData_ByGender()
		{
			HomeController controller = new HomeController();
			controller.ClearList();
			controller.AddRecord("1Last", "First", "Male", "blue", "02/13/1971");
			controller.AddRecord("2Last", "First", "Female", "blue", "04/13/1971");
			controller.AddRecord("3Last", "First", "Female", "blue", "06/13/1971");
			JsonResult result = controller.GetData("Gender");
			var test = result.Data;
			Assert.AreEqual(test, "{\"peoples\":[{\"LastName\":\"2Last\",\"FirstName\":\"First\",\"Gender\":\"Female\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"04/13/1971\"},{\"LastName\":\"3Last\",\"FirstName\":\"First\",\"Gender\":\"Female\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"06/13/1971\"},{\"LastName\":\"1Last\",\"FirstName\":\"First\",\"Gender\":\"Male\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"02/13/1971\"}]}");
		}

		[TestMethod]
		public void WebApp_Home_GetData_ByBirthDate()
		{
			HomeController controller = new HomeController();
			controller.ClearList();
			controller.AddRecord("1Last", "First", "Male", "blue", "02/13/1971");
			controller.AddRecord("2Last", "First", "Female", "blue", "04/13/1971");
			controller.AddRecord("3Last", "First", "Female", "blue", "06/13/1971");
			JsonResult result = controller.GetData("BirthDate");
			var test = result.Data;
			Assert.AreEqual(test, "{\"peoples\":[{\"LastName\":\"1Last\",\"FirstName\":\"First\",\"Gender\":\"Male\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"02/13/1971\"},{\"LastName\":\"2Last\",\"FirstName\":\"First\",\"Gender\":\"Female\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"04/13/1971\"},{\"LastName\":\"3Last\",\"FirstName\":\"First\",\"Gender\":\"Female\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"06/13/1971\"}]}");
		}

		[TestMethod]
		public void WebApp_Home_GetData_ByLastName()
		{
			HomeController controller = new HomeController();
			controller.ClearList();
			controller.AddRecord("1Last", "First", "Male", "blue", "02/13/1971");
			controller.AddRecord("2Last", "First", "Female", "blue", "04/13/1971");
			controller.AddRecord("3Last", "First", "Female", "blue", "06/13/1971");
			JsonResult result = controller.GetData("LastName");
			var test = result.Data;
			Assert.AreEqual(test, "{\"peoples\":[{\"LastName\":\"3Last\",\"FirstName\":\"First\",\"Gender\":\"Female\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"06/13/1971\"},{\"LastName\":\"2Last\",\"FirstName\":\"First\",\"Gender\":\"Female\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"04/13/1971\"},{\"LastName\":\"1Last\",\"FirstName\":\"First\",\"Gender\":\"Male\",\"FavoriteColor\":\"blue\",\"DateOfBirth\":\"02/13/1971\"}]}");
		}

	//	[TestMethod]
	//	public void WebApp_AddRecord()
	//	{
	//		//_webDriver.Navigate().GoToUrl("http://localhost:62979/");
	//		_webDriver.Navigate().GoToUrl("http://localhost:51616");
	//		Thread.Sleep(1000);
	//		IWebElement LastName = _webDriver.FindElement(By.Id("LastName"));
	//		IWebElement FirstName = _webDriver.FindElement(By.Id("FirstName"));
	//		IWebElement Gender = _webDriver.FindElement(By.Id("Gender"));
	//		IWebElement FavoriteColor = _webDriver.FindElement(By.Id("FavoriteColor"));
	//		IWebElement DateOfBirth = _webDriver.FindElement(By.Id("DateOfBirth"));
	//		IWebElement submitRecord = _webDriver.FindElement(By.Id("submitRecord"));
	//		LastName.SendKeys("Last");
	//		FirstName.SendKeys("First");
	//		Gender.SendKeys("Female");
	//		FavoriteColor.SendKeys("blue");
	//		DateOfBirth.SendKeys("04/13/1971");
	//		submitRecord.Click();
	//		Thread.Sleep(1000);
	//		IWebElement result = _webDriver.FindElement(By.Id("status"));
	//		Assert.AreEqual(result.Text, "'Result':'Success'");
	//	}
	}
}
