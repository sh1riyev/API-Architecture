using System;
using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
	public class CountryRepository :BaseRepository<Country>,ICountryRepositroy
	{
		public CountryRepository(AppDbContext context) : base(context) { }
	}
}

