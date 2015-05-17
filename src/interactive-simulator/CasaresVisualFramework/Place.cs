using System;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for Place.
	/// </summary>
	public class Place
	{
		public Container Container;
		public int Row;
		public int Column;

		private PersonView _occupant;
		public PersonView Occupant
		{
			get 
			{
				return _occupant;
			}
		}
		public void Release()
		{
			if (this._occupant != null)
				this._occupant.place = null;
			this._occupant = null;
		}
		public void Occupy(PersonView p)
		{
			this._occupant = p;
			p.place = this;
		}
		public Place(int row, int col)
		{
			Column = col;
			Row = row;
		}
	}
}
