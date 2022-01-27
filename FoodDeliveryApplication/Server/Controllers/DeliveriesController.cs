using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApplication.Server.Data;
using FoodDeliveryApplication.Shared;
using FoodDeliveryApplication.Server.IRepository;

namespace FoodDeliveryApplication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeliveriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Deliveries
        [HttpGet]
        public async Task<IActionResult> GetDeliveries()
        {
            var deliveries = await _unitOfWork.Deliveries.GetAll();
            return Ok(deliveries);
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDelivery(int id)
        {
            var delivery = await _unitOfWork.Deliveries.Get(q => q.Id == id);

            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }

        // PUT: api/Deliveries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Deliveries.Update(delivery);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DeliveryExists(id))
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

        // POST: api/Deliveries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
            await _unitOfWork.Deliveries.Insert(delivery);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetDelivery", new { id = delivery.Id }, delivery);
        }

        // DELETE: api/Deliveries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _unitOfWork.Deliveries.Get(q => q.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            await _unitOfWork.Deliveries.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> DeliveryExists(int id)
        {
            var delivery = await _unitOfWork.Deliveries.Get(q => q.Id == id);
            return delivery != null;
        }
    }
}
