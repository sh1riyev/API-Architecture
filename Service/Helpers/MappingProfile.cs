﻿using System;
using AutoMapper;
using Domain.Entities;
using Service.DTOs.Admin.Countries;

namespace Service.Helpers
{
	public class MappingProfile :Profile
	{
		public MappingProfile()
		{
			CreateMap<CountryCreateDto, Country>();
			CreateMap<CountryUpdateDto, Country>();
			CreateMap<Country, CountryDto>();
        }
	}
}

