using System;
using System.Drawing;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for ContainerMovementState.
	/// </summary>
	public class ContainerMovementState
	{
		public Point currentAbsoluteLocation; // it is used when it moves...
		public Place targetContainer;
		
		public ContainerMovementState()
		{
			
		}
	}
}
