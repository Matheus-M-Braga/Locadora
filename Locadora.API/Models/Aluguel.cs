namespace Locadora.API.Models {
    public class Aluguel {
        public Aluguel() { }
        public Aluguel(int id, int livroId, int usuarioId, DateTime dataAluguel, DateTime dataPrevisao, DateTime? dataDevolucao, string status) {
            this.Id = id;
            this.LivroId = livroId;
            this.UsuarioId = usuarioId;
            this.DataAluguel = dataAluguel;
            this.DataPrevisao = dataPrevisao;
            this.DataDevolucao = dataDevolucao;
            this.Status = status;
        }
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataAluguel { get; set; }
        public DateTime DataPrevisao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public string Status { get; set; }
    }
}
