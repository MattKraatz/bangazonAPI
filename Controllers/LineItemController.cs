using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{
    [ProducesAttribute("application/json")]
    [Route("[controller]")]
    public class LineItemController : Controller
    {
        private BangazonContext context;

        public LineItemController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> lineItem = from LineItem in context.LineItem select LineItem;

            if (lineItem == null)
            {
                return NotFound();
            }

            return Ok(lineItem);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetLineItem")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                LineItem lineItem = context.LineItem.Single(m => m.LineItemId == id);

                if (lineItem == null)
                {
                    return NotFound();
                }

                return Ok(lineItem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] LineItem lineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.LineItem.Add(lineItem);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineItem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetLineItem", new { id = lineItem.LineItemId }, lineItem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]LineItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (value.LineItemId != id)
                {
                    return NotFound();
                }
                context.LineItem.Update(value);
                context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                LineItem lineItem = context.LineItem.Single(m => m.LineItemId == id);
                if (lineItem == null)
                {
                    return NotFound();
                }
                context.LineItem.Remove(lineItem);
                context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine("Exception Fired");
                return NotFound();
            }
            return new NoContentResult();
        }
        private bool LineItemExists(int id)
        {
            return context.LineItem.Count(e => e.LineItemId == id) > 0;
        }
    }
}