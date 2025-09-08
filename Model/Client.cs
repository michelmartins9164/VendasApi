namespace API.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public string EnderecoEntrega { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
    }
}