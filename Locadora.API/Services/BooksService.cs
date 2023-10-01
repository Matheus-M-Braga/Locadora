using AutoMapper;
using Locadora.API.Dtos;
using Locadora.API.Dtos.Validations;
using Locadora.API.Services.Interfaces;
using Locadora.API.Repository;

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

        public async Task<ResultService<ICollection<BooksDto>>> GetAll()
        {
            var books = await _repo.GetAllBooks(true);
            return ResultService.Ok(_mapper.Map<ICollection<BooksDto>>(books));
        }

        public async Task<ResultService<BooksDto>> GetById(int id)
        {
            var book = await _repo.GetBookById(id, true);
            if (book == null)
                return ResultService.Fail<BooksDto>("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BooksDto>(book));
        }

        public async Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect()
        {
            var books = await _repo.GetAllBooks(false);
            return ResultService.Ok(_mapper.Map<ICollection<BookRentalDto>>(books));
        }

        public async Task<ResultService> Create(CreateBookDto model)
        {
            if (model == null)
                return ResultService.Fail<CreateBookDto>("Objeto deve ser informado!");

            var book = _mapper.Map<BooksDto>(model);

            var result = new BookDtoValidator().Validate(book);
            if (!result.IsValid)
                return ResultService.RequestError<BooksDto>("Problemas de validação", result);

            var bookExists = await _repo.GetBookByName(model.Name);
            if (bookExists.Count > 0)
                return ResultService.Fail<BooksDto>("Livro já cadastrado.");

            var publisher = await _publiRepo.GetPublisherById(model.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BooksDto>("Editora não encontrada!");

            await _repo.Add(book);
            await _repo.SaveChanges();

            return ResultService.Ok(book);
        }

        public async Task<ResultService> Update(UpdateBookDto model)
        {
            if (model == null)
                return ResultService.Fail<BooksDto>("Objeto deve ser informado!");

            var bookValidate = _mapper.Map<BooksDto>(model);

            var validation = new BookDtoValidator().Validate(bookValidate);
            if (!validation.IsValid)
                return ResultService.RequestError<BooksDto>("Problemas de validação", validation);

            var book = await _repo.GetBookById(model.Id);
            if (book == null)
                return ResultService.Fail<BooksDto>("Livro não encontrado!");

            var publisher = await _publiRepo.GetPublisherById(model.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BooksDto>("Editora não encontrada!");

            book = _mapper.Map(model, book);
            await _repo.Update(book);
            await _repo.SaveChanges();

            return ResultService.Ok("Livro atualizado com êxito!");
        }

        public async Task<ResultService> Delete(int id)
        {
            var book = await _repo.GetBookById(id);
            if (book == null)
                return ResultService.Fail<BooksDto>("Livro não encontrado!");

            var rentalAssociation = await _rentalRepo.GetAllRentalsByBookId(id);

            if (rentalAssociation.Count > 0)
                return ResultService.Fail<BooksDto>("Erro ao excluir livro: possui associação com aluguéis.");

            await _repo.Delete(book);
            await _repo.SaveChanges();

            return ResultService.Ok("Livro deletado com êxito!");
        }
    }
}
