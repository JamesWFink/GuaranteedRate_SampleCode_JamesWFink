using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Configuration;
using PersonnelData.Model;

namespace PersonnelData.DAL
{
	public class DataManager
	{
		public string Error { get; set; }
		
		private static List<Person> _personnelList = new List<Person>();
		public List<Person> PersonnelList { get { return _personnelList; } }
		public void ClearList() { _personnelList.Clear(); }

		public enum FileType
		{
			Pipe,
			Comma,
			Space,
			NotSet
		};

		public enum SortMethod
		{
			Gender,
			BirthDate,
			LastName,
			NotSet
		};

		public bool GetData(string path, string fileName)
		{
			bool rtn = true;
			string pathAndFileName;

			if (path == null)
			{
				string pathName = ConfigurationManager.AppSettings["dataPath"];
				pathAndFileName = pathName + fileName;
			}
			else
				pathAndFileName = path + fileName;
			
			try
			{
				using (StreamReader streamReader = new StreamReader(pathAndFileName))
				{
					if (!ParseData(streamReader))
					{
						Error = String.Format("File could not be parsed.");
						rtn = false;
					}
				}
			}
			catch
			{
				Error = String.Format("File not found.");
				rtn = false;
			}

			return rtn;
		}

		private bool ParseData(StreamReader streamReader)
		{
			FileType fileType = FileType.NotSet;
			bool firstRecord = true;
			string[] fields = new string[5];

			try
			{
				while (streamReader.Peek() >= 0)
				{
					string record = streamReader.ReadLine();
					if (firstRecord)
					{
						fileType = GetFileType(record);
						firstRecord = false;
					}

					switch (fileType)
					{
						case FileType.Pipe:
							fields = record.Split('|');
							break;
						case FileType.Comma:
							fields = record.Split(',');
							break;
						case FileType.Space:
							fields = record.Split(' ');
							break;
					}

					AddRecord(fields[0], fields[1], fields[2], fields[3], fields[4]);
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		private FileType GetFileType(string firstRecord)
		{
			// Assumptions:
			//		No field will contain a '|',  ',', or ' '
			//		The delimiter could be either:
			//		*	a single space (no multiple spaces)
			//		*	a single space (no multiple spaces) followed by '|' or ','
			//		*	or no space followed by '|' or ','

			FileType fileType = FileType.NotSet;

			bool haveFirstSpace = false;
			int index = 0;
			while (index < firstRecord.Length)
			{
				char c = (char)firstRecord[index];
				if (c == '|')
				{
					fileType = FileType.Pipe;
					break;
				}
				if (c == ',')
				{
					fileType = FileType.Comma;
					break;
				}
				if (c == ' ')
				{
					fileType = FileType.Space;
					if (haveFirstSpace)
						break;
					else
						haveFirstSpace = true;
				}
				index++;
			}

			return fileType;
		}

		public bool SortData(SortMethod sortMethod)
		{
			if (_personnelList.Count == 0)
			{
				Error = String.Format("The list is empty.");
				return false;
			}

			switch (sortMethod)
			{
				case SortMethod.Gender:
					_personnelList.Sort(
						delegate(Person prior, Person next)
						{
							return prior.Gender.CompareTo(next.Gender);
						});
					break;
				case SortMethod.BirthDate:
					_personnelList.Sort(
						delegate(Person prior, Person next)
						{
							return prior.DateOfBirth.CompareTo(next.DateOfBirth);
						});
					break;
				case SortMethod.LastName:
					_personnelList.Sort(
						delegate(Person prior, Person next)
						{
							return -prior.LastName.CompareTo(next.LastName);
						});
					break;
				default:
					Error = String.Format("File could not be sorted.");
					return false;
			}

			return true;
		}

		public bool AddRecord(
				string LastName,
				string FirstName,
				string gender,
				string FavoriteColor,
				string DateOfBirth)
		{
			string dateOfBirth = DateOfBirth.Trim();
			string[] dateElements = new string[3];
			dateElements = dateOfBirth.Split('/');
			DateTime dt = new DateTime(
				Int32.Parse(dateElements[2]),	// year,
				Int32.Parse(dateElements[0]),	// month,
				Int32.Parse(dateElements[1]));	// day);

			Person record = new Person(
				LastName.Trim(),
				FirstName.Trim(),
				(gender.Trim().Substring(0, 1).ToUpper() == "F") ? Gender.Female : Gender.Male,
				FavoriteColor.Trim(),
				dt);

			_personnelList.Add(record);
			return true;
		}
	}
}
