using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.DTO
{
    public class EventoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Local deve ser informado")]
        public string Local { get; set; }
        [Required(ErrorMessage = "Data do evento deve ser informada")]
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "Tema deve ser informado")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo deve ter entre 4 e 50 caracteres")]
        public string Tema { get; set; }
        [Range(minimum:1, maximum:12000, ErrorMessage = "{0} deve estar entre 1 e 120.000")]
        [Display(Name = "Quantidade de pessoas")]
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Imagem inválida")]
        public string ImagemURL { get; set; }
        [Required(ErrorMessage = "Telefone deve ser informado")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }
        public IEnumerable<LoteDTO> Lotes { get; set; }
        public IEnumerable<RedeSocialDTO> RedesSociais { get; set; }
        public IEnumerable<PalestranteDTO> PalestrantesEventos { get; set; }
    }
}