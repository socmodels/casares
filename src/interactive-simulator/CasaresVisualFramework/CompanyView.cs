using System;
using System.Drawing;
using CasaresModel;

namespace CasaresVisualFramework
{
	/// <summary>
	/// Summary description for Company.
	/// </summary>
	public class CompanyView: Container
	{
		private static Color localGreen = Color.FromArgb(0, 255, 0);
		private static Color localBrown = Color.FromArgb(128, 128, 0);
		public int Id
		{
			get
			{
				return Company.Id;
			}
		}
		public Company Company;
		public override System.Drawing.Color BorderColor
		{
			get
			{
				if (Company.CurrentBankrupcyDuration > -1)
					return localBrown;
				else
					return localGreen;
			}
		}

		public CompanyView(Space space, object Content) : base(space)
		{
			Company = (Company) Content;
		}
	}
}
