using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelData.DAL;

namespace PersonnelData.Model
{
	public enum Gender
	{
		Female,
		Male
	};

	public class Person : IEquatable<Person>
	{
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public Gender Gender { get; set; }
		public string FavoriteColor{ get; set; }
		public DateTime DateOfBirth { get; set; }

		public Person(string lastName, string firstName, Gender gender, string favoriteColor, DateTime dateOfBirth)
		{
			LastName = lastName;
			FirstName = firstName;
			Gender = gender;
			FavoriteColor = favoriteColor;
			DateOfBirth = dateOfBirth;
		}

		public string ToJsonString()
		{
			return String.Format("{{\"LastName\":\"{0}\",\"FirstName\":\"{1}\",\"Gender\":\"{2}\",\"FavoriteColor\":\"{3}\",\"DateOfBirth\":\"{4:MM/dd/yyyy}\"}}",
				LastName, FirstName, Gender, FavoriteColor, DateOfBirth);
		}

		public override string ToString()
		{
			return String.Format("{0}; {1}; {2}; {3}; {4:MM/dd/yyyy}",
				LastName, FirstName, Gender, FavoriteColor, DateOfBirth);
		}

		public static bool operator ==(Person obj1, Person obj2)
		{
			if (ReferenceEquals(obj1, null)) { return false; }
			if (ReferenceEquals(obj2, null)) { return false; }

			return (obj1.LastName == obj2.LastName)
			   && (obj1.FirstName == obj2.FirstName)
			   && (obj1.Gender == obj2.Gender)
			   && (obj1.FavoriteColor == obj2.FavoriteColor)
			   && (obj1.DateOfBirth == obj2.DateOfBirth);
		}

		public static bool operator !=(Person obj1, Person obj2)
		{
			return !(obj1 == obj2);
		}

		public bool Equals(Person other)
		{
			if (ReferenceEquals(null, other)) { return false; }

			return (LastName.Equals(other.LastName)
			   && FirstName.Equals(other.FirstName)
			   && Gender.Equals(other.Gender)
			   && FavoriteColor.Equals(other.FavoriteColor)
			   && DateOfBirth.Equals(other.DateOfBirth));
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) { return false; }
			return obj.GetType() == GetType() && Equals((Person)obj);
		}
	}
}
