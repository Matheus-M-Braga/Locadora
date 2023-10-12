using AutoMapper;
using Locadora.API.Repository.Interfaces;
using Locadora.API.Dtos;
using Locadora.API.Pagination;
using Locadora.API.Services.Interfaces;
using Locadora.API.Models;
using Locadora.API.Validations;

namespace Locadora.API.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBookRepository _repo;
        private readonly IPublisherRepository _publiRepo;
        private readonly IRentalRepository _rentalRepo;
        private readonly IMapper _mapper;

        public BooksService(IBookRepository repo, IPublisherRepository publiRepo, IRentalRepository rentalRepo, IMapper mapper)
        {
            _repo = repo;
            _publiRepo = publiRepo;
            _rentalRepo = rentalRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<PagedBaseResponseDto<BooksDto>>> GetAll(FilterDb filterDb)
        {
            var books = await _repo.GetAllBooksPaged(filterDb);
            var result = new PagedBaseResponseDto<BooksDto>(books.TotalRegisters, books.TotalPages, _mapper.Map<List<BooksDto>>(books.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<PagedBaseResponseDto<BooksDto>>("Nenhum registro encontrado.");

            return ResultService.Ok(result);
        }

        public async Task<ResultService<BooksDto>> GetById(int id)
        {
            var book = await _repo.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BooksDto>("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BooksDto>(book));
        }

        public async Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect()
        {
            var books = await _repo.GetAllBooks();
            return ResultService.Ok(_mapper.Map<ICollection<BookRentalDto>>(books));
        }

        public async Task<ResultService> Create(CreateBookDto model)
        {
            var validation = new CreateBookDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var book = _mapper.Map<Books>(model);

            var bookExists = await _repo.GetBookByName(book.Name);
            if (bookExists.Count > 0)
                return ResultService.Fail<BooksDto>("Livro já cadastrado.");

            var publisher = await _publiRepo.GetPublisherById(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BooksDto>("Editora não encontrada!");

            await _repo.Add(book);

            return ResultService.Ok("Livro adicionado com êxito!");
        }

        public async Task<ResultService> Update(UpdateBookDto model)
        {
            var book = _mapper.Map<Books>(model);

            var result = await _repo.GetBookById(book.Id);
            if (result == null)
                return ResultService.Fail<BooksDto>("Livro não encontrado!");

            var validation = new UpdateBookDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var publisher = await _publiRepo.GetPublisherById(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BooksDto>("Editora não encontrada!");

            await _repo.Update(book);

            return ResultService.Ok("Livro atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var book = await _repo.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BooksDto>("Livro não encontrado!");

            var rentalAssociation = await _rentalRepo.GetAllRentalsByBookId(id);
            if (rentalAssociation.Count > 0)
                return ResultService.Fail<BooksDto>("Livro possui associação com aluguéis.");

            await _repo.Delete(book);

            return ResultService.Ok("Livro deletado com êxito!");
        }
    }
}
