using System;
using System.Collections;

namespace CasaresModel
{
	/// <summary>
	/// Summary description for ArrayListCompanies.
	/// </summary>
	public class ArrayListCompanies	
	{
		private ArrayList arr = new ArrayList();
		public ArrayList Collection
		{
			get
			{
				return arr;
			}
		}
		public void RemoveAt(int n)
		{
			arr.RemoveAt(n);
		}
		public void Add(Company c)
		{
			arr.Add(c);
		}
		public int Count
		{
			get
			{
				return arr.Count;
			}
		}
		public Company this[int o]
		{
			get
			{
				return (Company) arr[o];
			}
		}
		public ArrayListCompanies()
		{
		}
	}
}
