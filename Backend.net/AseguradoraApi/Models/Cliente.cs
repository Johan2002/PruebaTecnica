using System.ComponentModel.DataAnnotations;

namespace AseguradoraApi.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        [Required]
        public string DocumentoIdentidad { get; set; }
        [Required]
        public string PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        [Required]
        public string PrimerApellido { get; set; }
        [Required]
        public string SegundoApellido { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FechaNacimiento { get; set; }
        [Required]
        public decimal ValorSeguro { get; set; }
        public string? Observaciones { get; set; }
    }
}
