namespace Locadora.API.Models {
    public class Livro {

        public Livro() { }
        public Livro(int id, string nome, string autor, int editoraId, string lancamento, int quantidade, int alugados) {
            this.Id = id;
            this.Nome = nome;
            this.Autor = autor;
            this.EditoraId = editoraId;
            this.Lancamento = lancamento;
            this.Quatidade = quantidade;        
            this.Alugados = alugados;
        }
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Autor { get; set; }
        public int? EditoraId { get; set; }
        //public Editora? Editora { get; set; }
        public string? Lancamento { get; set; }
        public int? Quatidade { get; set; }
        public int? Alugados { get; set; }
        //public IEnumerable<Aluguel> Aluguel { get; set; }
    }
}
