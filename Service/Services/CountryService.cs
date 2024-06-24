using System;
using System.Linq.Expressions;
using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class CountryService :ICountryService
	{
        private readonly ICountryRepositroy _countryRepo;
		public CountryService(ICountryRepositroy coutnryRepo)
		{
            _countryRepo = coutnryRepo;
		}

        public async Task Create(Country entity)
        {
            try
            {
                if (await IsExist(m => m.Name == entity.Name))
                {
                    throw new FormatException();
                }
                await _countryRepo.Create(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                if (!await IsExist(m => m.Id == id && !m.IsDeleted)) throw new ArgumentNullException();
                Country country =  await GetEntity(m => m.Id == id);
                country.IsDeleted = true;
                await _countryRepo.Delete(country);
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }

        public async Task<List<Country>> GetAll(Expression<Func<Country, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _countryRepo.GetAll(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Country> GetEntity(Expression<Func<Country, bool>> predicate = null, params string[] includes)
        {
            try
            {
                return await _countryRepo.GetEntity(predicate, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Country, bool>> predicate = null)
        {
            try
            {
                return await _countryRepo.IsExist(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(Country entity)
        {
            try
            {
                if (await IsExist(m => m.Id != entity.Id))
                {
                    throw new ArgumentNullException();
                }
                else if(await IsExist(m => m.Name == entity.Name))
                {
                    throw new FormatException();
                }
                await _countryRepo.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }
    }
}

