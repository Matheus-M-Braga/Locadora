using AutoMapper;
using System.Xml.Linq;
using Locadora.API.Models;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Validations;
using Locadora.API.Services.Interfaces;
using Locadora.API.Repository;
using Locadora.API.Helpers;
using Locadora.API.FiltersDb;

namespace Locadora.API.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublisherRepository _repo;
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public PublishersService(IPublisherRepository repo, IBookRepository bookRepo, IMapper mapper)
        {
            _repo = repo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<PagedBaseResponseDto<Publishers>>> GetAll(PublisherFilterDb publisherFilterDb)
        {
            var publishers = await _repo.GetAllPublishersPaged(publisherFilterDb);
            var result = new PagedBaseResponseDto<Publishers>(publishers.TotalRegisters, publishers.TotalPages, _mapper.Map<List<Publishers>>(publishers.Data));

            if(result.Data.Count == 0)
                return ResultService.Fail<PagedBaseResponseDto<Publishers>>("Nenhum registro encontrado.");

            return ResultService.Ok(result);
        }

        public async Task<ResultService<Publishers>> GetById(int id)
        {
            var publishers = await _repo.GetPublisherById(id);
            if (publishers == null)
                return ResultService.Fail<Publishers>("Editora não encontrada!");

            return ResultService.Ok(_mapper.Map<Publishers>(publishers));
        }

        public async Task<ResultService<ICollection<PublisherBookDto>>> GetAllSelect()
        {
            var publishers = await _repo.GetAllPublishers();
            return ResultService.Ok(_mapper.Map<ICollection<PublisherBookDto>>(publishers));
        }

        public async Task<ResultService> Create(CreatePublisherDto model)
        {
            if (model == null)
                return ResultService.Fail<CreatePublisherDto>("Objeto deve ser informado!");

            var publisher = _mapper.Map<Publishers>(model);

            var result = new PublisherDtoValidator().Validate(publisher);

            if (!result.IsValid)
                return ResultService.RequestError<PublisherDtoValidator>("Problmeas", result);

            var publisherExists = await _repo.GetPublisherByName(model.Name);
            if (publisherExists.Count > 0)
                return ResultService.Fail<CreatePublisherDto>("Editora já cadastrada!");

            await _repo.Add(publisher);

            return ResultService.Ok(publisher);
        }

        public async Task<ResultService> Update(Publishers model)
        {
            if (model == null)
                return ResultService.Fail<Publishers>("Objeto deve ser informado!");

            var validation = new PublisherDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError<Publishers>("Problemas de validação", validation);

            var publishers = await _repo.GetPublisherById(model.Id);
            if (publishers == null)
                return ResultService.Fail("Editora não encontrada!");

            publishers = _mapper.Map(model, publishers);
            await _repo.Update(publishers);

            return ResultService.Ok("Editora atualizada com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var publisher = await _repo.GetPublisherById(id);

            if (publisher == null)
                return ResultService.Fail<Publishers>("Editora não encontrada!");

            var booksAssociatedWithPublisher = await _bookRepo.GetAllBooksByPublisherId(id);
            if (booksAssociatedWithPublisher.Count > 0) 
                return ResultService.Fail<Publishers>("A editora não pode ser excluída, pois está associada a livros.");

            await _repo.Delete(publisher);

            return ResultService.Ok("Editora deletada com êxito!");
        }
    }
}
