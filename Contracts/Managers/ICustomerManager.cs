using CustomerApi;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Managers
{
	public interface ICustomerManager
	{
		Task AddCustomer(CustomerRequest customerRequest);
		Task<List<Customer>> GetAllCustomers();
		Task<Customer> GetById(int id);
		Task UpdateCustomer(CustomerRequest customerRequest, int id);
		Task DeleteCustomer(int id);
	}
}
