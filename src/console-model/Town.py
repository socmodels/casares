from random import random
from random import normalvariate
from random import randint
from Output import Output
from Environment import Environment
from Person import Person
from Company import Company
from Stddev import Stddev

class Town:
	# Class that handles the main processes of
	# the town (yearly dismissals, overall statistics, ..)
	
	# Publics
	TimeElapsed = 0
	TotalTime = 0
	Colors = 0
	PersonsCount = 0
	PersonsMarried = 0
	CompaniesCount = 0
	SenioritySalaryIncrement = 0
	AverageStartupWage=0
	DeviationStartupSeniority=0
	AverageStartupSeniority=0

	
	AverageStartupEmployees = 0
	DeviationStartupEmployees = 0
	MaximumEmployeesPerCompany = 0
	
	StatesOccupiedCount = 0
	StatesUnemployedCount = 0
	StatesInactiveCount = 0
	BankrupcyDuration = 0
	Productivity = 0
	
	ExpectationsDecay = 0
	MaximumUnemploymentLength = 0
	MaximumProfit = 0
	MaximumDismissals = 0
	AnnualDissmissalWaves = 0
	DismissalsOfTheMonth = 0
	MinimumExpectations = 0
	
	# Privates	
	_persons = None
	_companies = None
	_environment = None
	
	# Constructor
	def __init__ (self, environment):
		self._environment = environment
		self._persons = []
		self._companies = []
	
		# Loads the environment parameters
		self.TotalTime = eval(self._environment.TownSettings['TotalTime'])
		self.PersonsCount = eval(self._environment.TownSettings['Persons'])
		self.Colors = eval(self._environment.TownSettings['Colors'])
		self.PersonsMarried = eval(self._environment.TownSettings['Married'])
		self.CompaniesCount = eval(self._environment.TownSettings['Companies'])
		self.AverageStartupEmployees = eval(self._environment.TownSettings['AverageStartupEmployees'])
		self.DeviationStartupEmployees = eval(self._environment.TownSettings['DeviationStartupEmployees'])
		self.MaximumEmployeesPerCompany = eval(self._environment.TownSettings['MaximumEmployeesPerCompany'])
		self.SenioritySalaryIncrement = eval(self._environment.TownSettings['SenioritySalaryIncrement'])
		self.Productivity = eval(self._environment.TownSettings['Productivity'])
		self.BankrupcyDuration = eval(self._environment.TownSettings['BankrupcyDuration'])
		
		self.AverageStartupWage = eval(self._environment.TownSettings['AverageStartupWage'])
		self.DeviationStartupSeniority = eval(self._environment.TownSettings['DeviationStartupSeniority'])
		self.AverageStartupSeniority = eval(self._environment.TownSettings['AverageStartupSeniority'])
		self.AnnualDissmissalWaves = eval(self._environment.TownSettings['AnnualDissmissalWaves'])
		self.MinimumExpectations = eval(self._environment.TownSettings['MinimumExpectations'])

		# Initializes collections
		self._createCompanies()
		self._createPersons()
		# States the wages expectations
		# for the persons
		self._createStartupExpectations()
		# Puts employees into jobs according
		# to startup distribution settings
		self._setStartupJobs()
		# Updates the state of each person at the labor market
		self._setStartupLaborState()
		# Done!
		return
	
	# Receives parameters
	def SetParameters(self, expectationsDecay, maximumUnemploymentLength,maximumProfit,maximumDismissals):
		self.ExpectationsDecay = expectationsDecay
		self.MaximumUnemploymentLength = maximumUnemploymentLength
		self.MaximumProfit = maximumProfit
		self.MaximumDismissals = maximumDismissals		
		return
		
	# Initializes the list of persons
	def _createPersons(self):
		# Create each person
		for n in xrange(self.PersonsCount):
			self._persons.append(Person())
		# Make all weddings, so that all married persons has a spouse.
		couples = self.PersonsMarried / 2
		for n in xrange(couples):
			self._persons[n].Spouse = self._persons[n + couples]
			self._persons[n + couples].Spouse = self._persons[n] 
		# Set the colors for the persons
		self._paintItems(self._persons)
		# Done
		return
		
	# Initialize the list of companies 
	def _createCompanies(self):
		# Create the companies
		for n in xrange(self.CompaniesCount):
			self._companies.append(Company(self))
		# Sets the colors
		self._paintItems(self._companies)
		# Sets the number of jobs taken by
		# each company using the startup distribution
		for e in self._companies:
			e.CountEmpleados = int(round(normalvariate(self.AverageStartupEmployees, self.DeviationStartupEmployees)))
			if e.CountEmpleados > self.MaximumEmployeesPerCompany: e.CountEmpleados = self.MaximumEmployeesPerCompany
			if e.CountEmpleados < 0: e.CountEmpleados = 0
		# Done
		return

	# Take a list of elements and set a color for each
	# one of them
	def _paintItems(self, items):
		colorCurrent = 1
		total = len(items)
		for n in xrange(total):
			items[n].Color = colorCurrent
			colorCurrent += 1
			if colorCurrent > self.Colors: colorCurrent = 1
		return

	# For the initial distribution of seniority and
	# wage, it creates the corresponding expectations
	def _createStartupExpectations(self):
		for p in self._persons:
			seniority = normalvariate(self.AverageStartupSeniority, self.DeviationStartupSeniority)
			if seniority < 0: seniority = 0
			p.Seniority = seniority
			p.Expectations = max(self.MinimumExpectations, self.AverageStartupWage)
		return
		
	
	# Given two populations of persons and companies,
	# it arranges persons at the free places in the companies
	def _setStartupJobs(self):
		# In order to optimiza the search for free places
		# for a given color, it makes a dictionary having
		# the index for the next free company for each color
		NextCompanyByColor = {}
		for color in xrange(self.Colors):
			NextCompanyByColor[color+1] = self._getNextFreeStartup(color+1)
		# Goest through the persons lists until it
		# occupies as much persons as free places it has
		singlePersons = self.PersonsCount - self.PersonsMarried
		marriedPersons = self.PersonsMarried
		maximum = max(singlePersons, self.PersonsMarried)
		for n in xrange(maximum):
			# Puts a married one
			if n < marriedPersons :
				color = self._persons[n].Color
				if NextCompanyByColor[color] != -1:
					self._companies[NextCompanyByColor[color]].Emplear(self._persons[n], False);
					NextCompanyByColor[color] = self._getNextFreeStartup(color, NextCompanyByColor[color])
			# Puts a single one
			if n < singlePersons :
				color = self._persons[n+marriedPersons].Color
				if NextCompanyByColor[color] != -1:
					self._companies[NextCompanyByColor[color]].Emplear(self._persons[n+marriedPersons], False);
					NextCompanyByColor[color] = self._getNextFreeStartup(color, NextCompanyByColor[color])
		return
	
	# Gets the next company with a free place available 
	# for a given employee color
	def _getNextFreeStartup(self, color, position = 0):
		while position < self.CompaniesCount:
			if self._companies[position].Color == color:
				if len(self._companies[position]._employees) < self._companies[position].CountEmpleados:
					return position
			position+=1
		# There are no free places...
		return -1
		
	# Sets the inicial labor state for those who has
	# no job at the beginning of the run
	def _setStartupLaborState(self):
		for p in self._persons:
			if p.State == 0:
				if p.Spouse == None:
					p.State = 2 # unemployed... go and search
				else:
					if p.Spouse.State == 1:
						p.State = randint(2, 3) # inactive/unemployed
					else:
						p.State = 2 # souse without job... go and search
				self.UpdateCounter(0, p.State)
		return
		
	# Update the statistics for total number of occupied, 
	# unemployed and inactive personas, at instance of
	# changing one of these values to a person.
	def UpdateCounter(self, previousState, nextState):
		if previousState == 1: self.StatesOccupiedCount-=1
		elif previousState == 2: self.StatesUnemployedCount-=1
		elif previousState == 3: self.StatesInactiveCount-=1
		
		if nextState == 1: self.StatesOccupiedCount+=1
		elif nextState == 2: self.StatesUnemployedCount+=1
		elif nextState == 3: self.StatesInactiveCount+=1
		
		if self.TimeElapsed > 0 and self.StatesOccupiedCount + self.StatesUnemployedCount + self.StatesInactiveCount != self.PersonsCount:
			raise "ERROR: The total number of persons do not match the total number by labor state."
		return		
	
	# Moves forward in time
	def Evolve(self, cicles=1):
		for i in xrange(cicles):
			self._singleEvolve()	
		return
		
	# Makes the evolution (processes) for one time interval
	def _singleEvolve(self):
		self.TimeElapsed += 1	
		# It checks for dismissals
		intervaloDismissals = int(12 / self.AnnualDissmissalWaves)
		self.DismissalsOfTheMonth = 0
		if self.TimeElapsed % intervaloDismissals == 0:
			# Make dismissals
			for e in self._companies:
				if e.CountEmpleados > 0 and e.Dismissals > 0:
					dismissals = int(round(float(e.CountEmpleados) * e.Dismissals / self.AnnualDissmissalWaves))
					self.DismissalsOfTheMonth += float(dismissals) / e.CountEmpleados
					e.Fire(dismissals)
					
		
		# It checks for companies that should reopen 
		# and update the Bankrupcy duration for the others
		for e in self._companies:
			if e.CurrentBankrupcyDuration != -1:
				e.CurrentBankrupcyDuration += 1
				if e.CurrentBankrupcyDuration == self.BankrupcyDuration:
					e.Reopen()
		
		# Make a search cicle
		self.JobSearchProcess()
		
		# Finally, evaluates at each company
		# if it can affort it obligations (if salaries
		# are lower or hight than sales)
		for e in self._companies:
			e.CalculateProfit()
			if e.Profit < 0:
				e.GoToBankrupcy()
				e.Profit = 0
				e.GrossProduct = 0
		# If it is december, calculates the balance
		if self.TimeElapsed % 12 == 0:
			for e in self._companies:
				e.MakeBalance()
		
		# Done
		return
	
	# For all employees, it trys to relocate it in an
	# open company
	def JobSearchProcess(self):
		# Search for inactives that may need to start looking
		# for a job (become unemployed searchers)
		for n in xrange(self.PersonsMarried):
			# (only for marries, as single never get inactive)
			if self._persons[n].State == 3:
				if self._persons[n].Spouse.State != 1:
					self.UpdateCounter(3, 2)	  # If the spouse doesn't work
					self._persons[n].State = 2    # and is inactive, goes to unemployment
					self._persons[n].UnemploymentLength = 0
		# Makes an interview for each unemployed
		# In order to do this, it builds a list of companies
		# having free places, and goes through the list of unemployed
		# doing the 'interviews'.
		
		# TODO: it would be nice to avoid going through the list 
		companiesWithFreePlaces = []
		for e in self._companies:
			if e.FreePlaces > 0:
				companiesWithFreePlaces.append(e)
		for p in self._persons:
			if p.State == 2:
				# Picks a company to make an interview
				if len(companiesWithFreePlaces) > 0:
					indexCompany = randint(0, len(companiesWithFreePlaces)-1)
				else:
					indexCompany = -1
				if indexCompany > -1 and companiesWithFreePlaces[indexCompany].Color == p.Color:
					# Matched!!
					companiesWithFreePlaces[indexCompany].Emplear(p)
					# It checks if it is still a company with free places
					if companiesWithFreePlaces[indexCompany].FreePlaces == 0:
						companiesWithFreePlaces.pop(indexCompany)
				else:
					# Rejected!
					# Lowers expectations of those who didn't get a job
					p.Expectations = max(self.MinimumExpectations, p.Expectations * (1 - self.ExpectationsDecay))
					# Moves to inactivity those who are discouraged
					p.UnemploymentLength += 1
					if self._persons[n].Spouse != None:
						if self._persons[n].Spouse.State == 1:		
							if p.UnemploymentLength > self.MaximumUnemploymentLength:
								# Retires...
								self.UpdateCounter(2, 3)
								p.State = 3
			# Done
			
	# Statistics.
	# The Statistics reports the state of the town. 
	# The are accessed by the Statistics property and StatisticsHeadings attribute.
	StatisticsHeadings = [ 'Expectations_Decay', 'Discouragement', 'Profit_Maximum','Dismissals_Maximums', 'Month', 'Average_Wage', 'Wage_Deviation', 'Activity_rate', 'Unemployment_rate', 'Companies', 'Gross_Product', 'Profit', 'Average_Dismissals(%)', 'Average_Expectations', 'Family_Wage']
	def getStatistics(self):
		stats = []
		# Parametros
		stats.append(float(self.ExpectationsDecay)) 
		stats.append(self.MaximumUnemploymentLength) 
		stats.append(float(self.MaximumProfit)) 
		stats.append(float(self.MaximumDismissals)) 
		
		# Month
		stats.append(self.TimeElapsed) 
		# Average Wage 
		# TODO: it could be calculated in advance for better performance
		#stddev = Stddev()
		wageMedioTotal = 0
		personsCount = 0
		couples = self.PersonsMarried / 2
		singles = self.PersonsCount - self.PersonsMarried
		wageTotalMarriedFamilies = 0
		averageExpectationsTotal = 0
		wageTotalSingleFamilies = 0
		
		for n in xrange(self.PersonsCount):
			person = self._persons[n];
			# Keeps the sum for married families
			if n == self.PersonsMarried:
				wageTotalMarriedFamilies = wageMedioTotal
			# Keeps adding for all families
			if person.State == 1:
				wageMedioTotal += person.WageTotal
				#stddev.append(person.Wage)
				personsCount += 1
			if person.State == 2:
				averageExpectationsTotal += person.Expectations
				#stddev.append(person.Wage)
	
		if int(personsCount) > 0:
			# Average wage 
			stats.append(float(wageMedioTotal) / int(personsCount)) 
			# Wage deviation
			stats.append(0)
			# quoted for performance
			#stats.append(stddev.getStddev()) 
		else:	
			stats.append(0.0)
			stats.append(0.0)
	
		# Activity rate and unemployment rate
		activos = self.StatesOccupiedCount + self.StatesUnemployedCount
		stats.append(100 * float(activos) / self.PersonsCount)
		if int(activos) > 0:
			stats.append(100 * float(self.StatesUnemployedCount) / int(activos))
		else: 
			stats.append(0)
		
		# Solves total companies, Gross Product and Profit
		totalGrossProduct = 0
		totalProfit = 0
		count = 0
		for e in self._companies:
			if e.CurrentBankrupcyDuration == -1: 
				count+=1 ## it is open
			else:
				if e.GrossProduct > 0:
					print "ExpectationsDecay: ", self.ExpectationsDecay 
					print "MaximumUnemploymentLength: ", self.MaximumUnemploymentLength 
					print "MaximumProfit: ", self.MaximumProfit 
					print "MaximumDismissals: ", self.MaximumDismissals 
					raise "ERROR: Al menos una company tiene GrossProduct estando cerrada."
			totalGrossProduct += e.GrossProduct
			totalProfit += e.Profit
			
			
		# Writes results
		stats.append(count)
		stats.append(totalGrossProduct)
		stats.append(totalProfit)
	
		# Average Dismissals
		if count > 0:
			stats.append(float(self.DismissalsOfTheMonth) / count)
		else:
			stats.append(0.0)
		
		# Average Expectations
		if self.StatesUnemployedCount > 0:
			stats.append(float(averageExpectationsTotal) / int(self.StatesUnemployedCount)) 
		else:
			stats.append(0.0)
			
		# Wages.... 
		if personsCount > 0:
			# All familias 
			stats.append(round(float(wageMedioTotal) / (couples * 2 + singles),2))
		else:	
			stats.append(0.0)
		return stats
	Statistics = property(getStatistics)
	

