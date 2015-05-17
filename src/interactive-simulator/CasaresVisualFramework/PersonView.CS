using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using CasaresModel;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for Person.
	/// </summary>
	public class PersonView
	{
		public static int DownwardCorridor = 0;
		public static int UpwardCorridor = 0;
		private static Color localGreen = Color.FromArgb(0, 255, 0);
		private static Color localRed = Color.FromArgb(255, 0, 0);
		private static Color localYellow = Color.FromArgb(255, 255, 0);
		private Space IsRegisteredAt;
		private PersonMovementState personMovementState = null;
		public Place place;
		public Person Person;
		
		private const int step = 2;
		public bool IsMoving
		{
			get
			{
				return (personMovementState != null);
			}
		}
		public void Move(Container newcontainer)
		{
			if (place == null) throw new Exception("Person with no current location cannot move");
			Point Current;
			if (IsMoving)
			{
				Current = this.personMovementState.currentAbsoluteLocation;
				FinishMoving();
			}
			else
				Current = place.Container.GetLocationForPlace(this.place);
			// Gets current place
			PersonMovementState move = new PersonMovementState();
			move.currentAbsoluteLocation = Current;
			// It bounds for refresh updates
			//IsForeignTo = place.Container;
			//place.Container.ForeignPersons.Add(this);
			// Frees the person
			place.Container.ReleasePerson(this);
			// Assigns the new place
			newcontainer.AddPerson(this);
			// Puts the movement info
			personMovementState = move;
			// Registers
			newcontainer.Space.MovingPersons.Add(this);
			IsRegisteredAt = newcontainer.Space;
		}
		public bool UpdateMovingTarget()
		{
			// Gets target location
			Point pTarget = place.Container.GetLocationForPlace(place);
			Point pCurrent = this.personMovementState.currentAbsoluteLocation;
			// If it matches, it is done
			if (pCurrent.X == pTarget.X &&
				Math.Abs(pCurrent.Y - pTarget.Y) <= 2)
			{
				this.FinishMoving();
				return false;
			}
			int moveX = 0;
			int moveY = 0;
			// It states if the target is the corridor or the place
			int targetRow = pTarget.Y + (OffsetDueToDirection(pTarget.Y, pTarget.X, pCurrent.X));
			int situation = targetRow - pCurrent.Y;
			if (situation == 0)
			{
				// Target is the place
				int multi = 1;
				if (Math.Abs(pCurrent.X - pTarget.X) > 16)
					multi = 8;
				if (pCurrent.X > pTarget.X)
					moveX = -step * multi;
				else
					moveX = +step * multi;
			}
			else
			{	// Target is the corridor
				if (situation < 0)
					// Target is downward corridor
					pTarget.X = PersonView.DownwardCorridor;
				else if (situation > 0)
					// Target is upward corridor
					pTarget.X = PersonView.UpwardCorridor;
				// I am at it?
				if (pCurrent.X == pTarget.X)
				{
					int multi = 1;
					if (Math.Abs(pCurrent.Y - targetRow) > 16)
						multi = 8;
					if (pCurrent.Y > targetRow)
						moveY = -step * multi;
					else
						moveY = +step * multi;
				}
				else
				{
					// If it is unable to move horizontally, it moves up/down
					int voff = OffsetDueToDirection(pCurrent.Y, pTarget.X, pCurrent.X);
					if (voff == 0)
					{
						int multi = 1;
						if (Math.Abs(pCurrent.X - pTarget.X) > 16)
							multi = 8;
						// Move horizontally...
						if (pCurrent.X > pTarget.X)
							moveX = -step * multi;
						else
							moveX = +step * multi;
					}
					else
					{
						// Move vertically to an allowed line to move
						if (voff > 0)
							moveY = -step;
						else
							moveY = +step;
					}
				}
			}
			ChangeLocation(moveX, moveY);
			return true;
		}
		private void ChangeLocation(int moveX, int moveY)
		{
			personMovementState.previousAbsoluteLocation = personMovementState.currentAbsoluteLocation;

			personMovementState.currentAbsoluteLocation.X += moveX;
			personMovementState.currentAbsoluteLocation.Y += moveY;
		}
			
		private int OffsetDueToDirection(int currenty, int x1, int x2)
		{
			int closerLine;
			bool bEsPar = ((currenty / 4) % 2 == 0);
			int offset;
			if (x1 < x2)
				// Left
				offset = +2;
			else
				// Right
				offset = -2;
			if (bEsPar == false) offset = offset * -1;

			closerLine = 4 * (currenty / 4) + offset;

			int dif = currenty - closerLine;
			return dif;
		}
		private void FinishMoving()
		{
			IsRegisteredAt.MovingPersons.Remove(this);
			personMovementState = null;
			//this.place.Container.Space.Parent.Invalidate(this.place.Container.Space.GetRectangleForContainer(this.place.Container));
		}
		private const int size = 2;
		public static int Width 
		{
			get
			{
				return size; 
			}
		}
		public static int Height
		{
			get
			{
				return size; 
			}
		}
		public PersonView(Person p)
		{
			Person = p;
			p.StateChanged += new EventHandler(p_StateChanged);
		}
		public void CleanPrevious (PaintEventArgs e)
		{
			internalDraw(new Point(0,0), e, -1, true);
		}
		public void Draw (PaintEventArgs e, int PaintReference)
		{
			internalDraw(new Point(0,0), e, PaintReference, false);
		}
		public void Draw (Point MyPosition, PaintEventArgs e, int PaintReference)
		{
			internalDraw (MyPosition, e, PaintReference, false);
		}
		private void internalDraw (Point MyPosition, PaintEventArgs e, int PaintReference, bool JustClear)
		{
			if (this.IsMoving == true)
				MyPosition = personMovementState.currentAbsoluteLocation;
			//if (JustClear)
			//	MyPosition = personMovementState.previousAbsoluteLocation;
			if (e.ClipRectangle.Contains(MyPosition) == false)
				return;

			Brush b;
			if (JustClear)
				b = Brushes.Black;
			else
				//b = new SolidBrush(localGreen);
			{
				// HouseIncome
				double HouseIncome = this.Person.WageTotal;
				if (this.Person.Spouse != null)
				{
					HouseIncome += this.Person.Spouse.WageTotal;
                    HouseIncome *= .5;
				}
				if (HouseIncome < 5)
					b = new SolidBrush(localRed);
				else if (HouseIncome <= 10)
					b = new SolidBrush(localYellow);
				else
					b = new SolidBrush(localGreen);
			}

			e.Graphics.FillRectangle(b, MyPosition.X,
					MyPosition.Y, size, size);
		}

		private void p_StateChanged(object sender, EventArgs e)
		{
			// 1=Occupied, 2=Unemployed, 3=Inactive
			if (Person.State == 2)
			{
				this.Move(this.place.Container.Space.Parent.Unemployed.GetContainerWithSpace());
			}
			if (Person.State == 3)
			{
				this.Move(this.place.Container.Space.Parent.Inactive.GetContainerWithSpace());
			}
			if (Person.State == 1)
			{
				// Bounds to the right company...
				ViewManager vm = this.place.Container.Space.Parent;
				CompanyView cv = vm.GetCompanyById(Person.Company.Id);
				this.Move(cv);
			}
		}
	}
}

