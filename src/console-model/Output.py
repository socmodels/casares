import sys

def formatTime(totalSeconds):
	return "%s:%s:%s" % (formatInt(int(totalSeconds / 3600)), formatInt(int((totalSeconds % 3600) / 60)), formatFloat(totalSeconds % 60))
def formatInt(totalSeconds):
	if totalSeconds < 10:
		return "0%i" % totalSeconds
	else: return "%i" % totalSeconds
def formatFloat(totalSeconds):
	if totalSeconds < 10:
		return "0%f" % totalSeconds
	else: return "%f" % totalSeconds
	
class Output:
	
	# Method to show values of a dictionary
	# on screen
	def ShowDictionary(self, dict, ShowKeys = True):
		for key in dict.iterkeys():
				print key, "=", dict[key]
		return
		
	def __init__(self, filename=""):
		self._filename = filename
		if filename :
			self._file = open(filename, 'w')
		else :
			self._file = sys.stdout
		
	# Method to write a secuence of values 
	# in a line, using the file specified in the constructor
	def WriteList(self, list):
		# file = 
		separator = ""
		for item in list:
			self._file.write(separator + str(item))
			separator = '\t'
		self._file.write('\n')
		return

	# Destructor which closes the open file 
	def __del__(self):
		if self._filename != "" :
			self._file.close()

	# StartProgress and UpdateProgress initiates 
	# and manages the progress bar 
	_lastProgress = 0
	def StartProgress(self):
		print "0%                100%"
		self._lastProgress = 0;
		sys.stdout.write("|")
		return
	def UpdateProgress(self, value):
		if int(value / 5) > self._lastProgress:
			_newprogress = int(value / 5)
			sys.stdout.write("=" * (_newprogress - self._lastProgress))
			self._lastProgress = _newprogress 
			if _newprogress >= 20: 
				print "|"
		return
		

		