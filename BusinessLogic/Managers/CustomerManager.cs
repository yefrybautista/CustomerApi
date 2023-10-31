using Contracts;
using Contracts.Managers;
using Contracts.Repositories;
using CustomerApi;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
	public class CustomerManager : ICustomerManager
	{
        private readonly IUnitOfWork _repositories;
        public CustomerManager(IUnitOfWork repositories)
		{
			_repositories = repositories;
		}
        public async Task AddCustomer(CustomerRequest customerRequest)
		{
			var error = await ValidateModel(customerRequest, true);
			if (!string.IsNullOrWhiteSpace(error))
				throw new Exception(error);

			var newCustomer = new Customer
			{
				Name = customerRequest.Name,
				LastName = customerRequest.LastName,
				Email = customerRequest.Email,
				DateOfBirth = customerRequest.DateOfBirth,
			};
			await _repositories.Customers.AddAsync(newCustomer);
			await _repositories.Save();
		}

		public async Task UpdateCustomer(CustomerRequest customerRequest, int id)
		{

			var customerFound = await _repositories.Customers.FindAsync(id);
			if (customerFound == null)
				throw new Exception("Customer does not exists");

			var error = await ValidateModel(customerRequest, false);

			if (!string.IsNullOrWhiteSpace(error))
				throw new Exception(error);

			customerFound.Name = customerRequest.Name;
			customerFound.LastName = customerRequest.LastName;
			customerFound.Email = customerRequest.Email;
			customerFound.DateOfBirth = customerRequest.DateOfBirth;
			
			_repositories.Customers.UpdateAsync(customerFound);
			await _repositories.Save();
		}

		public async Task DeleteCustomer(int id)
		{
			var customerFound = await _repositories.Customers.FindAsync(id);
			if (customerFound == null)
				throw new Exception("Customer does not exists");

			await _repositories.Customers.RemoveAsync(customerFound);
			await _repositories.Save();
		}

		public async Task<List<Customer>> GetAllCustomers()
		{
			try
			{
				var customers = await _repositories.Customers.GetAllAsync();
				return customers;
			}
			catch (Exception)
			{
				throw;
			}

		}

		public async Task<Customer> GetById(int id)
		{
			try
			{
				var customer = await _repositories.Customers.FindAsync(id);
				return customer;
			}
			catch (Exception)
			{
				throw;
			}

		}
		private async Task<string> ValidateModel(CustomerRequest customerRequest, bool validateEmailExists)
		{
			if (customerRequest == null)
				return "Null properties";
			if (string.IsNullOrWhiteSpace(customerRequest.Name))
				return "Name can not be Null";
			if (string.IsNullOrWhiteSpace(customerRequest.LastName))
				return "LastName can not be Null";
			if (string.IsNullOrWhiteSpace(customerRequest.Email))
				return "Email can not be Null";

			var isAdult = EsMayorDeEdad(customerRequest.DateOfBirth);
			if (!isAdult)
				return "USer should be an adult";

			if (validateEmailExists)
			{
				var customerFound = await _repositories.Customers.FindByEmail(customerRequest.Email);
				if (customerFound != null)
					return "Email Already Exists";
			}

			return "";
		}

		private bool EsMayorDeEdad(DateTime fechaNacimiento)
		{
			DateTime fechaActual = DateTime.Today;
			int edad = fechaActual.Year - fechaNacimiento.Year;

			if (fechaActual.Month < fechaNacimiento.Month || (fechaActual.Month == fechaNacimiento.Month && fechaActual.Day < fechaNacimiento.Day))
			{
				edad--;
			}

			return edad >= 18;
		}
	}
}
