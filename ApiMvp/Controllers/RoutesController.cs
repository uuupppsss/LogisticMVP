using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMvp.Model;
using Route = ApiMvp.Model.Route;
using Microsoft.AspNetCore.Authorization;

namespace ApiMvp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly LogisticMvpContext _context;

        public RoutesController(LogisticMvpContext context)
        {
            _context = context;
        }

        // GET: api/Routes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDTO>>> GetRoutes()
        {
            List<RouteDTO> result = new();
           foreach(var r in _context.Routes)
           {
                result.Add(new RouteDTO()
                {
                    Id=r.Id,
                    StartPoint = r.StartPoint,
                    EndPoint = r.EndPoint,
                    TravelTime= r.TravelTime,
                    Distance=r.Distance,
                    IdOrder=r.IdOrder,
                    IdTransport=r.IdTransport,
                });
           }
           return Ok(result);
        }

        // GET: api/Routes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Route>> GetRoute(int id)
        {
            var route = await _context.Routes.FindAsync(id);

            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        // PUT: api/Routes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoute(int id, RouteDTO sent_route)
        {
            if (id != sent_route.Id)
            {
                return BadRequest();
            }

            Route route = new Route()
            {
                Id=sent_route.Id,
                StartPoint=sent_route.StartPoint,
                EndPoint=sent_route.EndPoint,
                Distance=sent_route.Distance,
                TravelTime=sent_route.TravelTime,
                IdOrder=sent_route.IdOrder,
                IdTransport=sent_route.IdTransport,
            };
            _context.Entry(route).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(id))
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

        // POST: api/Routes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostRoute(RouteDTO sent_route)
        {
            Route route = new Route()
            {
                StartPoint = sent_route.StartPoint,
                EndPoint = sent_route.EndPoint,
                Distance = sent_route.Distance,
                TravelTime = sent_route.TravelTime,
                IdOrder = sent_route.IdOrder,
                IdTransport = sent_route.IdTransport,
            };
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            if (_context.Routes.Contains(route)) return Ok();
            else return NoContent();
        }

        // DELETE: api/Routes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoute(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }
    }
}
