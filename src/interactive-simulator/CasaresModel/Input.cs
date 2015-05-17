using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace CasaresModel
{
	public class  Settings
	{
		// Inicializes using the name of the config file
		public Settings()
		{
		}
		public static Hashtable LoadSection(string file, string sectionName)
		{
			string filename;
			filename = System.Reflection.Assembly.GetExecutingAssembly().Location;
			filename = filename.Substring(0, filename.LastIndexOf("\\"));
			filename += "\\" + file;
			// Opens the file
			System.IO.StreamReader sr = new System.IO.StreamReader(filename);
			string content = sr.ReadToEnd();
			sr.Close();
			// Loads into memory
			content = content.Replace('\r', '\n');
			content = content.Replace("\n\n", "\n");
			// Close the file
			// Moves up to required section
			int startIndex = content.ToLower().IndexOf("[" + sectionName.ToLower() + "]\n");
			if (startIndex == -1)
				throw new Exception("ERROR: Section name '" + sectionName + "' count not be found");
			// Find the next one
			int endIndex = content.IndexOf("\n[", startIndex+1);
			if (endIndex == -1)
				endIndex = content.Length;
			// Sets values into a dictionary
			int from = startIndex + sectionName.Length + 3;
			string itemsString = content.Substring(from, endIndex - from);
			string [] itemsList = itemsString.Split('\n');
			Hashtable retDictionary = new Hashtable();
			foreach(string itemLine in itemsList)
			{
				string item = itemLine.Trim();
				if (item.Length > 0 && item[0] != '#')
				{
					string [] pair = item.Split('=');
					if (pair.Length < 2)
						throw new Exception("ERROR: The line: '" + item + "' is not a valid entry=value item");
					retDictionary[pair[0].Trim()] = pair[1].Trim();
				}
			}
			// Return the values
			return retDictionary;
		}
	
		// Reads an entry
		public string LoadEntry(string file, string sectionName, string entryName)
		{
			return (string) LoadSection(file, sectionName)[entryName];
		}
	}
}


