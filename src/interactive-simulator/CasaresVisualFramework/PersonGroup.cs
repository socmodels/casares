using System;
using System.Drawing;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for PersonGroup.
	/// </summary>
	public class PersonGroup : Container
	{
		public override System.Drawing.Color BorderColor
		{
			get
			{
				return Color.Black;
			}
		}

		public PersonGroup(Space space, object content) : base(space)
		{

		}
	}
}
