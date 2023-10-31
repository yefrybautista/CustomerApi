using Contracts.Managers;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : ControllerBase
	{

		private readonly ICustomerManager _customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
			_customerManager = customerManager;

		}

		[HttpPost]
		public async Task<IActionResult> Post(CustomerRequest customer)
		{
			try
			{
				await _customerManager.AddCustomer(customer);
				return Created("", customer);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var customers = await _customerManager.GetAllCustomers();
			return Ok(customers);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var customer = await _customerManager.GetById(id);
			return Ok(customer);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] CustomerRequest customer, int id)
		{
			try
			{
				await _customerManager.UpdateCustomer(customer, id);
				return Ok(customer);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _customerManager.DeleteCustomer(id);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


	}
}