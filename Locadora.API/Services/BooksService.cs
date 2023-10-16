using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Pagination;
using Locadora.API.Models;
using Locadora.API.Validations;
using Locadora.API.Interfaces.IRepository;
using Locadora.API.Interfaces.IServices;
using Locadora.API.Dtos.Book;

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

        public async Task<ResultService<List<BookDto>>> GetAll(FilterDb filterDb)
        {
            var books = await _repo.GetAllBooksPaged(filterDb);
            var result = new PagedBaseResponseDto<BookDto>(books.TotalRegisters, books.TotalPages, _mapper.Map<List<BookDto>>(books.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<List<BookDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.TotalPages);
        }

        public async Task<ResultService<List<BookDashDto>>> GetAllDash()
        {
            var books = await _repo.GetAllBooks();
            var bookDashDto = _mapper.Map<List<BookDashDto>>(books);
            return ResultService.Ok(bookDashDto);
        }

        public async Task<ResultService<BookDto>> GetById(int id)
        {
            var book = await _repo.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BookDto>("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BookDto>(book));
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
                return ResultService.Fail<BookDto>("Livro já cadastrado.");

            var publisher = await _publiRepo.GetPublisherById(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BookDto>("Editora não encontrada!");

            var currentYear = DateTime.Now.Date.Year;
            if(book.Release > currentYear)
                return ResultService.Fail<BookDto>("Ano de lançamento não pode ser posterior ao ano atual!");

            await _repo.Add(book);

            return ResultService.Ok("Livro adicionado com êxito!");
        }

        public async Task<ResultService> Update(UpdateBookDto model)
        {
            var book = _mapper.Map<Books>(model);

            var result = await _repo.GetBookById(book.Id);
            if (result == null)
                return ResultService.Fail<BookDto>("Livro não encontrado!");

            var validation = new UpdateBookDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var publisher = await _publiRepo.GetPublisherById(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BookDto>("Editora não encontrada!");

            await _repo.Update(book);

            return ResultService.Ok("Livro atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var book = await _repo.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BookDto>("Livro não encontrado!");

            var rentalAssociation = await _rentalRepo.GetAllRentalsByBookId(id);
            if (rentalAssociation.Count > 0)
                return ResultService.Fail<BookDto>("Livro possui associação com aluguéis.");

            await _repo.Delete(book);

            return ResultService.Ok("Livro deletado com êxito!");
        }
    }
}
