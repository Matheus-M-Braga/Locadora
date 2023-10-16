﻿using Locadora.API.Models;
using Locadora.API.Pagination;

namespace Locadora.API.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task Add(Users entity);
        Task Update(Users entity);
        Task Delete(Users entity);

        Task<PagedBaseResponse<Users>> GetAllUsersPaged(FilterDb request);
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserById(int userId);
        Task<List<Users>> GetUserByEmail(string email);
    }
}