using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace CasaresModel
{
	public class Main
	{
		Environment Environment;
		public Town Casares;
		public void Load()
		{
			// Start the environment (intput and output stuff)
			Environment = new Environment();
			// Shows its settings
			Environment.Output.ShowDictionary(Environment.TownSettings);

			// Put the headers in the output file
			Environment.Output.WriteList(Town.StatisticsHeadings);
			Environment.OutputSummary.WriteList(Town.StatisticsHeadings);

			Casares = new Town(Environment);

			LoadParameters();
			return;
		}
		public static string FixDecimal(string s)
		{
			string separator = (1.5).ToString().Substring(1,1);
			string ret = s.Replace(",", separator).Replace(".", separator);
			return ret;
		}
		private void LoadParameters()
		{
			// Read all parameters into variables
			double ExpectationsDecayFrom = Double.Parse(Main.FixDecimal((Environment.RunSettings["ExpectationsDecayFrom"].ToString())));
			int MaximumUnemploymentLengthFrom = Int32.Parse((Environment.RunSettings["MaximumUnemploymentLengthFrom"].ToString()));
			double MaximumProfitFrom = Double.Parse(Main.FixDecimal((Environment.RunSettings["MaximumProfitFrom"].ToString())));
			double MaximumDismissalsFrom = Double.Parse(Main.FixDecimal((Environment.RunSettings["MaximumDismissalsFrom"].ToString())));
			
			double expectationsDecay = ExpectationsDecayFrom;
			int maximumUnemploymentLength = MaximumUnemploymentLengthFrom;
			double maximumProfit = MaximumProfitFrom;
			double maximumDismissals = MaximumDismissalsFrom;
			Casares.SetParameters(expectationsDecay, maximumUnemploymentLength, maximumProfit, maximumDismissals);
		}
	
	}
}