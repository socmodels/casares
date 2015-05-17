from random import random

class Person:
	# Class that handles attributes and actions
	# of each person
	
	# Publics
	Wage = 0
	WageTotal = 0
	Color = 0
	State = 0  # State in labor market:  1=Occupied, 2=Unemployed, 3=Inactive
	Seniority = 0
	Expectations = 0
	UnemploymentLength = 0
	Spouse = None # reference to its wife or husband
	
	# Privates
	
	# Constructor
	def __init__ (self):
		Spouse = None
		return
	
