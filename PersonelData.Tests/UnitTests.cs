using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using PersonnelData.DAL;
using PersonnelData.Model;

namespace PersonelData.Tests
{

	[TestClass]
	public class PersonelDataTest
	{
		private List<Person> GetInitialList()
		{
			List<Person> personnelList = new List<Person>();
			personnelList.Add(new Person("Jones", "Tom", Gender.Male, "Blue", new DateTime(1967, 4, 13)));
			personnelList.Add(new Person("Smith", "Dick", Gender.Male, "Black", new DateTime(1971, 12, 19)));
			personnelList.Add(new Person("Walker", "Harry", Gender.Male, "Red", new DateTime(1971, 4, 13)));
			personnelList.Add(new Person("Johnson", "Mary", Gender.Female, "Green", new DateTime(1971, 4, 26)));
			personnelList.Add(new Person("King", "Amy", Gender.Female, "Yellow", new DateTime(1982, 2, 9)));
			personnelList.Add(new Person("MacDonald", "Jennifer", Gender.Female, "Brown", new DateTime(1986, 8, 14)));
			return personnelList;
		}

		[TestMethod]
		public void CmdLineApp_GetData_Pipe()
		{
			List<Person> personnelList = GetInitialList();

			DataManager dataMngr = new DataManager();

			try
			{
				dataMngr.ClearList();
				string pathName = ConfigurationManager.AppSettings["testDataPath"];
				dataMngr.GetData(pathName, "Pipe.txt");
			}
			catch
			{
				Assert.Fail(" Exception");
			}

			CollectionAssert.AreEqual(dataMngr.PersonnelList, personnelList, " Mismatch");
		}

		[TestMethod]
		public void CmdLineApp_GetData_Comma()
		{
			List<Person> personnelList = GetInitialList();

			DataManager dataMngr = new DataManager();

			try
			{
				dataMngr.ClearList();
				string pathName = ConfigurationManager.AppSettings["testDataPath"];
				dataMngr.GetData(pathName, "Comma.txt");
			}
			catch
			{
				Assert.Fail(" Exception");
			}

			CollectionAssert.AreEqual(dataMngr.PersonnelList, personnelList, " Mismatch");
		}

		[TestMethod]
		public void CmdLineApp_GetData_Space()
		{
			List<Person> personnelList = GetInitialList();

			DataManager dataMngr = new DataManager();

			try
			{
				dataMngr.ClearList();
				string pathName = ConfigurationManager.AppSettings["testDataPath"];
				dataMngr.GetData(pathName, "Space.txt");
			}
			catch
			{
				Assert.Fail(" Exception");
			}

			CollectionAssert.AreEqual(dataMngr.PersonnelList, personnelList, " Mismatch");
		}

		[TestMethod]
		public void CmdLineApp_SortData_ByGender()
		{
			List<Person> personnelList = new List<Person>();
			personnelList.Add(new Person("Johnson", "Mary", Gender.Female, "Green", new DateTime(1971, 4, 26)));
			personnelList.Add(new Person("King", "Amy", Gender.Female, "Yellow", new DateTime(1982, 2, 9)));
			personnelList.Add(new Person("MacDonald", "Jennifer", Gender.Female, "Brown", new DateTime(1986, 8, 14)));
			personnelList.Add(new Person("Jones", "Tom", Gender.Male, "Blue", new DateTime(1967, 4, 13)));
			personnelList.Add(new Person("Smith", "Dick", Gender.Male, "Black", new DateTime(1971, 12, 19)));
			personnelList.Add(new Person("Walker", "Harry", Gender.Male, "Red", new DateTime(1971, 4, 13)));

			DataManager dataMngr = new DataManager();

			try
			{
				dataMngr.ClearList();
				string pathName = ConfigurationManager.AppSettings["testDataPath"];
				dataMngr.GetData(pathName, "Space.txt");
				dataMngr.SortData(DataManager.SortMethod.Gender);
			}
			catch
			{
				Assert.Fail(" Exception");
			}

			CollectionAssert.AreEqual(dataMngr.PersonnelList, personnelList, " Mismatch");
		}

		[TestMethod]
		public void CmdLineApp_SortData_ByBirthDate()
		{
			List<Person> personnelList = new List<Person>();
			personnelList.Add(new Person("Jones", "Tom", Gender.Male, "Blue", new DateTime(1967, 4, 13)));
			personnelList.Add(new Person("Walker", "Harry", Gender.Male, "Red", new DateTime(1971, 4, 13)));
			personnelList.Add(new Person("Johnson", "Mary", Gender.Female, "Green", new DateTime(1971, 4, 26)));
			personnelList.Add(new Person("Smith", "Dick", Gender.Male, "Black", new DateTime(1971, 12, 19)));
			personnelList.Add(new Person("King", "Amy", Gender.Female, "Yellow", new DateTime(1982, 2, 9)));
			personnelList.Add(new Person("MacDonald", "Jennifer", Gender.Female, "Brown", new DateTime(1986, 8, 14)));

			DataManager dataMngr = new DataManager();

			try
			{
				dataMngr.ClearList();
				string pathName = ConfigurationManager.AppSettings["testDataPath"];
				dataMngr.GetData(pathName, "Space.txt");
				dataMngr.SortData(DataManager.SortMethod.BirthDate);
			}
			catch
			{
				Assert.Fail(" Exception");
			}

			CollectionAssert.AreEqual(dataMngr.PersonnelList, personnelList, " Mismatch");
		}

		[TestMethod]
		public void CmdLineApp_SortData_ByLastName()
		{
			List<Person> personnelList = new List<Person>();
			personnelList.Add(new Person("Walker", "Harry", Gender.Male, "Red", new DateTime(1971, 4, 13)));
			personnelList.Add(new Person("Smith", "Dick", Gender.Male, "Black", new DateTime(1971, 12, 19)));
			personnelList.Add(new Person("MacDonald", "Jennifer", Gender.Female, "Brown", new DateTime(1986, 8, 14)));
			personnelList.Add(new Person("King", "Amy", Gender.Female, "Yellow", new DateTime(1982, 2, 9)));
			personnelList.Add(new Person("Jones", "Tom", Gender.Male, "Blue", new DateTime(1967, 4, 13)));
			personnelList.Add(new Person("Johnson", "Mary", Gender.Female, "Green", new DateTime(1971, 4, 26)));

			DataManager dataMngr = new DataManager();

			try
			{
				dataMngr.ClearList();
				string pathName = ConfigurationManager.AppSettings["testDataPath"];
				dataMngr.GetData(pathName, "Space.txt");
				dataMngr.SortData(DataManager.SortMethod.LastName);
			}
			catch
			{
				Assert.Fail(" Exception");
			}

			CollectionAssert.AreEqual(dataMngr.PersonnelList, personnelList, " Mismatch");
		}

		[TestMethod]
		public void CmdLineApp_AddRecord()
		{
			DataManager dataMngr = new DataManager();
			dataMngr.ClearList();
			dataMngr.AddRecord("Last ", " First", "f", "Blue", "04/13/1971");
			string testString = dataMngr.PersonnelList[0].ToString();
			Assert.AreEqual(testString, "Last; First; Female; Blue; 04/13/1971"); 
		}
	}
}
