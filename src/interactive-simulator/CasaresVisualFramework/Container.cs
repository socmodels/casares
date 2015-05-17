using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public abstract class Container
	{
		private ContainerMovementState containerMovementState = null;
		public bool IsMoving
		{
			get
			{
				return (containerMovementState != null);
			}
		}

		public abstract Color BorderColor
		{
			get ;
		}

		private const int size = 10;
		private Place[,] Places = new Place[size, size];
		public ArrayList Row;
		private ArrayList freePlaces = new ArrayList();
		private ArrayList takenPlaces = new ArrayList();
		private Space _space;		
		
		public Space Space
		{
			get
			{
				return _space;
			}
		}
		
		public static int Width 
		{
			get
			{
				return PersonView.Width * size + 
					PersonView.Width * (size + 9); 
			}
		}
		public static int Height
		{
			get
			{
				return PersonView.Height * size + 
					PersonView.Height * (size + 10); 
			}
		}
		
		public void AddPerson(PersonView p)
		{
			if (freePlaces.Count == 0)
				throw new Exception("No free places available");
			// Looks for a free place
			//int index = r.Next(freePlaces.Count - 1);
			int index = 0;
			// Puts the person in place
			((Place) freePlaces[index]).Occupy(p);
			takenPlaces.Add((Place) freePlaces[index]);
			// Remove the place from free places
			freePlaces.RemoveAt(index);
		}
		public Container(Space space)
		{
			_space = space;
			for (int row = 0; row < 10; row++)
				for (int col = 0; col < 10; col++)
				{
					Places[row,col] = new Place(row, col);
					freePlaces.Add(Places[row,col]);
					Places[row,col].Container = this;
				}
		}
		public void ReleasePerson(PersonView p)
		{
			// Remove from place and frees place
			Place place = p.place;
			place.Release();
			this.freePlaces.Add(place);
			this.takenPlaces.Remove(place);
		}
		public bool HasFreeSpaces
		{
			get
			{
				return (freePlaces.Count > 0);
			}
		}
		public void DrawChilds(PaintEventArgs e, Rectangle MyPosition, int PaintReference)
		{
			foreach(Place p in takenPlaces)
			{
				if (p.Occupant.IsMoving == false)
				{
					Point location = GetLocationForPlace(p);
					p.Occupant.Draw(location, e, PaintReference);
				}
			}
		}
		public void DrawFrame(Graphics g, Rectangle MyPosition)
		{
			// Draw frame
			g.DrawRectangle(new Pen(this.BorderColor, 1), MyPosition.X + PersonView.Width * 2, 
				MyPosition.Y + PersonView.Width * 2, MyPosition.Width - 1 - PersonView.Width * 4, MyPosition.Height - 1 -PersonView.Width * 4);
		}
		public void Draw (Rectangle MyPosition, PaintEventArgs e, int PaintReference)
		{
			// Clear background
			Rectangle back = e.ClipRectangle;
			back.Intersect(MyPosition);
			e.Graphics.FillRectangle(Brushes.Black, back);			
			
			// Draw frame
			DrawFrame(e.Graphics, MyPosition);
			
			// Draw childs
			DrawChilds(e, MyPosition, PaintReference);
		}
		public Point GetLocationForPlace(Place p)
		{
			Point currentLocation;
			if (IsMoving)
			{
				Point myLocation = containerMovementState.currentAbsoluteLocation;
				Rectangle myPosition = new Rectangle(myLocation.X, myLocation.Y, Container.Width, Container.Height);
				currentLocation = GetLocationForPlace(myPosition, p);
			}
			else
			{
				Rectangle myPosition = _space.GetRectangleForContainer(this);
				currentLocation = GetLocationForPlace(myPosition, p);

				if (currentLocation.Y % 4 > 0)
					throw new Exception("Invalid location of the space");
			}
			return currentLocation;
		}
		private Point GetLocationForPlace(Rectangle MyPosition, Place p)
		{
			Point ret = new Point(
				MyPosition.X +
				p.Column * 2 * PersonView.Width +
				5 * PersonView.Width,

				MyPosition.Y +
				p.Row * 2 * PersonView.Height +
				5 * PersonView.Height);
			return ret;
		}
	}
}
