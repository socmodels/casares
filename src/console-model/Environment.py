from Input import Settings
from time import clock
from Output import Output


class Environment:
	# This class handles the configuration files
	# of the model, as well as output for results
	
	# Publics
	RunSettings = None
	TownSettings = None
	Output = None
	OutputSummary = None
	
	# Privates
	_startTime = 0
	
	# Constructor
	def __init__ (self):
		self.Output = Output("results.txt")
		self.OutputSummary = Output("resultsSummary.txt")
		self.RunSettings = Settings("run.ini").LoadSection("General")
		self.TownSettings = Settings("town.ini").LoadSection("General")
		self._startTime = clock()
		return

	# Execution elapsed time indicator
	def getElapsedTime(self):
		return clock() - self._startTime
	ElapsedTime = property(getElapsedTime)
