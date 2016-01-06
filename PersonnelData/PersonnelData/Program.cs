using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PersonnelData.DAL;
using PersonnelData.Model;

namespace PersonnelData
{
	class Program
	{
		static void Main(string[] args)
		{
			DataManager dataMngr = new DataManager();

			Console.Write("Enter File Name:  ");
			string fileName = Console.ReadLine();

			char ch;
			bool validChar = true;
			if (dataMngr.GetData(null, fileName))
			{
				Console.Write("\nEnter Sort Method:\n  G - By Gender\n  B - By Birth Date\n  L - By Last Name\n");
				int asciiCode;

				do
				{
					asciiCode = Console.Read();

					if (asciiCode > 31)	// To handle LF and CR
					{
						switch (Convert.ToChar(asciiCode))
						{
							case 'g':
							case 'G':
								dataMngr.SortData(DataManager.SortMethod.Gender);
								break;
							case 'b':
							case 'B':
								dataMngr.SortData(DataManager.SortMethod.BirthDate);
								break;
							case 'l':
							case 'L':
								dataMngr.SortData(DataManager.SortMethod.LastName);
								break;
							case 'e':
							case 'E':
								break;
							default:
								Console.Write("Invalid character entered\n");
								validChar = false;
								break;
						}

						if (validChar)
						{
							Console.Write("\n");
							foreach (Person person in dataMngr.PersonnelList)
							{
								Console.Write(person.ToString() + "\n");
							}
						}
						else
							validChar = true;

						Console.Write("\nEnter Sort Method or E to exit:  ");
					}
				} while ((Convert.ToChar(asciiCode) != 'e') && (Convert.ToChar(asciiCode) != 'E'));
			}
			else
			{
				Console.Write(dataMngr.Error + "\n");
				Console.Write("\nPress Enter key to exit");
				ch = Convert.ToChar(Console.Read());
			}
		}
	}
}
