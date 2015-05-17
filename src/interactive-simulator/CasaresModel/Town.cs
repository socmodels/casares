using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace CasaresModel
{
	public class Town
	{
		// Class that handles the main processes of
		// the town (yearly dismissals, overall statistics, ..)
		
		// Publics
		public int TimeElapsed;
		public int TotalTime;
		public int Colors;
		public int PersonsCount = 0;
		public int PersonsMarried = 0;
		public int CompaniesCount = 0;
		public Stats Stats = new Stats();
		public double SenioritySalaryIncrement = 0;
		public double AverageStartupWage=0;
		public double DeviationStartupSeniority=0;
		public double AverageStartupSeniority=0;
		
		public double AverageStartupEmployees = 0;
		public double DeviationStartupEmployees = 0;
		public int MaximumEmployeesPerCompany = 0;
		
		public int StatesOccupiedCount = 0;
		public int StatesUnemployedCount = 0;
		public int StatesInactiveCount = 0;
		public int BankrupcyDuration = 0;
		public int Productivity = 0;
		
		public double ExpectationsDecay = 0;
		public int MaximumUnemploymentLength = 0;
		public double MaximumProfit = 0;
		public double MaximumDismissals = 0;
		public int AnnualDissmissalWaves = 0;
		public int DismissalsOfTheMonth = 0;
		public double MinimumExpectations = 0;
		
		// Privates	
		private ArrayListPersons _persons = new ArrayListPersons();
		private ArrayListCompanies _companies = new ArrayListCompanies();
		private Environment _environment = new Environment();
		public ArrayListCompanies Companies
		{
			get
			{
				return _companies;
			}
		}
		public ArrayListPersons Persons
		{
			get
			{
				return _persons;
			}
		}
		// Constructor
		public Town(Environment environment)
		{
			this._environment = environment;
		
			// Loads the environment parameters
			this.TotalTime = Int32.Parse(this._environment.TownSettings["TotalTime"].ToString());
			this.PersonsCount = Int32.Parse(this._environment.TownSettings["Persons"].ToString());
			this.Colors = Int32.Parse(this._environment.TownSettings["Colors"].ToString());
			this.PersonsMarried = Int32.Parse(this._environment.TownSettings["Married"].ToString());
			this.CompaniesCount = Int32.Parse(this._environment.TownSettings["Companies"].ToString());
			this.AverageStartupEmployees = Double.Parse(Main.FixDecimal(this._environment.TownSettings["AverageStartupEmployees"].ToString()));
			this.DeviationStartupEmployees = Double.Parse(Main.FixDecimal(this._environment.TownSettings["DeviationStartupEmployees"].ToString()));
			this.MaximumEmployeesPerCompany = Int32.Parse(this._environment.TownSettings["MaximumEmployeesPerCompany"].ToString());
			this.SenioritySalaryIncrement = Double.Parse(Main.FixDecimal(this._environment.TownSettings["SenioritySalaryIncrement"].ToString()));
			this.Productivity = Int32.Parse(this._environment.TownSettings["Productivity"].ToString());
			this.BankrupcyDuration = Int32.Parse(this._environment.TownSettings["BankrupcyDuration"].ToString());
		
			this.AverageStartupWage = Double.Parse(Main.FixDecimal(this._environment.TownSettings["AverageStartupWage"].ToString()));
			this.DeviationStartupSeniority = Double.Parse(Main.FixDecimal(this._environment.TownSettings["DeviationStartupSeniority"].ToString()));
			this.AverageStartupSeniority = Double.Parse(Main.FixDecimal(this._environment.TownSettings["AverageStartupSeniority"].ToString()));
			this.AnnualDissmissalWaves = Int32.Parse(this._environment.TownSettings["AnnualDissmissalWaves"].ToString());
			this.MinimumExpectations = Double.Parse(Main.FixDecimal(this._environment.TownSettings["MinimumExpectations"].ToString()));

			// Initializes collections
			this._createCompanies();
			this._createPersons();
			// States the wages expectations
			// for the persons
			this._createStartupExpectations();
			// Puts employees into jobs according
			// to startup distribution settings
			this._setStartupJobs();
			// Updates the state of each person at the labor market
			this._setStartupLaborState();
			// Done!
			return;
		}
	
		// Receives parameters
		public void SetParameters(double expectationsDecay, int maximumUnemploymentLength, double maximumProfit, double maximumDismissals)
		{
			this.ExpectationsDecay = expectationsDecay;
			this.MaximumUnemploymentLength = maximumUnemploymentLength;
			this.MaximumProfit = maximumProfit;
			this.MaximumDismissals = maximumDismissals;
			return;
		}
		
		// Initializes the list of persons
		public void _createPersons()
		{
			// Create each person
			for (int n=0; n < this.PersonsCount; n++)
				this._persons.Add(new Person(n));
			// Make all weddings, so that all married persons has a spouse.
			int couples = this.PersonsMarried / 2;
			for (int n=0; n < couples; n++)
			{
				this._persons[n].Spouse = this._persons[n + couples];
				this._persons[n + couples].Spouse = this._persons[n] ;
			}
			// Set the colors for the persons
			this._paintItemsPersons(this._persons);
			// Done
			return;
		}
		
		// Initialize the list of companies 
		public void _createCompanies()
		{
			// Create the companies
			for (int n=0; n < CompaniesCount; n++)
				this._companies.Add(new Company(this, n));
			// Sets the colors
			this._paintItemsCompanies(this._companies);
			// Sets the number of jobs taken by
			// each company using the startup distribution
			foreach(Company e in this._companies.Collection)
			{
				e.CountEmpleados = (int) Math.Round(NormalVariate(this.AverageStartupEmployees, this.DeviationStartupEmployees));
				if (e.CountEmpleados > this.MaximumEmployeesPerCompany)
					e.CountEmpleados = this.MaximumEmployeesPerCompany;
				if (e.CountEmpleados < 0)
					e.CountEmpleados = 0;
			}
			// Done
			return;
		}
		public double NormalVariate(double average, double stddev)
		{
			return GenerateStandardNormal() * stddev + average;
		}
		static Random rng = new Random(); 
		public static double GenerateStandardNormal() 
		{ 
			// Both r and theta should be in (0,1]. Unfortunately 
			// Random.NextDouble gives [0,1), so we just subtract it 
			// from 1. 
			double r = 1.0d-rng.NextDouble(); 
			double theta = 1.0d-rng.NextDouble(); 
			return Math.Sin (2*Math.PI*theta)*Math.Sqrt(-2*Math.Log(r)); 
		} 

		// Take a list of elements and set a color for each
		// one of them
		public void _paintItemsCompanies(ArrayListCompanies items)
		{
			int colorCurrent = 1;
			int total = items.Count;
			for (int n= 0; n < total; n++)
			{
				items[n].Color = colorCurrent;
				colorCurrent += 1;
				if (colorCurrent > this.Colors)
					colorCurrent = 1;
			}
			return;
		}
		public void _paintItemsPersons(ArrayListPersons items)
		{
			int colorCurrent = 1;
			int total = items.Count;
			for (int n= 0; n < total; n++)
			{
				items[n].Color = colorCurrent;
				colorCurrent += 1;
				if (colorCurrent > this.Colors)
					colorCurrent = 1;
			}
			return;
		}
	
		// For the initial distribution of seniority and
		// wage, it creates the corresponding expectations
		public void _createStartupExpectations()
		{
			foreach(Person p in this._persons.Collection)
			{
				double seniority = NormalVariate(this.AverageStartupSeniority, this.DeviationStartupSeniority);
				if (seniority < 0) seniority = 0;
				p.Seniority = (int) seniority;
				p.Expectations = Math.Max(this.MinimumExpectations, this.AverageStartupWage);
			}
			return;
		}
		
		// Given two populations of persons and companies,
		// it arranges persons at the free places in the companies
		public void _setStartupJobs()
		{
			// In order to optimiza the search for free places
			// for a given color, it makes a dictionary having
			// the index for the next free company for each color
			Hashtable NextCompanyByColor = new Hashtable();
			for (int color = 0; color < Colors; color++)
			{
				NextCompanyByColor[color+1] = this._getNextFreeStartup(color+1);
			}
			// Goest through the persons lists until it
			// occupies as much persons as free places it has
			int singlePersons = this.PersonsCount - this.PersonsMarried;
			int marriedPersons = this.PersonsMarried;
			int maximum = Math.Max(singlePersons, this.PersonsMarried);
			for (int n = 0; n < maximum; n++)
			{
				// Puts a married one
				if (n < marriedPersons)
				{
					int color = this._persons[n].Color;
					if ((int) NextCompanyByColor[color] != -1)
					{
						this._companies[(int) NextCompanyByColor[color]].Emplear(this._persons[n], false);
						NextCompanyByColor[color] = this._getNextFreeStartup(color, (int) NextCompanyByColor[color]);
					}
				}
				// Puts a single one
				if (n < singlePersons)
				{
					int color = this._persons[n+marriedPersons].Color;
					if ((int) NextCompanyByColor[color] != -1)
					{
						this._companies[(int) NextCompanyByColor[color]].Emplear(this._persons[n+marriedPersons], false);
						NextCompanyByColor[color] = this._getNextFreeStartup(color, (int) NextCompanyByColor[color]);
					}
				}
			}
			return;
		}
	
		// Gets the next company with a free place available 
		// for a given employee color
		public int _getNextFreeStartup(int color)
		{
			return _getNextFreeStartup(color, 0);
		}

		public int _getNextFreeStartup(int color, int position)
		{
			while (position < this.CompaniesCount)
			{
				if (this._companies[position].Color == color)
					if (this._companies[position]._employees.Count < this._companies[position].CountEmpleados)
						return position;
				position+=1;
			}
			// There are no free places...
			return -1;
		}
		
		// Sets the inicial labor state for those who has
		// no job at the beginning of the run
		public void _setStartupLaborState()
		{
			foreach(Person p in this._persons.Collection)
			{
				if (p.State == 0)
				{
					if (p.Spouse == null)
						p.State = 2; // unemployed... go and search
					else
					{
						if (p.Spouse.State == 1)
							p.State = 2 + r.Next(1); // randint(2, 3); // inactive/unemployed
						else
							p.State = 2; // souse without job... go and search
					}
					this.UpdateCounter(0, p.State);
				}
			}
			return;
		}
		
		// Update the statistics for total number of occupied, 
		// unemployed and inactive personas, at instance of
		// changing one of these values to a person.
		public void UpdateCounter(int previousState, int nextState)
		{
			if (previousState == 1)
				this.StatesOccupiedCount-=1;
			else if (previousState == 2)
				this.StatesUnemployedCount-=1;
			else if (previousState == 3)
				this.StatesInactiveCount-=1;
		
			if (nextState == 1) 
				this.StatesOccupiedCount+=1;
			else if (nextState == 2)
				this.StatesUnemployedCount+=1;
			else if (nextState == 3)
				this.StatesInactiveCount+=1;
		
			if (this.TimeElapsed > 0 && this.StatesOccupiedCount + this.StatesUnemployedCount + this.StatesInactiveCount != this.PersonsCount)
				throw new Exception("ERROR: The total number of persons do not match the total number by labor state.");
			return;
		}
	
		// Moves forward in time
		public void Evolve()
		{
			Evolve(1);
		}
		public void Evolve(int cicles)
		{
			for (int i=0; i < cicles; i++)
			{
				this._singleEvolve();
				getStatistics();
			}
			return;
		}
		
		// Makes the evolution (processes) for one time interval
		public void _singleEvolve()
		{
			this.TimeElapsed += 1;
			// It checks for dismissals
			int intervaloDismissals = (int) (12 / this.AnnualDissmissalWaves);
			this.DismissalsOfTheMonth = 0;
			if (this.TimeElapsed % intervaloDismissals == 0)
			{	// Make dismissals
				foreach (Company e in this._companies.Collection)
				{	
					if (e.CountEmpleados > 0 && e.Dismissals > 0)
					{	
						int dismissals = (int) Math.Round((double)(e.CountEmpleados) * e.Dismissals / this.AnnualDissmissalWaves);
						this.DismissalsOfTheMonth += (int) ((dismissals) / e.CountEmpleados);
						e.Fire(dismissals);
					}
				}
			}			
		
			// It checks for companies that should reopen 
			// and update the Bankrupcy duration for the others
			foreach (Company e in this._companies.Collection)
			{
				if (e.CurrentBankrupcyDuration != -1)
				{
					e.CurrentBankrupcyDuration += 1;
					if (e.CurrentBankrupcyDuration == this.BankrupcyDuration)
						e.Reopen();
				}
			}
		
			// Make a search cicle
			this.JobSearchProcess();
		
			// Finally, evaluates at each company
			// if it can affort it obligations (if salaries
			// are lower or hight than sales)
			foreach (Company e in this._companies.Collection)
			{
				e.CalculateProfit();
				if (e.Profit < 0)
				{
					e.GoToBankrupcy();
					e.Profit = 0;
					e.GrossProduct = 0;
				}
			}
			// If it is december, calculates the balance
			if (this.TimeElapsed % 12 == 0)
				foreach (Company e in this._companies.Collection)
					e.MakeBalance();
		
			// Done
			return;
		}

	
		// For all employees, it trys to relocate it in an
		// open company
		static Random r = new Random();
		public void JobSearchProcess()
		{
			// Search for inactives that may need to start looking
			// for a job (become unemployed searchers)
			for (int n=0; n < this.PersonsMarried; n++)
			{
				// (only for marries, as single never get inactive)
				if (this._persons[n].State == 3)
					if (this._persons[n].Spouse.State != 1)
					{
						this.UpdateCounter(3, 2);	  // If the spouse doesn"t work
						this._persons[n].State = 2;    // and is inactive, goes to unemployment
						this._persons[n].UnemploymentLength = 0;
					}
			}
			// Makes an interview for each unemployed
			// In order to do this, it builds a list of companies
			// having free places, and goes through the list of unemployed
			// doing the "interviews".
		
			// TODO: it would be nice to avoid going through the list 
			ArrayListCompanies companiesWithFreePlaces = new ArrayListCompanies();
			foreach (Company e in this._companies.Collection)
			{
				if (e.FreePlaces > 0)
					companiesWithFreePlaces.Add(e);
			}
			int indexCompany;
			foreach(Person p in this._persons.Collection)
				if (p.State == 2)
				{
					// Picks a company to make an interview
					if (companiesWithFreePlaces.Count > 0)
						indexCompany = r.Next(companiesWithFreePlaces.Count-1);
					else
						indexCompany = -1;
					if (indexCompany > -1 && companiesWithFreePlaces[indexCompany].Color == p.Color)
					{
						// Matched!!
						companiesWithFreePlaces[indexCompany].Emplear(p);
						// It checks if it is still a company with free places
						if (companiesWithFreePlaces[indexCompany].FreePlaces == 0)
							companiesWithFreePlaces.RemoveAt(indexCompany);
					}
					else
					{
						// Rejected!
						// Lowers expectations of those who didn"t get a job
						p.Expectations = Math.Max(this.MinimumExpectations, p.Expectations * (1 - this.ExpectationsDecay));
						// Moves to inactivity those who are discouraged
						p.UnemploymentLength += 1;
						if (p.Spouse != null)
							if (p.Spouse.State == 1)		
								if (p.UnemploymentLength > this.MaximumUnemploymentLength)
								{
									// Retires...
									this.UpdateCounter(2, 3);
									p.State = 3;
								}
					}
					// Done
				}
		}
	
		// Statistics.
		// The Statistics reports the state of the town. 
		// The are accessed by the Statistics property and StatisticsHeadings attribute.
		public static string [] StatisticsHeadings = new string[] { "Expectations_Decay", "Discouragement", "Profit_Maximum","Dismissals_Maximums", "Month", "Average_Wage", "Wage_Deviation", "Activity_rate", "Unemployment_rate", "Companies", "Gross_Product", "Profit", "Average_Dismissals(%)", "Average_Expectations", "Family_Wage" };
		private ArrayList getStatistics()
		{
			ArrayList stats = new ArrayList();
			// Parametros
			stats.Add(this.ExpectationsDecay);
			stats.Add(this.MaximumUnemploymentLength); 
			stats.Add(this.MaximumProfit);
			stats.Add(this.MaximumDismissals);
		
			// Month
			stats.Add(this.TimeElapsed);
			// Average Wage 
			// TODO: it could be calculated in advance for better performance
			//stddev = Stddev()
			double wageMedioTotal = 0;
			int personsCount = 0;
			int couples = this.PersonsMarried / 2;
			int singles = this.PersonsCount - this.PersonsMarried;
			double wageTotalMarriedFamilies = 0;
			double averageExpectationsTotal = 0;
			double wageTotalSingleFamilies = 0;
		
			for (int n = 0; n < this.PersonsCount; n++)
			{
				Person person = this._persons[n];
				// Keeps the sum for married families
				if (n == this.PersonsMarried)
					wageTotalMarriedFamilies = wageMedioTotal;
				// Keeps adding for all families
				if (person.State == 1)
				{
					wageMedioTotal += person.WageTotal;
					//stddev.append(person.Wage)
					personsCount += 1;
				}
				if (person.State == 2)
				{
					averageExpectationsTotal += person.Expectations;
					//stddev.append(person.Wage)
				}
			}
	
			if (personsCount > 0)
			{
				// Average wage 
				Stats.Salary = (double)(wageMedioTotal) / (int)(personsCount);
				stats.Add(Stats.Salary);
				// Wage deviation
				stats.Add(0);
				// quoted for performance
				//stats.append(stddev.getStddev()) 
			}
			else
			{
				Stats.Salary = 0;
				stats.Add(0.0);
				stats.Add(0.0);
			}
	
			// Activity rate and unemployment rate
			int activos = this.StatesOccupiedCount + this.StatesUnemployedCount;
			
			this.Stats.ActivityRate = 100 * (double)(activos) / this.PersonsCount;
			stats.Add(this.Stats.ActivityRate);
			
			if (activos > 0)
				this.Stats.UnemploymentRate = 100 * (double)(this.StatesUnemployedCount) / (int)(activos);
			else
				this.Stats.UnemploymentRate = 0;
			stats.Add(this.Stats.UnemploymentRate);
		
			// Solves total companies, Gross Product and Profit
			double totalGrossProduct = 0;
			double totalProfit = 0;
			int count = 0;
			foreach(Company e in this._companies.Collection)
			{
				if (e.CurrentBankrupcyDuration == -1)
					count+=1; //// it is open
				else
				{
					if (e.GrossProduct > 0)
					{
						/*print "ExpectationsDecay: ", this.ExpectationsDecay 
						print "MaximumUnemploymentLength: ", this.MaximumUnemploymentLength 
						print "MaximumProfit: ", this.MaximumProfit 
						print "MaximumDismissals: ", this.MaximumDismissals 
						*/
						throw new Exception("ERROR: Al menos una company tiene GrossProduct estando cerrada.");
					}
				}
				totalGrossProduct += e.GrossProduct;
				totalProfit += e.Profit;
			}
			
			// Writes results
			Stats.Companies = count;
			stats.Add(Stats.Companies);
			Stats.Profit = 100 * totalProfit / totalGrossProduct;
			stats.Add(totalGrossProduct);
			stats.Add(totalProfit);
	
			// Average Dismissals
			if (count > 0)
				stats.Add((double)(this.DismissalsOfTheMonth) / count);
			else
				stats.Add(0.0);
		
			// Average Expectations
			if (this.StatesUnemployedCount > 0)
				stats.Add((double)(averageExpectationsTotal) / (int)(this.StatesUnemployedCount));
			else
				stats.Add(0.0);
			
			// Wages.... 
			if (personsCount > 0)
				// All familias 
				Stats.Income = Math.Round((double)(wageMedioTotal) / (couples * 2 + singles),2);
			else
				Stats.Income = 0;
			stats.Add(Stats.Income );
			return stats;
		}
	
		public ArrayList Statistics 
		{
			get
			{
				return getStatistics();
			}
		}
	}
}

