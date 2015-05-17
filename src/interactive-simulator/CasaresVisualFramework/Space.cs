using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for Space.
	/// </summary>
	public class Space
	{
		public ArrayList MovingPersons = new ArrayList();
		private int _maxContainersPerRow;
		private bool _canCreateChildEntities;
		private Type _childEntityType;
		private ViewManager _parent;
		public ArrayList Rows = new ArrayList();
		private Point _location;
		
		public ViewManager Parent
		{
			get
			{
				return _parent;
			}
		}
		public Point Location
		{
			get
			{
				return _location;
			}
		}
		public int Width
		{
			get
			{
				return this._maxContainersPerRow * 
					Container.Width + Container.Width;
			}
		}
		public int Height
		{
			get
			{
				return this.Rows.Count * 
					Container.Height;
			}
		}

		public Container GetContainerWithSpace()
		{
			if (_canCreateChildEntities == false)
				throw new Exception("This space does not allow to self-create entities");
			// Search for a container with space
			foreach(ArrayList row in Rows)
			{
				foreach(Container container in row)
				{
					if (container.HasFreeSpaces)
						return container;
				}
			}
			// Could not find a container
			return CreateEntity(null);
		}
		public Container CreateEntity(object Content)
		{
			Container newContainer = (Container) Activator.CreateInstance(_childEntityType, new object[] { this, Content });
			this.StoreContainer(newContainer);			
			return newContainer;
		}
		public void StoreContainer(Container container)
		{
			ArrayList row = null;
			// Search for a row to put the container
			for (int n = 0; n < Rows.Count; n++)
				if (((ArrayList)Rows[n]).Count < _maxContainersPerRow)
				{
					row = (ArrayList)Rows[n];
					break;
				}
			if (row == null)
			{
				row = new ArrayList();
				Rows.Add(row);
			}
			// It appends the container to the row
			if (container.Row != null)
				container.Row.Remove(container);
			row.Add(container);
		}
		public Space(int maxContainersPerRow, Point location, Type childEntityType, ViewManager parent) : this(maxContainersPerRow, location, childEntityType, parent, false)
		{
		}
		public Space(int maxContainersPerRow, Point location, Type childEntityType, ViewManager parent, bool canCreateChildEntities)
		{
			_parent = parent;
			_canCreateChildEntities = canCreateChildEntities;
			_maxContainersPerRow = maxContainersPerRow;
			_location = location;
			_childEntityType = childEntityType;
		}
		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle(this.Location, new Size(this.Width, this.Height));
			}
		}
		public void Draw (PaintEventArgs e, int PaintReference)
		{
			for(int r = 0; r < Rows.Count; r++)
			{
				ArrayList row = (ArrayList) Rows[r];
				for(int col = 0; col < row.Count; col++)
				{
					Rectangle rect = GetRectangleForContainer(r, col);
					if (e.ClipRectangle.IntersectsWith(rect))
						((Container) row[col]).Draw(rect, e, PaintReference);
				}
			}
			// Dibuja a las moving persons
			// Draw foreign
			foreach(PersonView p in MovingPersons)
			{
				p.Draw(e, PaintReference);
			}
		}
		public Rectangle GetRectangleForContainer(Container c)
		{
			int row;
			int col;
			findContainer(c, out row, out col);
			return GetRectangleForContainer(row, col);
		}
		private void findContainer(Container c, out int orow, out int ocol)
		{
			for(int r = 0; r < Rows.Count; r++)
			{
				ArrayList row = (ArrayList) Rows[r];
				for(int col = 0; col < row.Count; col++)
				{
					if (((Container) row[col]).Equals(c))
					{
						orow = r;
						ocol = col;
						return;
					}
				}
			}
			throw new Exception("Could not find container in the space");
		}
		public void UpdateMovingObjects()
		{
			// primero empresas...
			
			// luego personas
			for(int n = 0; n < MovingPersons.Count; n++)
			{
				PersonView p = (PersonView) MovingPersons[n];
				if (p.UpdateMovingTarget() == false)
					n--;
			}
		}
		public void ClearMovingObjects()
		{
			// Cuando ya colocó a todos, los dibuja
			Graphics g = _parent.CreateGraphics();
			// 1. limpia
			for(int n = 0; n < MovingPersons.Count; n++)
			{
				PersonView p = (PersonView) MovingPersons[n];
				p.CleanPrevious(new PaintEventArgs(g, _parent.ClientRectangle));
			}
		}
		public void RedrawMovingObjects()
		{
			// Cuando ya colocó a todos, los dibuja
			Graphics g = _parent.CreateGraphics();
			// 2. dibuja
			for(int n = 0; n < MovingPersons.Count; n++)
			{
				PersonView p = (PersonView) MovingPersons[n];
				p.Draw(new PaintEventArgs(g, _parent.ClientRectangle), -1);
			}
		}
		public void FinalRedrawMovingObjects()
		{
			// Cuando ya colocó a todos, los dibuja
			Graphics g = _parent.CreateGraphics();
			PaintEventArgs parg = new PaintEventArgs(g, Parent.ClientRectangle);
			for(int r = 0; r < Rows.Count; r++)
			{
				ArrayList row = (ArrayList) Rows[r];
				for(int col = 0; col < row.Count; col++)
				{
					Rectangle MyPosition = this.GetRectangleForContainer(r, col);
					((Container) row[col]).DrawFrame(g, MyPosition);

					((Container) row[col]).DrawChilds(parg, MyPosition, -1);

					//((Container) row[col]).Draw(MyPosition, new PaintEventArgs(g, this.Parent.ClientRectangle), -1);
				}
			}
		}

		private Rectangle GetRectangleForContainer(int row, int col)
		{
			int x = this.Location.X + col * Container.Width;
			int y = this.Location.Y + row * Container.Height;
			return new Rectangle(x, y, Container.Width, Container.Height);
		}

	}
}
