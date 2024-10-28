using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> Orders = new();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(Orders);
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            Orders.Add(order);
            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        // Реалізація методів для PUT та DELETE
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.ProductName = order.ProductName;
            existingOrder.Price = order.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            Orders.Remove(order);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5000/api/users/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var user = await response.Content.ReadFromJsonAsync<User>();
            return Ok(user);
        }


    }

}
