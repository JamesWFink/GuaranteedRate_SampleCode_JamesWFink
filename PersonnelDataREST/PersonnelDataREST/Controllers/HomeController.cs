using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonnelData.Model;
using PersonnelData.DAL;
using System.Web.Script.Serialization;

namespace PersonnelDataREST.Controllers
{
	public class HomeController : Controller
	{
		private DataManager dataMngr = new DataManager();

		public void ClearList() { dataMngr.ClearList(); }

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult AddRecord(string LastName, string FirstName, string Gender, string FavoriteColor, string DateOfBirth)
		{
			try
			{
				if (!dataMngr.AddRecord(LastName, FirstName, Gender, FavoriteColor, DateOfBirth))
				{
					return Json(String.Format("'Result':'AddRecord failed'"));
				}
			}
			catch
			{
				return Json(String.Format("'Result':'Exception'"));
			}
			return Json(String.Format("'Result':'Success'"));
		}

		[HttpGet]
		public JsonResult GetData(string SortMethod)
		{
			switch (SortMethod)
			{
				case "Gender":
					dataMngr.SortData(DataManager.SortMethod.Gender);
					break;
				case "BirthDate":
					dataMngr.SortData(DataManager.SortMethod.BirthDate);
					break;
				case "LastName":
					dataMngr.SortData(DataManager.SortMethod.LastName);
					break;
			}

			List<Person> personList = dataMngr.PersonnelList;
			var response = "{\"peoples\":[";
			for (int i = 0; i < personList.Count; i++)
			{
				response += personList[i].ToJsonString();
				if (i != (personList.Count - 1)) response += ",";
			}
			response += "]}";
			return Json(response, JsonRequestBehavior.AllowGet);
		}
	}
}