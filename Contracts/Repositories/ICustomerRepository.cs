using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
	public interface ICustomerRepository
	{
		public ValueTask<EntityEntry<Customer>> AddAsync(Customer customer);
		public Task<Customer?> FindAsync(int id);
		public Task<List<Customer>> GetAllAsync();
		public Task<EntityEntry<Customer>?> RemoveAsync(Customer customer);
		Task<Customer> FindByEmail(string email);
		EntityEntry<Customer>? UpdateAsync(Customer Customer);
	}
}
