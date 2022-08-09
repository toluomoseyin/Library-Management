using LibraryManagement.Core.Repositories.Base;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly LibraryManagementDbContext _libraryMgtDbCxt;
        public Repository(LibraryManagementDbContext libraryManagementDbContext)
        {
            _libraryMgtDbCxt = libraryManagementDbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _libraryMgtDbCxt.Set<T>().AddAsync(entity);
            await _libraryMgtDbCxt.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _libraryMgtDbCxt.Set<T>().Remove(entity);
            await _libraryMgtDbCxt.SaveChangesAsync();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _libraryMgtDbCxt.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(string id)
        {
            return await _libraryMgtDbCxt.Set<T>().FindAsync(id);
        }
        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
