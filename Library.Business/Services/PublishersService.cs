using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Library.Business.Models;
using Library.Business.Models.Dtos;
using Library.Business.Models.Dtos.Publisher;
using Library.Business.Models.Dtos.Validations;
using Library.Business.Pagination;

namespace Library.Business.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public PublishersService(IPublisherRepository repo, IBookRepository bookRepo, IMapper mapper)
        {
            _publisherRepository = repo;
            _bookRepository = bookRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<List<PublisherDto>>> GetAll(FilterDb filterDb)
        {
            var publishers = await _publisherRepository.GetAllPublishersPaged(filterDb);
            var result = new PagedBaseResponseDto<PublisherDto>(publishers.TotalRegisters, publishers.TotalPages, publishers.PageNumber, _mapper.Map<List<PublisherDto>>(publishers.Data));

            if (result.Data.Count == 0) return ResultService.NotFound<List<PublisherDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.PageNumber, result.TotalPages);
        }

        public async Task<ResultService<List<PublisherListDto>>> GetSummary()
        {
            var publishers = await _publisherRepository.GetSummary();
            return ResultService.Ok(_mapper.Map<List<PublisherListDto>>(publishers));
        }

        public async Task<ResultService<PublisherDto>> GetById(int id)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);
            if (publisher == null) return ResultService.NotFound<PublisherDto>("Editora não encontrada!");

            return ResultService.Ok(_mapper.Map<PublisherDto>(publisher));
        }

        public async Task<ResultService> Create(CreatePublisherDto model)
        {
            var validation = new PublisherDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (_publisherRepository.Search(p => p.Name.ToLower() == model.Name.ToLower()).Result.Any()) return ResultService.BadRequest("Editora já cadastrada!");

            await _publisherRepository.Add(_mapper.Map<Publishers>(model));
            return ResultService.Created("Editora adicionada com êxito.");
        }

        public async Task<ResultService> Update(UpdatePublisherDto model)
        {
            if (!_publisherRepository.Search(p => p.Id == model.Id).Result.Any()) return ResultService.NotFound("Editora não encontrada!");

            var validation = new UpdatePublisherDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (_publisherRepository.Search(p => p.Name == model.Name && p.Id != model.Id).Result.Any()) return ResultService.BadRequest("Editora já cadastrada");

            await _publisherRepository.Update(_mapper.Map<Publishers>(model));
            return ResultService.Ok("Editora atualizada com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            if (!_publisherRepository.Search(p => p.Id == id).Result.Any()) return ResultService.NotFound("Editora não encontrada!");

            if (_bookRepository.Search(b => b.PublisherId == id).Result.Any()) return ResultService.BadRequest("Possui associação com livros.");

            await _publisherRepository.Delete(id);
            return ResultService.Ok("Editora deletada com êxito!");
        }
    }
}
