using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;

namespace CasaresModel
{
	public class Company
	{
		// Class that handles the attributes and actions
		// of the companies
		
		// Publics
		public int Id;
		public int Color = 0;
		public int CountEmpleados = 0;
		public double Expectations = 0;
		public double GrossProduct = 0;
		public double Profit = 0;
		public int CurrentBankrupcyDuration = 0;
		public double Dismissals = 0;
		public double ProfitAnnual = 0;
		public double GrossProductAnnual = 0;
		
		// Privates
		public ArrayList _employees = new ArrayList();// list of employees 
		private Town _town = null;
		
		// Constructor
		public Company(Town town, int id)
		{
			Id = id;
			_town = town;
			CurrentBankrupcyDuration = -1;
			return;
		}
		
		// Hires an employee at the company
		public void Emplear(Person employee)
		{
			Emplear(employee, true);
		}
		public void Emplear(Person employee, bool updateCount)
		{
			if (employee.State == 1)
				throw new Exception("ERROR: The person is already working somewhere else.");
			if (employee.Color != this.Color) 
				throw new Exception("ERROR: The colors do not match.");
			// Updates the statistics of the tows 
			// populations (telling previous and current value)
			_town.UpdateCounter(employee.State, 1);
			// Updates the employee
			employee.Company = this;
			employee.Wage = employee.Expectations;
			employee.State = 1;
			// Puts the guy into the collection
			_employees.Add(employee);
			if (updateCount)
				this.CountEmpleados += 1;
			// Done
			return;
		}
		
		// Calculates the total sales for the month (GrossProduct)
		// and the profit (Profit=GrossProduct-Salaries). 
		// Updates seniorities for the employees.
		public void CalculateProfit()
		{
			// Calculates total product (product = sales)
			GrossProduct = _employees.Count * _town.Productivity;
			// Calculates profit using salaries
			this.Profit = this.GrossProduct;
			foreach (Person p in _employees)
			{
				p.Seniority += 1;
				p.WageTotal = p.Wage * (1 + ((int) (p.Seniority / 12)) * this._town.SenioritySalaryIncrement);
				this.Profit -= p.WageTotal;
			}
			this.ProfitAnnual += this.Profit;
			this.GrossProductAnnual += this.GrossProduct;
			
			return;
		}

		// Calculates the balance for the end of the year
		public void MakeBalance()
		{
			if (_employees.Count == 0)
			{
				ProfitAnnual = 0;
				GrossProductAnnual = 0;
				Dismissals = 0;
				return;
			}
			if (_employees.Count > this._town.MaximumEmployeesPerCompany)
				throw new Exception("ERROR: It is not possible for a company to have more than " + _town.MaximumEmployeesPerCompany.ToString() + " employees");
	
			// Using the profit, the gross product,
			// and the maximum profit and maximim dimissals parameters,
			// it calculates the proportion/ratio of desmissals for the next year
			double profits;
			profits = ProfitAnnual / GrossProductAnnual;
			Dismissals = Math.Max(_town.MaximumDismissals * (_town.MaximumProfit - profits) / _town.MaximumProfit, 0);
			ProfitAnnual = 0;
			GrossProductAnnual = 0;
			return;
		}
	
		// Closes the company (goes to bankrupcy)
		public void GoToBankrupcy()
		{
			// 'Frees' the employees
			// (sets all of them as unemployed)
			Fire(_employees.Count);
			if (_employees.Count > 0)
				throw new Exception("ERROR: Is is not possible for a company to close if it still has employees.");
			// Updates its values
			Profit = 0;
			GrossProduct = 0;
			ProfitAnnual = 0;
			GrossProductAnnual = 0;
			CurrentBankrupcyDuration = 0;
			return;
		}
		
		// Rebuild a new company
		public void Reopen()
		{
			if (Profit > 0 || GrossProduct > 0)
				throw new Exception("ERROR: It is not possible for a reopening company to have gross product");
			if (_employees.Count > 0)
				throw new Exception("ERROR: It is not possible for a reopening company to have employees before it reopens");
			CurrentBankrupcyDuration = -1;
			Dismissals = 0;
			ProfitAnnual = 0;
			GrossProductAnnual = 0;
			return;
		}
		
		// Fires a number n of employees
		public void Fire(int n)
		{
			// Makes the list of persons to fire
			ArrayList elegidos = (ArrayList) _employees.Clone();
			if (n < _employees.Count)
			{
				sample(elegidos, n);
			}
			// Goes backwards through the list picking them
			// up from the list
			foreach(Person person in elegidos)
			{
				_employees.Remove(person);
				person.Expectations = person.WageTotal;
				person.Wage = 0;
				person.WageTotal = 0;
				person.Seniority = 0;
				person.Company = null;
				person.UnemploymentLength = 0;
				person.State = 2;
				_town.UpdateCounter(1, 2);
			}
			this.CountEmpleados -= elegidos.Count;
			return;
		}
		private void sample(ArrayList l, int nCount)
		{
			Random r = new Random();
			while (l.Count > nCount)
				l.RemoveAt(r.Next(l.Count - 1));	
		}
		
		public int getFreePlaces()
		{
			if (CurrentBankrupcyDuration != -1)
				return 0; // it is closed
			else
				return _town.MaximumEmployeesPerCompany - this.CountEmpleados;
		}
	
		public int FreePlaces 
		{
			get 
			{
				return getFreePlaces();
			}
		}
	}
}
			

