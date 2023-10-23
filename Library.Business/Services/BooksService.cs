using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Models.Dtos.Book;
using Library.Business.Pagination;
using Library.Business.Interfaces.IServices;
using Library.Business.Models;
using Library.Business.Models.Dtos;
using Library.Business.Models.Dtos.Validations;

namespace Library.Business.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public BooksService(IBookRepository repo, IPublisherRepository publiRepo, IRentalRepository rentalRepo, IMapper mapper)
        {
            _bookRepository = repo;
            _publisherRepository = publiRepo;
            _rentalRepository = rentalRepo;
            _mapper = mapper;
        }

        public async Task<ResultService<List<BookDto>>> GetAll(FilterDb filterDb)
        {
            var books = await _bookRepository.GetAllBooksPaged(filterDb);
            var result = new PagedBaseResponseDto<BookDto>(books.TotalRegisters, books.TotalPages, filterDb.Page, _mapper.Map<List<BookDto>>(books.Data));

            if (result.Data.Count == 0)
                return ResultService.Fail<List<BookDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters,result.Page, result.TotalPages);
        }

        public async Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect()
        {
            var books = await _bookRepository.GetAllBooks();
            return ResultService.Ok(_mapper.Map<ICollection<BookRentalDto>>(books));
        }

        public async Task<ResultService<List<BookCountDto>>> GetAllCount()
        {
            var books = await _bookRepository.GetAllBooks();
            var bookDashDto = _mapper.Map<List<BookCountDto>>(books);
            return ResultService.Ok(bookDashDto);
        }

        public async Task<ResultService<List<BookRentedDto>>> GetMostRented()
        {
            var mostrented = await _bookRepository.GetMostRented();
            var bookRentedDto = _mapper.Map<List<BookRentedDto>>(mostrented);
            return ResultService.Ok(bookRentedDto);
        }

        public async Task<ResultService<BookDto>> GetById(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BookDto>("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BookDto>(book));
        }

        public async Task<ResultService> Create(CreateBookDto model)
        {
            var validation = new CreateBookDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var book = _mapper.Map<Books>(model);

            var bookExists = await _bookRepository.GetBookByName(book.Name);
            if (bookExists.Count > 0)
                return ResultService.Fail<BookDto>("Livro já cadastrado.");

            var publisher = await _publisherRepository.GetPublisherById(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BookDto>("Editora não encontrada!");

            var currentYear = DateTime.Now.Date.Year;
            if (book.Release > currentYear)
                return ResultService.Fail<BookDto>("Ano de lançamento não pode ser posterior ao ano atual!");

            await _bookRepository.Add(book);

            return ResultService.Ok("Livro adicionado com êxito!");
        }

        public async Task<ResultService> Update(UpdateBookDto model)
        {
            var book = _mapper.Map<Books>(model);

            var result = await _bookRepository.GetBookById(book.Id);
            if (result == null)
                return ResultService.Fail<BookDto>("Livro não encontrado!");

            var validation = new UpdateBookDtoValidator().Validate(model);
            if (!validation.IsValid)
                return ResultService.RequestError(validation);

            var publisher = await _publisherRepository.GetPublisherById(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BookDto>("Editora não encontrada!");

            await _bookRepository.Update(book);

            return ResultService.Ok("Livro atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BookDto>("Livro não encontrado!");

            var rentalAssociation = await _rentalRepository.GetAllRentalsByBookId(id);
            if (rentalAssociation.Count > 0)
                return ResultService.Fail<BookDto>("Possui associação com aluguéis.");

            await _bookRepository.Delete(book);

            return ResultService.Ok("Livro deletado com êxito!");
        }
    }
}
