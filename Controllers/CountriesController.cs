using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelHosting.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using HotelHosting.Models.Country;
using AutoMapper;
using HotelHosting.Repository.Interfaces;
using HotelHosting.Repository.Implementation;

namespace HotelHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountriesController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
          var countries = await _countryRepository.GetAllAsync();

          if (countries == null)
          {
              return NotFound("No record found of Countries");
          }

          /*var Countries = await _context.Countries.ToListAsync();*/

          var records = _mapper.Map<List<GetCountryDTO>>(countries);

          return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryByIdDTO>> GetCountry(int id)
        {
            var country = await _countryRepository.GetCountryWithHotelsList(id);
            /*
          if (_context.Countries == null)
          {
              return NotFound("No record found of Countries");
          }
            var country = await _context.Countries.FindAsync(id);*/

            if (country == null)
                {
                return NotFound("Country record not found with id: " + id);
                }

            /*var countryWithHotels = await _context.Countries.Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);*/

            var record = _mapper.Map<GetCountryByIdDTO>(country);

            return Ok(record);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            if (id != updateCountryDTO.Id)
            {
                return BadRequest("Invalid record ID");
            }
            var country = await _countryRepository.GetAsync(id);
            if (country == null)
                {
                return NotFound("Country record not found with id: " + id);
                }
            //_context.Entry(country).State = EntityState.Modified;
            _mapper.Map(updateCountryDTO, country);

            try
            {
                await _countryRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("The record is updated.");
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CountryDTO createCountry)
        {
            /*
             * This code can replaced using the AutoMapper
             * 
             * var countryOld = new Country
                {
                Name = createCountry.CountryName,
                CountryCode = createCountry.CountryCode
                };
            */
            var country = _mapper.Map<Country>(createCountry);

          /*if (_context.Countries == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
          }*/
          await _countryRepository.AddAsync(country);

            /*_context.Countries.Add(country);
            await _context.SaveChangesAsync();*/

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            /*if (_context.Countries == null)
            {
                return NotFound("No record found in Countries table");
            }*/
            var country = await _countryRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound("Country record not found with id: " + id);
            }

            await _countryRepository.DeleteAsync(id);
           
            return Content("Country record with Id:"+id+" deleted successfullly");
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countryRepository.Exists(id);
        }
    }
}
