using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Exceptions;
using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext applicationDbContext,
                                 IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _applicationDbContext.AddAsync(entity);   
            await _applicationDbContext.SaveChangesAsync();
            return entity;  
        }

        public async Task<TResult> AddAsync<TSource,TResult>(TSource source)
        {
            var entity = _mapper.Map<T>(source);

            await _applicationDbContext.AddAsync(entity);   
            await _applicationDbContext.SaveChangesAsync(); 

            return _mapper.Map<TResult>(entity);    
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity is null)
                throw new NotFoundException(typeof(T).Name, id);

            _applicationDbContext.Set<T>().Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _applicationDbContext.Set<T>().ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            var totalSize = await _applicationDbContext.Set<T>().CountAsync();
            var items = await _applicationDbContext.Set<T>()
                                                   .Skip(queryParameters.StartIndex)
                                                   .Take(queryParameters.PageSize)
                                                   .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                                                   .ToListAsync();
            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = queryParameters.PageNumber,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize
            };
        }

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            return await _applicationDbContext.Set<T>()
                                              .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                                              .ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null) 
            {
                return null;
            }

            return await _applicationDbContext.Set<T>().FindAsync(id);
        }

        public async Task<TResult?> GetAsync<TResult>(int? id)
        {
            if (id is null)
                throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No key Provided");

            var result = await _applicationDbContext.Set<T>().FindAsync(id);
            return _mapper.Map<TResult>(result);
        }

        public async Task UpdateAsync(T entity)
        {
            _applicationDbContext.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
        {
            var entity = await GetAsync(id);

            if (entity is null)
                throw new NotFoundException(typeof(T).Name, id);

            _mapper.Map(source, entity);
            _applicationDbContext.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
