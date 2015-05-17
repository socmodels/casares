
class Settings:
	# Inicializes using the name of the config file
	def __init__ (self, filename):
		self._filename = filename;
	
	# Reads a section from the ini
	def LoadSection(self, sectionName):
		# Opens the file
		file = open(self._filename)
		# Loads into memory
		content = file.read()
		content = content.replace('\r', '\n')
		content = content.replace("\n\n", "\n")
		# Close the file
		file.close()
		# Moves up to required section
		startIndex = content.capitalize().find("[" + sectionName.lower() + "]\n")
		if startIndex == -1: raise "ERROR: Section name '" + sectionName + "' count not be found"
		# Find the next one
		endIndex = content.find("\n[", startIndex+1)
		if endIndex == -1: endIndex = len(content)
		# Sets values into a dictionary
		itemsString = content[startIndex + len(sectionName) + 3 : endIndex];
		itemsList = itemsString.splitlines()
		retDictionary = {}
		for item in itemsList:
			item = item.strip()
			if len(item) > 0 and item[0] != '#' :
				pair = item.split('=')
				if len(pair) < 2: raise "ERROR: The line: '" + item + "' is not a valid entry=value item"
				retDictionary[pair[0].strip()] = pair[1].strip()
		# Return the values
		return retDictionary;
	
	# Reads an entry
	def LoadEntry(self, sectionName, entryName):
		return LoadSection(self, sectionName)[entryName]


