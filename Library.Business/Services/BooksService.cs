using AutoMapper;
using Library.Business.Interfaces.IRepository;
using Library.Business.Interfaces.IServices;
using Library.Business.Models;
using Library.Business.Models.Dtos;
using Library.Business.Models.Dtos.Book;
using Library.Business.Models.Dtos.Validations;
using Library.Business.Pagination;

namespace Library.Business.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public BooksService(IBookRepository bookRepository, IPublisherRepository publisherRepository, IRentalRepository rentalRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _publisherRepository = publisherRepository;
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<List<BookDto>>> GetAll(FilterDb filterDb)
        {
            var books = await _bookRepository.GetAllBooksPaged(filterDb);
            var result = new PagedBaseResponseDto<BookDto>(books.TotalRegisters, books.TotalPages, filterDb.PageNumber, _mapper.Map<List<BookDto>>(books.Data));

            if (result.Data.Count == 0) return ResultService.NotFound<List<BookDto>>("Nenhum registro encontrado.");

            return ResultService.OkPaged(result.Data, result.TotalRegisters, result.PageNumber, result.TotalPages);
        }

        public async Task<ResultService<List<BookListDto>>> GetSummary()
        {
            var books = await _bookRepository.GetSummary();
            return ResultService.Ok(_mapper.Map<List<BookListDto>>(books));
        }

        public async Task<ResultService<BookDto>> GetById(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null) return ResultService.NotFound<BookDto>("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BookDto>(book));
        }

        public async Task<ResultService> Create(CreateBookDto model)
        {
            var validation = new CreateBookDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (_bookRepository.Search(b => b.Name == model.Name).Result.Any()) return ResultService.BadRequest("Livro já cadastrado.");

            if (!_publisherRepository.Search(p => p.Id == model.PublisherId).Result.Any()) return ResultService.NotFound<BookDto>("Editora não encontrada!");

            if (model.Release > DateTime.Now.Date.Year) return ResultService.BadRequest("Ano de lançamento não pode ser posterior ao ano atual!");

            await _bookRepository.Add(_mapper.Map<Books>(model));
            return ResultService.Created("Livro adicionado com êxito!");
        }

        public async Task<ResultService> Update(UpdateBookDto model)
        {
            if (!_bookRepository.Search(b => b.Id == model.Id).Result.Any()) return ResultService.NotFound("Livro não encontrado.");

            var validation = new UpdateBookDtoValidator().Validate(model);
            if (!validation.IsValid) return ResultService.BadRequest(validation);

            if (_bookRepository.Search(b => b.Name == model.Name && b.Id != model.Id).Result.Any()) return ResultService.BadRequest("Livro já cadastrado.");

            if (!_publisherRepository.Search(p => p.Id == model.PublisherId).Result.Any()) return ResultService.NotFound<BookDto>("Editora não encontrada!");

            await _bookRepository.Update(_mapper.Map<Books>(model));
            return ResultService.Ok("Livro atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            if (!_bookRepository.Search(b => b.Id == id).Result.Any()) return ResultService.NotFound("Livro não encontrado!");

            if (_rentalRepository.Search(r => r.BookId == id).Result.Any()) return ResultService.BadRequest("Possui associação com aluguéis.");

            await _bookRepository.Delete(id);
            return ResultService.Ok("Livro deletado com êxito!");
        }
    }
}
