﻿using Locadora.API.Models;
using Locadora.API.Context;
using Microsoft.EntityFrameworkCore;
using Locadora.API.Repository.Pagination;
using Locadora.API.FiltersDb;

namespace Locadora.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Users> Add(Users entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(Users entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Users entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedBaseResponse<Users>> GetAllUsersPaged(FilterDb request)
        {
            var users = _context.Users.AsQueryable();
            if (request.FilterValue != null)
               users = FilterHelper.ApplyFilter(users, request.FilterValue);

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Users>, Users>(users, request);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserById(int userId)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<Users>> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).ToListAsync();
        }
    }
}
