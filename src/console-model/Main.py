#!/usr/bin/env python
from Environment import Environment
from Town import Town
from Frange import frange
from Output import formatTime

print formatTime(10000)

# Start the environment (intput and output stuff)
Environment = Environment()
# Shows its settings
Environment.Output.ShowDictionary(Environment.TownSettings)

# Put the headers in the output file
Environment.Output.WriteList(Town.StatisticsHeadings)
Environment.OutputSummary.WriteList(Town.StatisticsHeadings)

# Read all parameters into variables
EstimateOnly = int(eval(Environment.RunSettings['EstimateOnly']))

ExpectationsDecayFrom = float(eval(Environment.RunSettings['ExpectationsDecayFrom']))
ExpectationsDecayTo = float(eval(Environment.RunSettings['ExpectationsDecayTo']))
ExpectationsDecayIncrement = float(eval(Environment.RunSettings['ExpectationsDecayIncrement']))

MaximumUnemploymentLengthFrom = int(eval(Environment.RunSettings['MaximumUnemploymentLengthFrom']))
MaximumUnemploymentLengthTo = int(eval(Environment.RunSettings['MaximumUnemploymentLengthTo']))
MaximumUnemploymentLengthIncrement = int(eval(Environment.RunSettings['MaximumUnemploymentLengthIncrement']))

MaximumProfitFrom = float(eval(Environment.RunSettings['MaximumProfitFrom']))
MaximumProfitTo = float(eval(Environment.RunSettings['MaximumProfitTo']))
MaximumProfitIncrement = float(eval(Environment.RunSettings['MaximumProfitIncrement']))

MaximumDismissalsFrom = float(eval(Environment.RunSettings['MaximumDismissalsFrom']))
MaximumDismissalsTo = float(eval(Environment.RunSettings['MaximumDismissalsTo']))
MaximumDismissalsIncrement = float(eval(Environment.RunSettings['MaximumDismissalsIncrement']))

# Generates the ranges to iterate
maximumDismissalsRange = frange(MaximumDismissalsFrom, MaximumDismissalsTo+MaximumDismissalsIncrement, MaximumDismissalsIncrement)
maximumProfitRange = frange(MaximumProfitFrom, MaximumProfitTo+MaximumProfitIncrement, MaximumProfitIncrement)
maximumUnemploymentLengthRange = frange(MaximumUnemploymentLengthFrom, MaximumUnemploymentLengthTo+MaximumUnemploymentLengthIncrement, MaximumUnemploymentLengthIncrement)
expectationsDecayRange = frange(ExpectationsDecayFrom, ExpectationsDecayTo+ExpectationsDecayIncrement, ExpectationsDecayIncrement)

# Start the progress
progressTotal = len(maximumDismissalsRange) * len(maximumProfitRange) * len(maximumUnemploymentLengthRange) * len(expectationsDecayRange)
progressCurrent = 0
print "Runs: ", progressTotal
Environment.Output.StartProgress()

# Checks wether has to only estimate times
lastTime = Environment.ElapsedTime
if EstimateOnly == 1:
	maximumDismissalsRange = [maximumDismissalsRange[0]]
	maximumProfitRange = [maximumProfitRange[0]]
	maximumUnemploymentLengthRange = [maximumUnemploymentLengthRange[0]]
	expectationsDecayRange = [expectationsDecayRange[0]]
	progressTotalEstimated = progressTotal
	progressTotal = 1

# Makes the loop for the parameter level of dismissals
for maximumDismissals in maximumDismissalsRange:
	# Makes the loop for the parameter level of profit
	for maximumProfit in maximumProfitRange:
		# Makes the loop for the parameter discouragement (maximum seniority en el desemplo)
		for maximumUnemploymentLength in maximumUnemploymentLengthRange:
			# Makes the loop for the parameter expectations
			for expectationsDecay in expectationsDecayRange:
				# Creates an instance of town
				Casares = Town(Environment)
				Casares.SetParameters(expectationsDecay, maximumUnemploymentLength, maximumProfit, maximumDismissals)

				# It makes it evolve over 
				while (Casares.TimeElapsed < Casares.TotalTime):
					Casares.Evolve()
					Environment.Output.WriteList(Casares.Statistics)
					if Casares.TimeElapsed % 12 == 0:
						Environment.OutputSummary.WriteList(Casares.Statistics)
				# Update the progress bar
				progressCurrent+=1
				# Shows the estimated time so far
				print "\nCompleted %i of %i. Partial time: %f seconds." % (progressCurrent, progressTotal, Environment.ElapsedTime - lastTime)
				lastTime = Environment.ElapsedTime
				print "Elapsed time: %s. Estimated time: %s." % (formatTime(Environment.ElapsedTime), formatTime(float(Environment.ElapsedTime)/progressCurrent*progressTotal))
				Environment.Output.StartProgress()
				Environment.Output.UpdateProgress(100 * progressCurrent / progressTotal)
# Done
if EstimateOnly != 1:
	print "Done! Total time: %f seconds. " % Environment.ElapsedTime,
	print "Partial time: %f seconds." % (Environment.ElapsedTime / progressTotal)
else:
	totalSeconds = Environment.ElapsedTime * progressTotalEstimated
	print "Done! Estimated total time: %s hours. " % formatTime(totalSeconds ),
	print "Partial time: %f seconds." % (Environment.ElapsedTime)
	
