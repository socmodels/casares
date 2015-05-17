using System;
using System.Drawing;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for PersonMovementState.
	/// </summary>
	public class PersonMovementState
	{
		public Point currentAbsoluteLocation; // it is used when it moves...
		public Point previousAbsoluteLocation; // it is used when it moves...
		public Place targetContainer;
		
		public PersonMovementState()
		{
		}
	}
}
