using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Countries;
using Service.Services.Interfaces;

namespace API_Architecture.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CountryController : Controller
	{
		private readonly ICountryService _countryService;
		private readonly IMapper _mapper;
		public CountryController(ICountryService countryService,
			IMapper mapper)
		{
			_countryService = countryService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var categories = await _countryService.GetAll();
				var mappedCategoies = _mapper.Map<List<CountryDto>>(categories.OrderBy(m => m.CreateDate));
				return Ok(mappedCategoies);

			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int? id)
		{
			try
			{
				if (id is null) return BadRequest();
				var country = await _countryService.GetEntity(m => m.Id == id&&!m.IsDeleted);
				if (country is null) return NotFound();
				return Ok(_mapper.Map<CountryDto>(country));
			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CountryCreateDto request)
		{
			try
			{
                if (!ModelState.IsValid) return BadRequest(ModelState);
                if (await _countryService.IsExist(m => m.Name == request.Name)) return BadRequest("This country is exist");
				var mappedCountry = _mapper.Map<Country>(request);
				await _countryService.Create(mappedCountry);
                return CreatedAtAction(nameof(Create), mappedCountry);
            }
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }

        }

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int? id)
		{
			try
			{
				if (id is null) return BadRequest();
				else if (await _countryService.IsExist(m => m.Id == id && m.IsDeleted)) return BadRequest();
				var response = _countryService.Delete((int)id);
				return Ok(response);
			}
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromForm] CountryUpdateDto request,int ? id)
		{
			try
			{
                if (id is null) return BadRequest();
                else if (!ModelState.IsValid) return BadRequest(ModelState);
                Country country = await _countryService.GetEntity(m => m.Id == id);
				var mappedCountry = _mapper.Map(request, country);
				var cnt =  _countryService.Update(mappedCountry);
				return Ok(cnt);
            }
			catch (Exception ex)
			{
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }

 		}
	}
}

