using System;
using System.Collections;

namespace CasaresModel
{
	/// <summary>
	/// Summary description for ArrayListCompanies.
	/// </summary>
	public class ArrayListPersons
	{
		private ArrayList arr = new ArrayList();

		public ArrayList Collection
		{
			get
			{
				return arr;
			}
		}
		public void Add(Person p)
		{
			arr.Add(p);
		}
		public int Count
		{
			get
			{
				return arr.Count;
			}
		}
		public Person this[int o]
		{
			get
			{
				return (Person) arr[o];
			}
		}
		public ArrayListPersons()
		{
		}
	}
}
