using Contracts;
using Contracts.Repositories;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
	public  class UnitOfWork : IUnitOfWork
	{
		private readonly CustomerContext _context;
		private CustomerRepository? _customers;
		public UnitOfWork(CustomerContext context)
		{
			this._context = context;
		}

		public ICustomerRepository Customers
		{
			get { return this._customers ??= new CustomerRepository(this._context); }
		}

		public async Task<int> Save()
		{
			return await this._context.SaveChangesAsync();
		}
	}
}
