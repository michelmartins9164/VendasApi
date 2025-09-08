using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class CreateClientDTO
    {

        public string Empresa { get; set; } = string.Empty;
        [Required]
        public string NomeCompleto { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public string EnderecoEntrega { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
    }
}