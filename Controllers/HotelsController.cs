using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHosting.Data;
using HotelHosting.Repository.Interfaces;
using HotelHosting.Configurations;
using HotelHosting.Models.Hotel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using HotelHosting.ExceptionMiddleWare;

namespace HotelHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
          var hotels = await _hotelRepository.GetAllAsync();
          if (hotels == null)
          {
              return NotFound();
          }
            return Ok(_mapper.Map<List<HotelDTO>>(hotels));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);
          if (hotel == null)
          {
                throw new NotFoundException(nameof(GetHotel), id);
          }
            return Ok(_mapper.Map<HotelDTO>(hotel));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }
            var hotel = await _hotelRepository.GetAsync(id);
            if(hotel == null)
                {
                throw new NotFoundException(nameof(GetHotel), id);
                }
            _mapper.Map(hotelDto, hotel);
            //_context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _hotelRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO createHotelDTO)
        {
            /*if (_context.Hotels == null)
            {
                return Problem("Entity set 'HotelListingDbContext.Hotels'  is null.");
            }
              _context.Hotels.Add(hotel);
              await _context.SaveChangesAsync();*/
            var hotel = _mapper.Map<Hotel>(createHotelDTO);
            await _hotelRepository.AddAsync(hotel);
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            /*if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotels.FindAsync(id);*/
            var hotel = await _hotelRepository.GetAsync(id);
            if (hotel == null)
            {
                throw new NotFoundException(nameof(GetHotel), id);
            }
            await _hotelRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
