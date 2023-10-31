using DataAccess;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Entities.Models;
using Contracts.Repositories;

namespace Repository
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly CustomerContext _context;

		public CustomerRepository(CustomerContext context)
		{
			this._context = context;
		}

		public ValueTask<EntityEntry<Customer>> AddAsync(Customer Customer)
		{
			return this._context.Set<Customer>().AddAsync(Customer);
		}

		public EntityEntry<Customer>? UpdateAsync(Customer Customer)
		{
			return this._context.Set<Customer>().Update(Customer);
		}

		public Task<Customer?> FindAsync(int id)
		{
			return this._context.Set<Customer>().FindAsync(id).AsTask();
		}

		public async Task<Customer> FindByEmail(string email)
		{
			return await this._context.Set<Customer>().Where(x => x.Email.Contains(email)).FirstOrDefaultAsync();
		}

		public async Task<List<Customer>> GetAllAsync()
		{
			return await this._context.Set<Customer>()
				.ToListAsync();
		}

		public async Task<EntityEntry<Customer>?> RemoveAsync(Customer customer)
		{
			return this._context.Set<Customer>().Remove(customer);
		}
	}
}
