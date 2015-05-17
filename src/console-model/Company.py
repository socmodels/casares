from Person import Person
from random import sample

class Company:
	# Class that handles the attributes and actions
	# of the companies
	
	# Publics
	Color = 0
	CountEmpleados = 0
	Expectations = 0
	GrossProduct = 0
	Profit = 0
	CurrentBankrupcyDuration = 0
	Dismissals = 0
	ProfitAnnual = 0
	GrossProductAnnual = 0
	
	# Privates
	_employees = None # list of employees 
	_town = None
	
	# Constructor
	def __init__ (self, town):
		self._employees = []
		self._town = town
		self.CurrentBankrupcyDuration = -1
		return
		
	# Hires an employee at the company
	def Emplear(self, employee, updateCount = True):
		if employee.State == 1: raise "ERROR: The person is already working somewhere else."
		if employee.Color != self.Color: 
			raise "ERROR: The colors do not match."
		# Updates the statistics of the tows 
		# populations (telling previous and current value)
		self._town.UpdateCounter(employee.State, 1)
		# Updates the employee
		employee.State = 1
		employee.Wage = employee.Expectations
		# Puts the guy into the collection
		self._employees.append(employee)
		if updateCount: 
			self.CountEmpleados += 1
		# Done
		return
		
	# Calculates the total sales for the month (GrossProduct)
	# and the profit (Profit=GrossProduct-Salaries). 
	# Updates seniorities for the employees.
	def CalculateProfit(self):
		# Calculates total product (product = sales)
		self.GrossProduct = len(self._employees) * self._town.Productivity
		# Calculates profit using salaries
		self.Profit = self.GrossProduct
		for p in self._employees:
			p.Seniority += 1
			p.WageTotal = p.Wage * (1 + int(p.Seniority / 12) * self._town.SenioritySalaryIncrement)
			self.Profit -= p.WageTotal
		self.ProfitAnnual += self.Profit
		self.GrossProductAnnual += self.GrossProduct
		return

	# Calculates the balance for the end of the year
	def MakeBalance(self):
		if len(self._employees) == 0:
			self.ProfitAnnual = 0
			self.GrossProductAnnual = 0
			self.Dismissals = 0
			return
		if len(self._employees) > self._town.MaximumEmployeesPerCompany:
			raise "ERROR: It is not possible for a company to have more than %i employees." % self._town.MaximumEmployeesPerCompany
	
		# Using the profit, the gross product,
		# and the maximum profit and maximim dimissals parameters,
		# it calculates the proportion/ratio of desmissals for the next year
		profits = self.ProfitAnnual / self.GrossProductAnnual
		self.Dismissals = max(self._town.MaximumDismissals * (self._town.MaximumProfit - profits) / self._town.MaximumProfit, 0)
		self.ProfitAnnual = 0
		self.GrossProductAnnual = 0
		return			
	
	# Closes the company (goes to bankrupcy)
	def GoToBankrupcy(self):
		# 'Frees' the employees
		# (sets all of them as unemployed)
		self.Fire(len(self._employees))
		if len(self._employees) > 0:
			raise "ERROR: Is is not possible for a company to close if it still has employees."
		# Updates its values
		self.Profit = 0
		self.GrossProduct = 0
		self.ProfitAnnual = 0
		self.GrossProductAnnual = 0
		self.CurrentBankrupcyDuration = 0
		return
		
	# Rebuild a new company
	def Reopen(self):
		if self.Profit > 0 or self.GrossProduct:
			raise "ERROR: It is not possible for a reopening company to have gross product"
		if len(self._employees) > 0:
			raise "ERROR: It is not possible for a reopening company to have employees before it reopens"
		self.CurrentBankrupcyDuration = -1
		self.Dismissals = 0
		self.ProfitAnnual = 0
		self.GrossProductAnnual = 0
		return
		
	# Fires a number n of employees
	def Fire(self, n):
		# Makes the list of persons to fire
		if n < len(self._employees):
			elegidos = sample(xrange(len(self._employees)), n)
			elegidos.sort()
		else:
			elegidos = xrange(len(self._employees))
		# Goes backwards through the list picking them
		# up from the list
		for i in xrange(len(elegidos)):
			personIndex = elegidos[len(elegidos) - i - 1]
			person = self._employees.pop(personIndex)
			person.Expectations = person.WageTotal
			person.Wage = 0
			person.WageTotal = 0
			person.Seniority = 0
			person.UnemploymentLength = 0
			person.State = 2
			self._town.UpdateCounter(1, 2)
		self.CountEmpleados -= len(elegidos)
		return
		
	def getFreePlaces(self):
		if self.CurrentBankrupcyDuration != -1:
			return 0 # it is closed
		else:
			return self._town.MaximumEmployeesPerCompany - self.CountEmpleados;
	FreePlaces = property(getFreePlaces)
			

