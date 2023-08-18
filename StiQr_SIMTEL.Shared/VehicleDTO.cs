namespace StiQr_SIMTEL.Shared
{
    using System.ComponentModel.DataAnnotations;
    public class VehicleDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Plate { get; set; } = null!;

        public string? Alias { get; set; }

        public string? Description { get; set; }

        //public UserDTO? User { get; set; }
    }
}
