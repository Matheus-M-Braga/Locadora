using AutoMapper;
using Library.Business.Models;
using Library.Business.Models.Dtos.Publisher;
using Library.Business.Interfaces.IRepository;
using Library.Business.Pagination;
using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Validations;
using Library.Business.Models.Dtos;

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

        public async Task<ResultService<List<Publishers>>> GetAll(FilterDb filterDb)
        {
            var publishers = await _publisherRepository.GetAllPublishersPaged(filterDb);
            var result = new PagedBaseResponseDto<Publishers>(publishers.TotalRegisters, publishers.TotalPages, publishers.Page, _mapper.Map<List<Publishers>>(publishers.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<List<Publishers>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.Page, result.TotalPages);
        }

        public async Task<ResultService<List<PublisherBookDto>>> GetAllSelect()
        {
            var publishers = await _publisherRepository.GetAllPublishers();
            return ResultService.Ok(_mapper.Map<List<PublisherBookDto>>(publishers));
        }

        public async Task<ResultService<Publishers>> GetById(int id)
        {
            var publishers = await _publisherRepository.GetPublisherById(id);
            if (publishers == null)
                return ResultService.Fail<Publishers>("Editora não encontrada!");

            return ResultService.Ok(_mapper.Map<Publishers>(publishers));
        }  

        public async Task<ResultService> Create(CreatePublisherDto model)
        {
            var validation = new PublisherDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var publisherExists = await _publisherRepository.GetPublisherByName(model.Name);
            if (publisherExists.Count > 0)
                return ResultService.Fail<CreatePublisherDto>("Editora já cadastrada!");

            var publisher = _mapper.Map<Publishers>(model);
            await _publisherRepository.Add(publisher);

            return ResultService.Ok("Editora adicionada com êxito.");
        }

        public async Task<ResultService> Update(UpdatePublisherDto model)
        {
            var publisher = _mapper.Map<Publishers>(model);

            var result = await _publisherRepository.GetPublisherById(publisher.Id);
            if (result == null)
                return ResultService.Fail("Editora não encontrada!");

           if(result.Name != model.Name)
            {
                var publisherExists = await _publisherRepository.GetPublisherByName(model.Name);
                if (publisherExists.Count > 0) return ResultService.Fail("Editora já cadastrada");
            }

            var validation = new UpdatePublisherDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            await _publisherRepository.Update(publisher);

            return ResultService.Ok("Editora atualizada com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);

            if (publisher == null)
                return ResultService.Fail<Publishers>("Editora não encontrada!");

            var bookAssociation = await _bookRepository.GetAllBooksByPublisherId(id);
            if (bookAssociation.Count > 0)
                return ResultService.Fail<Publishers>("A editora não pode ser excluída, pois está associada a livros.");

            await _publisherRepository.Delete(publisher);

            return ResultService.Ok("Editora deletada com êxito!");
        }
    }
}
