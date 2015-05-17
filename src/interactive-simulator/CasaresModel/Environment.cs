using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace CasaresModel
{
	public class Environment
	{
		// This class handles the configuration files
		// of the model, as well as output for results
		
		// Publics
		public Hashtable RunSettings;
		public Hashtable TownSettings;
		public Output Output;
		public Output OutputSummary;
		
		// Privates
		public DateTime _startTime;
		
		// Constructor
		public Environment ()
		{
			Output = new Output("results.txt");
			OutputSummary = new Output("resultsSummary.txt");
			RunSettings = Settings.LoadSection("run.ini", "General");
			TownSettings = Settings.LoadSection("town.ini", "General");
			_startTime = DateTime.Now;
		}
		
		// Execution elapsed time indicator
		public TimeSpan getElapsedTime()
		{
			return DateTime.Now - _startTime;
		}
		public TimeSpan	ElapsedTime 
		{
			get
			{
				return getElapsedTime();
			}
		}
	}
}
