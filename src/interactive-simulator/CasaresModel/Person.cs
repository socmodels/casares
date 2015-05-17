using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace CasaresModel
{
	public class Person
	{
		// Class that handles attributes and actions
		// of each person
	
		// Publics
		public double Wage = 0;
		public Company Company = null;
		public double WageTotal = 0;
		public int Color = 0;
		private int _state = 0;  // State in labor market:  1=Occupied, 2=Unemployed, 3=Inactive
		public int State
		{
			get
			{
				return _state;
			}
			set
			{
				if (_state != value)
				{
					_state = value;
					OnStateChanged();
				}
			}
		}
		public event EventHandler StateChanged;
		protected void OnStateChanged()
		{
			if (StateChanged != null)
				StateChanged(this, null);
		}
		public int Seniority = 0;
		public double Expectations = 0;
		public int UnemploymentLength = 0;
		public Person Spouse = null; // reference to its wife or husband
		public int Id;

		// Privates
	
		// Constructor
		public Person(int id)
		{
			Id = id;
		}
	}
}
	
