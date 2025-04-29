using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMvp.Model;
using Microsoft.AspNetCore.Authorization;

namespace ApiMvp.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly LogisticMvpContext _context;

        public OrdersController(LogisticMvpContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            List<OrderDTO> result = new();
            foreach (var order in _context.Orders.Include(o=>o.Status))
            {
               
                result.Add(new OrderDTO()
                {
                    Id = order.Id,
                    StatusId = order.StatusId,
                    CustomerName = order.CustomerName,
                    OrderDate = order.OrderDate,
                    Status = order.Status.Title,
                });
            }
            return Ok(result);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutOrder(int id, OrderDTO sent_order)
        {
            if (id != sent_order.Id)
            {
                return BadRequest();
            }
            Order order = new Order()
            {
                Id=sent_order.Id,
                StatusId = _context.OrderStatuses.FirstOrDefault(s=>s.Title==sent_order.Status).Id,
                CustomerName=sent_order.CustomerName,
                OrderDate=sent_order.OrderDate

            };
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderDTO sent_order)
        {
            Order order = new Order()
            {
                Id = sent_order.Id,
                StatusId = _context.OrderStatuses.FirstOrDefault(s => s.Title == sent_order.Status).Id,
                CustomerName = sent_order.CustomerName,
                OrderDate = sent_order.OrderDate
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            if(_context.Orders.Contains(order)) return Ok();
            else return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        [HttpGet ("Status")]
        public async Task<ActionResult<List<string>>> GetOrderStatuses()
        {
            List<string> result = new();
            foreach (var s in _context.OrderStatuses)
            {
                result.Add(s.Title);
            }
            return Ok(result);
        }
    }
}
