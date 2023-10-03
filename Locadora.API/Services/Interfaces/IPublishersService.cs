﻿using Locadora.API.Dtos;
using Locadora.API.Services;
using Locadora.API.Models;
using Locadora.API.Helpers;
using Locadora.API.FiltersDb;

namespace Locadora.API.Services.Interfaces
{
    public interface IPublishersService
    {
        Task<ResultService<PagedBaseResponseDto<Publishers>>> GetAll(PublisherFilterDb publisherFilterDb);
        Task<ResultService<Publishers>> GetById(int id);
        Task<ResultService<ICollection<PublisherBookDto>>> GetAllSelect(PublisherFilterDb publisherFilterDb);
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(Publishers model);
        Task<ResultService> Delete(int id);
    }
}
