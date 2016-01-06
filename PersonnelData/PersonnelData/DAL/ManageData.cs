using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Configuration;
using PersonnelData.Model;

namespace PersonnelData.DAL
{
	class DataManage
	{
		//private FileStream _inputStream = null;
		//private StreamReader _stream = null;
		public string Error { get; set; }
		
		private List<Person> _personnelList = new List<Person>();
		public List<Person> PersonnelList { get { return _personnelList; } }

		internal enum FileType
		{
			Pipe,
			Comma,
			Space,
			NotSet
		};

		internal enum SortMethod
		{
			Gender,
			BirthDate,
			LastName,
			NotSet
		};

		public bool GetData(string fileName)
		{
			bool rtn = true;

			string _pathName = ConfigurationManager.AppSettings["dataPath"];
			
			//FileStream inputStream = null;
			//StreamReader streamReader = null;
			//using (FileStream inputStream = new FileStream(_pathName + fileName, FileMode.Open, FileAccess.Read))
			try
			{
				using (StreamReader streamReader = new StreamReader(_pathName + fileName))
				{
					//inputStream = new FileStream(_pathName + fileName, FileMode.Open, FileAccess.Read);
					//streamReader = new StreamReader(_pathName + fileName);
				//}
				//catch
				//{
				//	Error = String.Format("File not found.");
				//	return false;
				//}

					//FileType fileType = GetFileType(inputStream);
					FileType fileType = GetFileType(streamReader);

					//if (!ParseData(inputStream, fileType))
					if (!ParseData(streamReader, fileType))
					{
						Error = String.Format("File could not be parsed.");
						rtn = false;
					}

					//if (inputStream != null)
					//	inputStream.Close();
				}
			}
			catch
			{
				Error = String.Format("File not found.");
				rtn = false;
			}

			return rtn;
		}

		//private bool ParseData(FileStream inputStream, FileType fileType)
		private bool ParseData(StreamReader inputStream, FileType fileType)
		{
			if (fileType == FileType.NotSet)
				return false;

			//long streamLength = inputStream.Length;
			//byte[] byteStream = new byte[streamLength];
			//int bytesRead = inputStream.ReadLinRead(byteStream, 0, streamLength);
			string bytesRead = inputStream.ReadLine();

			string lastName;
			string firstName;
			string gender;
			string favoriteColor;
			string dateOfBirth;

			//while (!EOF)
			switch (fileType)
			{
				case FileType.Pipe:
						// parse each field (String.Split)
					break;
				case FileType.Comma:
					break;
				case FileType.Space:
					break;
			}

			// TrimStart and End
			// Assign to string (lastName, firstName)
			// Determine Gender (ToLower, get 1st byte)
			// Determine dateOfBirth (split on '/', trim leading 0, set DateTime)
			// Add to list

			return true;
		}

		// TODO:
		private bool SortData(SortMethod sortMethod)
		{
			switch (sortMethod)
			{
				case SortMethod.Gender:
					//_personnelList.Sort();
					break;
				case SortMethod.BirthDate:
					break;
				case SortMethod.LastName:
					break;
				default:
					Error = String.Format("File could not be sorted.");
					return false;
			}
			return true;
		}

		// TODO:
		private bool CompareGender(string prior, string next)
		{
			// trim leading spaces
			// get first char
			// Tolower

			prior.TrimStart();
			next.TrimStart();
			return true;
		}

		// TODO:
		private bool CompareBirthDate(string prior, string next)
		{
			// Assumptions:
			//		Month and day are dnumeric

			// Ascending
			// MM/DD/YYYY
			// MM and DD could be single digits
			return true;
		}

		// TODO:
		private bool CompareLastName(string prior, string next)
		{
			// Descending
			return true;
		}

		//private FileType GetFileType(FileStream inputStream)
		private FileType GetFileType(StreamReader streamReader)
		{
			// Assumptions:
			//		No field will contain a '|',  ',', or ' '
			//		The delimiter could be either:
			//		*	a single space
			//		*	a single space (no multiple spaces) followed by '|' or ','
			//		*	no space followed by '|' or ','

			FileType fileType = FileType.NotSet;
			int maxRecordLength = 100;

			if (streamReader != null)
			{
				byte[] firstRecord = new byte[maxRecordLength];
				//int bytesRead = streamReader.ReadLine()(firstRecord, 0, maxRecordLength);
				string s = streamReader.ReadLine();

				bool haveFirstSpace = false;
				int index = 0;
				//while (index < bytesRead)
				while (index < s.Length)
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

				//streamReader.Seek(0L, SeekOrigin.Begin);
			}

			return fileType;
		}
	}
}
