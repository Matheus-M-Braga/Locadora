namespace Locadora.API.Models {
    public class Usuario {

        public Usuario() {}
        public Usuario(int id, string nome, string cidade, string endreco, string email) {
            this.Id = id;
            this.Nome = nome;   
            this.Cidade = cidade;
            this.Endereco = endreco;
            this.Email = email;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        
    }
}
