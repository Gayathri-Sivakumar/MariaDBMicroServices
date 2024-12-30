using Orders.API.Models;
using Orders.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Orders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Constructor to inject IOrderService
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Get all orders
            var result = await _orderService.FindAllAsync();
            return Ok(result);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Get order by Id
            var result = await _orderService.FindOneAsync(id);
            if (result == null)
            {
                return NotFound(); // Return 404 if the order is not found
            }
            return Ok(result);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            // Create a new order
            var result = await _orderService.InsertAsync(order);
            return CreatedAtAction(nameof(Get), new { id = result }, order); // Return 201 with a location header
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Order order)
        {
            // Update an existing order
            if (id != order.Id)
            {
                return BadRequest("Order ID mismatch"); // Return 400 if the IDs don't match
            }

            var result = await _orderService.UpdateAsync(order);
            if (result == 0)
            {
                return NotFound(); // Return 404 if the order was not found to update
            }

            return Ok(result);
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Delete an order by ID
            var result = await _orderService.DeleteAsync(id);
            if (result == 0)
            {
                return NotFound(); // Return 404 if the order was not found to delete
            }

            return Ok(result); // Return 200 if the order was successfully deleted
        }
    }
}
